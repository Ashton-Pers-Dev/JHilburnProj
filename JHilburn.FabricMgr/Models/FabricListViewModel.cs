using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JHilburn.FabricMgr.Models
{
    public class FabricListViewModel
    {
        public List<FabricViewModel> Fabrics { get; set; }
        public List<string> Categories { get; set; }
        public Dictionary<string, List<FabricViewModel>> GroupedFabrics { get; set; }

        public FabricListViewModel()
        {

        }

        public FabricListViewModel(List<FabricViewModel> fabrics)
        {
            var _fabrics = fabrics;
            var _cats = _fabrics.Select(c => c.category).Distinct().ToList();
            var _dict = new Dictionary<string, List<FabricViewModel>>();

            foreach (var c in _cats)
            {
                var group = _fabrics.Where(f => f.category == c).ToList();
                _dict.Add(c, group);
            }

            Fabrics = _fabrics;
            Categories = _cats;
            GroupedFabrics = _dict;
        }
    }
}
