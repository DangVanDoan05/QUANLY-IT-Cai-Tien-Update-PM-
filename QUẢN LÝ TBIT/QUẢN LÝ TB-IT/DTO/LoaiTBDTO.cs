using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoaiTBDTO
    {
        public LoaiTBDTO(DataRow row)
        {
            this.ID= int.Parse(row["ID"].ToString());
            this.MATB = row["MATB"].ToString();
            this.TENTB = row["TENTB"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
           
        }

        // LOAITB(ID,MATB, TENTB, GHICHU)

        private int iD;
        private string mATB;
        private string tENTB;
        private string gHICHU;
     
       
        public int ID { get => iD; set => iD = value; }
        public string MATB { get => mATB; set => mATB = value; }
        public string TENTB { get => tENTB; set => tENTB = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
