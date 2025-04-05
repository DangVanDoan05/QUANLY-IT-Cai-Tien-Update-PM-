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
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmQuanLyKeyLicense : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyKeyLicense()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;
        int IDselected = 0;

        private void LoadControl()
        {
            LockControl(true);
            LoadData();
            CleanText();
            LoadEditLookup();
            IDselected = 0;
        }

        // Mã License KASS DD1, KAS DD2

        private void LoadEditLookup()
        {
            sglPhanMem.Properties.DataSource = QLPhanMemDAO.Instance.GetTable();
            sglPhanMem.Properties.DisplayMember = "MAPM";
            sglPhanMem.Properties.ValueMember = "ID";
        }


        // đang ko biết hệ thống lưu làm sao
        private void CleanText()
        {
            txtMaLicense.Clear();
            chkKhongTH.Checked = true;          
            txtSoLuong.Clear();
        }

        private void LoadData()
        {
            gcLicense.DataSource = QLLicenseDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaLicense.Enabled = false;
                sglPhanMem.Enabled = false;
                dtpNgayMua.Enabled = false;
                dtpNgayHetHan.Enabled = false;
                chkKhongTH.Enabled = false;
                txtSoLuong.Enabled = false;


                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
               
            }
            else
            {

                txtMaLicense.Enabled = true;
                sglPhanMem.Enabled = true;
                dtpNgayMua.Enabled = true;
                dtpNgayHetHan.Enabled = true;
                chkKhongTH.Enabled = true;
                txtSoLuong.Enabled = true;


                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;              
            }
        }



        void Save()
        {
            try
            {
                if (them)
                {

                    string MaLicense =txtMaLicense.Text.Trim();
                    int IDPM =int.Parse(sglPhanMem.EditValue.ToString());
                    
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string ngayhethan = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");

                    if(chkKhongTH.Checked=true)
                    {
                         ngayhethan = "Không thời hạn.";
                    }
                    else
                    {
                        ngayhethan = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");
                    }
                    int SoLuong = int.Parse(txtSoLuong.Text.Trim());
                    if(SoLuong>=1)
                    {
                        bool CheckMaLicenseExist = QLLicenseDAO.Instance.CheckMaLicense(MaLicense);
                        if (CheckMaLicenseExist)
                        {
                            MessageBox.Show(" Mã License đã tồn tại.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã License {MaLicense}", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                QLLicenseDAO.Instance.Insert(MaLicense,IDPM,ngaymua,ngayhethan,0); // 0: là trạng thái chưa sử dụng, 1: là trạng thái đã sử dụng.
                                MessageBox.Show($" Thêm mã phần mềm {MaLicense} thành công! ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            them = false;
                            LoadControl();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Số lượng License phải ít nhất là bằng 1.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else // Sửa thông tin.
                {
                    string MaLicense = txtMaLicense.Text.Trim();
                    int IDPM = int.Parse(sglPhanMem.EditValue.ToString());

                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string ngayhethan = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");

                    if (chkKhongTH.Checked = true)
                    {
                        ngayhethan = "Không thời hạn.";
                    }
                    else
                    {
                        ngayhethan = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");
                    }
                 
                     QLLicenseDAO.Instance.Update(IDselected,MaLicense,IDPM,ngaymua,ngayhethan); // 0: là trạng thái chưa sử dụng, 1: là trạng thái đã sử dụng.
                     MessageBox.Show($"Đã sửa thông tin License.", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                              
                }
            }
            catch
            {
                MessageBox.Show("Hãy chọn nhà cung cấp có trong danh sách ", "Thông Báo:");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Chưa có chức năng xóa.

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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
            //string mamt = view.GetRowCellValue(e.RowHandle, view.Columns["MAMT"]).ToString();
            int STATUS = int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["STATUS"]).ToString());
           

            if (STATUS>0)
            {
                e.Appearance.BackColor = btnDaSD.Appearance.BackColor;
            }
            else
            { 
                e.Appearance.BackColor = btnChuaSD.Appearance.BackColor;
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int Count = 0;

            List<int> LsIDselected = new List<int>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                int ID = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                IDselected = ID;
                LsIDselected.Add(ID);
                Count++;
            }
            if (Count == 1)
            {
                LockControl(false);
               
            }
            else
            {
                MessageBox.Show("Chưa chọn mã License để xóa hoặc chọn quá 1 mã License để sửa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gcLicense_Click(object sender, EventArgs e)
        {
            try
            {

                IDselected = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
                // 320 thì sẽ lấy theo ID đầu tiên.
                //  MessageBox.Show($"ID lấy là ID {IDselected} ");
               
                // đang không đúng lý ở đây // Lấy ra giá trị ID IP
                sglPhanMem.EditValue = gridView1.GetFocusedRowCellValue("IDPM").ToString();

                txtMaLicense.Text = gridView1.GetFocusedRowCellValue("MALICENSE").ToString();
               

            }
            catch
            {


            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }
}