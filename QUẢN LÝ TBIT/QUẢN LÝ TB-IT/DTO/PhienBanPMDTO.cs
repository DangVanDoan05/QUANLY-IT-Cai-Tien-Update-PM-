using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhienBanPMDTO
    {
        public PhienBanPMDTO(DataRow row)
        {
            this.ID = int.Parse(row["ID"].ToString());
            this.TENPHIENBANPM = row["TENPHIENBANPM"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }


        private int iD;
        private string tENPHIENBANPM;
        private string gHICHU;

        public int ID { get => iD; set => iD = value; }

        public string GHICHU { get => gHICHU; set => gHICHU = value; }
      
        public string TENPHIENBANPM { get => tENPHIENBANPM; set => tENPHIENBANPM = value; }
    }
}
