using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toolbox.WebApi.UnitTests.FormatterTests
{
    public class DateTimeSerialisationMockingObject
    {
        public DateTime DatetimeProperty1 { get; set; }
        public DateTime DatetimeProperty2 { get; set; }
        public string StringProperty1 { get; set; }
        public string StringProperty2 { get; set; }
        public string StringProperty3 { get; set; }
        public int? IntPropertyNullable1 { get; set; }
        public int? IntPropertyNullable2 { get; set; }
    }
}
