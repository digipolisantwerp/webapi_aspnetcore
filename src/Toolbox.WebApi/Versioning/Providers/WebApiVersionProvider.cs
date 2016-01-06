using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Framework.Runtime;

namespace Toolbox.WebApi.Versioning
{
    class WebApiVersionProvider : IVersionProvider
    {
        private IApplicationEnvironment _appEnv;

        public WebApiVersionProvider(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
        }

        /// <summary>
        /// Haal de huidige versie op van de component
        /// </summary>
        /// <returns>AppVersion object</returns>
        public AppVersion GetCurrentVersion()
        {
            try
            {
                AppVersion retAV = new AppVersion()
                {
                    AppName = _appEnv.ApplicationName ?? ""
                };

                    string[] splitbuildnr = (_appEnv.Version ?? "").Split('-');                    
                    string[] splitversion = (splitbuildnr[0]).Split('.');

                retAV.MajorVersion = splitversion[0];
                retAV.MinorVersion = splitversion.Length > 1 ? splitversion[1] : "?";
                retAV.Revision = splitversion.Length > 2 ? splitversion[2] : "?";

                retAV.BuildNumber = splitbuildnr.Length > 1 ? splitbuildnr[1] : "?";


                string[] Paths = Directory.GetFiles(_appEnv.ApplicationBasePath ?? ".", "project.json");
               
                if (Paths.Length == 1)
                {
                    FileInfo versieFileInfo = new FileInfo(Paths[0]);
                    retAV.BuildDate = versieFileInfo.CreationTime.ToString();
                    if (_appEnv.ApplicationBasePath == null)
                    {
                        retAV.BuildDate = "?"; //Truuk van de foor => TODO rc01831
                    }
                    //retAV.BuildNumber = versieFileInfo.Name.Substring(versieFileInfo.Name.LastIndexOf('_') + 1);
                }
                else
                {
                    //throw new Exception("Het buildnummer kon niet worden vastgesteld.");
                    retAV.BuildDate = "?";
                    //retAV.BuildNumber = "?";
                }

                return retAV;

            }
            catch (Exception ex)
            {
                throw new Exception("Er liep iets mis bij het vaststellen van de versiegegevens.", ex);
            }
        }

    }
}
