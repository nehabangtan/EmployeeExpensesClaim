using System;
using System.Collections.Generic;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeExpensesClaim.DataAccessLayer.DbContexts;

public partial class ExpenseDbContext : DbContext
{
    public ExpenseDbContext()
    {
    }

    public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ExpenseClaim> ExpenseClaims { get; set; }

    public virtual DbSet<FilesTable> FilesTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExpensesClaimDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditLog__3214EC0760271FC7");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Claim).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuditLogs__Claim__45F365D3");

            entity.HasOne(d => d.Emp).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuditLogs__EmpId__44FF419A");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__3214EC077E8D6A35");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534503EF51C").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpRole)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExpenseClaim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__ExpenseC__3214EC07989857C3");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpenseDate).HasColumnType("datetime");
            entity.Property(e => e.ExpenseDescription).HasColumnType("text");
            entity.Property(e => e.ExpenseStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ExpenseType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Emp).WithMany(p => p.ExpenseClaims)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExpenseCl__EmpId__3C69FB99");
        });

        modelBuilder.Entity<FilesTable>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__Files__3214EC079BDB4F16");

            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UploadedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Claim).WithMany(p => p.FilesTables)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Files__ClaimId__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
