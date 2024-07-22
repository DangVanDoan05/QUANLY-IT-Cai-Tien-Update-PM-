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
    public partial class frmChiTietUpdatePM : DevExpress.XtraEditors.XtraForm
    {

        public frmChiTietUpdatePM()
        {
            InitializeComponent();
            LoadControl();
        }

        string MaPMdangchon = "";

        private void LoadControl()
        {
            MaPMdangchon = "";
            LoadData();
        }

        private void LoadData()
        {
            // Load danh sách phần mềm.
            gcDsPM.DataSource = DanhSachPhanMemDAO.Instance.GetTable();

        }

        private void btnLichSuUpdate_Click(object sender, EventArgs e)
        {
            frmLichSuUpdatePM f = new frmLichSuUpdatePM();
            f.ShowDialog();
            LoadControl();
        }

        private void gcDsPM_Click(object sender, EventArgs e)
        {
            try
            {
                MaPMdangchon= gridView1.GetFocusedRowCellValue("MAPM").ToString();
                // Load bảng những máy tính cài phần mềm trên.
                gcDsMayTinh.DataSource = DsCaiDatDAO.Instance.GetLsMTcaiPM(MaPMdangchon);
            }
            catch
            {


            }
        }

        private void btnYeuCauUpdate_Click(object sender, EventArgs e)
        {
            #region  Thêm thông tin vào bảng "danh sách cài đặt" và bảng " Lịch sử Update".
            string TgYeucau = dtpTgYeuCau.Value.ToString("dd/MM/yyyy HH:mm:ss:tt");
            int DemMT = 0;
            List<string> ListMaMT = new List<string>();
            foreach (var item in gridView2.GetSelectedRows()) // Sai ở đây này
            {
                try
                {
                    string ma = gridView2.GetRowCellValue(item, "MAMT").ToString();
                    ListMaMT.Add(ma);
                    DemMT++;
                }
                catch
                { 
                   MessageBox.Show("Chưa hiện ra danh sách máy tính.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            int dem = ListMaMT.Count;
            if (DemMT == 0)
            {
                MessageBox.Show("Chưa chọn máy tính để cập nhật phần mềm.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {                             
                    DialogResult kq = MessageBox.Show($"Bạn muốn yêu cầu cập nhật phần mềm {MaPMdangchon} cho {dem} máy tính?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                       
                        DanhSachPhanMemDTO PhanMemDTO = DanhSachPhanMemDAO.Instance.GetMaPM(MaPMdangchon);
                        List<string> LsNhaMay = new List<string>();
                        List<string> LsPB = new List<string>();
                        foreach (string item in ListMaMT)
                        {
                            QuanLyMayTinhDTO MaMTdto = QuanLyMayTinhDAO.Instance.GetMaMT(item);
                            
                            // Lưu vào bảng chi tiết Update, lưu vào bảng quản lý Update.

                            DsCaiDatDAO.Instance.Insert(item, MaMTdto.PB, MaMTdto.NHAMAY, PhanMemDTO.MAPM, PhanMemDTO.TENPM, TgYeucau, "");
                            if(!LsNhaMay.Contains(MaMTdto.NHAMAY))
                            {
                                LsNhaMay.Add(MaMTdto.NHAMAY);
                            }
                            if(!LsPB.Contains(MaMTdto.PB))
                            {
                                LsPB.Add(MaMTdto.PB);
                            }                           
                        }
                        string NhaMay = "";
                        string PhongBan = "";
                        foreach (string  item in LsNhaMay)
                        {
                            NhaMay = NhaMay + " " + item + ", ";
                        }
                        foreach (string item1 in LsPB)
                        {
                            PhongBan = PhongBan + " " + item1 + ", ";
                        }

                        // Lưu vào lịch sử Update.

                        LsUpdatePMDAO.Instance.Insert(PhanMemDTO.MAPM, PhanMemDTO.TENPM, NhaMay, PhongBan, TgYeucau, "");
                        MessageBox.Show($"Đã thiết lập yêu cầu cập nhật phần mềm.", "Thành công:");


                    }
                
            }

           
          
            LoadControl();
            #endregion
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;          
            string MaPM = view.GetRowCellValue(e.RowHandle, view.Columns["MAPM"]).ToString();
            bool CheckChuaHTUpdate = LsUpdatePMDAO.Instance.CheckChuaHTUpdate(MaPM);
           
            if (CheckChuaHTUpdate) // Chưa hoàn tất Update.
            {
                e.Appearance.BackColor = btnDangCapNhat.Appearance.BackColor;
            }
          
        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;           
            string MaMT = view.GetRowCellValue(e.RowHandle, view.Columns["MAMT"]).ToString();
            bool CheckChuaHT = DsCaiDatDAO.Instance.CheckChuaHTCD(MaMT, MaPMdangchon);
            
            if (CheckChuaHT) 
            {
                e.Appearance.BackColor =btnDangCapNhat.Appearance.BackColor;
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

        private void btnHoanTatUpdate_Click(object sender, EventArgs e)
        {
            #region  Cập nhật thông tin vào bảng "danh sách cài đặt" và bảng " Lịch sử Update".

            string TgYeucau = dtpTgYeuCau.Value.ToString("dd/MM/yyyy HH:mm:ss:tt");
            int DemMT = 0;
            List<string> ListMaMT = new List<string>();
            foreach (var item in gridView2.GetSelectedRows())
            {
                try
                {
                    string ma = gridView2.GetRowCellValue(item, "MAMT").ToString();
                    ListMaMT.Add(ma);
                    DemMT++;
                }
                catch
                {
                    MessageBox.Show("Chưa hiện ra danh sách máy tính.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
         
            if (DemMT == 0)
            {
                MessageBox.Show("Chưa chọn máy tính để hoàn tất cập nhật.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                DialogResult kq = MessageBox.Show($"Bạn muốn hoàn tất cập nhật phần mềm {MaPMdangchon} cho {DemMT} máy tính?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(kq == DialogResult.Yes)
                {

                    DanhSachPhanMemDTO PhanMemDTO = DanhSachPhanMemDAO.Instance.GetMaPM(MaPMdangchon);
                   
                    foreach (string item in ListMaMT)
                    {
                        QuanLyMayTinhDTO MaMTdto = QuanLyMayTinhDAO.Instance.GetMaMT(item);

                        // Update vào bảng quản lý cài đặt.

                        DsCaiDatDAO.Instance.Insert(item, MaMTdto.PB, MaMTdto.NHAMAY, PhanMemDTO.MAPM, PhanMemDTO.TENPM, TgYeucau, "");
                      
                    }
                   

                    // Lưu vào lịch sử Update.

                    LsUpdatePMDAO.Instance.Insert(PhanMemDTO.MAPM, PhanMemDTO.TENPM, NhaMay, PhongBan, TgYeucau, "");
                    MessageBox.Show($"Đã thiết lập yêu cầu cập nhật phần mềm.", "Thành công:");


                }

            }



            LoadControl();
            #endregion
        }
    }
}