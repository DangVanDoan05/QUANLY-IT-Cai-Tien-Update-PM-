using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class LoaiTBDAO
    {
        private static LoaiTBDAO instance;

        public static LoaiTBDAO Instance
        {
            get { if (instance == null) instance = new LoaiTBDAO(); return LoaiTBDAO.instance; }
            private set { LoaiTBDAO.instance = value; }
        }
        private LoaiTBDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from LOAITB";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public LoaiTBDTO GetTBDTO(int ID)
        {
            string query = "select* from LOAITB where ID= @ID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { ID});
            LoaiTBDTO a = new LoaiTBDTO(data.Rows[0]);
            return a;
        }


        //public List<BoPhanDTO> GetLsTB(string NhaMay)
        //{
        //    string query = " select * from BOPHAN where NHAMAY= @nhamay ";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { NhaMay });
        //    List<BoPhanDTO> Ls = new List<BoPhanDTO>();
        //    foreach (DataRow item in data.Rows)
        //    {
        //        BoPhanDTO a = new BoPhanDTO(item);
        //        Ls.Add(a);
        //    }
        //    return Ls;
        //}

        public bool CheckLoaiTBExist(string MaTB)
        {
            string query = "select * from LOAITB where MATB= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaTB });
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

        

        // LOAITB(MATB, TENTB, GHICHU)

        public int Insert(string MaTB, string TenTB, string Ghichu)
        {
            string query = "insert LOAITB(MATB, TENTB, GHICHU) values( @matb , @tentb , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  MaTB, TenTB, Ghichu });
            return data;
        }

        // HAM SUA

        public int Update(int ID, string MaTB, string TenTB, string Ghichu)
        {
            string query = "UPDATE LOAITB set MATB= @ma ,TENTB= @ten ,GHICHU= @ghichu WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  MaTB,TenTB, Ghichu, ID });
            return data;
        }

        // HAM XOA

        public int Delete(int ID)
        {
            string query = "DELETE LOAITB WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {ID});
            return data;
        }

    }
}
