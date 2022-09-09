using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlayListProject.Domain.Context;
using Microsoft.Extensions.DependencyInjection;

namespace PlayListProject.Test
{
    public class BaseTest
    {
        public readonly PlayListProjectDbContext _context;
        public readonly IMapper _mapper;
        /// <summary>
        /// This class is created to mock mapper and context
        /// Use a sqlite in memory to avoid use the 'real' database, is like a mock of PlayListProjectDbContext
        /// And create real _mapper to inject at the tests
        /// </summary>
        public BaseTest()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            
            serviceCollection.AddDbContext<PlayListProjectDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                //Sqlite dont support migration when is used in memory, then I use other db
                dbContextOptionsBuilder.UseSqlite("Data Source=playListXUnite.db");
            });
            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            _mapper = serviceCollection.BuildServiceProvider().GetRequiredService<IMapper>();
            _context = serviceCollection.BuildServiceProvider().GetRequiredService<PlayListProjectDbContext>();
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
        }

        /// <summary>
        /// Destructor to delete db
        /// </summary>
         ~BaseTest()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
