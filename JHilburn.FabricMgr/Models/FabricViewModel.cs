using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JHilburn.FabricMgr.Models
{
    public class FabricViewModel
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public string category { get; set; }
        public string imgurl { get; set; }
        public int inventory { get; set; }
    }
}
