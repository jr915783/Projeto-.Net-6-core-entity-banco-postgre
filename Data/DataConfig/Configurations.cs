using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataConfig
{
    public class Configurations : IEntityTypeConfiguration<EntityCadastrosVeiculo>
    {
        public void Configure(EntityTypeBuilder<EntityCadastrosVeiculo> builder)
        {
            builder.ToTable("CadastrosVeiculos");
            builder.HasKey(k => k.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
