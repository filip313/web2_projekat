﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configs
{
    public class UserCfg : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).HasMaxLength(100).IsRequired(true);
            builder.HasIndex(x => x.Username).IsUnique(true);
            builder.Property(x => x.Email).HasMaxLength(320).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(512).IsRequired(true);
            builder.Property(x => x.Ime).HasMaxLength(100);
            builder.Property(x => x.Prezime).HasMaxLength(100);
            builder.Property(x => x.DatumRodjenja).HasConversion(typeof(string));
            builder.Property(x => x.Adresa).HasMaxLength(200);
            builder.Property(x => x.UserType).HasConversion(typeof(string));
            builder.Property(x => x.Slika).HasMaxLength(500);
            builder.Property(x => x.Verifikovan).HasDefaultValue(false);
        }
    }
}
