using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.Communication.Domain.Model.Entities;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Entities;
using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;

namespace SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public partial class SweetManagerContext : DbContext
    {
        public SweetManagerContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            // Enable Audit Fields Interceptors
            builder.AddCreatedUpdatedInterceptor();
        }
        
        public SweetManagerContext
            (DbContextOptions<SweetManagerContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("admins");

                entity.HasIndex(e => e.RolesId, "fk_admins_roles_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.RolesId).HasColumnName("roles_id");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role).WithMany(p => p.Admins)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_admins_roles_id");
            });

            modelBuilder.Entity<AdminCredential>(entity =>
            {
                entity.HasKey(e => e.AdminsId).HasName("PRIMARY");

                entity.ToTable("admins_credentials");

                entity.Property(e => e.AdminsId).HasColumnName("admins_id");
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.HasOne(d => d.Admin).WithOne(p => p.AdminCredential)
                    .HasForeignKey<AdminCredential>(d => d.AdminsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_admins_credentials_admins_id");
            });

            modelBuilder.Entity<AssignmentWorker>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("assignments_workers");

                entity.HasIndex(e => e.AdminsId, "fk_assignments_workers_admins_id");

                entity.HasIndex(e => e.WorkersAreasId, "fk_assignments_workers_workers_areas_id");

                entity.HasIndex(e => e.WorkersId, "fk_assignments_workers_workers_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AdminsId).HasColumnName("admins_id");
                entity.Property(e => e.FinalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("final_date");
                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.WorkersAreasId).HasColumnName("workers_areas_id");
                entity.Property(e => e.WorkersId).HasColumnName("workers_id");

                entity.HasOne(d => d.Admin).WithMany(p => p.AssignmentsWorkers)
                    .HasForeignKey(d => d.AdminsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assignments_workers_admins_id");

                entity.HasOne(d => d.WorkerArea).WithMany(p => p.AssignmentsWorkers)
                    .HasForeignKey(d => d.WorkersAreasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assignments_workers_workers_areas_id");

                entity.HasOne(d => d.Worker).WithMany(p => p.AssignmentsWorkers)
                    .HasForeignKey(d => d.WorkersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assignments_workers_workers_id");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("bookings");

                entity.HasIndex(e => e.PaymentsCustomersId, "fk_bookings_payments_customers_id");

                entity.HasIndex(e => e.RoomsId, "fk_bookings_rooms_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");
                entity.Property(e => e.FinalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("final_date");
                entity.Property(e => e.NightCount).HasColumnName("night_count");
                entity.Property(e => e.PaymentsCustomersId).HasColumnName("payments_customers_id");
                entity.Property(e => e.PriceRoom)
                    .HasPrecision(10)
                    .HasColumnName("price_room");
                entity.Property(e => e.RoomsId).HasColumnName("rooms_id");
                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");

                entity.HasOne(d => d.PaymentCustomer).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PaymentsCustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_bookings_payments_customers_id");

                entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_bookings_rooms_id");
            });

            modelBuilder.Entity<ContractOwner>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("contracts_owners");

                entity.HasIndex(e => e.OwnersId, "fk_contracts_owners_owners_id");

                entity.HasIndex(e => e.SubscriptionsId, "fk_contracts_owners_subscriptions_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FinalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("final_date");
                entity.Property(e => e.OwnersId).HasColumnName("owners_id");
                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.SubscriptionsId).HasColumnName("subscriptions_id");

                entity.HasOne(d => d.Owner).WithMany(p => p.ContractsOwners)
                    .HasForeignKey(d => d.OwnersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contracts_owners_owners_id");

                entity.HasOne(d => d.Subscription).WithMany(p => p.ContractsOwners)
                    .HasForeignKey(d => d.SubscriptionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contracts_owners_subscriptions_id");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("customers");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("hotels");

                entity.HasIndex(e => e.OwnersId, "fk_hotels_owners_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.OwnersId).HasColumnName("owners_id");
                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.HasOne(d => d.Owner).WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.OwnersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_hotels_owners_id");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("notifications");

                entity.HasIndex(e => e.AdminsId, "fk_notifications_admins_id");

                entity.HasIndex(e => e.OwnersId, "fk_notifications_owners_id");

                entity.HasIndex(e => e.TypesNotificationsId, "fk_notifications_types_notifications_id");

                entity.HasIndex(e => e.WorkersId, "fk_notifications_workers_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AdminsId).HasColumnName("admins_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
                entity.Property(e => e.OwnersId).HasColumnName("owners_id");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
                entity.Property(e => e.TypesNotificationsId).HasColumnName("types_notifications_id");
                entity.Property(e => e.WorkersId).HasColumnName("workers_id");

                entity.HasOne(d => d.Admin).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AdminsId)
                    .HasConstraintName("fk_notifications_admins_id");

                entity.HasOne(d => d.Owner).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.OwnersId)
                    .HasConstraintName("fk_notifications_owners_id");

                entity.HasOne(d => d.TypeNotification).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TypesNotificationsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notifications_types_notifications_id");

                entity.HasOne(d => d.Worker).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.WorkersId)
                    .HasConstraintName("fk_notifications_workers_id");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("owners");

                entity.HasIndex(e => e.RolesId, "fk_owners_roles_id");
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.RolesId).HasColumnName("roles_id");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
                
                entity.HasOne(d => d.Role).WithMany(p => p.Owners)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_owners_roles_id");
            });

            modelBuilder.Entity<OwnerCredential>(entity =>
            {
                entity.HasKey(e => e.OwnersId).HasName("PRIMARY");

                entity.ToTable("owners_credentials");

                entity.Property(e => e.OwnersId).HasColumnName("owners_id");
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.HasOne(d => d.Owner).WithOne(p => p.OwnerCredential)
                    .HasForeignKey<OwnerCredential>(d => d.OwnersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_owners_credentials_owners_id");
            });

            modelBuilder.Entity<PaymentCustomer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("payments_customers");

                entity.HasIndex(e => e.CustomersId, "fk_payments_customers_customers_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomersId).HasColumnName("customers_id");
                entity.Property(e => e.FinalAmount)
                    .HasPrecision(10)
                    .HasColumnName("final_amount");

                entity.HasOne(d => d.Customer).WithMany(p => p.PaymentsCustomers)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payments_customers_customers_id");
            });

            modelBuilder.Entity<PaymentOwner>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("payments_owners");

                entity.HasIndex(e => e.OwnersId, "fk_payments_owners_owners_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
                entity.Property(e => e.FinalAmount)
                    .HasPrecision(10)
                    .HasColumnName("final_amount");
                entity.Property(e => e.OwnersId).HasColumnName("owners_id");

                entity.HasOne(d => d.Owner).WithMany(p => p.PaymentsOwners)
                    .HasForeignKey(d => d.OwnersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payments_owners_owners_id");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("providers");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("reports");

                entity.HasIndex(e => e.AdminsId, "fk_reports_admins_id");

                entity.HasIndex(e => e.TypesReportsId, "fk_reports_types_reports_id");

                entity.HasIndex(e => e.WorkersId, "fk_reports_workers_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AdminsId).HasColumnName("admins_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
                entity.Property(e => e.FileUrl)
                    .HasMaxLength(6000)
                    .HasColumnName("file_url");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
                entity.Property(e => e.TypesReportsId).HasColumnName("types_reports_id");
                entity.Property(e => e.WorkersId).HasColumnName("workers_id");

                entity.HasOne(d => d.Admin).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.AdminsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reports_admins_id");

                entity.HasOne(d => d.TypeReport).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.TypesReportsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reports_types_reports_id");

                entity.HasOne(d => d.Worker).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.WorkersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reports_workers_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("rooms");

                entity.HasIndex(e => e.HotelsId, "fk_rooms_hotels_id");

                entity.HasIndex(e => e.TypesRoomsId, "fk_rooms_types_rooms_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.HotelsId).HasColumnName("hotels_id");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.TypesRoomsId).HasColumnName("types_rooms_id");

                entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rooms_hotels_id");

                entity.HasOne(d => d.TypeRoom).WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.TypesRoomsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rooms_types_rooms_id");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("subscriptions");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<SuppliesRequest>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("supplies_requests");

                entity.HasIndex(e => e.PaymentsOwnersId, "fk_supplies_requests_payments_owners_id");

                entity.HasIndex(e => e.SuppliesId, "fk_supplies_requests_supplies_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");
                entity.Property(e => e.Count).HasColumnName("count");
                entity.Property(e => e.PaymentsOwnersId).HasColumnName("payments_owners_id");
                entity.Property(e => e.SuppliesId).HasColumnName("supplies_id");

                entity.HasOne(d => d.PaymentOwner).WithMany(p => p.SuppliesRequests)
                    .HasForeignKey(d => d.PaymentsOwnersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supplies_requests_payments_owners_id");

                entity.HasOne(d => d.Supply).WithMany(p => p.SuppliesRequests)
                    .HasForeignKey(d => d.SuppliesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supplies_requests_supplies_id");
            });

            modelBuilder.Entity<Supply>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("supplies");

                entity.HasIndex(e => e.ProvidersId, "fk_supplies_providers_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");
                entity.Property(e => e.ProvidersId).HasColumnName("providers_id");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.Provider).WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.ProvidersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supplies_providers_id");
            });

            modelBuilder.Entity<TypeNotification>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("types_notifications");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TypeReport>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("types_reports");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TypeRoom>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("types_rooms");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("workers");

                entity.HasIndex(e => e.RolesId, "fk_workers_roles_id");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.RolesId).HasColumnName("roles_id");
                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role).WithMany(p => p.Workers)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_workers_roles_id");
            });

            modelBuilder.Entity<WorkerArea>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("workers_areas");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<WorkerCredential>(entity =>
            {
                entity.HasKey(e => e.WorkersId).HasName("PRIMARY");

                entity.ToTable("workers_credentials");

                entity.Property(e => e.WorkersId).HasColumnName("workers_id");
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.HasOne(d => d.Worker).WithOne(p => p.WorkerCredential)
                    .HasForeignKey<WorkerCredential>(d => d.WorkersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_workers_credentials_workers_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}