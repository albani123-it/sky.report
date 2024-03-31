using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sky.report.Libs;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sky.report.Controllers
{
    [Route("api/[controller]")]
    public class BasetrxController : Controller
    {
        private lDbConn dbconn = new lDbConn();
        private BaseController bc = new BaseController();


        public string insert_report_header(JObject json)
        {

            string strout = "";
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.insert_report_header", connection, trans);
                cmd.Parameters.AddWithValue("p_code", json.GetValue("code").ToString());
                cmd.Parameters.AddWithValue("p_name", json.GetValue("name").ToString());
                cmd.Parameters.AddWithValue("p_desc", json.GetValue("description").ToString());
                cmd.Parameters.AddWithValue("p_sql", json.GetValue("sql").ToString());
                cmd.Parameters.AddWithValue("p_guidance", json.GetValue("guidance").ToString());
                cmd.Parameters.AddWithValue("p_usr", json.GetValue("user").ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }

        public string update_report_header(JObject json)
        {

            string strout = "";
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.update_report_header", connection, trans);
                cmd.Parameters.AddWithValue("p_code", json.GetValue("code").ToString());
                cmd.Parameters.AddWithValue("p_name", json.GetValue("name").ToString());
                cmd.Parameters.AddWithValue("p_desc", json.GetValue("description").ToString());
                cmd.Parameters.AddWithValue("p_sql", json.GetValue("sql").ToString());
                cmd.Parameters.AddWithValue("p_guidance", json.GetValue("guidance").ToString());
                cmd.Parameters.AddWithValue("p_usr", json.GetValue("user").ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }

        public string delete_report_detail(JObject json)
        {

            string strout = "";
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.delete_report_detail", connection, trans);
                cmd.Parameters.AddWithValue("p_code", json.GetValue("code").ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }

        public string insert_report_detail(JObject json)
        {

            string strout = "";
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.insert_report_detail", connection, trans);
                cmd.Parameters.AddWithValue("p_code", json.GetValue("code").ToString());
                cmd.Parameters.AddWithValue("p_field", json.GetValue("field").ToString());
                cmd.Parameters.AddWithValue("p_data_type", json.GetValue("data_type").ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }

        public string update_report_detail(JObject json)
        {

            string strout = "";
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.update_report_detail_for_updated", connection, trans);
                cmd.Parameters.AddWithValue("p_id", Convert.ToInt32(json.GetValue("rptd_id").ToString()));
                cmd.Parameters.AddWithValue("p_header_id", Convert.ToInt32(json.GetValue("rpth_id").ToString()));
                cmd.Parameters.AddWithValue("p_field", json.GetValue("field").ToString());
                cmd.Parameters.AddWithValue("p_data_type", json.GetValue("data_type").ToString());
                cmd.Parameters.AddWithValue("p_field_isfilter", Convert.ToInt32(json.GetValue("is_filter").ToString()));
                cmd.Parameters.AddWithValue("p_field_filter_type", json.GetValue("filter_type").ToString());
                cmd.Parameters.AddWithValue("p_field_alias", json.GetValue("alias").ToString());
                cmd.Parameters.AddWithValue("p_field_isview", Convert.ToInt32(json.GetValue("is_view").ToString()));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }
    }
}