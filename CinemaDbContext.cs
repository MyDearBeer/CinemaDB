using System;
using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration.Conventions;

using Microsoft.EntityFrameworkCore;
using CinemaDB.Models;


namespace CinemaDB
{

    public partial class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
        {
            //Database.EnsureDeleted();
          // Database.EnsureCreated();
        }

        //public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        //    : base(options)
        //{
        //   // Database.EnsureCreated();
        //}

        public virtual DbSet<Cinema> Cinemas { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Film> Films { get; set; }

        public virtual DbSet<Hall> Halls { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<Seance> Seances { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<Worker> Workers { get; set; }

        public virtual DbSet<MiniCinema> MiniCinemas { get; set; }
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
           : base(options)
        {
             Database.EnsureCreated();
        }


        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //            => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CinemaDB;Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_CI_AS");

            

            Cinema cinema1 = new Cinema { Id = 1, CName = "Байрактар", CAddress = "вул. Ринкова", Halls = 6, WorkersCount = 31, Clients = 666, AdminPhone = "(096) 352-69-23" };
            Cinema cinema2 = new Cinema { Id = 2, CName = "Бескінечне літо", CAddress = "вул. Лагерна, 4/1", Halls = 4, WorkersCount = 25, Clients = 410, AdminPhone = "(067) 783-11-22" };
            //Position boss= new Position ("Керуючий кінотетру" );
            // Position sysAdmin = new Position("Системний адміністратор") ;
            // Position barman = new Position("Бармен");
            Position boss = new Position { Id = 1, PName = "Керуючий кінотетру" };
            Position sysAdmin = new Position { Id = 2, PName = "Системний адміністратор" };
            Position barman = new Position { Id = 3, PName = "Бармен" };
            //Worker worker1 = new Worker(cinema2.Id, "Семен", "Персунов", 77777, sysAdmin.Id, "(099) 111-11-11", 30000);
             Worker worker1 = new Worker { Id = 1, CinemaId = cinema2.Id, WName = "Семен", WSurname = "Персунов", Passport = 77777, PositionId = sysAdmin.Id, Phone = "(099) 111-11-11", Salary = 30000 };
            Worker worker2 = new Worker { Id =2, CinemaId = cinema2.Id, WName = "Ольга", WSurname = "Дімітрео", Passport = 55555, PositionId = boss.Id, Phone = "(060) 345-97-34", Salary = 100000 };
            Worker worker3 = new Worker { Id = 3, CinemaId = cinema2.Id, WName = "Аліса", WSurname = "Двачевська", Passport = 66666, PositionId = barman.Id, Phone = "(099) 111-22-33", Salary = 28000 };
            //Worker worker2 = new Worker(cinema2.Id, "Ольга", "Дімітрео", 55555, boss.Id, "(060) 345-97-34", 100000);
            //Worker worker3 = new Worker(cinema2.Id, "Аліса", "Двачевська", 66666, barman.Id, "(099) 111-22-33", 28000);

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasKey(e => e.Id );

                entity.ToTable("Cinema");

                entity.HasIndex(e => e.CName).IsUnique();

                entity.HasIndex(e => e.AdminPhone).IsUnique();

                entity.HasIndex(e => e.CAddress).IsUnique();

               

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AdminPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.CAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("C_Address");
                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("C_Name");
              //    entity.ToTable(t=>t.HasCheckConstraint("WorkersCount", "WorkersCount>0"));


                entity.HasData(cinema1, cinema2
                    //        new Cinema { Id = 1, CName = "Байрактар", CAddress = "вул. Ринкова", Halls = 6, Workers = 31, Clients = 666, AdminPhone = "(096) 352-69-23" },
                    //new Cinema { Id = 2, CName = "Бескінечне літо", CAddress = "вул. Лагерна, 4/1", Halls = 4, Workers = 25, Clients = 410, AdminPhone = "(067) 783-11-22" }

                    );

            });

            modelBuilder.Entity<MiniCinema>().ToTable("Minicinemas");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC27FF254FBD");

                entity.HasIndex(e => e.Phone, "UQ__Clients__5C7E359EC9B3A9C9").IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NONAME')")
                    .HasColumnName("C_Name");
                entity.Property(e => e.CSurname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NONAME')")
                    .HasColumnName("C_Surname");
                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Films__3214EC274FDFF09F");

                entity.HasIndex(e => e.FName, "UQ__Films__423F2C5B1820F14D").IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AgeRate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NOT FOUND')");
                entity.Property(e => e.Company)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NOT FOUND')");
                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("F_Name");
                entity.Property(e => e.FTime)
                    .HasDefaultValueSql("('00:00')")
                    .HasColumnName("F_Time");
                entity.Property(e => e.Genre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NOT FOUND')");
                entity.Property(e => e.Stars)
                    .HasDefaultValueSql("((0.0))")
                    .HasColumnType("decimal(4, 2)");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(e => e.Number).HasName("PK__Halls__78A1A19C29B53E9D");

                entity.Property(e => e.Number).ValueGeneratedNever();
                entity.Property(e => e.HColumns).HasColumnName("H_Columns");
                entity.Property(e => e.HSeats).HasColumnName("H_Seats");
                entity.Property(e => e.SeatsType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Default')");
                entity.Property(e => e.Technology)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Absent')");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC27C4EE9770");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ClientId).HasColumnName("Client_ID");
                entity.Property(e => e.ODate)
                    .HasColumnType("datetime")
                    .HasColumnName("O_Date");
                entity.Property(e => e.WorkerId).HasColumnName("Worker_ID");

                entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Orders__Client_I__4E88ABD4");

                entity.HasOne(d => d.Worker).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Orders__Worker_I__4F7CD00D");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Position__3214EC274BF3B25B");

                entity.HasIndex(e => e.PName, "UQ__Position__9634B4F2C64A5220").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.PName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("P_Name");
                entity.HasData(sysAdmin, boss, barman);
            });

            modelBuilder.Entity<Seance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Seances__3214EC27D3270BA1");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FilmId).HasColumnName("Film_ID");
                entity.Property(e => e.HallNum).HasColumnName("Hall_Num");
                entity.Property(e => e.SDate)
                    .HasColumnType("datetime")
                    .HasColumnName("S_Date");
                entity.Property(e => e.TypeOf3D)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Film).WithMany(p => p.Seances)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Seances__Film_ID__60A75C0F");

