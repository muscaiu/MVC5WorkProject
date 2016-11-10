
using System.Data.Entity;

namespace MVC5WorkProject.Models
{
    public class IpMap
    {
        public string R1P1 { get; set; }
        public string R1P2 { get; set; }
    }

    //Connecting to the DataBase
    public class IpMapContext : DbContext
    {
        public DbSet<IpMap> IpMapList { get; set; }
    }
    public class IpMapInitializer : DropCreateDatabaseIfModelChanges<IpMapContext>
    {

    }


}