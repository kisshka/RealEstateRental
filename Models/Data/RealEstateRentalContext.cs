using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Real_Estate_Rental_Practic.Data;

namespace Real_Estate_Rental_Practic.Models.Data;

public partial class RealEstateRentalContext : Real_Estate_Rental_PracticIdentityContext
{


    public RealEstateRentalContext(DbContextOptions<Real_Estate_Rental_PracticIdentityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EstateAgency> EstateAgencies { get; set; }

    public virtual DbSet<EstateObject> EstateObjects { get; set; }

    public virtual DbSet<EstateRental> EstateRentals { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<OwnerEstateAgency> OwnerEstateAgencies { get; set; }

    public virtual DbSet<Realtor> Realtors { get; set; }

    // public virtual DbSet<RentInfo> RentInfos { get; set; }

    // public virtual DbSet<RentObjectsInfo> RentObjectsInfos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Определение первычного ключа для IdentityUserLogin
        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(l => l.UserId);
        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasAlternateKey(l => new { l.LoginProvider, l.ProviderKey });

        modelBuilder.Entity<EstateAgency>(entity =>
        {
            entity.HasKey(e => e.IdEstateAgency).HasName("PK_Estate_Agency");

            entity.ToTable("Estate_agency");

            entity.Property(e => e.IdEstateAgency).HasColumnName("ID_Estate_agency");
            entity.Property(e => e.AgencyName)
                .HasMaxLength(30)
                .HasColumnName("Agency_name");
            entity.Property(e => e.Comission).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.DirectorName)
                .HasMaxLength(30)
                .HasColumnName("Director_name");
            entity.Property(e => e.DirectorPatronymic)
                .HasMaxLength(30)
                .HasColumnName("Director_patronymic");
            entity.Property(e => e.DirectorSurname)
                .HasMaxLength(30)
                .HasColumnName("Director_surname");
            entity.Property(e => e.Home).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.Street).HasMaxLength(30);
            entity.Property(e => e.Town).HasMaxLength(30);
        });

