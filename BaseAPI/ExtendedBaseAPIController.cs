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
        [Route("findbyname/{name}")]
        public IActionResult FindByName(string? name)
        {
            try
            {
                return Push(((IExtendedRepo<T>)Repo).FindByName(name!));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
