namespace Web_Tic_tac_toe.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TttModel : DbContext
    {
        public TttModel()
            : base("name=TttModel")
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.GameID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rooms1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.User_1);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rooms2)
                .WithOptional(e => e.User2)
                .HasForeignKey(e => e.User_2);
        }
    }
}
