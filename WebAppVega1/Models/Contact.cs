using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Models
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }

        [StringLength(255)]
        public string ContactPhone { get; set; }
    }
}
