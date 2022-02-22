using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    //Bildiğimiz Context Sınıfı.
    public class DataBaseContext : DbContext
    {
        public DbSet<NoteUser> NoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public DbSet<About> Abouts { get; set; }

        //++
        public DataBaseContext()
        {
            Database.SetInitializer(new MyInitilaizer());
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // FluentAPI İlişkili tablo silme yöntemi

        //    modelBuilder.Entity<Note>()
        //        .HasMany(n => n.Comments)
        //        .WithRequired(c => c.Note)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Note>()
        //        .HasMany(n => n.Likes)
        //        .WithRequired(c => c.Note)
        //        .WillCascadeOnDelete(true);
        //}
    }
}
