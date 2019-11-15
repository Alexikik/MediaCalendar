using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaCalendar.Data.Media;

namespace MediaCalendar.Configurations
{
    class SeriesConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            builder.HasKey(s => s.key);
            builder.Property(s => s.id).IsRequired(true);
            builder.Property(s => s.seriesName).IsRequired(true);
        }
    }
}
