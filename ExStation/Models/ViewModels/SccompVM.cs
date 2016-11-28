using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExStation.Models.ViewModels
{
    public class SccompVM
    {
        public sccomp sccomp { get; set; }
        public int id { get; set; }
        public string dbname { get; set; }
        public string compname { get; set; }
        public DateTime credate { get; set; }
        public string compgroup { get; set; }

    }
}
