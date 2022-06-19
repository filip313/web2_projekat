using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configs
{


    public class PorudzbinaProizvodCfg : IEntityTypeConfiguration<PorudzbinaProizvod>
    {
        public void Configure(EntityTypeBuilder<PorudzbinaProizvod> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasAlternateKey(x => new { x.PorudzbinaId, x.ProizvodId });
        }
    }
}
