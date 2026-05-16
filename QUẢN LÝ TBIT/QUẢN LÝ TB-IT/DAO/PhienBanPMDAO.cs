using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class PhienBanPMDAO
    {
        private static PhienBanPMDAO instance;

        public static PhienBanPMDAO Instance
        {
            get { if (instance == null) instance = new PhienBanPMDAO(); return PhienBanPMDAO.instance; }
            private set { PhienBanPMDAO.instance = value; }
        }
        private PhienBanPMDAO() { }

        // HAM LAY BANG



        public List<PhienBanPMDTO> GetListPhienBanPM()
        {
            string query = "select* from PHIENBANPM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<PhienBanPMDTO> lsv = new List<PhienBanPMDTO>();
            foreach (DataRow item in data.Rows)
            {
                PhienBanPMDTO loaiDTO = new PhienBanPMDTO(item);
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

    }
}
