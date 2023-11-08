using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film
{
    public class PagedList<T> : List<T>
    {
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public List<T> Items { get; set; } 

        public PagedList(List<T> items,int Count,int pageNumber,int pageSize)
        {
            TotalCount = Count;
            TotalPage = (int)Math.Ceiling(Count / (double)pageSize);
            PageNumber = pageNumber;
            Items = items;
        }

        public bool HasNext=> PageNumber>1;
        public bool HasPrev=> PageNumber < TotalPage;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pagesize)
        {
            var count = await source.CountAsync();
            var items =await source.Skip((pageNumber-1)*pagesize).Take(pagesize).ToListAsync();
            return new PagedList<T>(items,count,pageNumber,pagesize);
        }
    }
}
