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

        int IDMTsuaTT = 0;
        bool them;

        List<int> LsIDPMSua = new List<int>();

        private void LoadControl()
        {
            LockControl(true);
            IDMTsuaTT = 0;
            them = false;
            gcDsMayTinh.DataSource = QuanLyMayTinhDAO.Instance.GetListMaMT();
            gcDsPhanMem.DataSource = QLPhanMemDAO.Instance.GetListPMDTOKoGH();// Không có Key thì không hiện lên phần mềm ở trên này
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
                gcDsPhanMem.Enabled = false;

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
            // Đang ở nút thêm phần mềm.
            #region  Thêm thông tin cài đặt phần mềm.

          

            int DemMT = 0;

            // LẤY ID cùa máy tính.
            List<int> ListIDMaMT = new List<int>();


            foreach (var item in gridView1.GetSelectedRows())
            {
                int IDMaMT =int.Parse( gridView1.GetRowCellValue(item, "ID").ToString());             
                DemMT++;
                ListIDMaMT.Add(IDMaMT);
            }

            if (DemMT == 0)
            {
                MessageBox.Show("Chưa chọn máy tính.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadControl();
            }
            else
            {
                LockControl(false);
                them = true;
            }
            
            
            #endregion
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(them)
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

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (LsIDPMSua.Count > 0)
            {
                // đặt biến vào để khóa sự kiện.
                GridView view = sender as GridView;              
                int IDPM = int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["ID"]).ToString());                                       
               

                if (LsIDPMSua.Contains(IDPM))
                {
                    e.Appearance.BackColor = btnPMDC.Appearance.BackColor;
                }
                else
                {
                  

                }
            }





        }
    }
}