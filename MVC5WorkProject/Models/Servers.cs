using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MVC5WorkProject.Models
{
    public class Servers
    {
        [Key]
        public int? ServerId { get; set; }
        [Display(Name = "Status")]
        public Status EnumStatus { get; set; }
        [Required, Display(Name = "Type")]
        public Type EnumType { get; set; }

        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Ip1 { get; set; }
        public string Ip2 { get; set; }
        public string Ip3 { get; set; }

        public string Details { get; set; }

    }
    public enum Status
    {
        On,
        Off,
        Standby,
        Disabled
    }
    public enum Type
    {
        Server,
        Router,
        Nas,
        Switch,
        Vpn
    }


    public class ServersVM
    {
        public IEnumerable<Servers> Servers { get; set; }

        public ServersVM()
        {
            Servers = new List<Servers>();
        }
    }


    public class ServersContext : DbContext
    {
        public DbSet<Servers> ServerList { get; set; }

        //public System.Data.Entity.DbSet<MVC5WorkProject.Models.TwoColumns> TwoColumns { get; set; }
    }

    public class ServerInitializer : DropCreateDatabaseIfModelChanges<ServersContext>
    {
    }
}