using CriadorSkuGtin.Domain;
using Microsoft.EntityFrameworkCore;

namespace CriadorSkuGtin.EntityFramework
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			
		}
		public DbSet<Fabricante>? Fabricantes { get; set; }
		public DbSet<Grupo>? Grupos { get; set; }
		public DbSet<NextGtinGenerator>? GtinStorage { get; set; }
		public DbSet<StoredSku>? Skus { get; set; }
	}
}