namespace AdveChan.Entities
{
    using System.Data.Entity;
    using System.Diagnostics;

    public class ChanContext:DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; } 
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public ChanContext()
        {
            //Database.Log = (x) => Trace.WriteLine(x);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().ToTable("Users");

            modelBuilder.Entity<Board>()
                .HasMany(x => x.Threads)
                .WithRequired(x => x.Board)
                .HasForeignKey(x => x.BoardId);

            modelBuilder.Entity<Thread>()
               .HasMany(x => x.Posts)
               .WithRequired(x => x.Thread)
               .HasForeignKey(x => x.ThreadId);
        }
    }
}