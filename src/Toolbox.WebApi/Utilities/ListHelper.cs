using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.WebApi.Utilities
{
    internal class ListHelper
    {
        public static void RemoveTypes<TType>(IList<TType> list, Type type)
        {
            var instances = list.Where(e => e.GetType().IsAssignableFrom(type)).ToList();
            foreach ( var item in instances )
            {
                list.Remove(item);
            }
        }
    }
}
