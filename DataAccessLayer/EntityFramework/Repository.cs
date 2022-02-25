using Common;
using Core.DataAccess;
using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
   public class Repository<T> :RepositoryBase, IRepository<T> where T:class //RepositoryBase'den miras alarak DB'yi efektif kullanıyorum
    {
        //Repository pattern

        //private DataBaseContext db = new DataBaseContext(); Eski Hali BasedClass öncesi Lock hatası verdi.
        
        private DbSet<T> _objectSet;
        public Repository()
        {
            //db =RepositoryBase.CreateContext(); //Bu bana databaseContexti verecek.
            _objectSet = context.Set<T>(); //Ctor Calıstıgında dbSet<T> türü calısacak böylece her seferinde ilgili T'yi cagırmakla ugraşmayacak Category,Comment  icin.
        }
        public List<T> List()
        {
            //return db.Set<T>().ToList(); //Burada set edecegimiz icin bir kısıt yazmamız gerekiyor.Bu int da olabilir fakat kısıtladığımız zaman hata gidiyor.
           return _objectSet.ToList();
        }
        public IQueryable<T> ListQueryable() //Ne zaman sorguyu çağırırsak o zaman sorgu çalışsın istiyorsak.OrderBy eklenebilsin 
        {
            //Şartlı sorgu için..
            return _objectSet.AsQueryable<T>(); //AsQuaeryable daha hızlı
        }
        public List<T> List(Expression<Func<T,bool>> where) //İstenilen kritere göre Listeletme
        {
            return _objectSet.Where(where).ToList(); //Expressionu parametre olarak yazdım.
        }

        public int Insert(T obj) //int yapma sebebimiz kaç kayıt etkinlenecekse o kadar kayıt dönmek.
        {
            //db.Set<T>().Add(obj);
            _objectSet.Add(obj);
            if (obj is EntityBase) //Miras almışssa
            {
                EntityBase o = obj as EntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = App.Common.GetCurrentUserName(); //TODO: İşlem yapan kullanıcı adı yazılmalı...
                //Burada normalde o.ModifiedUserName ="Default" diyoduk burası Common katmandan geliyor
            }
            return Save();  //Methodu asagıdan cagırdık.

        }
        public int Update(T obj) //sadece nesneyi cagırdım.
        {
            if (obj is EntityBase) //Miras almışssa
            {
                EntityBase o = obj as EntityBase;

            
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUserName = App.Common.GetCurrentUserName(); //Artık bu şekilde çalışacak.

            }
            return Save();
        }

        public int Delete(T obj)
        {
            //if (obj is EntityBase) //Miras almışssa
            //{
            //    EntityBase o = obj as EntityBase;


            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUserName =App.Common.GetUserName();

            //}

            _objectSet.Remove(obj);
            return Save();
        }
        //Her zaman kullanılsın istemediğimiz için bu şekilde private yapıldı.
        public int Save()//Kaç adet kayıt dönecek
        {

            return context.SaveChanges();
        } 
        public T Find(Expression<Func<T, bool>> where) //geriye tek bir tür döndürüyorum list döndürmüyorum.
        {
            return _objectSet.FirstOrDefault(where); //bulabilirse nesneyi bulamazsa null döner.
        }
             
    }
}
