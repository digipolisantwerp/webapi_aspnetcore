using System;

namespace Toolbox.WebApi.Formatters
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RootListObjectAttribute : Attribute
    {
        public RootListObjectAttribute(string name)
        {
            if ( String.IsNullOrWhiteSpace(name) ) throw new ArgumentException("name mag niet null of leeg zijn.", "name");
            this.Name = name;
        }

        public string Name { get; set; }
    }
}