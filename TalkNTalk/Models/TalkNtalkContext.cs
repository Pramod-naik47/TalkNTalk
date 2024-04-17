using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TalkNTalk.Models;

public partial class TalkNtalkContext : DbContext
{
    public TalkNtalkContext()
    {
    }

    public TalkNtalkContext(DbContextOptions<TalkNtalkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JH7IE1R\\SQLEXPRESS;Database=TalkNTalk;Encrypt=false;TrustServerCertificate=true;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__chats__FD040B1784008CA6");

            entity.ToTable("chats");

            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_userId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F9D1C282A");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
