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
    public class PotrosacCfg : IEntityTypeConfiguration<Potrosac>
    {
        public void Configure(EntityTypeBuilder<Potrosac> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Porudzbine).WithOne(x => x.Narucilac).HasForeignKey(x => x.PotrosacId);
        }
    }
}
