using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LsUpdatePMDTO
    {
        public LsUpdatePMDTO(DataRow row)
        {
            this.ID =int.Parse( row["ID"].ToString());
            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.PB = row["PB"].ToString();
            this.TGYC = row["TGYC"].ToString();
            this.TGHT = row["TGHT"].ToString();
        }


        private int iD;
        private string mAPM;
        private string tENPM;
        private string tGYC;
        private string nHAMAY;
        private string pB;
        private string tGHT;

        public string MAPM { get => mAPM; set => mAPM = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
      
        public int ID { get => iD; set => iD = value; }
        public string TGYC { get => tGYC; set => tGYC = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string PB { get => pB; set => pB = value; }
        public string TGHT { get => tGHT; set => tGHT = value; }
    }
}
