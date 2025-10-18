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
            radMaThuCong.Checked = true;
            LockControl(true);
            LoadData();
            CleanText();
            LoadEditLookup();
            IDselected = 0;
        }


        // Load nguồn đơn hàng.

      

        // Mã License KASS DD1, KAS DD2 

            // Mệt không buồn nghĩ.


        private void LoadEditLookup()
        {
            // Load nguồn đơn hàng.

            sglDonHang.Properties.DataSource = QlyDonHangPBDAO.Instance.GetDHPMNhapKho(); // Lấy những đơn hàng đã nhận và trạng thái nhập kho bằng không
            sglDonHang.Properties.DisplayMember = "MAPM";
            sglDonHang.Properties.ValueMember = "ID";


            sglPhanMem.Properties.DataSource = QLPhanMemDAO.Instance.GetTable();
            sglPhanMem.Properties.DisplayMember = "MAPM";
            sglPhanMem.Properties.ValueMember = "ID";
        }


        // đang ko biết hệ thống lưu làm sao
        private void CleanText()
        {
            txtMaLicense.Clear();
            chkKhongTH.Checked = false;          
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
                sglDonHang.Enabled = false;
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
                sglDonHang.Enabled = true;
                txtMaLicense.Enabled = true;
                sglPhanMem.Enabled = true;
                dtpNgayMua.Enabled = true;
                dtpNgayHetHan.Enabled = true;
                chkKhongTH.Enabled = true;
                txtSoLuong.Enabled = false;


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
                    string TenPM = QLPhanMemDAO.Instance.GetPMDTO(IDPM).TENPM;
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string ngayhethan = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");

                    if(chkKhongTH.Checked==true)
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
                        if(SoLuong==1)
                        {
                            bool CheckMaLicenseExist = QLLicenseDAO.Instance.CheckMaLicense(MaLicense);
                            if (CheckMaLicenseExist)
                            {
                                MessageBox.Show(" Mã License đã tồn tại.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult kq = MessageBox.Show($"Bạn muốn thêm  mã License {MaLicense} cho phần mềm {TenPM} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (kq == DialogResult.Yes)
                                {                                  
                                    QLLicenseDAO.Instance.Insert(MaLicense, IDPM, ngaymua, ngayhethan, 0); // 0: là trạng thái chưa sử dụng, 1: là trạng thái đã sử dụng.
                                    MessageBox.Show($" Thêm mã phần mềm {MaLicense} thành công! ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);                                  
                                }
                                them = false;
                                LoadControl();
                            }
                        }
                        else
                        {
                            DialogResult kq = MessageBox.Show($"Bạn muốn thêm {SoLuong} mã License {MaLicense} cho phần mềm {TenPM} ? ", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                int demthem = 0;
                                for (int i = 1; i <= SoLuong; i++)
                                {
                                    bool CheckMaLicenseExist = QLLicenseDAO.Instance.CheckMaLicense(MaLicense + i.ToString());
                                    if (!CheckMaLicenseExist)
                                    {
                                        QLLicenseDAO.Instance.Insert(MaLicense + i.ToString(), IDPM, ngaymua, ngayhethan, 0); // 0: là trạng thái chưa sử dụng, 1: là trạng thái đã sử dụng.
                                        demthem++;
                                    }                                
                                   
                                }
                                MessageBox.Show($"Đã thêm {demthem} mã License cho phần mềm {TenPM}, có { SoLuong - demthem } ? thành công! ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                else // SỬA THÔNG TIN.
                {
                    string MaLicense = txtMaLicense.Text.Trim();
                    int IDPM = int.Parse(sglPhanMem.EditValue.ToString());

                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string ngayhethan = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");

                    if (chkKhongTH.Checked == true)
                    {
                        ngayhethan = "Không thời hạn.";
                    }
                    else
                    {
                        
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
            // Check khóa phụ trong phần danh sách máy tính
            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            //  int demloi = 0;

            List<int> LsIDLSdc = new List<int>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                int ID = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                LsIDLSdc.Add(ID);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} License được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demXoa = 0;
                    foreach (int item in LsIDLSdc)
                    {
                        // Check trạng thái của Key xem đã active chưa để xóa để xóa.
                        QLLicenseDTO LicenseDTO = QLLicenseDAO.Instance.GetLicenseDTO(item);
                        if(LicenseDTO.STATUS==0) // Nếu Key chưa active thì có thể xóa
                        {
                            QLLicenseDAO.Instance.Delete(item);
                            demXoa++;
                        }
                                             
                    }
                   
                    MessageBox.Show($"Có  {demXoa} License đã xóa.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    demXoa = 0;
                    dem = 0;
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn phần mềm để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Tạo ra hàng loạt Key sẵn cho các máy không còn bảo hành.

            //// Lấy ra được các Mã máy tính không còn bảo hành trước
            //List<QuanLyMayTinhDTO> LsMT = QuanLyMayTinhDAO.Instance.GetLsMTKhongConBH();

            //// Thêm Key Win

            //foreach (var item in LsMT)
            //{
            //    try
            //    {
            //        string MaMT = item.MAMT;
            //        QLLicenseDAO.Instance.Insert("Win-" + MaMT, 45, "01/01/2024", "Không thời hạn.", 0);
            //        QLLicenseDAO.Instance.Insert("Office-" + MaMT, 29, "01/01/2024", "Không thời hạn.", 0);
            //    }
            //    catch 
            //    {
                 
            //    }
                
            //}
            //MessageBox.Show("Đã cập nhật thành công.", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
                //string mamt = view.GetRowCellValue(e.RowHandle, view.Columns["MAMT"]).ToString();
                int STATUS = int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["STATUS"]).ToString());


                if (STATUS > 0)
                {
                    e.Appearance.BackColor = btnDaSD.Appearance.BackColor; // Trạng thái bằng 1 là Key đã được add.
                }
                else
                {
                    e.Appearance.BackColor = btnChuaSD.Appearance.BackColor;
                }
            }
            catch
            {

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
                QLLicenseDTO A = QLLicenseDAO.Instance.GetLicenseDTO(IDselected);
                int STATUS = A.STATUS;
                if(STATUS==0) // trạng thái chưa kích hoạt thì mới có khả năng sửa key
                {
                    LockControl(false);
                }
                else
                {
                    MessageBox.Show("Mã License không thể sửa do đã kích hoạt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }             
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

        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void radMaTuDong_CheckedChanged(object sender, EventArgs e)
        {
            txtMaLicense.Enabled = false;
        }
    }
}