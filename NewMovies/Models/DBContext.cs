using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NewMovies.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PhimBo> PhimBos { get; set; }

    public virtual DbSet<PhimHot> PhimHots { get; set; }

    public virtual DbSet<PhimLe> PhimLes { get; set; }

    public virtual DbSet<QuanLyTaiKhoan> QuanLyTaiKhoans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhimBo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhimBo__3214EC2705E7AE88");

            entity.ToTable("PhimBo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Anh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QuocGia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenPhimEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenPhimEN");
            entity.Property(e => e.TenPhimTv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenPhimTV");
            entity.Property(e => e.TheLoai)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PhimHot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhimHot__3214EC27D3B8D68B");

            entity.ToTable("PhimHot");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Anh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LuotXem).HasDefaultValue(0);
            entity.Property(e => e.QuocGia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenPhimEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenPhimEN");
            entity.Property(e => e.TenPhimTv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenPhimTV");
            entity.Property(e => e.TheLoai)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PhimLe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhimLe__3214EC277DE371E6");

            entity.ToTable("PhimLe");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Anh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QuocGia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenPhimEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenPhimEN");
            entity.Property(e => e.TenPhimTv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenPhimTV");
            entity.Property(e => e.TheLoai)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ThoiLuong)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<QuanLyTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuanLyTa__3214EC27AA837095");

            entity.ToTable("QuanLyTaiKhoan");

            entity.HasIndex(e => e.Email, "UQ__QuanLyTa__A9D10534B5D71F70").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayDangKy).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TaiKhoan).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(50);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}