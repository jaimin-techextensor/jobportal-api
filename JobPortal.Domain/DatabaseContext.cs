using JobPortal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Candidate> Candidate { get; set; } = null!; 
    }
}
