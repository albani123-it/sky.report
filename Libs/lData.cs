using sky.report.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sky.report.Libs
{
    public class lData
    {
        private BaseController bc = new BaseController();

        public List<dynamic> getTotalSummaryDetailByDate(string date_from, string date_to)
        {
            List<dynamic> retObject = new List<dynamic>();
            string spname = "public.get_total_summary_detail_by_date";
            string p1 = "p_from_date," + date_from + ",s";
            string p2 = "p_to_date," + date_to + ",s";
            retObject = bc.getDataToObject(spname, p1, p2);
            return retObject;
        }

        public List<dynamic> getDisplayDataReportSales (JObject json)
        {
            List<dynamic> retObject = new List<dynamic>();
            string spname = "public.get_all_data_report_sales";
            string p1 = "p_from_date," + json.GetValue("date_from").ToString() + ",s";
            string p2 = "p_to_date," + json.GetValue("date_to").ToString() + ",s";
            string p3 = "p_usr," + json.GetValue("usr").ToString() + ",s";
            retObject = bc.getDataToObject(spname, p1, p2, p3);
            return retObject;
        }

        public List<dynamic> getAllDataReportSales(JObject json)
        {
            List<dynamic> retObject = new List<dynamic>();
            string spname = "public.get_display_data_report_sales";
            string p1 = "p_from_date," + json.GetValue("date_from").ToString() + ",s";
            string p2 = "p_to_date," + json.GetValue("date_to").ToString() + ",s";
            string p3 = "p_usr," + json.GetValue("usr").ToString() + ",s";
            retObject = bc.getDataToObject(spname, p1, p2, p3);
            return retObject;
        }
    }
}
