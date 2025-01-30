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

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmNhaMay : DevExpress.XtraEditors.XtraForm
    {

        public frmNhaMay()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;
        int IDNMdc=0;

        private void LoadControl()
        {
            IDNMdc = 0;
            LoadData();
            LockControl(true);
            CleanText();
        }

        void CleanText()
        {
            txtMaNhaMay.Clear();
            txtTenNhaMay.Clear();
            txtDiaChi.Clear();
            txtGhiChu.Clear();
        }
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaNhaMay.Enabled = false;
                txtTenNhaMay.Enabled = false;
                txtDiaChi.Enabled = false;
                txtGhiChu.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;

            }
            else
            {
                txtMaNhaMay.Enabled = true;
                txtTenNhaMay.Enabled = true;
                txtDiaChi.Enabled = true;
                txtGhiChu.Enabled = true;
                
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {
            gcNM.DataSource = NHAMAYDAO.Instance.GetTable();
        }

        void Save()
        {
            if (them)
            {
                string maNM = txtMaNhaMay.Text.Trim();
                string tenNM = txtTenNhaMay.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
                bool CheckExits = NHAMAYDAO.Instance.CheckMaNMExist(maNM);              
                if (CheckExits)
                {
                    MessageBox.Show($" Mã nhà máy {maNM} đã tồn tại", "Lỗi:", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {                  
                    NHAMAYDAO.Instance.Insert(maNM, tenNM, diaChi,ghichu);
                    MessageBox.Show($"Đã thêm mã nhà máy {maNM} .","THÀNH CÔNG:", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                }
                them = false;
            }
            else
            {
                string maNM = txtMaNhaMay.Text.Trim();
                string tenNM = txtTenNhaMay.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
               
                NHAMAYDAO.Instance.Update(IDNMdc,maNM, tenNM, diaChi,ghichu);
                MessageBox.Show($"Đã sửa mã nhà máy {maNM}", "THÀNH CÔNG:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(IDNMdc==0)
            {
                MessageBox.Show($" Chưa chọn nhà máy muốn sửa thông tin.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LockControl(false);
                txtMaNhaMay.Enabled = false;
            }
          
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            if (IDNMdc == 0)
            {
                MessageBox.Show(" Bạn chưa chọn nhà máy để xóa! ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string maNM = txtMaNhaMay.Text.Trim();
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã nhà máy {maNM} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    NHAMAYDAO.Instance.Delete(IDNMdc);
                    MessageBox.Show($"Xóa thành công mã nhà máy:{maNM}", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
            }
            LoadControl();

        }

      

      
        private void gcNM_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMdc = int.Parse(gridView2.GetFocusedRowCellValue("ID").ToString());
                txtMaNhaMay.Text = gridView2.GetFocusedRowCellValue("MANM").ToString();
                txtTenNhaMay.Text = gridView2.GetFocusedRowCellValue("TENNM").ToString();
                txtDiaChi.Text = gridView2.GetFocusedRowCellValue("DIACHI").ToString();
                txtGhiChu.Text = gridView2.GetFocusedRowCellValue("GHICHU").ToString();
            }
            catch
            {

            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }
}