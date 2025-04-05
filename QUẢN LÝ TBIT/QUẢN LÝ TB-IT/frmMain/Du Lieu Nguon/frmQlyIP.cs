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
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmQlyIP : DevExpress.XtraEditors.XtraForm
    {

        public frmQlyIP()
        {
            InitializeComponent();
            LoadControl();
        }


        // Đang khó lấy tên của máy tính.

        bool them;
        int idquyen = CommonUser.Quyen;
        int IDselected = 0;

        // Load dải mạng

        // Đang cần một giải thuật để sắp xếp IP


       

        private void LoadControl()
        {
            LoadLookupEdit();
            LockControl(true);
            LoadData();
            CleanText();
            IDselected = 0;
        }

        private void LoadLookupEdit()
        {
            sglLoaiTB.Properties.DataSource = LoaiTBDAO.Instance.GetTable();
            sglLoaiTB.Properties.DisplayMember = "MATB";
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
                txtSoDaiMang.Enabled = false;
                txtIPsua.Enabled = false;
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
                txtSoDaiMang.Enabled = true;
                txtIPsua.Enabled = false;
                txtDaiMang.Enabled = false;
                txtStartIP.Enabled = true;
                txtEndIP.Enabled = true;
                sglLoaiTB.Enabled = true;

                btnThem.Enabled = false;              
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }

        }

        // ĐANG KHÔNG LẤY ĐƯỢC TÊN MÁY TÍNH.

        private void LoadData()
        {

            // Chỗ này đang cần một giải thuật để sắp xếp
            gridControl1.DataSource = QlyIPDAO.Instance.GetLsIPDTODaSX();

            // Dùng luôn cột STATUS LÀM IDMT 
        }

        void Save()
        {
            if (them)
            {
                // Thêm một địa chỉ mạng và thêm một dải mạng:               
                string DaiMang =txtDaiMang.Text;
                if (DaiMang == "")
                {
                    MessageBox.Show($"Hãy nhập số dải mạng.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int IdTB = 0;
                    int IPStart = 0;
                    int IPEnd = 0;

                    try
                    {

                        IdTB = int.Parse(sglLoaiTB.EditValue.ToString());
                        IPStart = int.Parse(txtStartIP.Text.Trim());
                        IPEnd = int.Parse(txtEndIP.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show($"Hãy nhập giá trị số nguyên cho địa chỉ IP.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (IPStart > IPEnd)
                    {
                        MessageBox.Show($"IP bắt đầu lớn hơn IP kết thúc.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Khởi tạo một biến đếm với bước nhảy là 1 tăng từ IPStart đến IpEnd
                        int SoDaiMang = int.Parse(txtSoDaiMang.Text);
                        int demthem = 0;
                        int demtrung = 0;

                        // Chạy vòng lặp for để Insert IP vào CSDL

                        for (int i = IPStart; i <= IPEnd; i++)
                        {
                            // Số IP sẽ trùng với cả i luôn.
                            string IP = DaiMang + i.ToString();
                            bool CheckIPExits = QlyIPDAO.Instance.CheckIPExist(IP);
                            if (CheckIPExits)  // Nếu IP đó đã tồn tại
                            {
                                demtrung++;
                            }
                            else
                            {
                                //IP chưa được gán thì có trạng thái bằng 0, ban đầu thêm vào thì mã máy tính bằng trống,người sử dụng bằng trống.
                                 QlyIPDAO.Instance.Insert(DaiMang, IP, 0, IdTB,SoDaiMang,i,"","");
                                demthem++;
                            }
                        }
                        MessageBox.Show($"Đã thêm {demthem} địa chỉ IP, có {demtrung} địa chỉ IP đã tồn tại. ", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        them = false;
                    }
                }
            }
            else
            {                          
                int IdTB = int.Parse(sglLoaiTB.EditValue.ToString());
                QlyIPDAO.Instance.UpdateIDTB(IDselected, IdTB);
                MessageBox.Show($"Đã sửa thông tin thành công.", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            int demxoa = 0;
                            int demloi = 0;
                            foreach (int item in LsIPdcChon)
                            {

                                try
                                {
                                    // Cần vòng Try Catch do sẽ bị dính khóa phụ.
                                    QlyIPDAO.Instance.Delete(item);
                                    demxoa++;
                                }
                                catch 
                                {
                                    demloi++;
                                }
                                                      
                            }
                            MessageBox.Show($" Đã xóa thành công {demxoa} địa chỉ IP, có {demloi} không thể xóa do đang được tham chiếu đến.", "THÀNH CÔNG!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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

            //  Bây h cần CẬP nhật mã máy tính.

            // Chạy lệnh cập nhật các tên người sử dụng

            //List<QlyIPDTO> LsIPDTO = QlyIPDAO.Instance.GetLsIPDTODaSX();

            //foreach (QlyIPDTO item in LsIPDTO)
            //{
            //    if (item.STATUS != 0) // Lúc này Status đang là ID máy tính.
            //    {

            //        int IDMT = item.STATUS;
            //        QuanLyMayTinhDTO MTDTO = QuanLyMayTinhDAO.Instance.GetMTDTO(IDMT);

            //        string NGUOISD = MTDTO.NGUOISD;

            //        // cập nhật MÃ MÁY TÍNH

            //        QlyIPDAO.Instance.UpdateNGUOISD(IDMT, NGUOISD);
            //    }
            //}

            //MessageBox.Show("Đã cập nhật xong  máy tính.", "Thành  công", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
            int ID =int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["STATUS"]).ToString());
           
           

            if (ID == 0) // BẰNG 0 thì IP khả dụng.
            {
                e.Appearance.BackColor = btnIPkd.Appearance.BackColor;
            }
            else //  Khác 0 thì IP đã cấp phát.
            {
                e.Appearance.BackColor = btnIPdcp.Appearance.BackColor;
            }

          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            //  int demloi = 0;
            List<int> LsIDdcChon = new List<int>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                int id =int.Parse( gridView1.GetRowCellValue(item, "ID").ToString());
                LsIDdcChon.Add(id);
                dem++;
            }
            if(dem>1||dem<=0)
            {
                MessageBox.Show($"Chưa chọn IP để sửa hoặc chọn quá 1 IP", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadControl();
            }
            else
            {

                //Sửa thì sẽ sửa loại thiết bị của IP.
                // Không thể sửa đối với IP đã cấp phát.
                foreach (int item in LsIDdcChon)
                {
                   IDselected = item;                  
                }
                QlyIPDTO IPDTO = QlyIPDAO.Instance.GetIPDTO(IDselected);
                int IDSTATUS = IPDTO.STATUS;
                if(IDSTATUS==1) // trường hợp IP đã được phân.
                {
                    MessageBox.Show($"Không thể sửa do IP đã  được cấp phát.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // trường hợp IP chưa  được phân.
                {
                    LockControl(false);
                    txtIPsua.Enabled = false;
                    sglLoaiTB.Enabled = true;
                    txtSoDaiMang.Enabled = false;
                    txtDaiMang.Enabled = false;
                    txtStartIP.Enabled = false;
                    txtEndIP.Enabled = false;
                }
                
            }                     
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

            try
            {
                IDselected =int.Parse( gridView1.GetFocusedRowCellValue("ID").ToString());
                txtIPsua.Text = gridView1.GetFocusedRowCellValue("IP").ToString();
                sglLoaiTB.EditValue= gridView1.GetFocusedRowCellValue("IDTB").ToString();
            }
            catch
            {

            }

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
           

            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            //  int demloi = 0;
            List<int> LsIDselected = new List<int>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                int ID = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                LsIDselected.Add(ID);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} địa chỉ IP được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demXoa = 0;
                    foreach (int item in LsIDselected)
                    {
                        // Chỉ cần xét xem IP có bị chiếm đóng hay không thôi.

                        QlyIPDTO IPDTO = QlyIPDAO.Instance.GetIPDTO(item);

                        if(IPDTO.STATUS==0) // IP chưa bị chiếm đóng thì có thể xóa.

                        {
                            QlyIPDAO.Instance.Delete(item);
                            demXoa++;
                        }
                            

                       
                    }

                    if (demXoa < dem)
                    {
                        MessageBox.Show($"Đã xóa {demXoa} địa chỉ IP,có {dem - demXoa} không thể xóa do đang được cấp phát.", "THÀNH CÔNG:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xóa {dem} địa chỉ IP được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    demXoa = 0;
                    dem = 0;

                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn địa chỉ IP để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtSoDaiMang_TextChanged(object sender, EventArgs e)
        {
            string dai = txtSoDaiMang.Text;
            if(dai !="" && dai != null)
            {
                txtDaiMang.Text = "192.168."+dai+".";
            }
            else
            {
                txtDaiMang.Text = "";
            }
        }
    }
}