using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Models
{
    public class Feature
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
    }
}
