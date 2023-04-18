using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Domain;

namespace WebApplication2.Data
{
	public class MVCContext : DbContext
	{
		public MVCContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Employee> Employees {get;set;}
	}
}
