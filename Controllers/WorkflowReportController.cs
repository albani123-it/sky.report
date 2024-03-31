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
    public class WorkflowReportController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private lflow lf = new lflow();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lDbConn dbconn = new lDbConn();

        [HttpGet("getcodeworkflow")]
        public JObject getcodeworkflow()
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");

            string spname = "workflow.get_workflow_bycode";
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

        [HttpGet("getfieldworkflow")]
        public JObject getddlaxisparam()
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");

            string spname = "workflow.get_allfield_workflow";
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

        [HttpPost("getddl3_byddl1")]
        public JObject getddl3byddl1([FromBody]JObject json)
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");
            var data = new JObject();
            try
            {
                var datatype = json.GetValue("datatype").ToString().ToLower();

                if (datatype != "integer" )
                {
                    if (datatype != "numeric")
                    {
                        string spname = "workflow.get_workflow_byfield";
                        string p1 = "p_field," + json.GetValue("field").ToString() + ",s";
                        var retObject = new List<dynamic>();
                        retObject = bc.getDataToObject_flows(provider, cstrname, spname, p1);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("message", mc.GetMessage("process_success"));
                        data.Add("data", lc.convertDynamicToJArray(retObject));
                    }
                       
                }
                
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpPost("Show/workflow/Report")]
        public JObject ShowWorkflowReport([FromBody]JObject json)
        {
            var data = new JObject();
            var joReturn = new JObject();
            var joReport = new JObject();
            var jaLabel = new JArray();
            var joLabel = new JObject();
            var jaDataSet = new JArray();
            var jaRawData = new JArray();
            var joHorizontal = new JObject();
          
            try
            {
                JArray jaParamsx = JArray.Parse(json["x_param"].ToString());
                for (int a = 0; a < jaParamsx.Count; a++)
                {
                    if (jaParamsx[a]["type"].ToString().ToLower() != "count")
                    {
                        if (jaParamsx[a]["datatype"].ToString().ToLower() == "integer")
                        {
                            joLabel.Add("data" + Convert.ToString(a + 1), jaParamsx[a]["type"].ToString().ToUpper() + " " + jaParamsx[a]["field"].ToString().ToUpper());
                        }
                        else if (jaParamsx[a]["datatype"].ToString().ToLower() == "numeric")
                        {
                            joLabel.Add("data" + Convert.ToString(a + 1), jaParamsx[a]["type"].ToString().ToUpper() + " " + jaParamsx[a]["field"].ToString().ToUpper());

                        }
                       
                    }
                    else 
                    {
                        if (jaParamsx[a]["datatype"].ToString().ToLower() == "integer")
                        {
                            joLabel.Add("data" + Convert.ToString(a + 1), jaParamsx[a]["type"].ToString().ToUpper() + " #OF " + jaParamsx[a]["field"].ToString().ToUpper());
                        }
                        else if (jaParamsx[a]["datatype"].ToString().ToLower() == "numeric")
                        {
                            joLabel.Add("data" + Convert.ToString(a + 1), jaParamsx[a]["type"].ToString().ToUpper() + " #OF " + jaParamsx[a]["field"].ToString().ToUpper());
                        }
                        else
                        {
                            joLabel.Add("data" + Convert.ToString(a + 1), jaParamsx[a]["type"].ToString().ToUpper() + " #OF " + jaParamsx[a]["filter"].ToString().ToUpper());
                        }


                    }
                    

                    
                }
                jaLabel.Add(joLabel);
                data.Add("labels_chart", jaLabel);

               
                JArray janame = JArray.Parse(json["workflow"].ToString());
                for (int x = 0; x < janame.Count; x++)
                {
                    var joDataSet = new JObject();
                    var jaHorizontal = new JArray();

                    joDataSet.Add("label_name", janame[x]["name"].ToString().ToUpper());
                    var values = "workflow_code ='" + janame[x]["code"].ToString().ToUpper() + "'";
                    var condition = json.GetValue("rules").ToString();
                    var tablename = "workflow_log_history";
                    jaHorizontal = lf.getexecData(jaParamsx, condition, tablename, values);
                    joDataSet.Add("dataHorizontal", jaHorizontal);
                    jaRawData.Add(joDataSet);
                }
               
               
                data.Add("datasets_chart", new JArray(jaRawData));


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