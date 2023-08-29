using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HrApi.Models;

public partial class HrApiContext : DbContext
{
    public HrApiContext()
    {
    }

    public HrApiContext(DbContextOptions<HrApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<EmployeesAttendance> EmployeesAttendances { get; set; }

    public virtual DbSet<EmployeesCredential> EmployeesCredentials { get; set; }

    public virtual DbSet<EmployeesDetailsAdmin> EmployeesDetailsAdmins { get; set; }

    public virtual DbSet<EmployeesFamiliesJunction> EmployeesFamiliesJunctions { get; set; }

    public virtual DbSet<EmployeesLeavesJunction> EmployeesLeavesJunctions { get; set; }

    public virtual DbSet<FamiliesDetail> FamiliesDetails { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=HrApiDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDetails)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeDetails_EmployeeDetailsAdmin");
        });

        modelBuilder.Entity<EmployeesAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId);

            entity.ToTable("EmployeesAttendance");

            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesAttendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeesAttendance_EmployeesDetailsAdmin");
        });

        modelBuilder.Entity<EmployeesCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmployeeCredential");

            entity.ToTable("EmployeesCredential");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesCredentials)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeCredential_EmployeeDetailsAdmin");
        });

        modelBuilder.Entity<EmployeesDetailsAdmin>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_EmployeeDetailsAdmin");

            entity.ToTable("EmployeesDetailsAdmin");

            entity.Property(e => e.DateOfJoin).HasColumnType("datetime");

            entity.HasOne(d => d.ReportManager).WithMany(p => p.InverseReportManager)
                .HasForeignKey(d => d.ReportManagerId)
                .HasConstraintName("FK_EmployeeDetailsAdmin_EmployeeDetailsAdmin");
        });

        modelBuilder.Entity<EmployeesFamiliesJunction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmployeeFamilyJunction");

            entity.ToTable("EmployeesFamiliesJunction");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesFamiliesJunctions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeFamilyJunction_EmployeeDetailsAdmin");

            entity.HasOne(d => d.Family).WithMany(p => p.EmployeesFamiliesJunctions)
                .HasForeignKey(d => d.FamilyId)
                .HasConstraintName("FK_EmployeeFamilyJunction_FamilyDetails");
        });

        modelBuilder.Entity<EmployeesLeavesJunction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmployeeLeaveJunction");

            entity.ToTable("EmployeesLeavesJunction");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesLeavesJunctions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeLeaveJunction_EmployeeDetailsAdmin");

            entity.HasOne(d => d.Leave).WithMany(p => p.EmployeesLeavesJunctions)
                .HasForeignKey(d => d.LeaveId)
                .HasConstraintName("FK_EmployeeLeaveJunction_Leaves");
        });

        modelBuilder.Entity<FamiliesDetail>(entity =>
        {
            entity.HasKey(e => e.FamilyId).HasName("PK_FamilyDetails");
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.Property(e => e.HolidayDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(e => e.LeaveId);
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LeaveRequest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppileOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ApproveOn).HasColumnType("datetime");
            entity.Property(e => e.LeaveEnd).HasColumnType("datetime");
            entity.Property(e => e.LeaveStart).HasColumnType("datetime");
            entity.Property(e => e.Reason).IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_LeaveRequest_EmployeeDetailsAdmin");

            entity.HasOne(d => d.Leave).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveId)
                .HasConstraintName("FK_LeaveRequest_Leaves");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
