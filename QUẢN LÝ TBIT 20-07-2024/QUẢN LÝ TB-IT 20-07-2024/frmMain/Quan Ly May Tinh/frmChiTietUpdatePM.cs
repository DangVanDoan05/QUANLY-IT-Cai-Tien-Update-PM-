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


namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmChiTietUpdatePM : DevExpress.XtraEditors.XtraForm
    {
        public frmChiTietUpdatePM()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            throw new NotImplementedException();
        }

        private void btnLichSuUpdate_Click(object sender, EventArgs e)
        {
            frmLichSuUpdatePM f = new frmLichSuUpdatePM();
            f.ShowDialog();
            LoadControl();
        }
    }
}