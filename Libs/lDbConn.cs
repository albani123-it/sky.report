using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sky.report.Libs
{
    public class lDbConn
    {
        private lConvert lc = new lConvert();

        public string sqlprovider()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return "" + config.GetSection("SqlProvider:provider").Value.ToString();
        }


        public string constringName(string cstr)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return "" + config.GetSection("constringName:" + cstr).Value.ToString();
        }


        public string constringList_v2(string dbprv, string strname)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();

            //var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());

            var configDB = config.GetSection("DbContextSettings:" + dbprv + ":" + strname).Value.ToString();

            //var repPass = configDB.Replace("{pass}", configPass);
            return "" + configDB;


        }

        public string conStringDynamic(string database)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:ConnectionString_Dynamic").Value.ToString();

            var repDB = configDB.Replace("{database}", database);
            var repPass = repDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

        public string conString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:ConnectionString_reportetl").Value.ToString();

            var repPass = configDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

        #region -- connnection string by database --
        public string constringList(string strname)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:" + strname).Value.ToString();

            var repPass = configDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

        public string constringListDynamic(string dbname, string strname)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:" + strname).Value.ToString();

            var repDB = configDB.Replace("{database}", dbname);
            var repPass = repDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

        #endregion


        public string conStringReport()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:ConnectionString_en").Value.ToString();

            var repPass = configDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

       
        public string getAppSettingParam(string group, string api)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            return "" + config.GetSection(group + ":" + api).Value.ToString();
        }

        public string domainGetApi(string api)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            return "" + config.GetSection("APISettings:" + api).Value.ToString();
        }

        public string domainGetTokenCredential(string param)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return config.GetSection("TokenAuthentication:" + param).Value.ToString();
        }

        public string domainPostApi()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return "" + config.GetSection("DomainSettings:urlPostDomainAPI").Value.ToString();
        }

        public string PathFileSlik()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            return "" + config.GetSection("slik_config:pathfile").Value.ToString();
        }
        public string SeparatorFileSlik()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            return "" + config.GetSection("slik_config:separator").Value.ToString();
        }

        public string conStringReport_flow()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
            var configDB = config.GetSection("DbContextSettings:ConnectionString_en").Value.ToString();

            var repPass = configDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

        public string dbname(string cstr)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return "" + config.GetSection("DBConfig:" + cstr).Value.ToString();
        }

    }
}
