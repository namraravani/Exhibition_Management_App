using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace exhibition_management_backend.Models;

public partial class ExhibitionManagementDbContext : DbContext
{
    public ExhibitionManagementDbContext()
    {
    }

    public ExhibitionManagementDbContext(DbContextOptions<ExhibitionManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Exhibition> Exhibitions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:WebApiDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Addressline1).HasColumnName("addressline1");
            entity.Property(e => e.Addressline2).HasColumnName("addressline2");
            entity.Property(e => e.Addressline3).HasColumnName("addressline3");
            entity.Property(e => e.Googlemapslink).HasColumnName("googlemapslink");
        });

        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exhibition_pkey");

            entity.ToTable("exhibition");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Addressid).HasColumnName("addressid");
            entity.Property(e => e.Bannerimage).HasColumnName("bannerimage");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Layoutimage).HasColumnName("layoutimage");
            entity.Property(e => e.Nooftables).HasColumnName("nooftables");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Venueimages).HasColumnName("venueimages");
            entity.Property(e => e.Venuename)
                .HasMaxLength(255)
                .HasColumnName("venuename");

            entity.HasOne(d => d.Address).WithMany(p => p.Exhibitions)
                .HasForeignKey(d => d.Addressid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exhibition_addressid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
