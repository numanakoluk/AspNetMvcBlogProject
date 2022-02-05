using DataAccessLayer;
using DataAccessLayer.Abstract;
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

        //private DataBaseContext db = new DataBaseContext(); Eski Hali BasedClass öncesi
        
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
        public List<T> List(Expression<Func<T,bool>> where) //İstenilen kritere göre Listeletme
        {
            return _objectSet.Where(where).ToList(); //Expressionu parametre olarak yazdım.
        }

        public int Insert(T obj)
        {
            //db.Set<T>().Add(obj);
            _objectSet.Add(obj);
            return Save();  //Methodu asagıdan cagırdık.

        }
        public int Update(T obj) //sadece nesneyi cagırdım.
        {
            return Save();
        }

        public int Delete(T obj)
        {
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
