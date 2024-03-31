using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sky.report.Libs;
using sky.report.Controllers;
using Newtonsoft.Json.Linq;

namespace sky.report.Parsing
{
    public class lPgsqlMapping
    {
        private BaseController bc = new BaseController();
        private lPgsql pgsql = new lPgsql();

        internal string Mapping_Into(JObject json)
        {
            string str = "", str2 = "";

            Random r = new Random();
            var tablename = "tmp_" + DateTime.Now.ToString("yyyyMMddHHmmssffffff").ToString() + r.Next().ToString();
            List<dynamic> retObject = new List<dynamic>();

            str = " DROP TABLE IF EXISTS " + tablename + ";";
            str2 = "SELECT als.* into " + tablename + " FROM (" + json.GetValue("sql").ToString().Replace("\r\n", "") + ")als limit 1;";
            str = str + str2;
            retObject = pgsql.execSQLWithOutput(str);
            
            return tablename;
        }

        public void DropTmpTable(string tablename)
        {
            string str = "";
            str = " DROP TABLE IF EXISTS " + tablename + ";";
            pgsql.execSql(str);
        }
    }
}