        modelBuilder.Entity<EstateObject>(entity =>
        {
            entity.HasKey(e => e.IdEstateObject);

            entity.ToTable("Estate_object");

            entity.HasIndex(e => e.CittyArea, "IX_City_area");

            entity.HasIndex(e => e.NumberOfRooms, "IX_Number_of_rooms");

            entity.HasIndex(e => e.ObjectType, "IX_Object_type");

            entity.Property(e => e.IdEstateObject).HasColumnName("ID_Estate_object");
            entity.Property(e => e.AnimalsAllowed).HasColumnName("Animals_allowed");
            entity.Property(e => e.ChildrenAllowed).HasColumnName("Children_allowed");
            entity.Property(e => e.CittyArea)
                .HasMaxLength(30)
                .HasColumnName("Citty_area");
            entity.Property(e => e.Home).HasMaxLength(30);
            entity.Property(e => e.IdOwner).HasColumnName("ID_Owner");
            entity.Property(e => e.NumberOfRooms).HasColumnName("Number_of_rooms");
            entity.Property(e => e.ObjectType)
                .HasMaxLength(20)
                .HasColumnName("Object_type");
            entity.Property(e => e.RentalPeriod).HasColumnName("Rental_period");
            entity.Property(e => e.Square).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Street).HasMaxLength(30);
            entity.Property(e => e.Town).HasMaxLength(30);

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.EstateObjects)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK_Estate_object_Owner");
        });

        modelBuilder.Entity<EstateRental>(entity =>
        {
            entity.HasKey(e => e.IdEstateRental);

            entity.ToTable("Estate_rental");

            entity.Property(e => e.IdEstateRental).HasColumnName("ID_Estate_rental");
            entity.Property(e => e.CostPerMonth)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Cost_per_month");
            entity.Property(e => e.Deposit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IdEstateObject).HasColumnName("ID_Estate_object");
            entity.Property(e => e.IdRealtor).HasColumnName("ID_Realtor");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.RentBeginning).HasColumnName("Rent_beginning");
            entity.Property(e => e.RentEnding).HasColumnName("Rent_ending");

            entity.HasOne(d => d.IdEstateObjectNavigation).WithMany(p => p.EstateRentals)
                .HasForeignKey(d => d.IdEstateObject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estate_rental_Estate_object");

            entity.HasOne(d => d.IdRealtorNavigation).WithMany(p => p.EstateRentals)
                .HasForeignKey(d => d.IdRealtor)
                .HasConstraintName("FK_Estate_rental_Realtor");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.EstateRentals)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estate_rental_User");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.IdOwner);

            entity.ToTable("Owner");

            entity.Property(e => e.IdOwner).HasColumnName("ID_Owner");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Email_address");
            entity.Property(e => e.Home).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.Street).HasMaxLength(30);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.Town).HasMaxLength(30);
        });

        modelBuilder.Entity<OwnerEstateAgency>(entity =>
        {
            entity.HasKey(e => e.IdOwnerEstateAgency);

            entity.ToTable("Owner_Estate_agency");

            entity.Property(e => e.IdOwnerEstateAgency).HasColumnName("ID_Owner_Estate_agency");
            entity.Property(e => e.IdEstateAgency).HasColumnName("ID_Estate_agency");
            entity.Property(e => e.IdOwner).HasColumnName("ID_Owner");

            entity.HasOne(d => d.IdEstateAgencyNavigation).WithMany(p => p.OwnerEstateAgencies)
                .HasForeignKey(d => d.IdEstateAgency)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Owner_Estate_agency_Estate_Agency");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.OwnerEstateAgencies)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK_Owner_Estate_agency_Owner");
        });

        modelBuilder.Entity<Realtor>(entity =>
        {
            entity.HasKey(e => e.IdRealtor);

            entity.ToTable("Realtor");

            entity.Property(e => e.IdRealtor).HasColumnName("ID_Realtor");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Email_address");
            entity.Property(e => e.IdEstateAgency).HasColumnName("ID_Estate_agency");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.IdEstateAgencyNavigation).WithMany(p => p.Realtors)
                .HasForeignKey(d => d.IdEstateAgency)
                .HasConstraintName("FK_Realtor_Estate_Agency");
        });

        // modelBuilder.Entity<RentInfo>(entity =>
        // {
        //     entity
        //         .HasNoKey()
        //         .ToView("Rent_info");

        //     entity.Property(e => e.AgencyName)
        //         .HasMaxLength(30)
        //         .HasColumnName("Agency_name");
        //     entity.Property(e => e.CostPerMonth)
        //         .HasColumnType("decimal(10, 2)")
        //         .HasColumnName("Cost_per_month");
        //     entity.Property(e => e.ObjectType)
        //         .HasMaxLength(20)
        //         .HasColumnName("Object_type");
        //     entity.Property(e => e.OwnerSurname)
        //         .HasMaxLength(30)
        //         .HasColumnName("Owner_surname");
        //     entity.Property(e => e.RentBeginning).HasColumnName("Rent_beginning");
        //     entity.Property(e => e.RentEnding).HasColumnName("Rent_ending");
        //     entity.Property(e => e.UserSurname)
        //         .HasMaxLength(30)
        //         .HasColumnName("User_surname");
        // });

        // modelBuilder.Entity<RentObjectsInfo>(entity =>
        // {
        //     entity
        //         .HasNoKey()
        //         .ToView("Rent_objects_info");

        //     entity.Property(e => e.AnimalsAllowed).HasColumnName("Animals_allowed");
        //     entity.Property(e => e.ChildrenAllowed).HasColumnName("Children_allowed");
        //     entity.Property(e => e.CittyArea)
        //         .HasMaxLength(30)
        //         .HasColumnName("Citty_area");
        //     entity.Property(e => e.EmailAddress)
        //         .HasMaxLength(30)
        //         .IsUnicode(false)
        //         .HasColumnName("Email_address");
        //     entity.Property(e => e.Name).HasMaxLength(30);
        //     entity.Property(e => e.NumberOfRooms).HasColumnName("Number_of_rooms");
        //     entity.Property(e => e.ObjectType)
        //         .HasMaxLength(20)
        //         .HasColumnName("Object_type");
        //     entity.Property(e => e.PhoneNumber)
        //         .HasMaxLength(11)
        //         .IsUnicode(false)
        //         .IsFixedLength()
        //         .HasColumnName("Phone_number");
        //     entity.Property(e => e.RentalPeriod).HasColumnName("Rental_period");
        //     entity.Property(e => e.Square).HasColumnType("decimal(10, 2)");
        //     entity.Property(e => e.Surname).HasMaxLength(30);
        //     entity.Property(e => e.Town).HasMaxLength(30);
        // });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User", tb => tb.HasTrigger("User_delete"));

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Email_address");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.Surname).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
