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
using frmMain.Quan_Ly_May_Tinh;

namespace frmMain
{
    public partial class frmChiTietCaiDat : DevExpress.XtraEditors.XtraForm
    {
        public frmChiTietCaiDat()
        {
            InitializeComponent();
            LoadControl();
        }

        int IDMTselected = 0;
        bool them;
        int idquyen = CommonUser.Quyen;
        string MaCVUserLogon = CommonUser.UserStatic.CHUCVU;

        List<int> LsIDPMSua = new List<int>();
        List<int> LsIDPMCaiTrenMT = new List<int>();

        private void LoadControl()
        {
            LockControl(true);
            IDMTselected = 0;
            them = false;
            gcDsMayTinh.DataSource = QuanLyMayTinhDAO.Instance.GetListMaMT();
            gcDsPhanMem.DataSource = QLPhanMemDAO.Instance.GetTable();// Không có Key thì không hiện lên phần mềm ở trên này
            LsIDPMSua.Clear();
            txtGhiChu.Clear();
        }

        private void LockControl(bool kt)
        {
            if(kt)
            {
                dtpNgayCaiDat.Enabled = false;
                txtGhiChu.Enabled = false;
                gcDsMayTinh.Enabled = true;
                gcDsPhanMem.Enabled = true;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;

                
            }
            else
            {
                dtpNgayCaiDat.Enabled = true;
                txtGhiChu.Enabled = true;
                gcDsMayTinh.Enabled = true;
                gcDsPhanMem.Enabled = true;

                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;

                
            }
            
        }


        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }


        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }


        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            GridView view = sender as GridView;        
            int IDMaMT =int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["ID"]).ToString());
            bool CheckCDPM = DsCaiDatDAO.Instance.CheckCDPM(IDMaMT);
          
           
            if (CheckCDPM)          // Đã cài đặt phần mềm.
            {
                e.Appearance.BackColor = btnDaCaiPM.Appearance.BackColor;
            }
            else                    // Chưa cập nhật phần mềm.
            {
                e.Appearance.BackColor = btnChuaCaiPM.Appearance.BackColor;
            }


        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

     

        private void btnLichSuUpdatePM_Click(object sender, EventArgs e)
        {
            frmLichSuUpdatePM f = new frmLichSuUpdatePM();
            f.ShowDialog();
            LoadControl();
        }

      
        private void btnCaiDatPM_Click(object sender, EventArgs e)
        {
            //CHƯA CÓ THÔNG TIN THÌ THÊM, ĐÃ CÓ THÔNG TIN THÌ SỬA.
            if (idquyen >= 2)
            {
                #region  Thêm thông tin cài đặt phần mềm.

                int DemMT = 0;

                // LẤY ID cùa máy tính.
                List<int> ListIDMaMT = new List<int>();

                foreach (var item in gridView1.GetSelectedRows())
                {
                    int IDMaMT = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                    DemMT++;
                    ListIDMaMT.Add(IDMaMT);
                }

                if (DemMT <= 0||DemMT>1)
                {
                    MessageBox.Show("Chưa chọn máy tính Hoặc chọn quá 1 máy", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadControl();
                }
                else // Đảm bảo đã chọn một máy.
                {
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                         IDMTselected = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                    }
                    bool CheckPMCDtrenMay = DsCaiDatDAO.Instance.CheckCDPM(IDMTselected);
                    if (CheckPMCDtrenMay) // Đã có thông tin thì báo hãy chọn nút sửa.
                    {
                        MessageBox.Show("Máy đã có thông tin cài đặt, Hãy chọn nút sửa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadControl();
                    }
                    else // Chưa có thông tin thì thêm
                    {
                        LockControl(false);
                        them = true;
                    }
                }


                #endregion
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
           
        }

        private void gcDsMayTinh_Click(object sender, EventArgs e)
        {
            // Phải cho chắc năng sửa

            try
            {

               // IDselected = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
               


            }
            catch
            {


            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                // Chọn máy tính xong thì mới mở ds phần mềm ra đúng không?
                #region  Thêm thông tin cài đặt phần mềm.



                int DemMT = 0;

                // LẤY ID cùa máy tính.
                List<int> ListIDMaMT = new List<int>();


                foreach (var item in gridView1.GetSelectedRows())
                {
                    int IDMaMT = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                    DemMT++;
                    ListIDMaMT.Add(IDMaMT);
                }

                if (DemMT != 1)
                {
                    MessageBox.Show("Chưa chọn máy tính hoặc chọn quá 1 máy tính để sửa thông tin.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadControl();
                }
                else
                {

                    LockControl(false);
                    int idselected = 0;

                    // Nếu số lượng là 1 thì nó phải Rowcell style cho tôi.
                    foreach (int item in ListIDMaMT)
                    {
                        idselected = item;
                    }
                    List<DsCaiDatDTO> LsDSCDDTO = DsCaiDatDAO.Instance.GetLsPMcaiMT(idselected);
                    foreach (DsCaiDatDTO item in LsDSCDDTO)
                    {
                        LsIDPMSua.Add(item.IDPM);
                    }

                }


                #endregion
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                if (them)
                {

                    #region  Kiểm tra xem Mã phần mềm đã có trên mã máy tính hay chưa. 

                    string ngaycaidat = dtpNgayCaiDat.Value.ToString("dd/MM/yyyy");


                    int DemMT = 0;
                    // LẤY ID cùa máy tính.
                    List<int> ListIDMaMT = new List<int>();


                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        int IDMaMT = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                        DemMT++;
                        ListIDMaMT.Add(IDMaMT);
                    }


                    int DemPM = 0;


                    List<int> ListIDMaPM = new List<int>();
                    foreach (var item in gridView2.GetSelectedRows())
                    {
                        int IDMaPM = int.Parse(gridView2.GetRowCellValue(item, "ID").ToString());
                        DemPM++;
                        ListIDMaPM.Add(IDMaPM);
                    }

                    if (DemPM == 0)
                    {
                        MessageBox.Show("Chưa chọn phần mềm.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn lưu {DemPM} phần mềm cài đặt cho {DemMT} máy tính?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            int dem = 0;
                            foreach (int itemMT in ListIDMaMT)
                            {
                                foreach (int itemPM in ListIDMaPM)
                                {
                                    bool CheckCDPM = DsCaiDatDAO.Instance.CheckPMtrenMT(itemMT, itemPM);
                                    if (!CheckCDPM) // Chưa có thông tin cài đặt.
                                    {
                                        string Ghichu = txtGhiChu.Text.Trim();
                                        DsCaiDatDAO.Instance.Insert(itemMT, itemPM, ngaycaidat, ngaycaidat, Ghichu);
                                        dem++;
                                    }
                                }
                            }
                            MessageBox.Show($"Đã lưu {dem} thông tin cài đặt phần mềm.", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    DemMT = 0;
                    DemPM = 0;
                    LoadControl();
                    #endregion

                }
                else  // Sửa thông tin cài đặt máy.
                {
                    #region  Kiểm tra xem Mã phần mềm đã có trên mã máy tính hay chưa. 

                    string ngaycaidat = dtpNgayCaiDat.Value.ToString("dd/MM/yyyy");



                    // LẤY ID cùa máy tính.
                    List<int> ListIDMaMT = new List<int>();

                    int DemMT = 0;
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        int IDMaMT = int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                        DemMT++;
                        ListIDMaMT.Add(IDMaMT);
                    }


                    int DemPM = 0;


                    List<int> ListIDMaPM = new List<int>();
                    foreach (var item in gridView2.GetSelectedRows())
                    {
                        int IDMaPM = int.Parse(gridView2.GetRowCellValue(item, "ID").ToString());
                        DemPM++;
                        ListIDMaPM.Add(IDMaPM);
                    }

                    if (DemPM == 0)
                    {
                        MessageBox.Show("Chưa chọn phần mềm để cập nhật lại.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn sửa {DemPM} phần mềm cài đặt cho {DemMT} máy tính?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            // Xóa hết toàn bộ phần mềm cũ đang cài đặt.


                            // Thêm toàn bộ phần mềm mới vào.

                            int dem = 0;
                            foreach (int itemMT in ListIDMaMT)
                            {
                                DsCaiDatDAO.Instance.DeleteWithIDMT(itemMT);
                                foreach (int itemPM in ListIDMaPM)
                                {

                                    bool CheckCDPM = DsCaiDatDAO.Instance.CheckPMtrenMT(itemMT, itemPM);
                                    if (!CheckCDPM) // Chưa có thông tin cài đặt.
                                    {
                                        string Ghichu = txtGhiChu.Text.Trim();
                                        DsCaiDatDAO.Instance.Insert(itemMT, itemPM, ngaycaidat, ngaycaidat, Ghichu);
                                        dem++;
                                    }
                                }
                            }
                            MessageBox.Show($"Đã lưu {dem} thông tin cài đặt phần mềm.", "Thành công:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    DemMT = 0;
                    DemPM = 0;
                    LoadControl();
                    #endregion
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
           

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
                int idMayTinh = Convert.ToInt32(gridView1.GetRowCellValue(e.FocusedRowHandle, "ID"));
                LsIDPMCaiTrenMT = DsCaiDatDAO.Instance.GetLsPMcaiMT(idMayTinh)
                                    .Select(x => x.IDPM).ToList();

                gridView2.RefreshData();

                // Tích chọn dòng focus
                gridView1.SetRowCellValue(e.FocusedRowHandle, "IsChecked", true);
            

            // Lấy danh sách phần mềm đã cài theo ID
            LsIDPMCaiTrenMT.Clear(); // Xóa hết phần tử nếu còn phần tử 
          
            LsIDPMCaiTrenMT = new List<int>();
            List<DsCaiDatDTO> List1 = DsCaiDatDAO.Instance.GetLsPMcaiMT(idMayTinh);
            foreach (DsCaiDatDTO item in List1)
            {
                int IDMaPM = item.IDPM;
                LsIDPMCaiTrenMT.Add(IDMaPM);
            }
            // Refresh lại grid bên phải để áp dụng màu
            gridView2.RefreshData();

         
        }

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            // Lấy giá trị ID của dòng hiện tại
            int IDPM = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "ID"));

            // Nếu ID này nằm trong danh sách phần mềm đã cài
            if (LsIDPMCaiTrenMT.Contains(IDPM))
            {
                e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.ForeColor = Color.Black;

                // Tích chọn checkbox cho dòng này
                view.SetRowCellValue(e.RowHandle, "IsChecked", true);
            }
            else
            {
                view.SetRowCellValue(e.RowHandle, "IsChecked", false);
            }


        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
           
                GridView view = sender as GridView;

                // Kiểm tra nếu đây là dòng đang được chọn (FocusedRow)
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.Red; // màu nền tùy chọn
                    e.Appearance.ForeColor = Color.Black;        // màu chữ tùy chọn
                }
            

        }
    }
}