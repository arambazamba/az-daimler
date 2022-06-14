using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkillsApi {
    //To manage Migrations & create the DB go to console:
    //[dotnet restore]
    //dotnet ef migrations add MIGRATION-NAME
    //dotnet ef database update

    public class SkillDBContext : DbContext {
        public SkillDBContext () {

        }

        public SkillDBContext (DbContextOptions<SkillDBContext> options) : base (options) {

        }
        
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Demo> Demos { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) { }
    }
}