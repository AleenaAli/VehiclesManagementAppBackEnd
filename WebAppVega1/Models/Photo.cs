using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public int vehicleId { get; set; }
    }
}
