using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class BoPhanDAO
    {
        private static BoPhanDAO instance;

        public static BoPhanDAO Instance
        {
            get { if (instance == null) instance = new BoPhanDAO(); return BoPhanDAO.instance; }
            private set { BoPhanDAO.instance = value; }
        }
        private BoPhanDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from BOPHAN"; // cẦN PHẢI TẠO THÊM KHÓA PHỤ
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public BoPhanDTO GetBoPhanDTO(string MaBP, string ThuocNM) // Khởi tạo khóa phụ để tham chiếu.

        {
            string query = "select* from BOPHAN where MABOPHAN= @ma and NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBP, ThuocNM });
            BoPhanDTO a = new BoPhanDTO(data.Rows[0]);
            return a;
        }

        public List<BoPhanDTO> GetLsBPdtoOfNM( string NhaMay)
        {
            string query = " select * from BOPHAN where NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { NhaMay });
            List<BoPhanDTO> Ls = new List<BoPhanDTO>();
            foreach (DataRow item in data.Rows)
            {
                BoPhanDTO a = new BoPhanDTO(item);
                Ls.Add(a);
            }         
            return Ls;
        }

        public bool CheckBPExistNM(string MaBP,int IdNM) // Check trùng theo Mã bộ phận và ID nhà máy
        {
            string query = " select * from BOPHAN where MABP= @ma and IDNM= @ID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaBP,IdNM});
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
     
        public int Insert(string MaBP, string TenBP, string Ghichu,int IdNM)
        {
            string query = "insert BOPHAN(MABP, TENBP, GHICHU, IDNM) values( @mabp , @tenbp , @ghichu , @IDNM )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBP, TenBP,  Ghichu, IdNM });
            return data;
        }


        // Xóa trong Nhà máy thì sẽ không được do bảng bộ phận đang tham chiếu tới.
        // HAM SUA

        public int Update(int IdBP,string MaBP, string TenBP, string Ghichu, int IdNM)
        {
            string query = "UPDATE	BOPHAN set MABP= @mabp ,TENBP= @ten ,GHICHU= @ghichu WHERE ID= @idBP  and IDNM= @IdNM ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaBP,TenBP, Ghichu, IdBP,IdNM});
            return data;
        }


        // HAM XOA
       
        public int Delete(int IdBP)
        {
            string query = "DELETE BOPHAN WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { IdBP});
            return data;
        }

    }
}
