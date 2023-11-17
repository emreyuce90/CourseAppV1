using CourseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Context.Ef_Core.Mappings {
    public class CourseMap : IEntityTypeConfiguration<Course> {
        public void Configure(EntityTypeBuilder<Course> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        }
    }
}
