using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = new DateTime(2024, 1, 1),
                DateModified = new DateTime(2024, 1, 1)
            });

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
