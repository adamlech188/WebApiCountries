using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConfiguration
{
    public static class AppConfigurationProvider
    {
        public static IConfiguration BuildConfigurtions()
        {
            return new ConfigurationBuilder().AddJsonFile("dbsettings.json", optional: false, reloadOnChange: true).Build();
        }
    }
}
