using sky.report.Libs;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace sky.report.Controllers
{
    public class lPgsql
    {
        public  lDbConn dbconn = new lDbConn();
        private BaseController bc = new BaseController();

        public List<dynamic> execSQLWithOutput(string str)
        {
            var conn = dbconn.conString();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(str, nconn);
            cmd.CommandType = CommandType.Text;
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr == null || dr.FieldCount == 0)
            {
                nconn.Close();
                return retObject;
            }
            retObject = bc.GetDataObj(dr);
            nconn.Close();
            return retObject;
        }

        public List<dynamic> execSQLWithOutputReport(string str)
        {
            var conn = dbconn.conStringReport();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(str, nconn);
            cmd.CommandType = CommandType.Text;
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr == null || dr.FieldCount == 0)
            {
                nconn.Close();
                return retObject;
            }
            retObject = bc.GetDataObj(dr);
            nconn.Close();
            return retObject;
        }

        public void execSql(string sql)
        {
            var conn = dbconn.conString();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, nconn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            nconn.Close();
        }

        public string execSqlWithExecption(string sql)
        {
            var conn = dbconn.conString();
            string message = "";
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, nconn);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                message = "success";
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
            }
            finally {
                nconn.Close();
            }
            return message;
        }
        public string CheckRuleCondition(string table, string condition)
        {
            return execSqlWithExecption("SELECT * FROM " + table + " WHERE " + condition + " ");
        }
        public List<dynamic> CheckRuleConditionRealtime(string table, string condition)
        {
            return execSQLWithOutput("SELECT * FROM " + table + " WHERE " + condition + " ");
        }
        //for master summary
        public List<dynamic> CheckRuleConditionResult(string table, string acc_no, string condition)
        {
            return execSQLWithOutput("SELECT * FROM " + table + " WHERE " + condition + " AND acc_no = '" + acc_no + "'");
        }
    }
}
