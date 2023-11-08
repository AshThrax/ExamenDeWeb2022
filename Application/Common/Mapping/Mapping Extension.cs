using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Film;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Mapping
{
    public static class Mapping_Extension
    {
        public static Task<PagedList<TDestination>> PagedListAsync<TDestination>(this IQueryable<TDestination> queryable,int pageNumber,int pageSize) where TDestination :class
            => PagedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable query,IConfigurationProvider configuration) where TDestination : class 
            => query.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
    }
}
