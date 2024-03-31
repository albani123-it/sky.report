using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sky.report.Controllers;

namespace sky.report.Libs
{
    public class lflow
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        internal JArray getexecData(JArray jaRaw, string condition, string tablename, string valcoundition)
        {
            var data = new JArray();
            var joData = new JObject();
            var joRaw = new JObject();
            string qry = "";
            string field = "";
            string type = "";
            string filter = "";
            for (int a = 0; a < jaRaw.Count; a++)
            {
                joRaw = JObject.Parse(jaRaw[a].ToString());
                field = joRaw.GetValue("field").ToString();
                type = joRaw.GetValue("type").ToString();
              
                if (joRaw.ContainsKey("filter"))
                {
                    if (condition == "")
                    {
                        filter = joRaw.GetValue("filter").ToString();
                        qry = " select " + type + " ( " + field + " ) :: varchar as value from workflow." + tablename + " where " + field + "='" + filter + "' and "+ valcoundition  + "; ";
                    }
                    else
                    {
                        filter = joRaw.GetValue("filter").ToString();
                        qry = " select " + type + " ( " + field + " ) :: varchar as value from workflow." + tablename + " where " + field + "='" + filter +"' and "+ valcoundition + " and "+ condition +"; ";
                    }
                   
                }
                else
                {
                    if (type.ToLower() == "sum")
                    {
                        if (condition == "")
                        {
                            qry = " select " + type + " ( coalesce(" + field + ",0) ) :: varchar as value from workflow." + tablename + " where " + valcoundition + "; ";
                        }
                        else
                        {
                            qry = " select " + type + " ( coalesce(" + field + ",0) ) :: varchar as value from workflow." + tablename + " where " + valcoundition + " and "+ condition +"; ";
                        }
                           
                    }
                    else
                    {
                        if (condition == "")
                        {
                            qry = " select " + type + " ( " + field + " ) :: varchar as value from workflow." + tablename + " where " + valcoundition + " ; ";
                        }
                        else
                        {
                            qry = " select " + type + " ( " + field + " ) :: varchar as value from workflow." + tablename + "  where " + valcoundition + " and " + condition + "; ";
                        }
                           
                    }
                }
                var strVal = this.GetValueRecord(qry);
                joData.Add("data" + Convert.ToString(a + 1), strVal);
            }
            data.Add(joData);

            return data;
        }

        


        public string GetValueRecord(string qry)
        {
            var strReturn = "";
            var jaReturn = new JArray();
            var joReturn = new JObject();
            var retObject = new List<dynamic>();
            retObject = bc.execSQLWithOutputDynamicPrm(qry);
            jaReturn = lc.convertDynamicToJArray(retObject);
            joReturn = new JObject();
            if (jaReturn.Count > 0)
            {
                joReturn = JObject.Parse(jaReturn[0].ToString());
            }

            strReturn = joReturn.GetValue("value").ToString().ToUpper();

            return strReturn;
        }


    }
}
