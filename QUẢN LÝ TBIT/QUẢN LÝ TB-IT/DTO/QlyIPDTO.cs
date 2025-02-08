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
            this.STATUS = int.Parse(row["STATUS"].ToString());
            this.IDTB = int.Parse(row["IDTB"].ToString());
        }


        // QLYIP(DAIMANG, IP, STATUS, IDTB)

        private int iD;
        private string dAIMANG;
        private string iP;
        private int sTATUS;
        private int iDTB;


        public int ID { get => iD; set => iD = value; }
        public string DAIMANG { get => dAIMANG; set => dAIMANG = value; }
        public string IP { get => iP; set => iP = value; }
        public int STATUS { get => sTATUS; set => sTATUS = value; }
        public int IDTB { get => iDTB; set => iDTB = value; }
    }
}
