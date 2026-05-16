using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class LoaiPhanMemDAO
    {
        private static LoaiPhanMemDAO instance;

        public static LoaiPhanMemDAO Instance
        {
            get { if (instance == null) instance = new LoaiPhanMemDAO(); return LoaiPhanMemDAO.instance; }
            private set { LoaiPhanMemDAO.instance = value; }
        }
        private LoaiPhanMemDAO() { }

        // HAM LAY BANG
      
       

        public List<LoaiPhanMemDTO> GetListLoaiPM()
        {
            string query = "select* from LOAIPHANMEM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<LoaiPhanMemDTO> lsv = new List<LoaiPhanMemDTO>();
            foreach (DataRow item in data.Rows)
            {
                LoaiPhanMemDTO loaiDTO = new LoaiPhanMemDTO(item);
                lsv.Add(loaiDTO);
            }
            return lsv;
        }

        public int CheckLoai(string tenloaiMT)
        {
            string query = "select* from LOAIMAYTINH where TENLOAIMT= @ten ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenloaiMT });
            return data.Rows.Count;
        }

        public int Insert(string Tenloai, string ghichu)
        {
            string query = "insert LOAIMAYTINH(TENLOAIMT,GHICHU) values( @ten , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Tenloai, ghichu });

            return data;

        }

        // HAM SUA
        public int Update(string Tenloai, string ghichu)
        {
            string query = "UPDATE	LOAIMAYTINH SET GHICHU= @GH WHERE TENLOAIMT= @ten ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ghichu, Tenloai });
            return data;

        }

        // HAM XOA
        public int Delete(string Tenloai)
        {
            string query = "DELETE LOAIMAYTINH WHERE TENLOAIMT= @ten ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Tenloai });
            return data;
        }

    }
}
