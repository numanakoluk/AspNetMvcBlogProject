using Core.DataAccess;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    //Amaç esas işin DAL'da yapılmasını sağlamak fakat araya bir katman daha çıktık Core olarak.Abstract yapma sebebimiz new'lenemesin.
    public abstract class ManagerBase<T> : IDataAccess<T> where T:class
    {
        private Repository<T> repo = new Repository<T>();
        public virtual int Delete(T obj) //Methodu bazı yerlerde ezmek için virtual yapıyorum
        {
            return repo.Delete(obj);
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
