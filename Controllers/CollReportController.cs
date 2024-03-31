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

    public class CollReportController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lReport lr = new lReport();
        private lDbConn dbconn = new lDbConn();

        [HttpPost("monitoring/dc/list")]
        public JObject ListMonitorDCReport([FromBody] JObject json)
        {
            var data = new JObject();
            var jFormCont = new JObject();
            var usrid = json.GetValue("userid").ToString();

            try
            {
                var dtretrunusr = lr.getDataUserdetail(usrid);

                if (dtretrunusr.Count > 0)
                {
                    var usr_id = dtretrunusr[0]["usrid"].ToString();
                    var period_from = json.GetValue("strdate").ToString();
                    var period_to = json.GetValue("enddate").ToString();
                    var name = json.GetValue("name").ToString();
                    var accno = json.GetValue("accno").ToString();
                    var dpdmin = json.GetValue("dpdmin").ToString();
                    var dpdmax = json.GetValue("dpdmax").ToString();
                    var alasan = json.GetValue("alasan").ToString();
                    var janji = json.GetValue("janji").ToString();
                    var branchid = json.GetValue("branchid").ToString();
                    var agentid = "";



                    if (period_from != "" && period_to != "" && name != "" && accno != "" && dpdmin != "" && dpdmax != "" && alasan != "" && janji != "" && branchid != "" && agentid != "")
                    {
                        var jasummary = lr.getsummarydcreportall(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        var jadata = lr.GetListDCReportall(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);

                    }
                    else if (name == "" && accno == "" && dpdmin == "" && dpdmax == "" && alasan == "" && janji == "" && branchid == "" && agentid == "")
                    {
                        var jasummary = lr.getsummarydcreportnotall(period_from, period_to, usr_id);
                        var jadata = lr.GetListDCReportnotall(period_from, period_to, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);
                    }
                    else
                    {
                        var jasummary = lr.getsummarydcreport(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        var jadata = lr.GetListDCReport(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);

                    }



                }
                else
                {
                    data = new JObject();
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", "data user not found");
                }


            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpGet("ddwon/agent")]
        public JObject GetDataDownAgent()
        {
            var retObject = new List<dynamic>();
            var data = new JObject();

            var jaFormDet = new JArray();
            try
            {
                var dtretrun = lr.GetDataDownAgent();

                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", dtretrun);


            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("distribusi/history")]
        public JObject ListDistribusiHistoryReport([FromBody] JObject json)
        {
            var data = new JObject();
            var jFormCont = new JObject();
     
            try
            {
           
                var period_from = json.GetValue("strdate").ToString();
                var period_to = json.GetValue("enddate").ToString();
                var name = json.GetValue("name").ToString();
                var accno = json.GetValue("accno").ToString();
                var dpdmin = json.GetValue("dpdmin").ToString();
                var dpdmax = json.GetValue("dpdmax").ToString();
                var agentid = json.GetValue("agentid").ToString();

                if (period_from != "" && period_to != "" && name != "" && accno != "" && dpdmin != "" && dpdmax != "" && agentid != "")
                {
                    var jadata = lr.GetListdistribusihistoryReportall(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && name != "" && accno != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && name != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && accno != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && dpdmin != "" && dpdmax != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && dpdmin != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && dpdmax != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && agentid != "")
                {
                    var jadata = lr.GetListdistribusihistoryReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else 
                {
                    var jadata = lr.GetListdistribusihistoryReportnotall(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);

                }

            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("payment/record")]
        public JObject ListpaymentrecordReport([FromBody] JObject json)
        {
            var data = new JObject();
            var jFormCont = new JObject();

            try
            {

                var period_from = json.GetValue("strdate").ToString();
                var period_to = json.GetValue("enddate").ToString();
                var name = json.GetValue("name").ToString();
                var accno = json.GetValue("accno").ToString();
                var agentid = json.GetValue("agentid").ToString();

                if (period_from != "" && period_to != "" && name != "" && accno != ""  && agentid != "")
                {
                    var jadata = lr.GetListpaymentredordReportall(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && name != "" && accno != "")
                {
                    var jadata = lr.GetListpaymentredordReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && name != "")
                {
                    var jadata = lr.GetListpaymentredordReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && accno != "")
                {
                    var jadata = lr.GetListpaymentredordReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
               
                else if (period_from != "" && period_to != "" && agentid != "")
                {
                    var jadata = lr.GetListpaymentredordReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else
                {
                    var jadata = lr.GetListpaymentredordReportnotall(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);

                }

            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("nasabah/baru")]
        public JObject ListNasabahBaruReport([FromBody] JObject json)
        {
            var data = new JObject();
            var jFormCont = new JObject();

            try
            {

                var period_from = json.GetValue("strdate").ToString();
                var period_to = json.GetValue("enddate").ToString();
                var name = json.GetValue("name").ToString();
                var accno = json.GetValue("accno").ToString();
                var agentid = json.GetValue("agentid").ToString();

                if (period_from != "" && period_to != "" && name != "" && accno != "" && agentid != "")
                {
                    var jadata = lr.GetListnasabahbaruReportall(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && name != "" && accno != "")
                {
                    var jadata = lr.GetListnasabahbaruReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && name != "")
                {
                    var jadata = lr.GetListnasabahbaruReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else if (period_from != "" && period_to != "" && accno != "")
                {
                    var jadata = lr.GetListnasabahbaruReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }

                else if (period_from != "" && period_to != "" && agentid != "")
                {
                    var jadata = lr.GetListnasabahbaruReport(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);
                }
                else
                {
                    var jadata = lr.GetListnasabahbaruReportnotall(json);
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("data", jadata);

                }

            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpGet("Geolocation")]
        public JObject GetDataGeoLocation()
        {
            var retObject = new List<dynamic>();
            var data = new JObject();

            var jaFormDet = new JArray();
            try
            {
                var dtretrun = lr.GetDataGeoLocation();

                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", dtretrun);


            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("activitas")]
        public JObject ListActivitasReport([FromBody] JObject json)
        {
            var data = new JObject();
            var jFormCont = new JObject();
            var usrid = json.GetValue("userid").ToString();

            try
            {
                var dtretrunusr = lr.getDataUserdetail(usrid);

                if (dtretrunusr.Count > 0)
                {
                    var usr_id = dtretrunusr[0]["usrid"].ToString();
                    var period_from = json.GetValue("strdate").ToString();
                    var period_to = json.GetValue("enddate").ToString();
                    var name = json.GetValue("name").ToString();
                    var accno = json.GetValue("accno").ToString();
                    var dpdmin = json.GetValue("dpdmin").ToString();
                    var dpdmax = json.GetValue("dpdmax").ToString();
                    var alasan = json.GetValue("alasan").ToString();
                    var janji = json.GetValue("janji").ToString();
                    var branchid = json.GetValue("branchid").ToString();
                    var agentid = "";



                    if (period_from != "" && period_to != "" && name != "" && accno != "" && dpdmin != "" && dpdmax != "" && alasan != "" && janji != "" && branchid != "" && agentid != "")
                    {
                        var jasummary = lr.getsummaryacrivityreportall(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        var jadata = lr.GetListActivitasReportall(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);

                    }
                    else if (name == "" && accno == "" && dpdmin == "" && dpdmax == "" && alasan == "" && janji == "" && branchid == "" && agentid == "")
                    {
                        var jasummary = lr.getsummaryacrivityreportnotall(period_from, period_to, usr_id);
                        var jadata = lr.GetListActivitasReportnotall(period_from, period_to, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);
                    }
                    else
                    {
                        var jasummary = lr.getsummaryacrivityreport(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        var jadata = lr.GetListActivitasReport(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);

                    }



                }
                else
                {
                    data = new JObject();
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", "data user not found");
                }


            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("monitor/fc/list")]
        public JObject ListmonitorFCReport([FromBody] JObject json)
        {
            var data = new JObject();
            var jFormCont = new JObject();
            var usrid = json.GetValue("userid").ToString();

            try
            {
                var dtretrunusr = lr.getDataUserdetail(usrid);

                if (dtretrunusr.Count > 0)
                {
                    var usr_id = dtretrunusr[0]["usrid"].ToString();
                    var period_from = json.GetValue("strdate").ToString();
                    var period_to = json.GetValue("enddate").ToString();
                    var name = json.GetValue("name").ToString();
                    var accno = json.GetValue("accno").ToString();
                    var dpdmin = json.GetValue("dpdmin").ToString();
                    var dpdmax = json.GetValue("dpdmax").ToString();
                    var alasan = json.GetValue("alasan").ToString();
                    var janji = json.GetValue("janji").ToString();
                    var branchid = json.GetValue("branchid").ToString();
                    var agentid = "";



                    if (period_from != "" && period_to != "" && name != "" && accno != "" && dpdmin != "" && dpdmax != "" && alasan != "" && janji != "" && branchid != "" && agentid != "")
                    {
                        var jasummary = lr.getsummaryfcreportall(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        var jadata = lr.GetListFCReportall(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);

                    }
                    else if (name == "" && accno == "" && dpdmin == "" && dpdmax == "" && alasan == "" && janji == "" && branchid == "" && agentid == "")
                    {
                        var jasummary = lr.getsummaryfcreportnotall(period_from, period_to, usr_id);
                        var jadata = lr.GetListFCReportnotall(period_from, period_to, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);
                    }
                    else
                    {
                        var jasummary = lr.getsummaryfcreport(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        var jadata = lr.GetListActivitasReport(period_from, period_to, name, accno, dpdmin, dpdmax, alasan, janji, branchid, agentid, usr_id);
                        if (jasummary.Count > 0)
                        {
                            jFormCont.Add("ttl_tunggakan", jasummary[0]["v_tunggakan_total"].ToString());
                            jFormCont.Add("ttl_kewajiban", jasummary[0]["v_kewajiban_total"].ToString());
                        }
                        else
                        {
                            jFormCont.Add("ttl_tunggakan", "0.00");
                            jFormCont.Add("ttl_kewajiban", "0.00");
                        }

                        jFormCont.Add("detail", jadata);
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("data", jFormCont);

                    }



                }
                else
                {
                    data = new JObject();
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", "data user not found");
                }


            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpGet("ddwon/janjibayar")]
        public JObject ddljanjibayar()
        {
            var data = new JObject();
            try
            {

                var dtReturn = lr.ddljanjibayar();
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", dtReturn);
            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpGet("ddwon/janjibayar/fc")]
        public JObject ddljanjibayarfc()
        {
            var data = new JObject();
            try
            {

                var dtReturn = lr.ddljanjibayarFC();
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", dtReturn);
            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpGet("ddwon/reason")]
        public JObject ddlreason()
        {
            var data = new JObject();
            try
            {

                var dtReturn = lr.ddlreason();
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", dtReturn);
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
