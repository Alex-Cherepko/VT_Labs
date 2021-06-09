using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherepkoLib.Entities
{
    public class Rod
    {
        public int RodId { get; set; } 
        public string RodName { get; set; } 
        public string Description { get; set; } 
        public float Price { get; set; } 
        public string Image { get; set; } 
                                          
        public int RodGroupId { get; set; }
        public RodGroup Group { get; set; }
    }
}
