using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
    public class QlyIPDTO
    {

        public QlyIPDTO(DataRow row)
        {
            this.ID = int.Parse(row["ID"].ToString());
            this.DAIMANG = row["DAIMANG"].ToString();
            this.IP = row["IP"].ToString();
            try
            {
                this.MATB = row["MATB"].ToString();
            }
            catch 
            {
                this.MATB = "";
            }
           
            this.STATUS = int.Parse(row["STATUS"].ToString());           
            this.IDTB = int.Parse(row["IDTB"].ToString());
            this.SODAIMANG = int.Parse(row["SODAIMANG"].ToString());
            this.SOIP = int.Parse(row["SOIP"].ToString());
            this.MAMT = row["MAMT"].ToString();
            this.NGUOISD = row["NGUOISD"].ToString();
        }


        // QLYIP(DAIMANG, IP, STATUS, IDTB,SODAIMANG,SOIP)


        
        private int iD;
        private string dAIMANG;
        private string iP;
        private string mATB;
        private int sTATUS;
        private string mAMT;
        private string nGUOISD;
        private int iDTB;
        private int sODAIMANG;
        private int sOIP;


        public int ID { get => iD; set => iD = value; }
        public string DAIMANG { get => dAIMANG; set => dAIMANG = value; }
        public string IP { get => iP; set => iP = value; }
        public int STATUS { get => sTATUS; set => sTATUS = value; }
        public int IDTB { get => iDTB; set => iDTB = value; }
        public int SODAIMANG { get => sODAIMANG; set => sODAIMANG = value; }
        public int SOIP { get => sOIP; set => sOIP = value; }
        public string MATB { get => mATB; set => mATB = value; }
        public string MAMT { get => mAMT; set => mAMT = value; }
        public string NGUOISD { get => nGUOISD; set => nGUOISD = value; }
    }
}
