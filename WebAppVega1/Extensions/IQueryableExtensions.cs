using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAppVega1.Models;

namespace WebAppVega1.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Vehicle> ApplyFiltering(this IQueryable<Vehicle> query, IQueryableVehicle queryObj)
        {
            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeID == queryObj.MakeId);
            }

            if (queryObj.ModelId.HasValue)
            {
                query = query.Where(v => v.ModelId == queryObj.ModelId);
            }

            return query;
        }
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, Object>>> columnsMap)
        {
            //List<string>columns = new List<string> { "id", "make", "model", "contactName" };

            //queryObj.SortBy = ;

            //if (!queryObj.SortBy.DefaultIfEmpty(columns))
            //{
            //    queryObj.SortBy = "id";
            //}
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
            {
                return query;
            }

            if (queryObj.IsSortAscending)
                return query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query = query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this  IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.PageSize <= 0)
            {
                queryObj.PageSize = 5;
            }
            if (queryObj.Page <= 0)
            {
                queryObj.Page = 1;
            }
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);

        }

    }
} 