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
            string query = "select * from QLYIP,LOAITB where IDTB=LOAITB.ID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public DataTable GetTableDaiMang()
        {
            string query = "SELECT DISTINCT DAIMANG FROM QLYIP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        // Lấy các IP trống của các dải mạng.
        public DataTable GetTableIPofDAIMANG(string DaiMang)
        {
            string query = " SELECT * FROM QLYIP where DAIMANG= @daimang and STATUS=0 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {DaiMang});
            return data;
        }


        public QlyIPDTO GetIPDTO(int ID)
        {
            string query = "select* from QLYIP WHERE ID= @ID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { ID });
            QlyIPDTO a = new QlyIPDTO(data.Rows[0]);
            return a;
        }


      

        public List<QlyIPDTO> GetLsIPDTODaSX()
        {
            string query = "select * from QLYIP ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });
            List<QlyIPDTO> LsIPDTOall = new List<QlyIPDTO>();
            List<int> LsSoDMOnly = new List<int>();
            List<int> LsSoIPOnly = new List<int>();

            foreach (DataRow item in data.Rows)
            {
                QlyIPDTO a = new QlyIPDTO(item);
                LsIPDTOall.Add(a);
                if(!LsSoDMOnly.Contains(a.SODAIMANG))
                {
                    LsSoDMOnly.Add(a.SODAIMANG);
                }
                if (!LsSoIPOnly.Contains(a.SOIP))
                {
                    LsSoIPOnly.Add(a.SOIP);
                }
            }

            // Lấy được dải mạng và IP duy nhất, bây h tiến hành sắp xếp.

            // Sắp xếp dải mạng.

            int dodaiDM = LsSoDMOnly.Count();

            int[] DaiMang = new int[dodaiDM];

            int m = 0;

            foreach (int item in LsSoDMOnly)
            {
                DaiMang[m] = item;
                m++;
            }

            // Tiến hành sắp xếp mảng dải mạng tăng dần

            for (int i = 0; i < dodaiDM - 1; i++)
            {
                for (int j = i + 1; j < dodaiDM; j++)
                {
                    if ( DaiMang[j] < DaiMang[i] )
                    {
                        int trunggian = DaiMang[j];
                        DaiMang[j] = DaiMang[i];
                        DaiMang[i] = trunggian;
                    }
                }
            }


            // Sắp xếp dải IP

            int dodaiDIP = LsSoIPOnly.Count();

            int[] DaiIP = new int[dodaiDIP];

            int n = 0;

            foreach (int item in LsSoIPOnly)
            {
                DaiMang[m] = item;
                m++;
            }

            // Tiến hành sắp xếp mảng dải mạng tăng dần

            for (int i = 0; i < dodaiDM - 1; i++)
            {
                for (int j = i + 1; j < dodaiDM; j++)
                {
                    if (DaiMang[j] < DaiMang[i])
                    {
                        int trunggian = DaiMang[j];
                        DaiMang[j] = DaiMang[i];
                        DaiMang[i] = trunggian;
                    }
                }
            }
            return LsDsDHdasx;
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

        // QLYIP(DAIMANG, IP, STATUS, IDTB,SODAIMANG,SOIP)

        public int Insert(string DaiMang , string IP , int status ,int IDTB,int SoDaiMang,int SoIP)
        {
            string query = "insert QLYIP(DAIMANG, IP, STATUS, IDTB,SODAIMANG,SOIP) values( @daimang , @ip , @status , @idtb , @soDM , @soIP )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  DaiMang, IP, status,  IDTB , SoDaiMang , SoIP });
            return data;
        }


        // HAM SUA, không cần Update chỉ cần Update lại Id thiêt bị.

        public int UpdateDMIP(int ID,int SoDaiMang, int SoIP)
        {
            string query = "update QLYIP set SODAIMANG= @sodaimang ,SOIP= @soip   where ID= @ID ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {SoDaiMang,SoIP,ID});
            return data;
        }

        public int UpdateIDTB(int ID, int IDTB)
        {
            string query = "update QLYIP set IDTB= @IDTB  where ID= @ID ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { IDTB, ID });
            return data;
        }

        public int UpdateStatus(int ID, int status)
        {
            string query = "update QLYIP set STATUS= @status  where ID= @ID ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  status, ID });
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
