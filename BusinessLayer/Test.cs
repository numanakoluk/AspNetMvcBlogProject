using DataAccessLayer.EntityFramework;
using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    
    public class Test
    {
        private Repository<NoteUser> repo_user = new Repository<NoteUser>();

        private Repository<Category> repo_category = new Repository<Category>();


        //İlişkili tablo için
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();



        public Test()
        {
            //DataAccessLayer.DataBaseContext db = new DataAccessLayer.DataBaseContext();
            ////db.Database.CreateIfNotExists(); //Ctor Tanımlayarak Açtığımda DB Oluştur diyorum.Bunu da Contreller'da çağırıcam
            //db.Categories.ToList(); // Fake Data için bir tane data çağırmam yeterli şimdilik.
            //Şimdi bu işlemi repository yapacak.
            //repo.List(x => x.Id > 5); expression şartlı olan sorgu aşırı yüklenmiş  
            List<Category> categories = repo_category.List();
            //Koşullu List İçin
            //List<Category> categories_filtered = repo_category.List(x=>x.Id>5);
            //BreakPointte List'in calıstığını Break Pointte gördüm
        }
        public void InsertTest()
        {
            int result = repo_user.Insert(new NoteUser() { 
                Name="aaa",
                Surname="bbb",
                Email="samedsargın@gmail.com",
                ActivateGuid=Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                UserName ="aabb",
                Password="111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName ="aabb"
                });;
        }
        public void UpdateTest()
        {
            NoteUser user = repo_user.Find(x => x.UserName == "aabb");
            if (user != null) //Eğer böyle bir kullanıcıyı bulursan
            {
                user.UserName = "xxx";
                
                int result = repo_user.Update(user);
            }
        }
        public void DeleteTest()
        {
            NoteUser user = repo_user.Find(x => x.UserName == "xxx");
            if (user!= null)
            {
                int result=repo_user.Delete(user);
            }
        }

        public void CommentTest()
        {
            //İlişkili tabloda veri ekleme
            NoteUser user = repo_user.Find(x => x.Id == 1); //Bu kullanıcıya
            Note note = repo_note.Find(x => x.Id == 3); //Bu notu
            Comment comment = new Comment() { 
            Text="Bu bir test'dir",
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            ModifiedUserName = "numanakoluk",
            Note = note,
            Owner = user
            
            };
            repo_comment.Insert(comment); //ekle
        }
    }
}
