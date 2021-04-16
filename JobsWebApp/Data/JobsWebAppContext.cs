using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsWebApp.Data
{
    public class JobsWebAppContext : DbContext
    {
        public JobsWebAppContext(DbContextOptions<JobsWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<VacancyModel> VacancyModel { get; set; }
    }
}