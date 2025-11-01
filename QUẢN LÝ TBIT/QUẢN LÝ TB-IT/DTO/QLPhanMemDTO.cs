using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QLPhanMemDTO
    {


       // QLYPHANMEM(MAPM, TENPM, LICENSE, NGAYMUA, HANSD, NCC, CHUCNANG, GHICHU)

        public QLPhanMemDTO(DataRow row)
        {
            this.ID =int.Parse(row["ID"].ToString());
            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.SLMAXKEY = int.Parse(row["SLMAXKEY"].ToString());
            this.LICENSE = row["LICENSE"].ToString();
            this.NGAYMUA = row["NGAYMUA"].ToString();
            this.HANSD = row["HANSD"].ToString();
            this.LINKHD = row["LINKHD"].ToString();
            this.NCC = row["NCC"].ToString();
            this.CHUCNANG = row["CHUCNANG"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.STATUS = int.Parse(row["STATUS"].ToString());
        }


        // QLYPHANMEM(MAPM, TENPM, LICENSE, NGAYMUA, HANSD, NCC, CHUCNANG, GHICHU)
        public QLPhanMemDTO(string maPM, string tenPM, string License, string ngaymua, string hansd, string ncc,string ChucNang, string ghichu)
        {
            this.MAPM = maPM;
            this.TENPM = tenPM;
            this.LICENSE = License;
            this.NGAYMUA = ngaymua;
            this.HANSD = hansd;
            this.CHUCNANG = ChucNang;
            this.NCC = ncc;
            this.GHICHU = ghichu;
        }


        private int iD;
        private string mAPM;
        private string tENPM;
        private int sLMAXKEY;
        private string lICENSE;
        private string nGAYMUA;
        private string hANSD;
        private string nCC;
        private string cHUCNANG;
        private string lINKHD;
        private string gHICHU;
        private int sTATUS;

        public string MAPM { get => mAPM; set => mAPM = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
        public string LICENSE { get => lICENSE; set => lICENSE = value; }
        public string NGAYMUA { get => nGAYMUA; set => nGAYMUA = value; }
        public string HANSD { get => hANSD; set => hANSD = value; }
        public string NCC { get => nCC; set => nCC = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string CHUCNANG { get => cHUCNANG; set => cHUCNANG = value; }
        public int ID { get => iD; set => iD = value; }
        public int STATUS { get => sTATUS; set => sTATUS = value; }
        public int SLMAXKEY { get => sLMAXKEY; set => sLMAXKEY = value; }
        public string LINKHD { get => lINKHD; set => lINKHD = value; }
    }
}
