using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Models
{
    public class VehicleFeature
    {
        [Key]
        [Column(Order =1)]
        public int VehicleId { get; set; }
        [Key]
        [Column(Order =2)]
        public int FeatureId { get; set; }
        //public Vehicle Vehicle { get; set; }
        //public Feature Feature { get; set; }
    }
}
