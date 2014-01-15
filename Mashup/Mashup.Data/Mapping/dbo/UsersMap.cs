using System.Data.Entity.ModelConfiguration;

using Mashup.Data.Model.dbo;

namespace Mashup.Data.Mapping.dbo
{
    using Mashup.Data.Extensions;

    class UsersMap : EntityTypeConfiguration<Users>
    {
        public UsersMap()
        {
            this.HasKey(x => x.ID);
            this.ToTable("Users");
            this.HasColumnName(x => x.ID).IsRequired();
            this.HasColumnName(x => x.Email).HasMaxLength(128).IsRequired();
            this.HasColumnName(x => x.Password).HasMaxLength(256).IsRequired();
            this.HasColumnName(x => x.Name).HasMaxLength(50).IsRequired();
            this.HasColumnName(x => x.Surname).HasMaxLength(50).IsRequired();

            this.HasMany(x => x.Weather).WithRequired().HasForeignKey(x => x.ID_user);
            this.HasMany(x => x.RSS).WithRequired().HasForeignKey(x => x.ID_user);

        }
    }
}
