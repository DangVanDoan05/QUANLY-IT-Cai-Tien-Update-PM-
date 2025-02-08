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

namespace frmMain
{
    public partial class frmLoaiTB : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiTB()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        int iddc = 0;

        private void LoadControl()
        {
            LoadData();
            LockControl(true);
            CleanText();
            iddc = 0;
        }


        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaTB.Enabled = false;
                txtTenTB.Enabled = false;              
                txtGhichu.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
               
            }
            else
            {
                txtMaTB.Enabled = true;
                txtTenTB.Enabled = true;               
                txtGhichu.Enabled = true;

                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
              
            }
        }

        private void LoadData()
        {
            gridControl1.DataSource = LoaiTBDAO.Instance.GetTable();
        }

       
        private void CleanText()
        {
            txtMaTB.Clear();
            txtTenTB.Clear();         
            txtGhichu.Clear();
        }
        void Save()
        {
            if (them)
            {
              
                    string matb = txtMaTB.Text.Trim();
                    string tentb = txtTenTB.Text.Trim();                 
                    string ghichu = txtGhichu.Text.Trim();

                    bool check = LoaiTBDAO.Instance.CheckLoaiTBExist(matb);
                    if (check)
                    {
                        MessageBox.Show(" Mã thiết bị đã tồn tại!", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã thiết bị {matb}", "Thông Báo:", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if (kq == DialogResult.Yes)
                        {
                            LoaiTBDAO.Instance.Insert(matb, tentb, ghichu);
                            MessageBox.Show($" Thêm mã công cụ {matb} thành công! ","Thành công:");
                        }
                        them = false;
                        LoadControl();
                    }
              
            }
            else
            {
                int ID = iddc;
                string matb = txtMaTB.Text.Trim();
                string tentb = txtTenTB.Text.Trim();
                string ghichu = txtGhichu.Text.Trim();

                LoaiTBDAO.Instance.Update(ID,matb, tentb, ghichu);
                MessageBox.Show($" Sửa thông tin mã công cụ {matb} thành công! ","Thành công:");                                   
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(iddc==0)
            {
                MessageBox.Show("Chưa chọn mã thiết bị để sửa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LockControl(false);
                txtMaTB.Enabled = false;
            }
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            // cho phép xóa nhiều dòng trong gridview

            int dem = 0;

            List<int> LsIDDcChon = new List<int>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                int MaMT =int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                LsIDDcChon.Add(MaMT);
                dem++;
            }


            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã thiết bị được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demxoa = 0;
                    foreach (int item in LsIDDcChon)
                    {
                        try
                        {
                            LoaiTBDAO.Instance.Delete(item);
                            demxoa++;
                        }
                        catch 
                        {
                            // Trường hợp bị dính khóa tham chiếu.
                        }                      
                    }

                    MessageBox.Show($"Đã xóa {demxoa} mã thiết bị được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã thiết bị để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }


        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

       
        private void gridControl1_Click(object sender, EventArgs e)
        {
            iddc = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
            txtMaTB.Text = gridView1.GetFocusedRowCellValue("MATB").ToString();
            txtTenTB.Text = gridView1.GetFocusedRowCellValue("TENTB").ToString();         
            txtGhichu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }
}