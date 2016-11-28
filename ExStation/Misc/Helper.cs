using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExStation.Models;
using ExStation.Models.ViewModels;

namespace ExStation.Misc
{
    public static class Helper
    {
        public static SccompVM ToViewModel(this sccomp sccomp)
        {
            if (sccomp == null)
                return null;

            SccompVM vm = new SccompVM
            {
                sccomp = sccomp,
                id = sccomp.id,
                dbname = sccomp.dbname,
                compgroup = sccomp.sccompgroup.groupname,
                compname = sccomp.compname,
                credate = sccomp.credate
            };

            return vm;
        }

        public static List<SccompVM> ToViewModel(this IEnumerable<sccomp> list)
        {
            if (list == null)
                return null;

            List<SccompVM> vm = new List<SccompVM>();
            foreach (var item in list)
            {
                vm.Add(item.ToViewModel());
            }

            return vm;
        }
    }
}
