using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace StringValidation.Library.Models
{
	public class DatabaseContext : DbContext
	{
		public DbSet<DbUserInput> UserInput { get; set; }

		public DatabaseContext([NotNull] DbContextOptions options) : base(options) { }
	}

}

