using System;
using AppConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CountriesLibrary.Models
{
    public partial class CountriesDBContext : DbContext
    {
        private IConfiguration Configuration { get; }
        public CountriesDBContext()
        {
            Configuration = AppConfigurationProvider.BuildConfigurtions();
        }

        public CountriesDBContext(DbContextOptions<CountriesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCountries> TblCountries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("CountryDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCountries>(entity =>
            {
                entity.HasKey(e => e.Countryid)
                    .HasName("CountryId");

                entity.ToTable("tbl_countries");

                entity.HasIndex(e => e.Countryname)
                    .HasName("tbl_countries_countryname_key")
                    .IsUnique();

                entity.Property(e => e.Countryid)
                    .HasColumnName("countryid")
                    .HasDefaultValueSql("nextval('country_id_seq'::regclass)");

                entity.Property(e => e.Countryname)
                    .HasColumnName("countryname")
                    .HasMaxLength(50);

                entity.Property(e => e.Threecharcountrycode)
                    .HasColumnName("threecharcountrycode")
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Twocharcountrycode)
                    .HasColumnName("twocharcountrycode")
                    .HasMaxLength(2)
                    .IsFixedLength();
            });

            modelBuilder.HasSequence("country_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
