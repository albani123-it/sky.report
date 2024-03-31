using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using sky.report.Libs;

namespace sky.report.Controllers
{
    [Route("skyreport/[controller]")]
    public class SLAController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lReportsla sla = new lReportsla();
        private lDbConn dbconn = new lDbConn();


        [HttpGet("getdecflow")]
        public JObject getcodeDecflow()
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");
            string spname = "workflow.getdatadecflow_bycode";
            var retObject = new List<dynamic>();
            var data = new JObject();
            try
            {
                data = new JObject();
                retObject = bc.getDataToObject_flows(provider, cstrname, spname);
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpPost("Show/Summary/Report")]
        public JObject detailsummary([FromBody] JObject json)
        {
            var data = new JObject();
            var joReturn = new JObject();
            var jaRawData = new JArray();
            var joHorizontal = new JObject();

            try
            {

                var jaHorizontal = new JArray();
                var code = json["code"].ToString();
                var str = json["strdate"].ToString();
                var end = json["enddate"].ToString();

                var dtresult = sla.getdetailsummary(code, str, end);
                for (int i = 0; i < dtresult.Count(); i++)
                {
                    var joDataSet = new JObject();
                    joDataSet.Add("id", dtresult[i]["v_rowact"].ToString());
                    joDataSet.Add("refid", dtresult[i]["v_dfr_rsh_id"].ToString());
                    joDataSet.Add("df_code", dtresult[i]["v_flh_code"].ToString());
                    joDataSet.Add("label_name", dtresult[i]["v_flh_name"].ToString());
                    joDataSet.Add("wf_id", dtresult[i]["v_dfr_wf_id"].ToString());
                    joDataSet.Add("strdate", dtresult[i]["v_strdate"].ToString());
                    joDataSet.Add("enddate", dtresult[i]["v_enddate"].ToString());
                    joDataSet.Add("proccessing", dtresult[i]["v_proctime"].ToString());
                    jaRawData.Add(joDataSet);
                }




                data.Add("data", new JArray(jaRawData));


            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("detail")]
        public JObject Detail([FromBody] JObject json)
        {
            var data = new JObject();
            var joReturn = new JObject();
            var jaRawData = new JArray();
            var joHorizontal = new JObject();

            try
            {

                var id = json.GetValue("id").ToString();
                var code = json.GetValue("code").ToString();
                var strdate = json.GetValue("strdate").ToString();
                var enddate = json.GetValue("enddate").ToString();

                var dtchecksource = sla.checksource(id, code, strdate, enddate);
                for (int i = 0; i < dtchecksource.Count(); i++)
                {
                    var joDataSet = new JObject();
                    joDataSet.Add("label", dtchecksource[0]["v_dfname"].ToString());
                    joDataSet.Add("counter", dtchecksource[i]["v_dfr_counter"].ToString());
                  
                    var source = dtchecksource[i]["v_dfr_source"].ToString();


                    if (source.Contains("CRX") == true)
                    {

                        string[] tokens = source.Split("CRX");
                        bool fHasSpace = tokens[1].Contains(" ");
                        string newText = "";
                        if (fHasSpace == true)
                        {
                            newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                        }
                        else
                        {
                            newText = tokens[1];
                        }

                        var dtcheckid = sla.getnamesource("CRX", newText);

                        if (dtcheckid.Count > 0)
                        {
                            joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            joDataSet.Add("type", "Rule Set");
                        }
                        else
                        {
                            joDataSet.Add("name", "");
                            joDataSet.Add("type", "Rule Set");
                        }

                    }
                    else if (source.Contains("RL") == true || source.Contains("RLX") == true || source.Contains("QUE") == true)
                    {
                        string[] tokens;
                        if (source.Contains("RLX") == true)
                        {
                            tokens = source.Split("RLX");
                            bool fHasSpace = tokens[1].Contains(" ");
                            string newText = "";
                            if (fHasSpace == true)
                            {
                                newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                            }
                            else
                            {
                                newText = tokens[1];
                            }
                            var dtcheckid = sla.getnamesource("RLX", newText);
                            if (dtcheckid.Count > 0)
                            {
                                joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            }
                            else
                            {
                                joDataSet.Add("name", "");
                            }

                        }
                        else if (source.Contains("QUE") == true)
                        {
                            tokens = source.Split("QUE");
                            bool fHasSpace = tokens[1].Contains(" ");
                            string newText = "";
                            if (fHasSpace == true)
                            {
                                newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                            }
                            else
                            {
                                newText = tokens[1];
                            }
                            var dtcheckid = sla.getnamesource("QUE", newText);
                            if (dtcheckid.Count > 0)
                            {
                                joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            }
                            else
                            {
                                joDataSet.Add("name", "");
                            }

                        }
                        else
                        {

                            tokens = source.Split("RL");
                            bool fHasSpace = tokens[1].Contains(" ");
                            string newText = "";
                            if (fHasSpace == true)
                            {
                                newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                            }
                            else
                            {
                                newText = tokens[1];
                            }
                            var dtcheckid = sla.getnamesource("RL", newText);
                            if (dtcheckid.Count > 0)
                            {
                                joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            }
                            else
                            {
                                joDataSet.Add("name", "");
                            }
                        }


                        joDataSet.Add("type", "Rule");
                    }
                    else if (source.Contains("DT") == true || source.Contains("DTX") == true)
                    {

                        string[] tokens = source.Split("DTX");
                        bool fHasSpace = tokens[1].Contains(" ");
                        string newText = "";
                        if (fHasSpace == true)
                        {
                            newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                        }
                        else
                        {
                            newText = tokens[1];
                        }
                        var dtcheckid = sla.getnamesource("DTX", newText);
                        if (dtcheckid.Count > 0)
                        {
                            joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            joDataSet.Add("type", "Decison Tree");
                        }
                        else
                        {
                            joDataSet.Add("name", "");
                            joDataSet.Add("type", "Decison Tree");
                        }

                    }
                    else if (source.Contains("DB") == true || source.Contains("DBX") == true)
                    {

                        string[] tokens = source.Split("DBX");
                        bool fHasSpace = tokens[1].Contains(" ");
                        string newText = "";
                        if (fHasSpace == true)
                        {
                            newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                        }
                        else
                        {
                            newText = tokens[1];
                        }
                        var dtcheckid = sla.getnamesource("DBX", newText);
                        if (dtcheckid.Count > 0)
                        {
                            joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            joDataSet.Add("type", "Decison Table");
                        }
                        else
                        {
                            joDataSet.Add("name", "");
                            joDataSet.Add("type", "Decison Table");
                        }

                    }
                    else if (source.Contains("SCX") == true)
                    {


                        string[] tokens = source.Split("SCX");
                        bool fHasSpace = tokens[1].Contains(" ");
                        string newText = "";
                        if (fHasSpace == true)
                        {
                            newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                        }
                        else
                        {
                            newText = tokens[1];
                        }
                        var dtcheckid = sla.getnamesource("SCX", newText);
                        if (dtcheckid.Count > 0)
                        {
                            joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            joDataSet.Add("type", "Scorecard");
                        }
                        else
                        {
                            joDataSet.Add("name", "");
                            joDataSet.Add("type", "Scorecard");
                        }

                    }
                    else if (source.Contains("MLE") == true)
                    {


                        string[] tokens = source.Split("MLE");
                        bool fHasSpace = tokens[1].Contains(" ");
                        string newText = "";
                        if (fHasSpace == true)
                        {
                            newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                        }
                        else
                        {
                            newText = tokens[1];
                        }
                        var dtcheckid = sla.getnamesource("MLE", newText);
                        if (dtcheckid.Count > 0)
                        {
                            joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            joDataSet.Add("type", "Machine Learning");
                        }
                        else
                        {
                            joDataSet.Add("name", "");
                            joDataSet.Add("type", "machine Learning");
                        }

                    }
                    else if (source.Contains("CBX") == true)
                    {
                        joDataSet.Add("name", "Pefindo-Personal");
                        joDataSet.Add("type", "Credit Biro");

                    }
                    else if (source.Contains("D") == true)
                    {
                        string[] tokens = source.Split("D");
                        bool fHasSpace = tokens[1].Contains(" ");
                        string newText = "";
                        if (fHasSpace == true)
                        {
                            newText = tokens[1].Remove(tokens[1].IndexOf(' '));
                        }
                        else
                        {
                            newText = tokens[1];
                        }
                        var dtcheckid = sla.getnamesource("D", newText);
                        if (dtcheckid.Count > 0)
                        {
                            joDataSet.Add("name", dtcheckid[0]["v_name"].ToString());
                            joDataSet.Add("type", "Decision Flow");
                        }
                        else
                        {
                            joDataSet.Add("name", "");
                            joDataSet.Add("type", "Decision Flow");
                        }


                    }

                    joDataSet.Add("processing", dtchecksource[i]["v_proctime"].ToString());
                    var ja1 = JArray.Parse(dtchecksource[i]["v_dfr_result"].ToString());

                    var jo3 = ja1[0]["status"].ToString().ToLower();
                    if (jo3 == "true")
                    {
                        joDataSet.Add("result", "Success");
                    }
                    else
                    {
                        joDataSet.Add("result", "Failed");
                    }

                    jaRawData.Add(joDataSet);

                }

                data.Add("data", new JArray(jaRawData));


            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

    }
}
