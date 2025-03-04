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
using DTO;
using frmMain.Quan_Ly_May_Tinh;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace frmMain
{
    public partial class frmDanhSachMayTinh : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachMayTinh()
        {
            InitializeComponent();
            LoadControl();
        }

        int IDselected=0;
        int luu = 0;


        private void LoadControl()
        {

            //Update lại biến bảo hành mỗi khi Load lại Form
          //   UpdateBaoHanh(); 
           
            LoadEditLookup();
            LoadCBX();
            LockControl(true);
            CleanText();    
            LoadData();
            IDselected = 0;
        }


        private void LoadEditLookup()
        {
           
        }

        private void LockControl(bool kt)
        {

            if (kt)
            {
                txtMaMT.Enabled = false;
                txtDiaChiIP.Enabled = false;
                txtDcMAC.Enabled = false;
                txtDomain.Enabled = false;
                cbLoaiMT.Enabled = false;
                cbNCC.Enabled = false;
                txtMaTSCD.Enabled = false;
                txtDiaChiIP.Enabled = false;
                sglPhongBan.Enabled = false;
                txtNhaMay.Enabled = false;
                txtNguoiSD.Enabled = false;
                txtDcMAC.Enabled = false;
                dtpNgayMua.Enabled = false;
                dtpHanBaoHanh.Enabled = false;
                txtNguoiSD.Enabled = false;
                txtGhiChu.Enabled = false;
                sglDaiIP.Enabled = false;
                sglDiaChiIP.Enabled = false;


                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;

                btnTaiForm.Enabled = true;
                btnNhapExcell.Enabled = true;
                btnXuatExcell.Enabled = true;

            }
            else
            {
                txtMaMT.Enabled = true;              
                txtDcMAC.Enabled = true;
                txtDomain.Enabled = true;
                cbLoaiMT.Enabled = true;
                cbNCC.Enabled = true;
                txtMaTSCD.Enabled = true;
                txtDiaChiIP.Enabled = true;
                sglPhongBan.Enabled = true;
                txtNhaMay.Enabled = false;
                txtNguoiSD.Enabled = true;
                txtDcMAC.Enabled = true;
                dtpNgayMua.Enabled = true;
                dtpHanBaoHanh.Enabled = true;
                txtNguoiSD.Enabled = true;
                txtGhiChu.Enabled = true;
                sglDaiIP.Enabled = true;
                sglDiaChiIP.Enabled = false;


                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;

                btnTaiForm.Enabled = true;
                btnNhapExcell.Enabled = true;
                btnXuatExcell.Enabled = true;

            }

        }

        private void LoadData()
        {
            // Khởi tạo ban đầu là máy tính sẽ chọn IP tĩnh.

            radIPtinh.Checked = true;

            // Load trong bảng kế hoạch bảo dưỡng, bảo trì xem có phòng ban nào cần bảo dưỡng không thì bôi màu các máy tính hết hạn bảo hành.

            gridControl1.DataSource = QuanLyMayTinhDAO.Instance.GetTable();
            lblTongSoMT.Text = QuanLyMayTinhDAO.Instance.TongMT() + "";
            string maMT = txtMaMT.Text;
        }


        private void UpdateBaoHanh()
        {
            // Load trong bảng kế hoạch bảo dưỡng, bảo trì xem có phòng ban nào cần bảo dưỡng không thì bôi màu các máy tính hết hạn bảo hành.

            List<QuanLyMayTinhDTO> LsMT = QuanLyMayTinhDAO.Instance.GetListMaMT();

            foreach (QuanLyMayTinhDTO item in LsMT)
            {
                    string HanBH = item.HANBH;
                    // Nếu hạn bảo hành bằng rỗng.
                    if(HanBH=="")
                    {
                        QuanLyMayTinhDAO.Instance.UpdateBH(item.ID, false);
                    }
                    else
                    {
                        DateTime Hanbh = Convert.ToDateTime(HanBH);
                        TimeSpan time = Hanbh- DateTime.Now  ;  // Hạn bảo hành - ngày hiện tại.
                        int songayBH = time.Days;
                        if (songayBH < 0) // không còn bảo hành thì Update lại là ko còn bảo hành.
                        {
                            QuanLyMayTinhDAO.Instance.UpdateBH(item.ID, false);
                        }
                    }
                   
            }                    
        }

        private void LoadCBX()
        {
            // Load NHÀ CUNG CẤP:

            cbNCC.DataSource = NhaCungCapDAO.Instance.GetListNCC();
            cbNCC.DisplayMember = "MANCC";
            cbNCC.ValueMember = "MANCC";



            cbLoaiMT.DataSource = LoaiMayTinhDAO.Instance.GetListLoaiMT();
            cbLoaiMT.DisplayMember = "TENLOAIMT";
            cbLoaiMT.ValueMember = "TENLOAIMT";

            // Load Phòng ban:

            sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetLsvPB();
            sglPhongBan.Properties.DisplayMember = "MAPB";
            sglPhongBan.Properties.ValueMember = "MAPB";

            // Load Dải  IP

            sglDaiIP.Properties.DataSource = QlyIPDAO.Instance.GetTableDaiMang();
            sglDaiIP.Properties.DisplayMember = "DAIMANG";
            sglDaiIP.Properties.ValueMember = "DAIMANG";


            // Load Địa chỉ IP:

            sglDiaChiIP.Properties.DataSource = QlyIPDAO.Instance.GetTable();
            sglDiaChiIP.Properties.DisplayMember = "IP";
            sglDiaChiIP.Properties.ValueMember = "ID";

        }



        private void CleanText()
        {
            txtMaMT.Clear();
          //  txtDiaChiIP.Clear();
            txtDcMAC.Clear();
            txtNguoiSD.Clear();
            txtMaTSCD.Clear();
            txtGhiChu.Clear();
        }


        void Save()
        {
            switch (luu)
            {
                case 1: // luu khi them du lieu
                    {
                        // QLYMAYTINH(ID,MAMT, MAC, LOAIMT, NCC, NHAMAY, PB, NGUOISD, MATSCD, NGAYMUA, HANBH, BAOHANH, GHICHU, IDIP)

                        string maMT = txtMaMT.Text.Trim();
                     
                        int IdIP = 0;
                      
                        if(radDHCP.Checked)
                        {
                            IdIP = 1053;  // ID= 1053 là ID của DHCP
                        }
                        if(radIPtinh.Checked)
                        {
                            try
                            {
                                IdIP = int.Parse(sglDiaChiIP.EditValue.ToString()); string mac = txtDcMAC.Text.Trim();
                                string Domain = txtDomain.Text.Trim();
                                string loaiMT = cbLoaiMT.SelectedValue.ToString();
                                string ncc = cbNCC.SelectedValue.ToString();
                                string Phongban = sglPhongBan.EditValue.ToString();
                                string NhaMay = txtNhaMay.Text;
                                string nguoisd = txtNguoiSD.Text;
                                string MaTSCD = txtMaTSCD.Text;
                                bool baohanh = false;
                                string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                                string hanbh = dtpHanBaoHanh.Value.ToString("dd/MM/yyyy");
                                string ghichu = txtGhiChu.Text;

                                bool CheckMaMTExist = QuanLyMayTinhDAO.Instance.CheckMaMTExist(maMT);

                                if (CheckMaMTExist)
                                {
                                    MessageBox.Show($" Mã máy tính {maMT} đã tồn tại!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                                else
                                {

                                    TimeSpan timebh = dtpHanBaoHanh.Value - DateTime.Now;
                                    int songay = timebh.Days;
                                    if (songay > 0)
                                    {
                                        baohanh = true;
                                    }

                                    // IP được lưu vào thì phải đổi trạng thái cho máy tính.

                                    QuanLyMayTinhDAO.Instance.Insert(maMT, mac, Domain, loaiMT, ncc, NhaMay, Phongban, nguoisd, MaTSCD, ngaymua, hanbh, baohanh, ghichu, IdIP);

                                    // Lúc này lại ko biết được ID của thằng này

                                    QuanLyMayTinhDTO MTDTO = QuanLyMayTinhDAO.Instance.GetMaMT(maMT);

                                    // Update trạng thái IP từ 0 chuyển sang Status là ID của máy tính.

                                    QlyIPDAO.Instance.UpdateStatus(IdIP, MTDTO.ID,maMT,nguoisd);

                                    MessageBox.Show($"Đã thêm mã máy tính {maMT}.", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                luu = 0;

                            }
                            catch
                            {
                                MessageBox.Show($"Chưa chọn địa chỉ IP.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            
                        }

                        
                    }

                    break;

                // Thực hiện sửa dữ liệu máy tính( Sửa thông tin là khó)

                case 2: // Sửa thông tin máy tính.

                    //  Từ ID máy tính đc chọn ====> Lấy ra được IDIP của máy tính theo DTO.

                    {

                        string maMT = txtMaMT.Text.Trim();
                        int IDIPpast = 0;
                        try
                        {
                            QuanLyMayTinhDTO MTDTO = QuanLyMayTinhDAO.Instance.GetMTDTO(IDselected);
                            IDIPpast = MTDTO.IDIP;
                        }
                        catch 
                        {
                         
                        }                       
                        string dcIP = txtDiaChiIP.Text;
                        int IdIPnew = 0;                    
                        if (radDHCP.Checked)
                        {
                            IdIPnew = 1053;
                        }
                        if (radIPtinh.Checked)
                        {
                            IdIPnew = int.Parse(sglDiaChiIP.EditValue.ToString()); // Lấy giá trị ID của IP 
                        }
                        string mac = txtDcMAC.Text.Trim();
                        string Domain = txtDomain.Text.Trim();
                        string loaiMT = cbLoaiMT.SelectedValue.ToString();
                        string ncc = cbNCC.SelectedValue.ToString();
                        string Phongban = sglPhongBan.EditValue.ToString();
                        string NhaMay = txtNhaMay.Text;
                        string nguoisd = txtNguoiSD.Text;
                        string MaTSCD = txtMaTSCD.Text;
                        bool baohanh = false;
                        string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                        string hanbh = dtpHanBaoHanh.Value.ToString("dd/MM/yyyy");
                        string ghichu = txtGhiChu.Text;

                        if (maMT == "")
                        {
                            MessageBox.Show("Chưa chọn mã máy tính để sửa. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            TimeSpan timebh = dtpHanBaoHanh.Value - DateTime.Now;
                            int songay = timebh.Days;
                            if (songay > 0)
                            {
                                baohanh = true;
                            }

                            // Sửa trong bảng quản lý máy tính

                            QuanLyMayTinhDAO.Instance.Update(IDselected,maMT, mac, Domain, loaiMT, ncc, NhaMay, Phongban, nguoisd, MaTSCD, ngaymua, hanbh, baohanh, ghichu,IdIPnew);

                            //Sửa trong cả bảng Quản lý IP.
                            // Kiểm tra sự khác biệt của 2 IDIP để chạy lệnh Update trạng thái IDIP.
                            // So sánh IDIP cũ và IDIP MỚI ====> ĐỂ ĐƯA RA QUYẾT ĐỊNH CHẠY LỆNH Update trạng thái IDIP TRONG BẢNG QUẢN LÝ IP.


                            if(IdIPnew!=IDIPpast)
                            {

                                // Update trạng thái đã bị chiếm cho IP new

                                QlyIPDAO.Instance.UpdateStatus(IdIPnew, IDselected,maMT,nguoisd); // Trạng thái  đã bị chiếm đóng STATUS bằng ID máy tính


                                // Update lại trạng thái khả dụng cho IP pass

                                QlyIPDAO.Instance.UpdateStatus(IDIPpast, 0,"","");  // Trạng thái 0 khả dụng.


                            }

                            MessageBox.Show($"Đã sửa thông tin mã máy tính {maMT}.", "Thành công:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        }
                        luu = 0;
                    }
                    break;
                case 3:   // Trường hợp nhập File Excell từ máy tính.
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn lưu lại danh sách máy tính bên dưới vào CSDL?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            // QLYMAYTINH(MAMT, IP, MAC, LOAIMT, NCC, NHAMAY, PB, NGUOISD, MATSCD, NGAYMUA, HANBH, BAOHANH, GHICHU)

                            DataTable dt = new DataTable();
                            dt.Columns.Add("MAMT");
                            dt.Columns.Add("IP");
                            dt.Columns.Add("MAC");
                            dt.Columns.Add("LOAIMT");
                            dt.Columns.Add("NCC");
                            dt.Columns.Add("NHAMAY");
                            dt.Columns.Add("PB");
                            dt.Columns.Add("NGUOISD");
                            dt.Columns.Add("MATSCD");
                            dt.Columns.Add("NGAYMUA");
                            dt.Columns.Add("HANBH");
                            dt.Columns.Add("BAOHANH");
                            dt.Columns.Add("GHICHU");

                            dt = data;
                            foreach (DataRow item in dt.Rows)
                            {

                                //QuanLyMayTinhDTO dsmtDTO = new QuanLyMayTinhDTO(item, 0);

                                //string maMT = dsmtDTO.MAMT;
                                //string dcIP = dsmtDTO.IP;
                                //string mac = dsmtDTO.MAC;
                                //string loaiMT = dsmtDTO.LOAIMT;
                                //string ncc = dsmtDTO.NCC;
                                //string phongban = dsmtDTO.PHONGBAN;
                                //string nguoisd = dsmtDTO.NGUOISD;
                                //string MaTSCD = dsmtDTO.MATSCD;
                                //bool baohanh = dsmtDTO.BAOHANH;
                                //string ngaymua = dsmtDTO.NGAYMUA;
                                //string hanbh = dsmtDTO.HANBH;
                                //string ghichu = dsmtDTO.GHICHU;


                                //QuanLyMayTinhDAO.Instance.Insert(maMT, baohanh, dcIP, mac, loaiMT, ncc, phongban, nguoisd, MaTSCD, ngaymua, hanbh, ghichu,0);



                            }
                        }
                    }
                    break;

            }

        }
        private DataTable NhapExCel()
        {
            // Đoạn Excell này xử lý sau.

            // Khai bao bien de luu duong dan file can import
            string filePath = "";
            // tao openfile dialog dde mo file excell
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            // chi loc ra cac file co dinh dang excel
            // dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng

            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                filePath = dialog.FileName;
            }

            // Nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Duong dan khong hop le. ");

            }
            // tao ra bảng mẫu danh sach User Infor rong de hung du lieu.

            DataTable dt = new DataTable();
            dt.Columns.Add("MAMT");
            dt.Columns.Add("BAOHANH");
            dt.Columns.Add("IP");
            dt.Columns.Add("MAC");
            dt.Columns.Add("LOAIMT");
            dt.Columns.Add("NCC");
            dt.Columns.Add("PHONGBAN");
            dt.Columns.Add("NGUOISD");
            dt.Columns.Add("MATSCD");
            dt.Columns.Add("NGAYMUA");
            dt.Columns.Add("HANBH");
            dt.Columns.Add("GHICHU");

            // mo file excell
            var package = new ExcelPackage(new FileInfo(filePath));
            // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Su dung muc dich khong thuong mai
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
            //   duyet tuan tu tu dong thu 2 den dong cuoi cung cua file, luu y file excel bat dau tu so 1
            for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
            {
                //  biến j biểu thị cho một cột dữ liệu trong file Excell

                int j = 2;
                string maMT = "";
                try
                {
                    maMT = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string baohanh = "";
                try
                {
                    baohanh = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ip = "";
                try
                {
                    ip = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string mac = "";
                try
                {
                    mac = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string loaiMT = "";
                try
                {
                    loaiMT = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ncc = "";
                try
                {
                    ncc = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string phongban = "";
                try
                {
                    phongban = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string nguoisd = "";
                try
                {
                    nguoisd = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string matscd = "";

                try
                {
                    matscd = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ngaymua = "";
                try
                {
                    ngaymua = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string hanbh = "";
                try
                {
                    hanbh = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ghichu = "";
                try
                {
                    ghichu = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                dt.Rows.Add(maMT, baohanh, ip, mac, loaiMT, ncc, phongban, nguoisd, matscd, ngaymua, hanbh, ghichu);

            }
            return dt;
        }
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
                    string taikhoan = "";

                    gridControl1.DataSource = null;

                    switch (fileExtenstion)
                    {
                        case ".xlsx":

                            //  gridColumn1.Visible = false; // Bỏ cột đầu tiên đi.
                            //   gridView1.OptionsSelection.MultiSelect = false; // Bỏ cột lựa chọn khi xuất file Excell.
                            gridControl1.ExportToXlsx(exportFilePath);

                            gridColumn1.Visible = true;
                            gridColumn1.VisibleIndex = 1;
                            gridView1.OptionsSelection.MultiSelect = true;
                            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                            LoadControl();


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

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            luu = 1;

           
            
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
                luu = 2;
            }
            else
            {
                MessageBox.Show("Chưa chọn mã máy tính để xóa hoặc chọn quá 1 mã máy tính để sửa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaMT.Enabled = false;

            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;

            List<string> LsMaMTDcChon = new List<string>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaMT = gridView1.GetRowCellValue(item, "MAMT").ToString();
                LsMaMTDcChon.Add(MaMT);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã máy tính được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {

                    foreach (string item in LsMaMTDcChon)
                    {
                        // Update lại trạng thái cho địa chỉ IP của máy tính.

                        QuanLyMayTinhDTO MTDTO = QuanLyMayTinhDAO.Instance.GetMaMT1(item);
                        int IdIP = MTDTO.IDIP;

                        // TRƯỜNG HỢP XÓA MÁY TÍNH
                        QlyIPDAO.Instance.UpdateStatus(IdIP,0,"",""); // Trạng thái IP 0 là trạng thái IP chưa được gán.

                        // Xóa trong bảng ds máy tính. 

                        QuanLyMayTinhDAO.Instance.Delete(item);

                        // Xóa trong bảng ds cài đặt phần mềm xóa theo mã.
                        DsCaiDatDAO.Instance.Delete1(item);


                    }
                    MessageBox.Show($"Đã xóa {dem} mã máy tính được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã máy tính để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Done: hoàn thành.
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        DataTable data = new DataTable();

        private void btnNhapExcell_Click(object sender, EventArgs e)
        {
            LockControl(false);

            frmNhapExcelDSMayTinh f = new frmNhapExcelDSMayTinh();
            f.ShowDialog();
            LoadControl();

        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách máy tính thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {

                IDselected = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
                // 320 thì sẽ lấy theo ID đầu tiên.
              //  MessageBox.Show($"ID lấy là ID {IDselected} ");
                txtMaMT.Text = gridView1.GetFocusedRowCellValue("MAMT").ToString();
                txtDomain.Text = gridView1.GetFocusedRowCellValue("DOMAIN").ToString();
                txtDiaChiIP.Text = gridView1.GetFocusedRowCellValue("IP").ToString();

                txtDcMAC.Text = gridView1.GetFocusedRowCellValue("MAC").ToString();
                txtNguoiSD.Text = gridView1.GetFocusedRowCellValue("NGUOISD").ToString();
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
                txtMaTSCD.Text = gridView1.GetFocusedRowCellValue("MATSCD").ToString();
                sglDaiIP.Properties.DataSource = QlyIPDAO.Instance.GetTableDaiMang();
                sglPhongBan.EditValue = gridView1.GetFocusedRowCellValue("PB").ToString();
                // đang không đúng lý ở đây // Lấy ra giá trị ID IP
                sglDiaChiIP.EditValue= gridView1.GetFocusedRowCellValue("IDIP").ToString();
                
                txtNhaMay.Text = gridView1.GetFocusedRowCellValue("NHAMAY").ToString();
                cbNCC.SelectedValue = gridView1.GetFocusedRowCellValue("NCC").ToString();
                cbLoaiMT.SelectedValue = gridView1.GetFocusedRowCellValue("LOAIMT").ToString();
                dtpNgayMua.Value = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("NGAYMUA").ToString());
                dtpHanBaoHanh.Value = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("HANBH").ToString());
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();


            }
            catch
            {


            }
           

        }




        private void searchLookUpEdit2View_Click(object sender, EventArgs e)
        {
            txtNhaMay.Text = searchLookUpEdit2View.GetFocusedRowCellValue("NHAMAY").ToString();
        }

        private void btnBaoDuong_Click(object sender, EventArgs e)
        {
            frmKHBaoDuongMT f = new frmKHBaoDuongMT();
            f.ShowDialog();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            //if (e.RowHandle > 0)
            //{
            // 
            // Sét thời gian cảnh báo trước 3 ngày ====> Load  trong 7 ngày tới xem có phòng ban nào cần vệ sinh máy tính không thì bôi màu

            //QlyDonHangITDTO a = new QlyDonHangITDTO(item);
            //DateTime ngayDH = Convert.ToDateTime(a.NGAYDH);

            //TimeSpan time = DateTime.Now - ngayDH;
            //int songay = time.Days;

            //if (songay > 15)
            //{
            //    LsQuaHan.Add(a);
            //}


            // Lấy ra 7 ngày sau 
            // THỜI gian để thực hiện câu lệnh này, Select theo năm và theo tháng



          

            //string MaP = view.GetRowCellValue(e.RowHandle, view.Columns["PB"]).ToString();
            //string MaNM = view.GetRowCellValue(e.RowHandle, view.Columns["NHAMAY"]).ToString();

            string mamt = view.GetRowCellValue(e.RowHandle, view.Columns["MAMT"]).ToString();
            //string loaimt = view.GetRowCellValue(e.RowHandle, view.Columns["LOAIMT"]).ToString();
            bool CheckCDPM =DsCaiDatDAO.Instance.CheckCDPM(mamt);
            bool kt = bool.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["BAOHANH"]).ToString());

                List<string> LsMaMT = new List<string>();
                List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHCanTH();

                foreach (KeHoachBDDTO item in LsKH)
                {
                    string Nhamay = item.NHAMAY;
                    string PB = item.PB;
                    // Lấy ra List máy tính cần bảo trì
                    List<QuanLyMayTinhDTO> LsMT = QuanLyMayTinhDAO.Instance.GetLsMTBaoTri(Nhamay, PB);
                    foreach (QuanLyMayTinhDTO item1 in LsMT)
                    {
                        if(!LsMaMT.Contains(item1.MAMT))
                        {
                            LsMaMT.Add(item1.MAMT);
                        }
                    }

                }

            // Lấy ra được List ngày bảo trì bảo dưỡng.


            //List<KeHoachBDDTO> LsKH1 = LsKH.Where(x => x.PB == MaP).ToList();
            //int dem = LsKH1.Count;
            //if (e.CellValue.ToString() == mamt && dem > 0 && !kt && loaimt != "LAPTOP")
            //{
            //    e.Appearance.BackColor = btnCanBD.Appearance.BackColor;
            //}

            if (e.CellValue.ToString() == mamt && LsMaMT.Contains(mamt))
            {
                e.Appearance.BackColor = btnCanBD.Appearance.BackColor;
            }
            else
            {
                if (e.CellValue.ToString() == mamt && kt) // Check BẢO HÀNH
                {
                    e.Appearance.BackColor = btnConBH.Appearance.BackColor;
                }

                if (e.CellValue.ToString() == mamt && !CheckCDPM)
                {
                    e.Appearance.BackColor = btnChuaCaiPM.Appearance.BackColor;
                }
                
            }
        }

        private void radDHCP_CheckedChanged(object sender, EventArgs e)
        {
            sglDaiIP.Enabled = false;
            sglDiaChiIP.Enabled = false;
        }

        private void sglDaiIP_EditValueChanged(object sender, EventArgs e)
        {
            sglDiaChiIP.Enabled = true;
            string DaiMang = sglDaiIP.EditValue.ToString();
            // Khi chọn thì Load ra dải địa chỉ mạng cho máy tính văn phòng của dải mạng đó 

            sglDiaChiIP.Properties.DataSource = QlyIPDAO.Instance.GetTableIPofDAIMANG(DaiMang);
            sglDiaChiIP.Properties.DisplayMember = "IP";
            sglDiaChiIP.Properties.ValueMember = "ID";

        }

        private void radIPtinh_CheckedChanged(object sender, EventArgs e)
        {
            sglDaiIP.Enabled = true;
            sglDiaChiIP.Enabled = false;
        }
    }

    
}