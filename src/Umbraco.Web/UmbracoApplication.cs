﻿using System.Configuration;
using System.Threading;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging.Serilog;
using Umbraco.Core.Runtime;
using Umbraco.Core.Configuration;
using Umbraco.Core.Hosting;
using Umbraco.Core.IO;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Web.Runtime;

namespace Umbraco.Web
{
    /// <summary>
    /// Represents the Umbraco global.asax class.
    /// </summary>
    public class UmbracoApplication : UmbracoApplicationBase
    {
        protected override IRuntime GetRuntime(Configs configs, IUmbracoVersion umbracoVersion, IIOHelper ioHelper, ILogger logger, IProfiler profiler, IHostingEnvironment hostingEnvironment, IBackOfficeInfo backOfficeInfo)
        {

            var connectionStringConfig = configs.ConnectionStrings()[Constants.System.UmbracoConnectionName];

            var dbProviderFactoryCreator = new UmbracoDbProviderFactoryCreator(connectionStringConfig?.ProviderName);

            // Determine if we should use the sql main dom or the default
            var appSettingMainDomLock = ConfigurationManager.AppSettings[Constants.AppSettings.MainDomLock];

            var mainDomLock = appSettingMainDomLock == "SqlMainDomLock"
                ? (IMainDomLock)new SqlMainDomLock(logger, configs, dbProviderFactoryCreator)
                : new MainDomSemaphoreLock(logger, hostingEnvironment);

            var mainDom = new MainDom(logger, hostingEnvironment, mainDomLock);

            return new WebRuntime(configs, umbracoVersion, ioHelper, logger, profiler, hostingEnvironment, backOfficeInfo, dbProviderFactoryCreator, mainDom);
        }
    }
}
