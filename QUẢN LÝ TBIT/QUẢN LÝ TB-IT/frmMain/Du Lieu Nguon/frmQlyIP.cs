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
using System.IO;
using DevExpress.XtraGrid.Columns;

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmQlyIP : DevExpress.XtraEditors.XtraForm
    {
        public frmQlyIP()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;

        private void LoadControl()
        {
            LoadLookupEdit();
            LockControl(true);
            LoadData();
            CleanText();
        }

        private void LoadLookupEdit()
        {
            sglLoaiTB.Properties.DataSource = LoaiTBDAO.Instance.GetTable();
            sglLoaiTB.Properties.DisplayMember = "ID";
            sglLoaiTB.Properties.ValueMember = "ID";
        }

        void CleanText()
        {
            txtDaiMang.Clear();
            txtStartIP.Clear();
            txtEndIP.Clear();
        }
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtDaiMang.Enabled = false;
                txtStartIP.Enabled = false;
                txtEndIP.Enabled = false;
                sglLoaiTB.Enabled = false;

                btnThem.Enabled = true;             
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                txtDaiMang.Enabled = true;
                txtStartIP.Enabled = true;
                txtEndIP.Enabled = true;
                sglLoaiTB.Enabled = true;

                btnThem.Enabled = false;              
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {                      
                gridControl1.DataSource = QlyIPDAO.Instance.GetTable();           
        }

        void Save()
        {
            if (them)
            {
                // Thêm một địa chỉ mạng và thêm một dải mạng:
                string DaiMang = txtDaiMang.Text.Trim();
                int IdTB = int.Parse(sglLoaiTB.EditValue.ToString());

                int IPStart = 0;
                int IPEnd = 0;

                try
                {
                     IPStart = int.Parse(txtStartIP.Text.Trim());
                     IPEnd = int.Parse(txtEndIP.Text.Trim());
                }
                catch 
                {
                    MessageBox.Show($"Hãy nhập giá trị số nguyên cho địa chỉ IP.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                if(IPStart> IPEnd)
                {
                    MessageBox.Show($"IP bắt đầu lớn hơn IP kết thúc.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Khởi tạo một biến đếm với bước nhảy là 1 tăng từ IPStart đến IpEnd
                    int demthem = 0;
                    int demtrung = 0;

                    // Chạy vòng lặp for để Insert IP vào CSDL

                    for (int i = IPStart; i <= IPEnd; i++)
                    {
                        string IP = DaiMang + i.ToString();
                        bool CheckIPExits = QlyIPDAO.Instance.CheckIPExist(IP);
                        if (CheckIPExits)  // Nếu IP đó đã tồn tại
                        {
                            demtrung++;
                        }
                        else                      
                        {
                            //IP chưa được gán thì có trạng thái bằng 0
                            QlyIPDAO.Instance.Insert(DaiMang, IP, 0, IdTB);
                            demthem++;
                        }
                    }
                    MessageBox.Show($"Đã thêm {demthem} địa chỉ IP, có {demtrung} địa chỉ IP đã tồn tại. ", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    them = false;
                }                
            }
            else
            {
                
                MessageBox.Show($"Không có chức năng sửa.", "THÔNG BÁO:");
                
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                                                                        
                    // cho phép xóa nhiều dòng trong gridview
                    int dem = 0;
                    List<int> LsIPdcChon = new List<int>();

                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        int ID =int.Parse( gridView1.GetRowCellValue(item, "ID").ToString());
                        LsIPdcChon.Add(ID);
                        dem++;
                    }

                    if(dem>0)
                    {

                        DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} địa chỉ IP được chọn. ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {

                            foreach (int item in LsIPdcChon)
                            {
                                // Cần vòng Try Catch do sẽ bị dính khóa phụ.
                                QlyIPDAO.Instance.Delete(item);
                            }
                            MessageBox.Show($" Đã xóa thành công {dem} địa chỉ IP.", "THÀNH CÔNG!");

                        }

                    }
                    else
                    {
                        MessageBox.Show(" Bạn chưa chọn IP để xóa! ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                               
                LoadControl();          
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

        // Xử lý Excell cho Form danh sách phần mềm:
        void XuatExCel()
        {
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

        private void btnNhapExcell_Click(object sender, EventArgs e)
        {
            //LockControl(false);
            //frmNhapExcelDSPhanMem f = new frmNhapExcelDSPhanMem();
            //f.ShowDialog();
            //LoadControl();
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách phần mềm thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            LoadControl();
        }
    }
}