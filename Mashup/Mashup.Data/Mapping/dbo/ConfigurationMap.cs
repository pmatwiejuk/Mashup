using Mashup.Data.Extensions;
using Mashup.Data.Model.dbo;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.Data.Mapping.dbo
{

    internal class ConfigurationMap : EntityTypeConfiguration<Configuration>
    {
        public ConfigurationMap()
        {
            this.HasKey(x => x.Key);
            this.ToTable("Configuration");
            this.HasColumnName(x => x.Key).HasMaxLength(50).IsRequired();
            this.HasColumnName(x => x.Value).HasMaxLength(256).IsRequired();

        }
    }
}
