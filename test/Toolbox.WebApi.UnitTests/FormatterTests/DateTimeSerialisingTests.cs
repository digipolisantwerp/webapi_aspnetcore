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

namespace Toolbox.WebApi.UnitTests.FormatterTests.JsonFormatter
{
    public class DateTimeSerialisingTests
    {
        private Gesprek DeserializeGesprekMetBeginUur(DateTime? datum)
        {
            var formatter = new RootObjectJsonInputFormatter();
            var httpContext = new DefaultHttpContext();

            var provider = new EmptyModelMetadataProvider();
            var metadata = provider.GetMetadataForType(typeof(Gesprek));

            var formatterContext = new InputFormatterContext(httpContext, typeof(Gesprek).Name, new ModelStateDictionary(), metadata);

            var datumUTCString = datum?.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            var dateStringWithQuotes = String.IsNullOrEmpty(datumUTCString) ? "null" : $"\"{datumUTCString}\"";

            var reader = new Mock<StreamReader>(new MemoryStream(), Encoding.UTF8);
            reader.Setup(x => x.ReadToEnd()).Returns("{\"Gesprek\":{\"geplandOp\":" + dateStringWithQuotes + "}}");

            var gesprek = formatter.ReadFromStream(formatterContext, reader.Object);
            return (Gesprek)gesprek;
        }

        [Fact]
        public void CheckGekozenUurWordtCorrectGedeserialisedNaarLocalTime()
        {
            var gekozenUur = 23;
            var datum = new DateTime(2015, 09, 10, gekozenUur, 0, 0, DateTimeKind.Local);

            Gesprek gesprek = DeserializeGesprekMetBeginUur(datum);
            Assert.Equal(gesprek.GeplandOp.TimeOfDay.Hours, gekozenUur);
        }

        [Fact]
        public void CheckMiddernachtWordtCorrectGedeserialisedNaarLocalTime()
        {
            var gekozenUur = 0;
            var datum = new DateTime(2015, 09, 10, gekozenUur, 30, 0, DateTimeKind.Local);

            Gesprek gesprek = DeserializeGesprekMetBeginUur(datum);
            Assert.Equal(gesprek.GeplandOp.TimeOfDay.Hours, gekozenUur);
        }
    }
}
