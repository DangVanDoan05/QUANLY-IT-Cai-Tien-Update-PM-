using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class NHAMAYDAO
    {
        private static NHAMAYDAO instance;

        public static NHAMAYDAO Instance
        {
            get { if (instance == null) instance = new NHAMAYDAO(); return NHAMAYDAO.instance; }
            private set { NHAMAYDAO.instance = value; }
        }
        private NHAMAYDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from NHAMAY";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public bool CheckMaNMExist(string MaNM)
        {
            string query = "select * from NHAMAY where MANM= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNM });
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


        //  HÀM LẤY RA CỘT PHÒNG BAN      
        public List<NHAMAYDTO> GetLsvNM()
        {
            string query = " select * from NHAMAY ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NHAMAYDTO> lsv = new List<NHAMAYDTO>();
            foreach (DataRow item in data.Rows)
            {
                NHAMAYDTO phongBanDTO = new NHAMAYDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;
        }

        // HAM THEM
        public int Insert(string MaNM, string TenNM, string DiaChi,string Ghichu)
        {
            string query = "insert NHAMAY(MANM,TENNM,DIACHI,GHICHU) values( @maNM , @tenNM , @diachi, @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNM, TenNM, DiaChi, Ghichu });
            return data;
        }

        // HAM SUA
        // THÊM KHÓA ID THÌ ƯU VIỆT CÓ THỂ SỬA ĐƯỢC MÃ NHÀ MÁY.
        public int Update(int id,string MaNM, string TenNM, string DiaChi, string Ghichu)
        {
            string query = "UPDATE	NHAMAY set MANM= @maNM ,TENNM= @tenNM ,DIACHI= @diachi ,GHICHU= @ghichu WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNM, TenNM, DiaChi, Ghichu,id});
            return data;
        }

        // HAM XOA
        public int Delete(int id)
        {
            string query = "DELETE NHAMAY WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return data;
        }
    }
}