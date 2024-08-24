using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<SearchResult> SearchResults { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
