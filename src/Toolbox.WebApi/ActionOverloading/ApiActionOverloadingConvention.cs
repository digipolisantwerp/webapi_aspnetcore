using System;
using Microsoft.AspNet.Mvc.ApplicationModels;

namespace Toolbox.WebApi.ActionOverloading
{
    public class ApiActionOverloadingConvention : IActionModelConvention
    {
        public void Apply(ActionModel actionModel)
        {
            if ( actionModel == null ) throw new ArgumentNullException(nameof(actionModel), $"{nameof(actionModel)} cannot be null.");
            actionModel.ActionConstraints.Add(new OverloadActionConstraint());      // should there be an option to enable/disable this per controller (via attribute) ?
        }
    }
}
