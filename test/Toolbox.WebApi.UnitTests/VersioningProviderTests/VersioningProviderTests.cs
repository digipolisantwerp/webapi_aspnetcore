using System;
using Toolbox.WebApi.Versioning;
using Moq;
using Xunit;
using Microsoft.Extensions.PlatformAbstractions;

namespace Toolbox.WebApi.UnitTests.VersioningProviderTests
{
    public class VersioningProviderTests
    {
        [Theory]
        [InlineData("1.2.3-5", "1")]
        [InlineData("1.2.3-", "1")]
        [InlineData("1.2.3", "1")]
        [InlineData("1", "1")]
        [InlineData("1-5", "1")]
        [InlineData(".2.3", "")]
        [InlineData("", "")]        
        public void MajorVersie_Wordt_Correct_Geparsed(string versie, string major)
        {
            var appenv_mock = new Mock<IApplicationEnvironment>();
            appenv_mock.Setup(foo => foo.ApplicationVersion).Returns(versie).Verifiable();
            
            var provider = new WebApiVersionProvider(appenv_mock.Object);

            Assert.Equal(major, provider.GetCurrentVersion().MajorVersion);
            appenv_mock.Verify();
        }

        [Theory]
        [InlineData("1.2.3", "2")]
        [InlineData("1.2.3-6", "2")]
        [InlineData("1.2.3-", "2")]
        [InlineData("1..3", "")]
        [InlineData("1", "?")]
        [InlineData("1-5", "?")]
        [InlineData("1-", "?")]
        [InlineData("1.", "")]
        [InlineData("", "?")]
        public void MinorVersie_Wordt_Correct_Geparsed(string versie, string minor)
        {
            var appenv_mock = new Mock<IApplicationEnvironment>();
            appenv_mock.Setup(foo => foo.ApplicationVersion).Returns(versie).Verifiable();

            var provider = new WebApiVersionProvider(appenv_mock.Object);

            Assert.Equal(minor, provider.GetCurrentVersion().MinorVersion);
            appenv_mock.Verify();
        }

        [Theory]
        [InlineData("1.2.3", "3")]
        [InlineData("1.2.3-666", "3")]
        [InlineData("1.2.3-", "3")]
        [InlineData("1..4", "4")]
        [InlineData("..9", "9")]
        [InlineData("1", "?")]
        [InlineData("1.", "?")]
        [InlineData("1.2", "?")]
        [InlineData("1.2.", "")]
        [InlineData("", "?")]
        public void RevisionVersie_Wordt_Correct_Geparsed(string versie, string revision)
        {
            var appenv_mock = new Mock<IApplicationEnvironment>();
            appenv_mock.Setup(foo => foo.ApplicationVersion).Returns(versie).Verifiable();

            var provider = new WebApiVersionProvider(appenv_mock.Object);

            Assert.Equal(revision, provider.GetCurrentVersion().Revision);
            appenv_mock.Verify();
        }


        [Theory]
        [InlineData("1.2.3", "?")]
        [InlineData("1.2.3-666", "666")]
        [InlineData("1.2.3-", "")]
        [InlineData("1..4-5", "5")]
        [InlineData("..9-3", "3")]
        [InlineData("1", "?")]
        [InlineData("1.", "?")]
        [InlineData("1.2", "?")]
        [InlineData("1.2.", "?")]
        [InlineData("", "?")]
        public void Buildnummer_Wordt_Correct_Geparsed(string versie, string buildnr)
        {
            var appenv_mock = new Mock<IApplicationEnvironment>();
            appenv_mock.Setup(foo => foo.ApplicationVersion).Returns(versie).Verifiable();

            var provider = new WebApiVersionProvider(appenv_mock.Object);

            Assert.Equal(buildnr, provider.GetCurrentVersion().BuildNumber);
            appenv_mock.Verify();
        }


        [Fact]
        public void Versie_Gelijkaan_null_Wordt_Correct_Geparsed()
        {
            var appenv_mock = new Mock<IApplicationEnvironment>();
            appenv_mock.Setup(foo => foo.ApplicationVersion).Verifiable();

            var provider = new WebApiVersionProvider(appenv_mock.Object);

            Assert.Equal("", provider.GetCurrentVersion().MajorVersion);
            appenv_mock.Verify();
        }
        
        [Fact]
        public void ApplicationBasePath_Gelijkaan_null_Wordt_Correct_Afgehandeld()
        {
            var appenv_mock = new Mock<IApplicationEnvironment>();
            appenv_mock.Setup(foo => foo.ApplicationBasePath).Verifiable();

            var provider = new WebApiVersionProvider(appenv_mock.Object);

            Assert.Equal("?", provider.GetCurrentVersion().BuildDate);
            //Assert.Equal("?", provider.GetCurrentVersion().BuildNumber);
            appenv_mock.Verify();
        }
        
    }
}
