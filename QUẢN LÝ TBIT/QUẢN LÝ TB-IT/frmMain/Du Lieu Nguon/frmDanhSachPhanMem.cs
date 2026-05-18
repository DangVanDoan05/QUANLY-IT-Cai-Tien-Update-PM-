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
using System.IO;
using OfficeOpenXml;
using frmMain.Quan_Ly_May_Tinh;
using static DevExpress.Utils.Svg.CommonSvgImages;
using DevExpress.XtraGrid.Columns;
using DTO;
using Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain
{
    public partial class frmDanhSachPhanMem : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachPhanMem()
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
         
            IDselected = 0;
            LoadEditlookup();
        }

       

        private void LoadEditlookup()
        {
            sglLoaiPM.Properties.DataSource = LoaiPhanMemDAO.Instance.GetListLoaiPM();
            sglLoaiPM.Properties.DisplayMember = "TENLOAIPM";
            sglLoaiPM.Properties.ValueMember = "ID";

            sglPhienBanPM.Properties.DataSource = PhienBanPMDAO.Instance.GetListPhienBanPM();
            sglPhienBanPM.Properties.DisplayMember = "TENPHIENBANPM";
            sglPhienBanPM.Properties.ValueMember = "ID";
        }


        // đang ko biết hệ thống lưu làm sao


        private void CleanText()
        {
            txtMaPhanMem.Clear();
            txtTenPhanMem.Clear();
            txtLicense.Clear();
            txtGhiChu.Clear();
            txtGioiHanSLKey.Clear();
            chkGHLC.Checked = true;
        }


        private void LoadData()
        {
            gridControl1.DataSource = QLPhanMemDAO.Instance.GetTable();
            // Trường STATUS để giới hạn Key hay không.
            chkGHLC.Checked = true;
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaPhanMem.Enabled = false;
                txtTenPhanMem.Enabled = false;
                txtGioiHanSLKey.Enabled = false;
                chkGHLC.Enabled = false;
                txtLicense.Enabled = false;
                dtpNgayMua.Enabled = false;
                sglLoaiPM.Enabled = false;
                sglPhienBanPM.Enabled = false;
                radKhongTH.Enabled = false;
                dtpHanSuDung.Enabled = false;
                txtChucnang.Enabled = false;
                txtGhiChu.Enabled = false;
                txtNCC.Enabled = false;
                txtLinkHD.Enabled = false;
                txtGhiChu.Enabled = false;


                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnTaiForm.Enabled = true;
                btnNhapExcel.Enabled = true;
                btnXuatExcel.Enabled = true;
               
            }

            else
            {
                txtMaPhanMem.Enabled = true;
                txtTenPhanMem.Enabled = true;
                txtGioiHanSLKey.Enabled = false;
                txtLicense.Enabled = true;
                chkGHLC.Enabled = true;            
                dtpNgayMua.Enabled = true;
                radKhongTH.Enabled = true;
                dtpHanSuDung.Enabled = false;
                txtChucnang.Enabled = true;
                txtLinkHD.Enabled = true;
                txtGhiChu.Enabled = true;
                txtNCC.Enabled = true;
                sglLoaiPM.Enabled = true;
                sglPhienBanPM.Enabled = true;


                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
                btnTaiForm.Enabled = false;
                btnNhapExcel.Enabled = false;
                btnXuatExcel.Enabled = false;

            }
        }



        void Save()
        {
            //try
            //{
                // 
                if (them)
                {

                    string maPM = txtMaPhanMem.Text;
                    string tenPM = txtTenPhanMem.Text;
                    string license = txtLicense.Text;
                //  int slmaxKey = int.Parse(txtGioiHanSLKey.Text);
                    int slmaxKey = 0;
                    if(chkGHLC.Checked)
                    {
                    // Nếu được check thì số lượng giới hạn bằng 0
                        slmaxKey = 0;
                    }
                    else
                    {
                        slmaxKey = int.Parse(txtGioiHanSLKey.Text);
                    }
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string hansd = dtpHanSuDung.Value.ToString("dd/MM/yyyy");
                    string ghichu = txtGhiChu.Text;
                    string chucnang = txtChucnang.Text;
                    int IDLoaiPM =int.Parse( sglLoaiPM.EditValue.ToString());
                    int IDPhienBanPM= int.Parse(sglPhienBanPM.EditValue.ToString());
                    string ncc = txtNCC.Text;
                    int status = 0;
                    bool CheckMaPMExist = QLPhanMemDAO.Instance.CheckMaPMExist(maPM);
                    if (CheckMaPMExist)
                    {
                        MessageBox.Show(" Mã phần mềm đã tồn tại!", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {                      
                        QLPhanMemDAO.Instance.Insert(maPM,tenPM,slmaxKey,license, ngaymua, hansd, ncc,chucnang, ghichu,status,IDLoaiPM,IDPhienBanPM);
                        MessageBox.Show($" Thêm mã phần mềm {maPM} thành công! ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);                      
                        them = false;
                        LoadControl();
                    }
                }
                else
                { 
                    // UPDATE THONG TIN
                    string maPM = txtMaPhanMem.Text;
                    string tenPM = txtTenPhanMem.Text;
                    string license = txtLicense.Text;
                    int slmaxKey = int.Parse(txtGioiHanSLKey.Text);
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string hansd = dtpHanSuDung.Value.ToString("dd/MM/yyyy");
                    string ghichu = txtGhiChu.Text;
                    string ChucNang = txtChucnang.Text;
                    int IDLoaiPM = int.Parse(sglLoaiPM.EditValue.ToString());
                    int IDPhienBanPM = int.Parse(sglPhienBanPM.EditValue.ToString());
                    string ncc = txtNCC.Text;
                    int GioiHanLC = 0;
                    if (chkGHLC.Checked)
                    {
                        GioiHanLC = 1;
                    }
                    else
                    {
                        GioiHanLC = 0;
                    }
                    if (maPM == "")
                    {
                        MessageBox.Show($" Bạn chưa chọn mã phần mềm để thay đổi thông tin!", "Thông Báo:");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của mã phần mềm {maPM}", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            QLPhanMemDAO.Instance.Update(IDselected,maPM, tenPM, slmaxKey,license, ngaymua, hansd, ncc,ChucNang, ghichu,GioiHanLC,IDLoaiPM,IDPhienBanPM);
                            MessageBox.Show($" Sửa thông tin mã phần mềm {maPM} thành công! ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Hãy chọn nhà cung cấp có trong danh sách ", "Thông Báo:");
            //}
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            if(IDselected==0)
            {
                MessageBox.Show("Chưa chọn phần mềm để sửa thông tin.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LockControl(false);
               
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            //  int demloi = 0;

            List<int> LsIDPMdc = new List<int>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                int ID = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                LsIDPMdc.Add(ID);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} phần mềm được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demXoa = 0;
                    foreach (int item in LsIDPMdc)
                    {
                       
                        // Không có khóa phụ tham chiếu đến bảng này.
                        try
                        {
                            // Xóa trong bảng quản lý phần mềm
                            QLPhanMemDAO.Instance.Delete(item);
                            demXoa++;

                            // Xóa trong bảng danh sách cài đặt.
                            DsCaiDatDAO.Instance.DeleteWithIDPM(item);

                            // Không cần xóa trong bảng lịch sử cài đặt
                        }
                        catch 
                        {
                            // Trường hợp nó đang có khóa phụ tham chiếu đến.
                            // Thông báo do bị dính khóa phụ.

                        }
                    }

                    if (demXoa < dem)
                    {
                        MessageBox.Show($"Đã xóa {demXoa} phần mềm, {dem - demXoa} phần mềm không thể xóa.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xóa {dem} nhân viên được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
            // Câu lệnh Insert đang bị sai
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                IDselected = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
                txtMaPhanMem.Text = gridView1.GetFocusedRowCellValue("MAPM").ToString();
                txtTenPhanMem.Text = gridView1.GetFocusedRowCellValue("TENPM").ToString();
                txtLicense.Text = gridView1.GetFocusedRowCellValue("LICENSE").ToString();
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
                txtGioiHanSLKey.Text = gridView1.GetFocusedRowCellValue("SLMAXKEY").ToString();
                dtpNgayMua.Value = DateTime.Parse(gridView1.GetFocusedRowCellValue("NGAYMUA").ToString());
                dtpHanSuDung.Value = DateTime.Parse(gridView1.GetFocusedRowCellValue("HANSD").ToString());
                txtNCC.Text = gridView1.GetFocusedRowCellValue("NCC").ToString();
            }
            catch
            {
                
            }
            
        }

        // Xử lý Excell cho Form danh sách phần mềm:
        void XuatExCel()
        {
            #region  Xuat Excel OK
            //using (System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog())
            //{
            //    saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
            //    if (saveDialog.ShowDialog() != DialogResult.Cancel)
            //    {
            //        string exportFilePath = saveDialog.FileName;
            //        string fileExtenstion = new FileInfo(exportFilePath).Extension;

            //        switch (fileExtenstion)
            //        {
            //            case ".xlsx":
            //                gridView1.OptionsSelection.MultiSelect = false;
            //              GridColumn A=  new GridColumn();
            //                A.FieldName = "MAPM";
            //                A.Caption = "Test";
            //                gridView1.Columns.Add(A);
            //                A.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;


            //                gridControl1.ExportToXlsx(exportFilePath);

            //                gridView1.OptionsSelection.MultiSelect = true;
            //                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //                break;

            //            default:
            //                break;
            //        }

            //        if (File.Exists(exportFilePath))
            //        {
            //            try
            //            {
            //                //Try to open the file and let windows decide how to open it.
            //                System.Diagnostics.Process.Start(exportFilePath);
            //            }
            //            catch
            //            {
            //                String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
            //                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        else
            //        {
            //            String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
            //            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            #endregion

            using (System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xlsx":
                            gridView1.OptionsSelection.MultiSelect = false;
                            GridColumn A = new GridColumn();
                            A.FieldName = "MAPM";
                            A.Caption = "Test";
                            gridView1.Columns.Add(A);
                            A.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;


                            gridControl1.ExportToXlsx(exportFilePath);

                            gridView1.OptionsSelection.MultiSelect = true;
                            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                            break;

                        default:
                            break;
                    }

                    string duongdan = exportFilePath;
                    gridView1.ExportToXlsx(duongdan);
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(duongdan);
                    Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];
                    int cot = ws.UsedRange.Columns.Count + 1;
                    ws.Cells[1, cot + 3] = "PHU";
                    for (int i = 2; i < ws.UsedRange.Rows.Count + 1; i++)
                    {
                        ws.Cells[i, cot + 3] = "=IF(COUNTIF($G$2:G"+i+",G"+i+ ")=1,G" + i + ","+'"'+'"'+")";
                    }
                    wb.Save();
                    wb.Close();
                    excel.Quit();
                

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            


        }

        void TaiForm()
        {
            using (System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    string MaPM = "";
                    // câu lệnh select trả ra một bảng trắng 
                    gridControl1.DataSource = DanhSachPhanMemDAO.Instance.GetRowMaPM(MaPM);

                    switch (fileExtenstion)
                    {
                        case ".xlsx":
                            gridControl1.ExportToXlsx(exportFilePath);
                            break;

                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            LoadControl();
        }

       
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách phần mềm thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            LoadControl();
        }

        private void btnTaiForm_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn tải Form Excel mẫu để nhập dữ liệu?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                TaiForm();
            }
            LoadControl();
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            LockControl(false);
            frmNhapExcelDSPhanMem f = new frmNhapExcelDSPhanMem();
            f.ShowDialog();
            LoadControl();
        }


        private void chkGHLC_CheckedChanged(object sender, EventArgs e)
        {
            if(chkGHLC.Checked)
            {
                txtGioiHanSLKey.Enabled = false;
            }
            else
            {
                txtGioiHanSLKey.Enabled = true;
            }
           
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            int IDLOAIPM =int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["IDLOAIPM"]).ToString());
                                 
            if (IDLOAIPM==1) // Loại phần mềm yêu cầu bản quyền
            {
                e.Appearance.BackColor = btnYCBanQuyen.Appearance.BackColor;              
            }       
            if (IDLOAIPM == 1)
            {
                e.Appearance.BackColor = btnKhongYCBQ.Appearance.BackColor;
            }
        }
    }

}