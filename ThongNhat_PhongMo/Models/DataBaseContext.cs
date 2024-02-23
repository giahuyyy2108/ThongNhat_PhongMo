using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Models
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            foreach (var entityType in modelbuilder.Model.GetEntityTypes())
            {
                var tablbename = entityType.GetTableName();
                if (tablbename.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tablbename.Substring(6));
                }

            }

        }

        public DbSet<PhongBan> phongBan { get; set; }

        public DbSet<ThongTinKhamBenh> benhnhan { get; set; }

        public DbSet<TinhTrang> tinhtrang { get; set; }

        public DbSet<User> user { get; set; }

        public DbSet<ThongNhat_PhongMo.Models.CT_PhongBan> CT_PhongBan { get; set; }

    }
}
