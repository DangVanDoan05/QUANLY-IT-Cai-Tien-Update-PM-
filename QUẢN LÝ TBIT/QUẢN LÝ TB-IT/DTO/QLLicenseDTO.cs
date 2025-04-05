using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class QLLicenseDTO
    {      
            public QLLicenseDTO(DataRow row)
            {
                this.ID = int.Parse(row["ID"].ToString());
                this.MALICENSE = row["MALICENSE"].ToString();
                this.IDPM =int.Parse( row["IDPM"].ToString());             
                this.NGAYMUA = row["NGAYMUA"].ToString();
                this.NGAYHETHAN = row["NGAYHETHAN"].ToString();              
                this.STATUS =int.Parse(row["STATUS"].ToString());
            }
         
            //  INSERT QLLICENSE(ID,MALICENSE, IDPM, NGAYMUA, NGAYHETHAN, STATUS)

            private int iD;
            private string mALICENSE;
            private int iDPM;          
            private string nGAYMUA;
            private string nGAYHETHAN;          
            private int sTATUS;

            public int ID { get => iD; set => iD = value; }
            public string MALICENSE { get => mALICENSE; set => mALICENSE = value; }
            public int IDPM { get => iDPM; set => iDPM = value; }
            public string NGAYMUA { get => nGAYMUA; set => nGAYMUA = value; }
            public string NGAYHETHAN { get => nGAYHETHAN; set => nGAYHETHAN = value; }
            public int STATUS { get => sTATUS; set => sTATUS = value; }
        }
    
}
