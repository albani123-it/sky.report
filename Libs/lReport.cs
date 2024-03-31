using sky.report.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sky.report.Libs
{
    public class lReport
    {
        private lDbConn dbconn = new lDbConn();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private MessageController mc = new MessageController();

        public JArray ListApplicationReport(JObject json)
        {
            var jaReturn = new JArray();
            
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "workflow";

            string spname = "rpt_application_filter";
            string p1 = "@prd_code" + split + json.GetValue("product_code") + split + "s";
            string p2 = "@program" + split + json.GetValue("program") + split + "s";
            string p3 = "@str_date" + split + json.GetValue("start_date") + split + "s";
            string p4 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            string p5 = "@status" + split + json.GetValue("status") + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3, p4, p5);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray ListInsuranceReport(JObject json)
        {
            var jaReturn = new JArray();
            
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "submission";

            //string spname = "rpt_insurance_filter";
            string spname = "an_rpt_insurance_filter";
            string p1 = "@prd_code" + split + json.GetValue("product_code") + split + "s";
            string p2 = "@program" + split + json.GetValue("program") + split + "s";
            string p3 = "@str_date" + split + json.GetValue("start_date") + split + "s";
            string p4 = "@end_date" + split + json.GetValue("end_date") + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3, p4);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray ListPaymentReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            
            var cstrname = dbconn.constringName("dynamic");
            var dbname = dbconn.dbname("idclms");
            var split = "||";
            var schema = "payment";

            string spname = "rpt_payment_filter";
            string p1 = "@prd_code" + split + json.GetValue("product_code") + split + "s";
            string p2 = "@program" + split + json.GetValue("program") + split + "s";
            string p3 = "@str_date" + split + json.GetValue("start_date") + split + "s";
            string p4 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplitDynamic(dbname, cstrname, split, schema, spname, p1, p2, p3, p4);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray ListPaymentWriteOffReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            
            var cstrname = dbconn.constringName("dynamic");
            var dbname = dbconn.dbname("idclms");
            var split = "||";
            var schema = "repayment";

            string spname = "rpt_repayment_write_off_filter";
            string p1 = "@str_date" + split + json.GetValue("start_date") + split + "s";
            string p2 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplitDynamic(dbname,  cstrname, split, schema, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray ListPaymentOverbookingReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            
            var cstrname = dbconn.constringName("dynamic");
            var dbname = dbconn.dbname("idclms");
            var split = "||";
            var schema = "payment";

            string spname = "rpt_payment_overbooking_filter";
            string p1 = "@status" + split + json.GetValue("status") + split + "s";
            string p2 = "@str_date" + split + json.GetValue("start_date") + split + "s";            
            string p3 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplitDynamic(dbname,  cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray ListGLReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            
            var cstrname = dbconn.constringName("dynamic");
            var dbname = dbconn.dbname("idclms");
            var split = "||";
            var schema = "general_ledger";

            string spname = "rpt_general_ledger_filter";
            string p1 = "@str_date" + split + json.GetValue("start_date") + split + "s";
            string p2 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplitDynamic(dbname,  cstrname, split, schema, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }


        public JArray ListDisbursementReport(JObject json)
        {
            var jaReturn = new JArray();
            
            var cstrname = dbconn.constringName("idcen");            
            var split = "||";
            var schema = "submission";

            string spname = "rpt_disbursement_filter";
            string p1 = "@prd_code" + split + json.GetValue("product_code") + split + "s";
            string p2 = "@program" + split + json.GetValue("program") + split + "s";
            string p3 = "@str_date" + split + json.GetValue("start_date") + split + "s";
            string p4 = "@end_date" + split + json.GetValue("end_date") + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3, p4);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }


        public JArray getListProductProgram()
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");


            var split = "||";
            var schema = "public";

            string spname = "get_program";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray ListCollectionSummaryReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            
            var cstrname = dbconn.constringName("dynamic");
            var dbname = dbconn.dbname("idclms");
            var split = "||";
            var schema = "interface";

            string spname = "rpt_oncsummary_filter";
            string p1 = "@prd_code" + split + json.GetValue("prd_code") + split + "s";
            string p2 = "@dpd_from" + split + json.GetValue("dpd_from") + split + "s";
            string p3 = "@dpd_to" + split + json.GetValue("dpd_to") + split + "s";
            string p4 = "@npl_status" + split + json.GetValue("npl_status") + split + "s";
            string p5 = "@write_off" + split + json.GetValue("write_off") + split + "s";

            retObject = bc.ExecSqlWithReturnCustomSplitDynamic(dbname,  cstrname, split, schema, spname, p1, p2, p3, p4, p5);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray ListBillingReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            
            var cstrname = dbconn.constringName("dynamic");
            var dbname = dbconn.dbname("idclms");
            var split = "||";
            var schema = "loan";

            string spname = "rpt_billing_filter";
            string p1 = "@prd_code" + split + json.GetValue("prd_code") + split + "s";
            string p2 = "@channel" + split + json.GetValue("channel") + split + "s";

            retObject = bc.ExecSqlWithReturnCustomSplitDynamic(dbname,  cstrname, split, schema, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray ListCBASLogsReport(JObject json)
        {
            var jaReturn = new JArray();
            
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "bk";

            string spname = "rpt_cbaslog_filter";
            string p1 = "@app_date_star" + split + json.GetValue("app_date_star") + split + "s";
            string p2 = "@app_date_end" + split + json.GetValue("app_date_end") + split + "s";
            string p3 = "@status" + split + json.GetValue("status") + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }
	
		public JArray ListETLSchedulerReport()
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skylog");
     
            var split = "||";
            var schema = "batch";

            string spname = "getlistetlscheduler";
            retObject = bc.ExecSqlWithReturnCustomSplit(provider,cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        //====================== rpt new =====================
        public JArray getScorecardCodeReport()
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();

            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skyen");
            var split = "||";
            var schema = "reports";

            string spname = "getscorecardcode";
            retObject = bc.ExecSqlWithReturnCustomSplit(provider,cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }
        public JArray getdataratingmodelReport(string data_applied)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
      
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "getscorecardcodebydataapplied";
            string p1 = "p_dataapplied" + split + data_applied + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }


        public JArray ListPopStabilityReport(string scc_code, string strdate, string enddate)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "rpt_PopStabilityReport";
            string p1 = "@scc_code" + split + scc_code + split + "s";
            string p2 = "@str_date" + split + strdate + split + "s";
            string p3 = "@end_date" + split + enddate + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray ListChartAnalysisReport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "rpt_chartanalysisreport";
            string p1 = "@scc_code" + split + json.GetValue("scc_code") + split + "s";
            string p2 = "@str_date" + split + json.GetValue("str_date") + split + "s";
            string p3 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray GBKSReport(string scc_code, string period_from, string period_to)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "rpt_GBKSReport";
            string p1 = "@scc_code" + split + scc_code + split + "s";
            string p2 = "@str_date" + split + period_from + split + "s";
            string p3 = "@end_date" + split + period_to + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }


        internal string GetAppliedData(JObject json)
        {
            string strResult = "";
            if (json.ContainsKey("data_applied"))
            {
                strResult = json.GetValue("data_applied").ToString();
            }
            else
            {
                strResult = "application_data";
            }
            if (string.IsNullOrWhiteSpace(strResult))
            {
                strResult = "application_data";
            }
            return strResult;
        }

        public JArray getchartCharacteristicreport(JObject json)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "get_chartCharacteristicreport";
            string p1 = "@scc_code" + split + json.GetValue("scc_code") + split + "s";
            string p2 = "@str_date" + split + json.GetValue("str_date") + split + "s";
            string p3 = "@end_date" + split + json.GetValue("end_date") + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray getchartPopStabilityreport(string scc_code, string strdate, string enddate)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "get_chartpopstabilityreport";
            string p1 = "@scc_code" + split + scc_code + split + "s";
            string p2 = "@str_date" + split + strdate + split + "s";
            string p3 = "@end_date" + split + enddate + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray GetChartGBKSReport(string scc_code, string period_from, string period_to)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "get_chartgbksreport";
            string p1 = "@scc_code" + split + scc_code + split + "s";
            string p2 = "@str_date" + split + period_from + split + "s";
            string p3 = "@end_date" + split + period_to + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        public JArray GetChartBadrateReport(string scc_code, string period_from, string period_to)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "get_chartgbksreport";
            string p1 = "@scc_code" + split + scc_code + split + "s";
            string p2 = "@str_date" + split + period_from + split + "s";
            string p3 = "@end_date" + split + period_to + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }


        public JArray GetChartRiskLevelReport(string scc_code, string period_from, string period_to)
        {
            var jaReturn = new JArray();
            List<dynamic> retObject = new List<dynamic>();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("idcen");
            var split = "||";
            var schema = "reports";

            string spname = "get_chartrisklvlreport";
            string p1 = "@scc_code" + split + scc_code + split + "s";
            string p2 = "@str_date" + split + period_from + split + "s";
            string p3 = "@end_date" + split + period_to + split + "s";
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);
            return jaReturn;
        }

        #region rpt collectium

        public JArray getsummaryMonitorDCReport(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_summary_rpt_dc";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListMonitorDCReport(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_list_rpt_dc";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListdistribusihistoryReport(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_distibusi_history_rpt";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";
            string p3 = "@name" + split + json.GetValue("name").ToString() + split + "s";
            string p4 = "@accno" + split + json.GetValue("accno").ToString() + split + "s";
            string p5 = "@dpdmin" + split + json.GetValue("dpdmin").ToString() + split + "s";
            string p6 = "@dpdmax" + split + json.GetValue("dpdmax").ToString() + split + "s";
            string p7 = "@agentid" + split + json.GetValue("agentid").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListdistribusihistoryReportall(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_distibusi_history_rpt_all";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";
            string p3 = "@name" + split + json.GetValue("name").ToString() + split + "s";
            string p4 = "@accno" + split + json.GetValue("accno").ToString() + split + "s";
            string p5 = "@dpdmin" + split + json.GetValue("dpdmin").ToString() + split + "s";
            string p6 = "@dpdmax" + split + json.GetValue("dpdmax").ToString() + split + "s";
            string p7 = "@agentid" + split + json.GetValue("agentid").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }
        public JArray GetListdistribusihistoryReportnotall(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_distibusi_history_rpt_not_all";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }


        public JArray GetListpaymentredordReport(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_payment_record_rpt";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";
            string p3 = "@name" + split + json.GetValue("name").ToString() + split + "s";
            string p4 = "@accno" + split + json.GetValue("accno").ToString() + split + "s";
            string p5 = "@agentid" + split + json.GetValue("agentid").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListpaymentredordReportall(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_payment_record_rpt_all";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";
            string p3 = "@name" + split + json.GetValue("name").ToString() + split + "s";
            string p4 = "@accno" + split + json.GetValue("accno").ToString() + split + "s";
            string p5 = "@agentid" + split + json.GetValue("agentid").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }
        public JArray GetListpaymentredordReportnotall(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_payment_record_rpt_not_all";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListnasabahbaruReport(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_nasabah_baru_rpt";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";
            string p3 = "@name" + split + json.GetValue("name").ToString() + split + "s";
            string p4 = "@accno" + split + json.GetValue("accno").ToString() + split + "s";
            string p5 = "@agentid" + split + json.GetValue("agentid").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListnasabahbaruReportall(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_nasabah_baru_rpt_all";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";
            string p3 = "@name" + split + json.GetValue("name").ToString() + split + "s";
            string p4 = "@accno" + split + json.GetValue("accno").ToString() + split + "s";
            string p5 = "@agentid" + split + json.GetValue("agentid").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }
        public JArray GetListnasabahbaruReportnotall(JObject json)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_nasabah_baru_rpt_not_all";
            string p1 = "@strdate" + split + json.GetValue("strdate").ToString() + split + "s";
            string p2 = "@enddate" + split + json.GetValue("enddate").ToString() + split + "s";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetDataGeoLocation()
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_geolocation_rpt";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetDataDownAgent()
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycore");
            var split = "||";
            var schema = "public";

            string spname = "get_name_agent";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }
        public JArray getDataUserdetail(string p_userid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycore");

            var split = "||";
            var schema = "public";

            string spname = "usr_getuser_detail";
            string p1 = "@userid" + split + p_userid + split + "s";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListActivitasReportnotall(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_activity_rpt_not_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListDCReportnotall(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_dc_rpt_not_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListFCReportnotall(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_fc_rpt_not_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListActivitasReportall(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_activity_rpt_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListDCReportall(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_dc_rpt_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListFCReportall(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_fc_rpt_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListActivitasReport(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_activity_rpt";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListDCReport(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_dc_rpt";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray GetListFCReport(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_data_dc_rpt";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummaryacrivityreportnotall(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_activity_summary_rpt_not_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummarydcreportnotall(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_dc_summary_rpt_not_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummaryfcreportnotall(string startdate, string enddate, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_fc_summary_rpt_not_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummaryacrivityreportall(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_activity_summary_rpt_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummarydcreportall(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_dc_summary_rpt_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummaryfcreportall(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_fc_summary_rpt_all";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummaryacrivityreport(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_activity_summary_rpt";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummarydcreport(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_dc_summary_rpt";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getsummaryfcreport(string startdate, string enddate, string name, string accno, string dpdmin, string dpdmax, string alasan, string janji, string branchid, string agentid, string usrid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            var split = "||";
            var schema = "public";

            string spname = "get_fc_summary_rpt";
            string p1 = "@strdate" + split + startdate + split + "s";
            string p2 = "@enddate" + split + enddate + split + "s";
            string p3 = "@name" + split + name + split + "s";
            string p4 = "@accno" + split + accno + split + "s";
            string p5 = "@dpdmin" + split + dpdmin + split + "s";
            string p6 = "@dpdmax" + split + dpdmax + split + "s";
            string p7 = "@alasan" + split + alasan + split + "s";
            string p8 = "@janji" + split + janji + split + "s";
            string p9 = "@branchid" + split + branchid + split + "s";
            string p10 = "@agentid" + split + agentid + split + "s";
            string p11 = "@usrid" + split + usrid + split + "bg";

            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }


        public JArray ddljanjibayar()
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");

            var split = "||";
            var schema = "public";

            string spname = "get_ddl_janji_bayar_all";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray ddljanjibayarFC()
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");

            var split = "||";
            var schema = "public";

            string spname = "get_ddl_janji_bayar_fc";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }
        public JArray ddlreason()
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");

            var split = "||";
            var schema = "public";

            string spname = "get_ddl_reason_all";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        #endregion

    }
}
