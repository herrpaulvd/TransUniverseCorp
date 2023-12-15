using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repos
{
    public interface IExtendedRepo<TEntity> : IUniversalRepo<TEntity> where TEntity : INamedBLEntity
    {
        TEntity? FindByName(string name);
    }
}
