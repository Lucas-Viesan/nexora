using Microsoft.EntityFrameworkCore;
using Nexora.Entities;

namespace Nexora.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        DbSet<Produto> Produtos { get; set; }
        DbSet<Pedido> Pedidos { get; set; }
        DbSet<ItemPedido> ItensPedidos { get; set; }
        DbSet<Cliente> Cliente { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Pagamento> Pagamentos { get; set; }
   
    }
}
