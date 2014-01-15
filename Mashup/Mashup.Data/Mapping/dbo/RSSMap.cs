using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Mashup.Data.Extensions;
using Mashup.Data.Model.dbo;
using System.Data.Entity.ModelConfiguration;

namespace Mashup.Data.Mapping.dbo
{
    internal class RssMap : EntityTypeConfiguration<RSS>
    {
        public RssMap()
        {
            this.HasKey(x => x.ID);
            this.ToTable("RSS");
            this.HasColumnName(x => x.ID).IsRequired();
            this.HasColumnName(x => x.ID_user).IsRequired();
            this.HasColumnName(x => x.RSS_URL).HasMaxLength(256).IsRequired();

        }
    }
}
