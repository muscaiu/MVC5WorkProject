
namespace MVC5WorkProject.Models
{
    public class ServersDbContextSeeder : ServerInitializer
    {
        protected override void Seed(ServersContext context)
        {
            var server1 = new Servers()
            {
                EnumStatus = Status.On,
                EnumType = Type.Server,
                Name = "Newzor",
                Password = "xxx",
                Ip1 = "1.1.1.1",
                Ip2 = "2.2.2.2",
                Ip3 = "3.3.3.3",
                Details = "Details ssss"
            };
            var server2 = new Servers()
            {
                EnumStatus = Status.Off,
                EnumType = Type.Vpn,
                Name = "OFF VPN",
                Password = "sss",
                Ip1 = "11.11.11.11",
                Ip2 = "22.22.22.22",
                Ip3 = "33.33.33.33",
                Details = "Details vpn off"
            };
            context.ServerList.Add(server1);
            context.ServerList.Add(server2);
            base.Seed(context);
        }
    }
}