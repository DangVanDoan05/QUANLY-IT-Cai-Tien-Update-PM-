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

namespace frmMain.Quản_Lý_Đặt_Hàng
{
    public partial class frmNhanHangPB : DevExpress.XtraEditors.XtraForm
    {
        public frmNhanHangPB()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            txtMaDH.Text = DHduocchon.MaDHdangchon;
            string MaDonHang = txtMaDH.Text;
            QlyDonHangPBDTO a = QlyDonHangPBDAO.Instance.GetDonHangDTO(MaDonHang);
            txtTenHang.Text = a.TENHANG;
            txtSlDat.Text = a.SLDAT + "";
            txtMaDH.Enabled = false;
            txtTenHang.Enabled = false;
            txtSlDat.Enabled = false;

            sglLoaiHH.Properties.DataSource = LoaiTBDAO.Instance.GetTable();
            sglLoaiHH.Properties.DisplayMember = "MATB";
            sglLoaiHH.Properties.ValueMember = "ID";


        }
     

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string MaDH = txtMaDH.Text;
            string Ngaynhan = dtpNgayNhanHang.Value.ToString("dd/MM/yyyy");
            string ghichu = txtGhiChu.Text;

            if (txtSLNhan.Text == "")
            {
                MessageBox.Show($" Hãy nhập số lượng nhận. ", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int Slnhan = int.Parse(txtSLNhan.Text);
                // public int UpdateNhanHang(string MaDonHang, string NgayNhan, int SLNhan, string ghichu)
                QlyDonHangPBDAO.Instance.UpdateNhanHang(MaDH, Ngaynhan, Slnhan, ghichu);
                MessageBox.Show("Đã nhận hàng thành công!", "Thông báo:");
            }
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string MaDH = txtMaDH.Text;

            int IDHH = int.Parse(sglLoaiHH.EditValue.ToString());
            string TenHH = LoaiTBDAO.Instance.GetTBDTO(IDHH).TENTB;
            QlyDonHangPBDAO.Instance.UpdateIDHH(MaDH,IDHH,TenHH);
            MessageBox.Show("Đã cập nhật thành công!", "Thông báo:");         
            this.Close();
            LoadControl();
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }
}