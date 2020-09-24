using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppVega1.Models;

namespace WebAppVega1.Controllers.Resources
{
    public class ModelResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MakeResource Make { get; set; }
        public int MakeID { get; set; }
    }
}
