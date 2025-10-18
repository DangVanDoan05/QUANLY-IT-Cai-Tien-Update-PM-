using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DAO;
using DTO;

namespace frmMain.Quan_Ly_Dat_Hang
{
    public partial class frmUpdateNote : DevExpress.XtraEditors.XtraForm
    {
        public frmUpdateNote()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            txtMaDH1.Text = DHduocchon.MaDHdangchon;
          
          


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string MaDH = txtMaDH1.Text;
           
            string ghichu = txtGhiChu.Text;

          
            
           
            // public int UpdateNhanHang(string MaDonHang, string NgayNhan, int SLNhan, string ghichu)
            QlyDonHangPBDAO.Instance.UpdateGhiChu(MaDH, ghichu);

            MessageBox.Show("UPDATE GHI CHÚ THÀNH CÔNG.", "THÀNH CÔNG:");
            
            this.Close();
        }
    }
}