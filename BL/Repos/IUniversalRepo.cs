using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repos
{
    public interface IUniversalRepo<TEntity> where TEntity : IBLEntity
    {
        IList<TEntity?> GetAll();
        TEntity? Get(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Add(TEntity entity);
    }
}
