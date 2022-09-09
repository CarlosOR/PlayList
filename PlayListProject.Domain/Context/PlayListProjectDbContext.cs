using Microsoft.EntityFrameworkCore;
using PlayListProject.Domain.Entities;

namespace PlayListProject.Domain.Context
{
    public class PlayListProjectDbContext : DbContext
    {
        public PlayListProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors {get;set;}
        public DbSet<Song> Songs {get;set;}
        public DbSet<PlayList> PlayLists {get;set;}
        public DbSet<PlayListSong> PlayListSong { get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PlayListSong>().HasKey(sc => new { sc.SongId, sc.PlayListId });
        }
    }
}
