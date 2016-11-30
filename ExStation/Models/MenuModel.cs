using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExStation.Models
{
    public class XMenu
    {
        public string menuID { get; set; }
        public string nameTh { get; set; }
        public string nameEn { get; set; }
        public string parentMenu { get; set; }
        public List<XMenu> childMenu { get; set; }

    }
}
