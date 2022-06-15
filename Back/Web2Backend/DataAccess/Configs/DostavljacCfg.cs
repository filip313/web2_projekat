using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configs
{
    public class DostavljacCfg : IEntityTypeConfiguration<Dostavljac>
    {
        public void Configure(EntityTypeBuilder<Dostavljac> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Verifikovan).IsRequired(false);
            builder.HasMany(x => x.Porudzbine).WithOne(x => x.Dostavljac).HasForeignKey(x => x.DostavljacId);
        }
    }
}
