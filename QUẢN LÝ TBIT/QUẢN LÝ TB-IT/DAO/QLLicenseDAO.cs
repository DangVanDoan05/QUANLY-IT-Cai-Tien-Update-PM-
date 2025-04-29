using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class QLLicenseDAO
    {
       
            private static QLLicenseDAO instance;

            public static QLLicenseDAO Instance
            {
                get { if (instance == null) instance = new QLLicenseDAO(); return QLLicenseDAO.instance; }
                private set { QLLicenseDAO.instance = value; }
            }


            private QLLicenseDAO() { }

            public List<QLLicenseDTO> GetLsKeyWinAvailable()
            {
                string query = "select * from QLLICENSE where IDPM=45 AND STATUS=0";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                List<QLLicenseDTO> lsv = new List<QLLicenseDTO>();
                foreach (DataRow item in data.Rows)
                {
                    QLLicenseDTO maPM = new QLLicenseDTO(item);
                    lsv.Add(maPM);
                }
                return lsv;
            }

            public List<QLLicenseDTO> GetLsKeyOfficeAvailable()
            {
                string query = "select * from QLLICENSE where IDPM=29 AND STATUS=0"; // ID=29 là key phần mềm của Office
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                List<QLLicenseDTO> lsv = new List<QLLicenseDTO>();
                foreach (DataRow item in data.Rows)
                {
                    QLLicenseDTO maPM = new QLLicenseDTO(item);
                    lsv.Add(maPM);
                }
                return lsv;
            }


            public List<QLLicenseDTO> GetLsKeyKasDD1Available()
            {
                string query = "select * from QLLICENSE where IDPM=21 AND STATUS=0"; // ID=21 là key phần mềm của KAS DD1
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                List<QLLicenseDTO> lsv = new List<QLLicenseDTO>();
                foreach (DataRow item in data.Rows)
                {
                    QLLicenseDTO maPM = new QLLicenseDTO(item);
                    lsv.Add(maPM);
                }
                return lsv;
            }

            public List<QLLicenseDTO> GetLsKeyKasDD2Available()
            {
                string query = "select * from QLLICENSE where IDPM=22 AND STATUS=0"; // ID=21 là key phần mềm của KAS DD1
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                List<QLLicenseDTO> lsv = new List<QLLicenseDTO>();
                foreach (DataRow item in data.Rows)
                {
                    QLLicenseDTO maPM = new QLLicenseDTO(item);
                    lsv.Add(maPM);
                }
                return lsv;
            }

            public List<QLLicenseDTO> GetLsKeyKasDDKAvailable()
            {
                string query = "select * from QLLICENSE where IDPM=23 AND STATUS=0"; // ID=21 là key phần mềm của KAS DD1
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                List<QLLicenseDTO> lsv = new List<QLLicenseDTO>();
                foreach (DataRow item in data.Rows)
                {
                    QLLicenseDTO maPM = new QLLicenseDTO(item);
                    lsv.Add(maPM);
                }
                return lsv;
            }



        public DataTable GetTable()
            {
                string query = "select * from QLLICENSE,QLYPHANMEM where IDPM=QLYPHANMEM.ID";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                return data;
            }

           
            public bool CheckMaLicense(string MaLICENSE)
            {
                string query = "select * from QLLICENSE where MALICENSE= @ma ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaLICENSE });

                int dem= data.Rows.Count;
                if(dem>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        public QLLicenseDTO GetLicenseDTO(int ID)
        {
            string query = "select * from QLLICENSE where ID= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { ID });
            QLLicenseDTO a = new QLLicenseDTO(data.Rows[0]);
            return a;
        }

        // HAM THEM: INSERT QLLICENSE(MALICENSE,IDPM,NGAYMUA,NGAYHETHAN,STATUS)

        public int Insert(string MaLicense,int IdPM, string Ngaymua, string ngayhethan, int status )
            {
                string query = "INSERT QLLICENSE(MALICENSE,IDPM,NGAYMUA,NGAYHETHAN,STATUS) values( @ma , @idpm , @ngaymua , @ngayhethan , @status )";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaLicense,  IdPM, Ngaymua,  ngayhethan, status });
                return data;
            }

            // HAM THEM: INSERT QLLICENSE(MALICENSE,IDPM,NGAYMUA,NGAYHETHAN,STATUS)

            public int Update(int ID,string MaLicense, int IdPM, string Ngaymua, string ngayhethan) // không Update Status trong trường này
            {
                string query = "UPDATE QLLICENSE set MALICENSE= @ma ,IDPM= @idpm ,NGAYMUA= @ngaymua ,NGAYHETHAN= @ngayhethan where ID= @id ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaLicense , IdPM , Ngaymua , ngayhethan , ID });
                return data;
            }

            public int UpdatesTATUS(int ID, int status) // không Update Status trong trường này
            {
                string query = "UPDATE QLLICENSE set STATUS= @status where ID= @id ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { status,ID });
                return data;
            }

        // HAM XOA
        public int Delete(int ID)
            {
                string query = "DELETE QLLICENSE WHERE ID= @ID ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ID });
                return data;
            }
        
    }
}
