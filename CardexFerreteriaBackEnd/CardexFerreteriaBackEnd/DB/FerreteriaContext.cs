using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardexFerreteriaBackEnd.Models;

namespace CardexFerreteriaBackEnd.DB
{
    public class FerreteriaContext:DbContext
    {
        public DbSet<Producto> productos { get; set; }

        public FerreteriaContext(DbContextOptions<FerreteriaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.nombre).IsRequired();                
            });

        }


    }
}
