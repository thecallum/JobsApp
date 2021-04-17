using DataLayer.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace JobsWebApp.Data
{
    public class JobsWebAppContext : DbContext
    {
        public JobsWebAppContext(DbContextOptions<JobsWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<VacancyBaseModel> VacancyModel { get; set; }
    }
}