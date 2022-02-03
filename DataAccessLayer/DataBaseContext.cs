using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    //Bildiğimiz Context Sınıfı.
    public class DataBaseContext : DbContext
    {
        public DbSet<NoteUser> NoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public DataBaseContext()
        {
            Database.SetInitializer(new MyInitilaizer());
        }
    }
}
