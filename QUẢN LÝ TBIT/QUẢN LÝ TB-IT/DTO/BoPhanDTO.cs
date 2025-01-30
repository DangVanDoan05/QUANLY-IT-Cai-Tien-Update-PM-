using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BoPhanDTO
    {
        //  BOPHAN(ID,MABP, TENBP, GHICHU, IDNM)
        public BoPhanDTO(DataRow row)
        {
            this.ID =int.Parse( row["ID"].ToString());
            this.MABP = row["MABP"].ToString();
            this.TENBP = row["TENBP"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.IDNM =int.Parse( row["IDNM"].ToString());
        }

       

        private int iD;
        private string mABP;
        private string tENBP;
        private string gHICHU;
        private int iDNM;


       
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string MABP { get => mABP; set => mABP = value; }
        public string TENBP { get => tENBP; set => tENBP = value; }
        public int ID { get => iD; set => iD = value; }
        public int IDNM { get => iDNM; set => iDNM = value; }
    }
}
