using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AdminLTE.Models
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Boleto> Boleto { get; set; }
        public DbSet<UsuarioTelefone> UsuarioTelefone { get; set; }
    }
}