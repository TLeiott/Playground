using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Serientermine.Series;

namespace SerientermineErmitteln.Data.Database
{
    public sealed class TerminDbContext : DbContext
    {
        //public DbSet<Termin> Termine { get; set; }
        public DbSet<SerieBase> TerminSerien { get; set; }

        public TerminDbContext(DbContextOptions<TerminDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(s => Debug.WriteLine(s));

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SerieBase>()
                .HasDiscriminator<string>("SerieType")
                .HasValue<DailySerie>("1")
                .HasValue<WeeklySerie>("2")
                .HasValue<MonthlySerie>("3")
                .HasValue<YearlySerie>("4");
            builder.Entity<SerieBase>().ToTable("Series");
            builder.Entity<SerieBase>().HasKey(x => x.Id);
            builder.Entity<SerieBase>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<SerieBase>().Property(x => x.Name).IsRequired();
            builder.Entity<SerieBase>().Property(x => x.Intervall).IsRequired();
            builder.Entity<SerieBase>().Property(x => x.Begin).IsRequired();
            builder.Entity<SerieBase>().Property(x => x.End);
            builder.Entity<SerieBase>().Property(x => x.Limit).HasColumnName("RepeatLimit");
            builder.Entity<SerieBase>().Ignore(x => x.Type1);

            builder.Entity<DailySerie>().ToTable("Series");

            builder.Entity<WeeklySerie>().ToTable("Series");
            builder.Entity<WeeklySerie>().Property(x => x.WeekDay   ).IsRequired();
        }
    }
}