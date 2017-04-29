using SpecflowMozart.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.DTO
{
    public class dtoCreateSaveSearch
    {

        public GridOptions gridOption { get; set; }

        public bool isSearchTag { get; set; }
        
        public string searchTagColor { get; set; }
    }
}
