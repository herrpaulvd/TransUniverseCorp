using BL.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ReposImpl
{
    internal abstract class ExtendedWebRepoImpl<TBLEntity>
        : UniversalWebRepoImpl<TBLEntity>
        , IExtendedRepo<TBLEntity>
        where TBLEntity : class, INamedBLEntity, new()
    {
        protected ExtendedWebRepoImpl(string url) : base(url) { }

        public TBLEntity? FindByName(string name)
        {
            TBLEntity result = new() { Id = INVALID_ID };
            if (!Pull(result, $"findbyname", name))
                return null;
            if (result.Id == INVALID_ID) return null;
            return result;
        }
    }
}
