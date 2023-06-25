using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AviaSea.DateBase;

public partial class AviaSeaContext : DbContext
{
    public AviaSeaContext()
    {
    }

    public AviaSeaContext(DbContextOptions<AviaSeaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LikedAviaSea> LikedAviaSeas { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewPost> ViewPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-BK5MRL0G\\STP;Trusted_Connection=True;DataBase=AviaSea;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LikedAviaSea>(entity =>
        {
            entity.HasKey(e => e.IdLiked);

            entity.ToTable("LikedAviaSea");

            entity.Property(e => e.IdLiked).HasColumnName("id_liked");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LikedToken)
                .HasMaxLength(50)
                .HasColumnName("liked_token");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPosts);

            entity.Property(e => e.IdPosts).HasColumnName("id_posts");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.ImagePosts).HasColumnName("image_posts");
            entity.Property(e => e.Info)
                .HasMaxLength(150)
                .HasColumnName("info");
            entity.Property(e => e.KolvoSee).HasColumnName("kolvo_see");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Family)
                .HasMaxLength(50)
                .HasColumnName("family");
            entity.Property(e => e.Img).HasColumnName("img");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<ViewPost>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPosts");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
