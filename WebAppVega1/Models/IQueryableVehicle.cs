using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppVega1.Extensions;

namespace WebAppVega1.Models
{
    public class IQueryableVehicle : IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public bool IsSortAscending { get; set; }
        public string SortBy { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }

        public IQueryableVehicle()
        {
            MakeId = 0;
            ModelId = 0;
            IsSortAscending = true;
            SortBy = "";
            Page = 1;
            PageSize = 5;
        }
    }
}
