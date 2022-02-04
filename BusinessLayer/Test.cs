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
        private Repository<Category> repo_category = new Repository<Category>();

        private Repository<NoteUser> repo_user = new Repository<NoteUser>();

        public Test()
        {
            //DataAccessLayer.DataBaseContext db = new DataAccessLayer.DataBaseContext();
            ////db.Database.CreateIfNotExists(); //Ctor Tanımlayarak Açtığımda DB Oluştur diyorum.Bunu da Contreller'da çağırıcam
            //db.Categories.ToList(); // Fake Data için bir tane data çağırmam yeterli şimdilik.
            //Şimdi bu işlemi repository yapacak.
            //repo.List(x => x.Id > 5); expression şartlı olan sorgu aşırı yüklenmiş  
            List<Category> categories = repo_category.List();
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
                repo_user.Save();
                int result = repo_user.Save();
            }
        }
    }
}
