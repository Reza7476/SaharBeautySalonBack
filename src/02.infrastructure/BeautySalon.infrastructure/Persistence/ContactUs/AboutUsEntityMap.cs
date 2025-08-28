using BeautySalon.Entities.ContactUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.infrastructure.Persistence.ContactUs;
public class AboutUsEntityMap : IEntityTypeConfiguration<AboutUs>
{
    public void Configure(EntityTypeBuilder<AboutUs> _)
    {
        _.ToTable("AboutUs").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        _.Property(_ => _.MobileNumber).IsRequired();
        
        _.Property(_ => _.Telephone).IsRequired(false);
        
        _.Property(_ => _.Address).IsRequired(false);
        
        _.Property(_ => _.Latitude).IsRequired(false);
        
        _.Property(_ => _.Longitude).IsRequired(false);
        
        _.Property(_ => _.Description).IsRequired(false);
        
        _.Property(_ => _.CreateDate).IsRequired();
    }
}
