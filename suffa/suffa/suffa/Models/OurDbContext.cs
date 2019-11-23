using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class OurDbContext : DbContext
    {
        public OurDbContext() : base("identity")
        {
            Database.SetInitializer<OurDbContext>(new DropCreateDatabaseIfModelChanges<OurDbContext>());
        }
        public DbSet<category> categories { get; set; }
        public DbSet<blogpost> blogposts { get; set; }
        public DbSet<employe> employes { get; set; }
        public DbSet<works> works { get; set; }
        public DbSet<about> abouts { get; set; }
        public DbSet<services> services { get; set; }
        public DbSet<intern> intern { get; set; }
        public DbSet<partner> partner { get; set; }
        public DbSet<process> process { get; set; }
        public DbSet<project> project { get; set; }
        public DbSet<user> user { get; set; }
    }
}