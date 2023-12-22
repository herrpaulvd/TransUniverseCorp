using Azure.Core;
using BL;
using BL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnviersalMV;

namespace TransUniverseCorp.Controllers
{
    public abstract class UniversalControllerBase<T> : Controller where T : class, IBLEntity, new()
    {
        private Func<RepoKeeper, IUniversalRepo<T>> getRepo;
        protected IUniversalRepo<T> Repo => getRepo(RepoKeeper.Instance);

        public UniversalControllerBase(Func<RepoKeeper, IUniversalRepo<T>> getRepo)
        {
            this.getRepo = getRepo;
        }

        [Route("v/{id:int}")]
        public IActionResult Single(int id)
        {
            var m = new UniversalViewBag(new CommonModel(Repo.Get(id)), "");
            return View("CommonSingle", m);
        }

        [Route("all")]
        public IActionResult List()
        {
            var m = Repo.GetAll().Select(o => new UniversalViewBag(new CommonModel(o), o.Id.ToString())).ToArray();
            return View("CommonList", m);
        }

        [NonAction]
        private IActionResult ConstructUpdateAction(int id, string? error)
        {
            var m = new UniversalViewBag(new CommonModel(Repo.Get(id)), id.ToString(), error);
            return View("CommonUpdate", m);
        }

        [Route("u/{id:int}")]
        public IActionResult UpdateGet(int id)
        {
            return ConstructUpdateAction(id, null);
        }

        [HttpPost]
        [Route("u/{id:int}")]
        public IActionResult UpdatePost(int id)
        {
            var obj = Repo.Get(id);
            var m = new CommonModel(obj);
            StringBuilder error = new();
            foreach (var kv in Request.Form)
            {
                var p = kv.Key;
                var v = kv.Value.ToString();
                if (!m.SetValue(p, v))
                {
                    if (error.Length == 0) error.Append("Ошибка:\n");
                    error.Append($"Невозможно присвоить значение \"{v}\" полю {p}\n");
                }
            }

            if (error.Length == 0)
            {
                Repo.Update(obj);
                return Redirect("../all");
            }
            else
                return ConstructUpdateAction(id, error.ToString());
        }

        [HttpPost]
        [Route("d/{id:int}")]
        public IActionResult Delete(int id)
        {
            Repo.Delete(Repo.Get(id));
            return Redirect("../all");
        }

        [NonAction]
        private IActionResult ConstructNewAction(string? error)
        {
            var m = new UniversalViewBag(CommonModel.Create<T>(), "new", error) { PrintDefault = true };
            return View("CommonUpdate", m);
        }

        [Route("new")]
        public IActionResult New()
        {
            return ConstructNewAction(null);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult NewPost()
        {
            var obj = new T();
            var m = new CommonModel(obj);
            StringBuilder error = new();
            foreach (var kv in Request.Form)
            {
                var p = kv.Key;
                var v = kv.Value.ToString();
                if (!m.SetValue(p, v))
                {
                    if (error.Length == 0) error.Append("Ошибка:\n");
                    error.Append($"Невозможно присвоить значение \"{v}\" полю {p}\n");
                }
            }

            if (error.Length == 0)
            {
                Repo.Add(obj);
                return Redirect("all");
            }
            else
                return ConstructNewAction(error.ToString());
        }
    }
}
