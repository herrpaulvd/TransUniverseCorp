using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TransUniverseCorp.Experimental;
using UnviersalMV;

namespace TransUniverseCorp.Controllers
{
    public abstract class UniversalControllerBase<T> : Controller where T : class, new()
    {
        private DbContext context;
        private DbSet<T> entities;
        private Func<int, T> selector;
        private Func<T, int> indexfinder;

        public UniversalControllerBase(DbContext context, DbSet<T> entities, Func<int, T> selector, Func<T, int> indexfinder)
        {
            this.context = context;
            this.entities = entities;
            this.selector = selector;
            this.indexfinder = indexfinder;
        }

        [Route("v/{id:int}")]
        public IActionResult Single(int id)
        {
            var m = new UniversalViewBag(new CommonModel(selector(id)), "");
            return View("CommonSingle", m);
        }

        [Route("all")]
        public IActionResult List()
        {
            var m = entities.AsEnumerable().Select(o => new UniversalViewBag(new CommonModel(o), indexfinder(o).ToString())).ToArray();
            return View("CommonList", m);
        }

        [NonAction]
        private IActionResult ConstructUpdateAction(int id, string? error)
        {
            var m = new UniversalViewBag(new CommonModel(selector(id)), id.ToString(), error);
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
            var m = new CommonModel(selector(id));
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
                context.SaveChanges();
                return Redirect("../all");
            }
            else
                return ConstructUpdateAction(id, error.ToString());
        }

        [HttpPost]
        [Route("d/{id:int}")]
        public IActionResult Delete(int id)
        {
            entities.Remove(selector(id));
            context.SaveChanges();
            return Redirect("../all");
        }

        [NonAction]
        private IActionResult ConstructNewAction(string? error)
        {
            var m = new UniversalViewBag(CommonModel.Create<T>(), "new", error);
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
                entities.Add(obj);
                context.SaveChanges();
                return Redirect("all");
            }
            else
                return ConstructNewAction(error.ToString());
        }
    }
}
