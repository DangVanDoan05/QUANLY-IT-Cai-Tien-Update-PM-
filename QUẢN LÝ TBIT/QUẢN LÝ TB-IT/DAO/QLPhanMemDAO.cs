using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;

namespace DAO
{
   public class QLPhanMemDAO
    {
        private static QLPhanMemDAO instance;

        public static QLPhanMemDAO Instance
        {
            get { if (instance == null) instance = new QLPhanMemDAO(); return QLPhanMemDAO.instance; }
            private set { QLPhanMemDAO.instance = value; }
        }


        private QLPhanMemDAO() { }

        public List<QLPhanMemDTO> GetListPMDTO()
        {
            string query = "select * from QLYPHANMEM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QLPhanMemDTO> lsv = new List<QLPhanMemDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLPhanMemDTO maPM = new QLPhanMemDTO(item);
                lsv.Add(maPM);
            }
            return lsv;
        }
        public List<QLPhanMemDTO> GetListPMDTOKoGH()
        {
            string query = "select * from QLYPHANMEM where STATUS=0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QLPhanMemDTO> lsv = new List<QLPhanMemDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLPhanMemDTO maPM = new QLPhanMemDTO(item);
                lsv.Add(maPM);
            }
            return lsv;
        }


        public QLPhanMemDTO GetPMDTO(int IDPM)
        {
            string query = "select * from QLYPHANMEM where ID= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { IDPM });
            DataRow row = data.Rows[0];
            QLPhanMemDTO phanMemDTO = new QLPhanMemDTO(row);

            return phanMemDTO;
        }


        public QLPhanMemDTO GetPMDTObyMaPM(string MaPM)
        {
            string query = "select * from QLYPHANMEM where MAPM= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPM });
            DataRow row = data.Rows[0];
            QLPhanMemDTO phanMemDTO = new QLPhanMemDTO(row);
            return phanMemDTO;
        }

        public DataTable GetTable()
        {
            string query = "select * from QLYPHANMEM, LOAIPHANMEM where IDLOAIPM=LOAIPHANMEM.ID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
      
        public bool CheckMaPMExist(string MaPM)
        {
            string query = "select * from QLYPHANMEM where MAPM= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPM });
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

        // HAM THEM

        public int Insert(string MaPM, string TenPM,int SlMaxKey,string license, string ngmua, string hansd, string ncc, string Chucnang, string ghichu, int status,int IDLoaiPM)
        {
            string query = "insert QLYPHANMEM(MAPM,TENPM,SLMAXKEY,LICENSE,NGAYMUA,HANSD,NCC,CHUCNANG,GHICHU,STATUS,IDLOAIPM)" +
                " values( @ma , @ten , @SLMAX , @LICENSE , @ngmua , @hsd , @ncc , @CN , @note , @status , @idloaiPM )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM, TenPM,SlMaxKey,license, ngmua, hansd, ncc, Chucnang, ghichu, status, IDLoaiPM });
            return data;

        }

        // HAM SUA
        public int Update(int ID,string MaPM, string TenPM, int SlMaxKey, string license, string ngmua, string hansd, string ncc, string Chucnang, string ghichu, int status, int IDLoaiPM)
        {
            string query = "UPDATE	QLYPHANMEM SET MAPM= @ma ,TENPM= @tenpm ,SLMAXKEY= @SLMAX ,LICENSE= @lisen ,NGAYMUA= @ngmua ,HANSD= @han ,NCC= @ncc ,CHUCNANG= @CN ,GHICHU= @ghichu ,STATUS= @status ,IDLOAIPM= @IdLoaiPM  WHERE ID= @id  ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM, TenPM, SlMaxKey, license, ngmua, hansd, ncc, Chucnang, ghichu,status, IDLoaiPM,ID });
            return data;
        }


        // HAM XOA

        public int Delete(int ID)
        {
            string query = "DELETE QLYPHANMEM WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ID });
            return data;
        }
     
    }
}
