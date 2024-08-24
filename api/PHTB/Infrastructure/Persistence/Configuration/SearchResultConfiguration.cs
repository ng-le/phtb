using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class SearchResultConfiguration : IEntityTypeConfiguration<SearchResult>
    {
        public void Configure(EntityTypeBuilder<SearchResult> builder)
        {
            builder.Property(e => e.Keywords).HasMaxLength(512).IsRequired();
            builder.Property(e => e.Result).HasMaxLength(512).IsRequired();
        }
    }
}
