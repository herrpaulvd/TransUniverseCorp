using BL;
using BL.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI
{
    public class ExtendedBaseAPIController<T> : UniversalBaseAPIController<T> where T : class, INamedBLEntity, new()
    {
        public ExtendedBaseAPIController(Func<RepoKeeper, IExtendedRepo<T>> getRepo) : base(getRepo)
        { }

        [HttpGet]
        [Route("findbyname")]
        public HttpResponseMessage FindByName()
        {
            try
            {
                return Push(((IExtendedRepo<T>)Repo).FindByName(ReadRequest()));
            }
            catch (Exception)
            {
                return MakeResponse("", false);
            }
        }
    }
}
