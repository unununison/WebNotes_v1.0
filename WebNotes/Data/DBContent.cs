using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Data
{
    public class DBContent : DbContext{
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DBContent(DbContextOptions<DBContent> options) : base(options) { Database.EnsureCreated(); }

    }

}
