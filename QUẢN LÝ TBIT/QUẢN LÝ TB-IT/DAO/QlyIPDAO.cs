using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QlyIPDAO
    {
        private static QlyIPDAO instance;

        public static QlyIPDAO Instance
        {
            get { if (instance == null) instance = new QlyIPDAO(); return QlyIPDAO.instance; }
            private set { QlyIPDAO.instance = value; }
        }
        private QlyIPDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from QLYIP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public QlyIPDTO GetWebDTO(string MaWeb)
        {
            string query = "select* from DSWEBSITE WHERE MAWEB= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaWeb });
            QlyIPDTO a = new QlyIPDTO(data.Rows[0]);
            return a;
        }


        public bool CheckIPExist(string IP)
        {

            string query = "select* from QLYIP WHERE IP= @ip ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { IP });
            int dem = data.Rows.Count;
            if (dem > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // HAM THEM

        // QLYIP(DAIMANG, IP, STATUS, IDTB)

        public int Insert(string DaiMang , string IP , int status ,int IDTB )
        {
            string query = "insert QLYIP(DAIMANG, IP, STATUS, IDTB) values( @daimang , @ip , @status , @idtb )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  DaiMang, IP, status,  IDTB });
            return data;
        }

        // HAM SUA

        public int Update(int ID,string DaiMang, string IP, int status, int IDTB)
        {
            string query = "update QLYIP set DAIMANG= @daimang ,IP= @ip ,STATUS= @status ,IDTB= @idtb  where ID= @ID ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {DaiMang,IP,status,IDTB,ID});
            return data;
        }

        // HAM XOA

        public int Delete(int ID)
        {
            string query = "DELETE QLYIP WHERE ID= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {ID});
            return data;
        }

    }
}
