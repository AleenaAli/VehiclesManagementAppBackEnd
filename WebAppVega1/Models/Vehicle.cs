using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Models
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdated { get; set; }
        public Contact Contact { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public Vehicle()
        {
            LastUpdated = System.DateTime.Now;
            Photos = new Collection<Photo>();
            Features = new Collection< VehicleFeature > ();
        }
    }
}
