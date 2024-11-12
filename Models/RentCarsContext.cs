using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyekRentCar.Models
{
    public partial class RentCarsContext : DbContext
    {
        public RentCarsContext()
        {
        }

        public RentCarsContext(DbContextOptions<RentCarsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MsCar> MsCars { get; set; } = null!;
        public virtual DbSet<MsCarImage> MsCarImages { get; set; } = null!;
        public virtual DbSet<MsCustomer> MsCustomers { get; set; } = null!;
        public virtual DbSet<MsEmployee> MsEmployees { get; set; } = null!;
        public virtual DbSet<TrMaintenance> TrMaintenances { get; set; } = null!;
        public virtual DbSet<TrPayment> TrPayments { get; set; } = null!;
        public virtual DbSet<TrRental> TrRentals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:RentCarDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MsCar>(entity =>
            {
                entity.HasKey(e => e.CarId)
                    .HasName("PK__MsCar__52395FD1DD1623D1");

                entity.ToTable("MsCar");

                entity.Property(e => e.CarId)
                    .HasMaxLength(36)
                    .HasColumnName("Car_id");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(50)
                    .HasColumnName("license_plate");

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .HasColumnName("model");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.NumberOfCarSeats).HasColumnName("number_of_car_seats");

                entity.Property(e => e.PricePerDay)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_per_day");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Transmission)
                    .HasMaxLength(100)
                    .HasColumnName("transmission");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<MsCarImage>(entity =>
            {
                entity.HasKey(e => e.ImageCarId)
                    .HasName("PK__MsCarIma__ADBC96273B1F0878");

                entity.Property(e => e.ImageCarId)
                    .HasMaxLength(36)
                    .HasColumnName("Image_car_id");

                entity.Property(e => e.CarId)
                    .HasMaxLength(36)
                    .HasColumnName("Car_id");

                entity.Property(e => e.ImageLink)
                    .HasMaxLength(2000)
                    .HasColumnName("image_link");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.MsCarImages)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__MsCarImag__Car_i__46E78A0C");
            });

            modelBuilder.Entity<MsCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__MsCustom__8CB382B1DA4CC8D5");

                entity.ToTable("MsCustomer");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(36)
                    .HasColumnName("Customer_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.DriverLicenseNumber)
                    .HasMaxLength(100)
                    .HasColumnName("driver_license_number");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");
            });

            modelBuilder.Entity<MsEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__MsEmploy__781228D974A19982");

                entity.ToTable("MsEmployee");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(36)
                    .HasColumnName("Employee_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(36)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Position)
                    .HasMaxLength(4000)
                    .HasColumnName("position");
            });

            modelBuilder.Entity<TrMaintenance>(entity =>
            {
                entity.HasKey(e => e.MaintenanceId)
                    .HasName("PK__TrMainte__CCA51FA78B21E7C8");

                entity.ToTable("TrMaintenance");

                entity.Property(e => e.MaintenanceId)
                    .HasMaxLength(36)
                    .HasColumnName("Maintenance_id");

                entity.Property(e => e.CarId)
                    .HasMaxLength(36)
                    .HasColumnName("car_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .HasColumnName("description");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(36)
                    .HasColumnName("employee_id");

                entity.Property(e => e.MaintenanceDate).HasColumnName("maintenance_date");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.TrMaintenances)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__TrMainten__car_i__4316F928");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TrMaintenances)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__TrMainten__emplo__440B1D61");
            });

            modelBuilder.Entity<TrPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__TrPaymen__DA638B1991CB7BAE");

                entity.ToTable("TrPayment");

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(36)
                    .HasColumnName("Payment_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(100)
                    .HasColumnName("payment_method");

                entity.Property(e => e.RentalId)
                    .HasMaxLength(36)
                    .HasColumnName("rental_id");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.TrPayments)
                    .HasForeignKey(d => d.RentalId)
                    .HasConstraintName("FK__TrPayment__renta__403A8C7D");
            });

            modelBuilder.Entity<TrRental>(entity =>
            {
                entity.HasKey(e => e.RentalId)
                    .HasName("PK__TrRental__9D20A03EBF374A49");

                entity.ToTable("TrRental");

                entity.Property(e => e.RentalId)
                    .HasMaxLength(36)
                    .HasColumnName("Rental_id");

                entity.Property(e => e.CarId)
                    .HasMaxLength(36)
                    .HasColumnName("car_id");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(36)
                    .HasColumnName("customer_id");

                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");

                entity.Property(e => e.RentalDate).HasColumnName("rental_date");

                entity.Property(e => e.ReturnDate).HasColumnName("return_date");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.TrRentals)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__TrRental__car_id__3D5E1FD2");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TrRentals)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__TrRental__custom__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
