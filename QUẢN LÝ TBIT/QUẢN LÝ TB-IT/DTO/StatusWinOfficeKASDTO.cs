using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StatusWinOfficeKASDTO
    {
        public StatusWinOfficeKASDTO(string a)
        {
            this.STATUS = a;          
        }     
        private string sTATUS;

        public string STATUS { get => sTATUS; set => sTATUS = value; }
    }

}
