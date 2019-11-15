using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaCalendar.Data.Media;

namespace MediaCalendar.Configurations
{
    class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(s => s.Key);
            builder.Property(s => s.id).IsRequired(true);
        }
    }
}
