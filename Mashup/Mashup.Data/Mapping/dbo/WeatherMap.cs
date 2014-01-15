using Mashup.Data.Extensions;
using Mashup.Data.Model;

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.Data.Mapping.dbo
{
    class WeatherMap : EntityTypeConfiguration<Weather>
    {

        public WeatherMap()
        {
            this.HasKey(x => x.ID);
            this.ToTable("Weather");
            this.HasColumnName(x => x.ID).IsRequired();
            this.HasColumnName(x => x.ID_user).IsRequired();
            this.HasColumnName(x => x.City).HasMaxLength(256).IsRequired();
            this.HasColumnName(x => x.Quantity).IsRequired();



        }
    }
}
