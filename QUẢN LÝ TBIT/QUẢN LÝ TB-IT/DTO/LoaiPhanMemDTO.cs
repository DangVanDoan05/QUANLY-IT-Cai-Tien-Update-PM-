using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoaiPhanMemDTO
    {
        public LoaiPhanMemDTO(DataRow row)
        {
            this.ID = int.Parse(row["ID"].ToString());
            this.TENLOAIPM = row["TENLOAIPM"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }


        private int iD;
        private string tENLOAIPM;
        private string gHICHU;

        public int ID { get => iD; set => iD = value; }
      
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string TENLOAIPM { get => tENLOAIPM; set => tENLOAIPM = value; }
    }
}
