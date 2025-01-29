using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NHAMAYDTO
    {
        //  NHAMAY(ID,MANM,TENNM,DIACHI,GHICHU)

        public NHAMAYDTO(DataRow row)
        {
            this.ID = int.Parse( row["ID"].ToString());
            this.MANM = row["MANM"].ToString();
            this.TENNM= row["TENNM"].ToString();
            this.DIACHI = row["DIACHI"].ToString();
            this.GHICHU= row["GHICHU"].ToString();
        }

        private int iD;
        private string mANM;
        private string tENNM;
        private string dIACHI;
        private string gHICHU;

      
        public string DIACHI { get => dIACHI; set => dIACHI = value; }
        public int ID { get => iD; set => iD = value; }
        public string MANM { get => mANM; set => mANM = value; }
        public string TENNM { get => tENNM; set => tENNM = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