                entity.HasOne(d => d.HallNumNavigation).WithMany(p => p.Seances)
                    .HasForeignKey(d => d.HallNum)
                    .HasConstraintName("FK__Seances__Hall_Nu__619B8048");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC2788C79FB0");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Booking)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('No booking')");
                entity.Property(e => e.DateOfPay).HasColumnType("datetime");
                entity.Property(e => e.OrderId).HasColumnName("Order_ID");
                entity.Property(e => e.SeanceId).HasColumnName("Seance_ID");

                entity.HasOne(d => d.Order).WithMany(p => p.TicketsNavigation)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Tickets__Order_I__6477ECF3");

                entity.HasOne(d => d.Seance).WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SeanceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Tickets__Seance___656C112C");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Workers__3214EC2785CA80B0");

                entity.HasIndex(e => e.Passport, "UQ__Workers__208C1D4DC02DEDCB").IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__Workers__5C7E359E2FA70C8B").IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CinemaId).HasColumnName("Cinema_ID");
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.PositionId).HasColumnName("Position_ID");
                entity.Property(e => e.WName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NONAME')")
                    .HasColumnName("W_Name");
                entity.Property(e => e.WSurname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NONAME')")
                    .HasColumnName("W_Surname");

                entity.HasOne(d => d.Cinema).WithMany(p => p.WorkersNavigation)
                    .HasForeignKey(d => d.CinemaId)
                    .HasConstraintName("FK__Workers__Cinema___4316F928");

                entity.HasOne(d => d.Position).WithMany(p => p.Workers)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__Workers__Positio__45F365D3");
                entity.HasData(worker1, worker2, worker3);
            });

      
            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

