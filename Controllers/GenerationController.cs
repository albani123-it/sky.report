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
    public class GenerationController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private lData oDat = new lData();
        private lParsingReader lpr = new lParsingReader();
        private BaseController bc = new BaseController();

        #region Get


        [HttpGet("list")]
        public JObject getListAllReport()
        {
            var data = new JObject();
            var retObject = new List<dynamic>();
            try
            {
              
                string spname = "public.listall_generation";
              
                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname);
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


        [HttpGet("listReport/{id}")]
        public JObject getListAllGeneration(string id)
        {
            var data = new JObject();
            var retObject = new List<dynamic>();
            try
            {
                var split = "|";

                string spname = "public.listdetail_report";
                string p1 = "p_headerid" + split + id + split + "i";

                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname, p1);
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


        [HttpGet("DetailGeneration/{id}")]
        public JObject getDetail(string id)
        {
            var data = new JObject();
            var retObject = new List<dynamic>();
            var retObject1 = new List<dynamic>();
            try
            {
                var split = "|";
                string spname = "public.Detail_generation";
                string p1 = "p_id" + split + id + split + "i";
                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname, p1);
                var jFilter = lc.convertDynamicToJArray(retObject);
                var field = jFilter[0]["rpthsql"].ToString();

                if (field != null)
                {
                    string spname1 = "public.Detail_generation_query";
                    string p2 = "p_sql," + field + ",s";
                     bc.execSqlWithExecptionReport(spname1, p2);
                }

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


        [HttpGet("DetailReport/{id}")]
        public JObject getDetailReport(string id)
        {
            var data = new JObject();
            var retObject = new List<dynamic>();
            var retObject1 = new List<dynamic>();
            try
            {
                var split = "|";
                string spname = "public.detail_generation_report";
                string p1 = "p_id" + split + id + split + "i";
                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname, p1);
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



        #endregion
        #region POST

        [HttpPost("Insert")]
        public JObject InsertGeneration([FromBody]JObject json)
        {
            var data = new JObject();
            try
            {
                var split = ";";

                string spname = "public.insert_generation";
                string p1 = "p_name" + split + json.GetValue("name").ToString() + split + "s";
                string p2 = "p_desc" + split + json.GetValue("desc").ToString() + split + "s";
                string p3 = "p_sql" + split + json.GetValue("sql").ToString() + split + "s";
                string p4 = "p_code" + split + json.GetValue("code").ToString() + split + "s";
                string p5 = "p_user" + split + json.GetValue("user").ToString() + split + "s";
                var retObject = new List<dynamic>();
                retObject = bc.ExecSqlWithReturnCustomSplitnotsymbolReport(spname, p1, p2, p3, p4, p5);
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


        [HttpPost("syingquery")]
        public JObject syingquery([FromBody]JObject json)
        {
            var data = new JObject();
            var retObject = new List<dynamic>();
            var retObject1 = new List<dynamic>();
            var retObject2 = new List<dynamic>();
            var retObject3 = new List<dynamic>();
            try
            {

                var headerid = json.GetValue("headerid").ToString();
                if (headerid == "0")
                {

                    var split = "|";
                    string spname = "public.check_countheader";

                    retObject = bc.ExecSqlWithReturnCustomSplitReport(spname);
                    var jFilter = lc.convertDynamicToJArray(retObject);
                    var header = jFilter[0]["header_count"].ToString();

                    if (header.Length > 0)
                    {
                        string spname3 = "public.check_delete_headerid";
                        string p1 = "p_headerid" + split + header + split + "i";
                        retObject3 = bc.ExecSqlWithReturnCustomSplitReport(spname3, p1);


                        var split1 = ";";
                        string spname1 = "public.last_objects";
                        string p2 = "p_sql" + split1 + json.GetValue("sql").ToString() + split1 + "s";

                        retObject1 = bc.ExecSqlWithReturnCustomSplitnotsymbolReport(spname1, p2);
                        var jFilter1 = lc.convertDynamicToJArray(retObject1);

                        for (int i = 0; i < jFilter1.Count(); i++)
                        {
                            var filed = jFilter1[i]["columnname"].ToString();
                            var typedata = jFilter1[i]["datatype"].ToString();
                            var alias = filed.Replace("_", " ");
                            var urutan = i + 1;
                            string spname2 = "public.insert_datasying";
                            string p3 = "p_header" + split + header + split + "i";
                            string p4 = "p_filed" + split + filed + split + "s";
                            string p5 = "p_typedata" + split + typedata + split + "s";
                            string p6 = "p_alias" + split + alias + split + "s";
                            string p7 = "p_urutan" + split + urutan + split + "i";
                            retObject2 = bc.ExecSqlWithReturnCustomSplitReport(spname2, p3, p4, p5, p6, p7);


                        }

                        data.Add("status", mc.GetMessage("api_output_ok"));
                        //data.Add("message", mc.GetMessage("process_success"));
                        data.Add("data", lc.convertDynamicToJArray(retObject2));

                    }

                }
                else {
                    var split = "|";

                    string spname3 = "public.check_delete_headerid";
                    string p1 = "p_headerid" + split + headerid + split + "i";
                    retObject3 = bc.ExecSqlWithReturnCustomSplitReport(spname3, p1);


                    var split1 = ";";
                    string spname1 = "public.last_objects";
                    string p2 = "p_sql" + split1 + json.GetValue("sql").ToString() + split1 + "s";

                    retObject1 = bc.ExecSqlWithReturnCustomSplitnotsymbolReport(spname1, p2);
                    var jFilter1 = lc.convertDynamicToJArray(retObject1);

                    for (int i = 0; i < jFilter1.Count(); i++)
                    {
                        var filed = jFilter1[i]["columnname"].ToString();
                        var typedata = jFilter1[i]["datatype"].ToString();
                        var alias = filed.Replace("_", " ");
                        var urutan = i + 1;
                        string spname2 = "public.insert_datasying";
                        string p3 = "p_header" + split + headerid + split + "i";
                        string p4 = "p_filed" + split + filed + split + "s";
                        string p5 = "p_typedata" + split + typedata + split + "s";
                        string p6 = "p_alias" + split + alias + split + "s";
                        string p7 = "p_urutan" + split + urutan + split + "i";
                        retObject2 = bc.ExecSqlWithReturnCustomSplitReport(spname2, p3, p4, p5, p6, p7);


                    }

                    data.Add("status", mc.GetMessage("api_output_ok"));
                    //data.Add("message", mc.GetMessage("process_success"));
                    data.Add("data", lc.convertDynamicToJArray(retObject2));

                }



                //var result = lpr.ValidationFormat(header, sql); 
                //data.Add("status", mc.GetMessage("api_output_ok"));
                //data.Add("message", mc.GetMessage("process_success"));
                //data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpPost("Update")]
        public JObject UpdateGeneration([FromBody]JObject json)
        {
            var data = new JObject();
            try
            {
                var split = ";";

                string spname = "public.update_generation";
                string p1 = "p_id" + split + json.GetValue("id").ToString() + split + "i";
                string p2 = "p_name" + split + json.GetValue("name").ToString() + split + "s";
                string p3 = "p_desc" + split + json.GetValue("desc").ToString() + split + "s";
                string p4 = "p_sql" + split + json.GetValue("sql").ToString() + split + "s";
                string p5 = "p_code" + split + json.GetValue("code").ToString() + split + "s";
                string p6 = "p_user" + split + json.GetValue("user").ToString() + split + "s";
                var retObject = new List<dynamic>();

                retObject = bc.ExecSqlWithReturnCustomSplitnotsymbolReport(spname, p1, p2, p3, p4, p5, p6);
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

        [HttpPost("UpdateDetailReport")]
        public JObject UpdateDetailReport([FromBody]JObject json)
        {
            var data = new JObject();
            try
            {
                var split = "|";

                string spname = "public.update_reportdetail";
                string p1 = "p_id" + split + json.GetValue("id").ToString() + split + "i";
                string p2 = "p_name" + split + json.GetValue("name").ToString() + split + "s";
                string p3 = "p_type" + split + json.GetValue("type").ToString() + split + "s";
                string p4 = "p_isfilter" + split + json.GetValue("isfilter").ToString() + split + "b";
                string p5 = "p_filter" + split + json.GetValue("filter").ToString() + split + "s";
                string p6 = "p_filter_alias" + split + json.GetValue("filteralias").ToString() + split + "s";
                string p7 = "p_filter_isview" + split + json.GetValue("filterisview").ToString() + split + "b";
                string p8 = "p_filter_order" + split + json.GetValue("filterorder").ToString() + split + "i";
                var retObject = new List<dynamic>();

                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname, p1, p2, p3, p4, p5, p6, p7, p8);
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

        [HttpPost("DeleteDetailReport")]
        public JObject DeleteDetailReport([FromBody]JObject json)
        {
            var data = new JObject();
            try
            {
                var split = "|";

                string spname = "public.delete_reportdetail";
                string p1 = "p_id" + split + json.GetValue("id").ToString() + split + "i";
               
                var retObject = new List<dynamic>();

                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname, p1);
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

        [HttpPost("Delete")]
        public JObject Deletegeneration([FromBody]JObject json)
        {
            var data = new JObject();
            try
            {
                var split = "|";

                string spname = "public.delete_generation";
                string p1 = "p_id" + split + json.GetValue("id").ToString() + split + "i";

                var retObject = new List<dynamic>();

                retObject = bc.ExecSqlWithReturnCustomSplitReport(spname, p1);
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

        #endregion

    }
}