
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MVC5WorkProject.Models
{
    public class DbTestModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class DbTestContext : DbContext
    {
        public DbSet<DbTestModel> DbTestModels { get; set; }
    }

    public class DbTestInitializer : DropCreateDatabaseIfModelChanges<DbTestContext>
    {

    }
}