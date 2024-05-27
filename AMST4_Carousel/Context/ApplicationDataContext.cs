using AMST4_Carousel.Models;
using Microsoft.EntityFrameworkCore;

namespace AMST4_Carousel.Context
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> opts) :base(opts) 
        {
        
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<AMST4_Carousel.Models.Product> Product { get; set; } = default!;
        public DbSet<AMST4_Carousel.Models.Tamanho> Tamanho { get; set; } = default!;
    }
}
