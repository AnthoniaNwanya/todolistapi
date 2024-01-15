using Microsoft.EntityFrameworkCore;

namespace todolistapi.Models
{
    public class MyListContext: DbContext
    {
        public MyListContext(DbContextOptions options): base(options) { }
        public DbSet<MyList> MyList { get; set; }

    }
}
