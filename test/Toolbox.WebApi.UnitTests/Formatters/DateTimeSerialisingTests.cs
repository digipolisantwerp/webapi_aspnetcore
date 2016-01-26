using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Moq;
using System;
using System.IO;
using System.Text;
using Xunit;
using Toolbox.WebApi.Formatters;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace Toolbox.WebApi.UnitTests.Formatters
{
    public class DateTimeSerialisingTests
    {
        private DateTimeSerialisationMockingObject DeserializeDateTimeSerialisationMockingObjectWithDateTimeProperty1(DateTime? date)
        {
            var formatter = new RootObjectJsonInputFormatter();
            var httpContext = new DefaultHttpContext();

            var provider = new EmptyModelMetadataProvider();
            var metadata = provider.GetMetadataForType(typeof(DateTimeSerialisationMockingObject));

            var formatterContext = new InputFormatterContext(httpContext, typeof(DateTimeSerialisationMockingObject).Name, new ModelStateDictionary(), metadata);

            var dateUTCString = date?.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            var dateStringWithQuotes = String.IsNullOrEmpty(dateUTCString) ? "null" : $"\"{dateUTCString}\"";

            var reader = new Mock<StreamReader>(new MemoryStream(), Encoding.UTF8);
            reader.Setup(x => x.ReadToEnd()).Returns("{\"DateTimeSerialisationMockingObject\":{\"DatetimeProperty1\":" + dateStringWithQuotes + "}}");

            var dateTimeSerialisationMockingObject = formatter.ReadFromStream(formatterContext, reader.Object);
            return (DateTimeSerialisationMockingObject)dateTimeSerialisationMockingObject;
        }

        [Fact]
        public void CheckChosenHourIsCorrectlyDeserializedToLocalTime()
        {
            var chosenHour = 23;
            var date = new DateTime(2015, 09, 10, chosenHour, 0, 0, DateTimeKind.Local);

            DateTimeSerialisationMockingObject dateTimeSerialisationMockingObject = DeserializeDateTimeSerialisationMockingObjectWithDateTimeProperty1(date);
            Assert.Equal(dateTimeSerialisationMockingObject.DatetimeProperty1.TimeOfDay.Hours, chosenHour);
        }

        [Fact]
        public void CheckMidnightIsCorrectlyDeserialisedToLocalTime()
        {
            var chosenHour = 0;
            var date = new DateTime(2015, 09, 10, chosenHour, 30, 0, DateTimeKind.Local);

            DateTimeSerialisationMockingObject gesprek = DeserializeDateTimeSerialisationMockingObjectWithDateTimeProperty1(date);
            Assert.Equal(gesprek.DatetimeProperty1.TimeOfDay.Hours, chosenHour);
        }
    }
}
