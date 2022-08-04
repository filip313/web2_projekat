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
    public class PorudzbinaCfg : IEntityTypeConfiguration<Porudzbina>
    {
        public void Configure(EntityTypeBuilder<Porudzbina> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Adresa).HasMaxLength(200);
            builder.Property(x => x.Komentar).HasMaxLength(500);
            builder.Property(x => x.Cena).HasPrecision(8, 2); 
            builder.Property(x => x.CenaDostave).HasPrecision(8, 2);
            builder.Property(x => x.TrajanjeDostave).HasConversion(typeof(long));
            builder.Property(x => x.Status).HasDefaultValue(StatusPorudzbine.CekaDostavu).HasConversion(typeof(string));

            builder.HasOne(x => x.Dostavljac).WithMany(x => x.Dostave).HasForeignKey(x => x.DostavljacId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Narucialc).WithMany(x => x.Porudzbine).HasForeignKey(x => x.NarucilacId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.NacinPlacanja).HasConversion(typeof(string));
        }
    }
}
