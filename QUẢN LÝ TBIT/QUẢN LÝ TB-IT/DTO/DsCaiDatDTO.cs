using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class DsCaiDatDTO
    {
        public DsCaiDatDTO(DataRow row)
        {
            this.IDMAMT =int.Parse(row["IDMAMT"].ToString());
            this.IDPM =int.Parse( row["IDPM"].ToString());                    
            this.NGAYCD = row["NGAYCD"].ToString();
            this.NGAYHT = row["NGAYHT"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }

        // IDMAMT,IDPM,NGAYCD,NGAYHT,GHICHU

        private int iDMAMT;
        private int iDPM;     
        private string nGAYCD;
        private string nGAYHT;
        private string gHICHU;

      
        public string NGAYCD { get => nGAYCD; set => nGAYCD = value; }
        public string NGAYHT { get => nGAYHT; set => nGAYHT = value; }    
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public int IDMAMT { get => iDMAMT; set => iDMAMT = value; }
        public int IDPM { get => iDPM; set => iDPM = value; }
    }
}
