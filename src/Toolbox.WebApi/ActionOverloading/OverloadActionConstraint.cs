﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Toolbox.WebApi.ActionOverloading
{
    public class OverloadActionConstraint : IActionConstraint
    {
        public int Order
        {
            get { return Int32.MaxValue; }
        }

        public bool Accept(ActionConstraintContext context)
        {
            var queryString = context.RouteContext.HttpContext.Request.QueryString.ToUriComponent();

            var queryStringComponent = queryString.StartsWith("?", StringComparison.Ordinal) ? queryString.Substring(1) : queryString;
            var queryData = new QueryStringParameterCollection(queryStringComponent);

            var exactCandidates = context.Candidates.Where(c => c.Action.Parameters.Count == queryData.Count);
            var selectedCandidate = SelectExactCandidate(exactCandidates, queryData);
            if ( context.CurrentCandidate.Action == selectedCandidate?.Action ) return true;

            var overloadedCandidates = context.Candidates.Where(c => c.Action.Parameters.Count > queryData.Count)
                                                         .OrderByDescending(c => c.Action.Parameters.Count);

            selectedCandidate = SelectOverloadedCandidate(overloadedCandidates, queryData);
            if ( context.CurrentCandidate.Action == selectedCandidate?.Action ) return true;

            return false;
        }

        private ActionSelectorCandidate SelectExactCandidate(IEnumerable<ActionSelectorCandidate> candidates, QueryStringParameterCollection queryData)
        {
            foreach ( var candidate in candidates )
            {
                var action = candidate.Action;

                var isMatch = true;
                foreach ( var actionParam in action.Parameters )
                {
                    var nameProvider = actionParam.BindingInfo as IModelNameProvider;
                    var name = nameProvider?.Name ?? actionParam.Name;

                    if ( queryData.ContainsKey(name) )
                    {
                        isMatch = HasFromQueryAttribute(actionParam);
                        if ( !isMatch ) break;

                        if ( queryData[name].Count() > 1 && !IsCollection(actionParam.ParameterType) )
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        isMatch = false;
                        break;
                    }
                }

                if ( isMatch ) return candidate;
            }

            return null;
        }

        private ActionSelectorCandidate SelectOverloadedCandidate(IEnumerable<ActionSelectorCandidate> candidates, QueryStringParameterCollection queryData)
        {
            foreach ( var candidate in candidates )
            {
                var action = candidate.Action;

                var isMatch = true;
                var exactCount = 0;
                foreach ( var actionParam in action.Parameters )
                {
                    var nameProvider = actionParam.BindingInfo as IModelNameProvider;
                    var name = nameProvider?.Name ?? actionParam.Name;

                    if ( queryData.ContainsKey(name) )
                    {
                        if ( HasFromQueryAttribute(actionParam) )
                        {
                            if ( queryData[name].Count() > 1 && !IsCollection(actionParam.ParameterType) )
                            {
                                isMatch = false;
                                break;
                            }

                            exactCount++;
                        }
                        else
                        {
                            // parameter with the right name but without FromQueryAttribute
                            isMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        if ( HasFromQueryAttribute(actionParam) )
                        {
                            // parameter was expected in the query string
                            isMatch = false;
                            break;
                        }
                    }
                }

                if ( isMatch && exactCount == queryData.Count ) return candidate;
            }

            return null;

        }


        private bool HasFromQueryAttribute(ParameterDescriptor actionParam)
        {
            var source = actionParam.BindingInfo?.BindingSource;
            if ( source == null ) return false;

            var type = actionParam.ParameterType;
            return source.CanAcceptDataFrom(BindingSource.Query);
        }

        private bool IsCollection(Type type)
        {
            if ( type == typeof(string) ) return false;     // only real collections
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
    }
}
