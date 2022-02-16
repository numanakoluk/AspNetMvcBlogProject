using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntiyLayers;

namespace DataAccessLayer.EntityFramework
{
    //Fake Data İçin
    class MyInitilaizer : CreateDatabaseIfNotExists<DataBaseContext> //Database yokken çalışsın
    {
        protected override void Seed(DataBaseContext context) //seed:ORNEK Database oluştuktan sonraki deki method.
        {
            //Admin Kullanıcı Ekleme
            NoteUser admin = new NoteUser()
            {
                Name = "Numan",
                Surname = "Akoluk",
                Email = "numanakoluk01@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                UserName ="numanakoluk",          
                Password = "123456",
                ProfileImageFileName = "user.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "numanakoluk"
            };
            //Standar Kullanıcı Ekleme
            NoteUser standartUser = new NoteUser()
            {
                Name = "Musa",
                Surname = "Akoluk",
                Email = "musakoluk01@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "musaakoluk",
                Password = "654321",
                ProfileImageFileName = "user.png",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUserName = "numanakoluk"
            };

            //Ekle
            context.NoteUsers.Add(admin);
            context.NoteUsers.Add(standartUser);

            //8 Değer ekle...
            for (int i = 0; i < 8; i++)
            {
                NoteUser user = new NoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFileName = "user.png",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = true,
                    UserName = $"user{i}", //İsimlendirme
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = "numanakoluk"
                };
                context.NoteUsers.Add(user);
            }
            
            context.SaveChanges();
            //Kullanıcı Listesi Kullanımı için
            List<NoteUser> userList = context.NoteUsers.ToList();
            //Fake Kategori Oluşturma
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "numanakoluk"
                };
                context.Categories.Add(cat);
                //Note oluşturma For içi for
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    NoteUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5,25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1,3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1,9),
                        //Owner = (k %2 ==0)? admin : standartUser, //çiftse admin değilse standartkullanıcı
                        Owner = owner, //List kadar
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.UserName,
                    };
                    cat.Notes.Add(note);
                    //Yorumları Ekleme
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        NoteUser comment_ovner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_ovner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_ovner.UserName,
                        };
                        note.Comments.Add(comment);
                    }
                    
                    //Fake like Ekleme
                    for (int m = 0; m <note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]
                        };
                        note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
