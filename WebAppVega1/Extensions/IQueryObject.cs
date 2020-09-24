using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Extensions
{
    public interface IQueryObject
    {
        bool IsSortAscending { get; set; }
        string SortBy { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}
