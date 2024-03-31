using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sky.report.Controllers;

namespace sky.report.Libs
{

    public class lReportsla
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lDbConn dbconn = new lDbConn();


        public static string getBetween(string strSource, string strStart)
        {
            if (strSource.Contains(strStart))
            {
                int Start;
                Start = strSource.IndexOf(strStart, 0) + strSource.Length;
                //End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, Start);
            }

            return "";
        }


        public JArray checksource(string id, string code, string strdate, string enddate)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");

            string spname = "workflow.check_source";
            string p1 = "p_id," + id + ",s";
            string p2 = "p_code," + code + ",s";
            string p3 = "p_strdate," + strdate + ",s";
            string p4 = "p_enddate," + enddate + ",s";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject_flows(provider, cstrname, spname, p1, p2, p3,p4);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray getdaterptslasummary(string code, string strdate, string enddate)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");
            string spname = "workflow.getdatarptslasummary";
            string p1 = "p_code," + code + ",s";
            string p2 = "p_strdate," + strdate + ",s";
            string p3 = "p_enddate," + enddate + ",s";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject_flows(provider, cstrname, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray getdaterptslaCompliance(string code, string strdate, string enddate)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");
            string spname = "getdatarptslacompliance";
            string p1 = "@code," + code + ",s";
            string p2 = "@strdate," + strdate + ",s";
            string p3 = "@enddate," + enddate + ",s";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject_flows(provider, cstrname, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

        public JArray getdetailsummary(string code, string str, string end)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");

            string spname = "workflow.getdatadetailslasummary";
            string p1 = "p_code," + code + ",s";
            string p2 = "p_strdate," + str + ",s";
            string p3 = "p_enddate," + end + ",s";
            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject_flows(provider, cstrname,spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }


        public JArray getnamesource(string name, string id)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");

            string spname = "workflow.getdateallsource";
            string p1 = "p_name," + name + ",s";
            string p2 = "p_id," + id + ",s";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject_flows(provider, cstrname, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;

        }

    }
}
