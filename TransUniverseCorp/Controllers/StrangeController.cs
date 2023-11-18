using Microsoft.AspNetCore.Mvc;
using System.Text;
using TransUniverseCorp.Experimental;
using UnviersalMV;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransUniverseCorp.Controllers
{
    [Route("strange")]
    public class StrangeController : Controller
    {
        [Route("{id:int}")]
        public IActionResult Index(int id)
        {
            var m = new UniversalViewBag(new CommonModel(StrangeObject.Pool[id]), "~/strange");
            return View("CommonSingle", m);
        }

        [Route("all")]
        public IActionResult List()
        {
            var m = StrangeObject.Pool.Select((o, i) => new UniversalViewBag(new CommonModel(o), i.ToString())).ToArray();
            return View("CommonList", m);
        }

        private IActionResult ConstructUpdateAction(int id, string? error)
        {
            var m = new UniversalViewBag(new CommonModel(StrangeObject.Pool[id]), id.ToString(), error);
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
            var m = new CommonModel(StrangeObject.Pool[id]);
            StringBuilder error = new();
            foreach (var kv in Request.Form)
            {
                var p = kv.Key;
                var v = kv.Value.ToString();
                if(!m.SetValue(p, v))
                {
                    if (error.Length == 0) error.Append("Ошибка:\n");
                    error.Append($"Невозможно присвоить значение \"{v}\" полю {p}\n");
                }
            }

            if (error.Length == 0)
                return Redirect("../all");
            else
                return ConstructUpdateAction(id, error.ToString());
        }

        [HttpPost]
        [Route("d/{id:int}")]
        public IActionResult Delete(int id)
        {
            StrangeObject.Pool.RemoveAt(id);
            return Redirect("../all");
        }

        private IActionResult ConstructNewAction(string? error)
        {
            var m = new UniversalViewBag(CommonModel.Create<StrangeObject>(), "new", error);
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
            var obj = new StrangeObject();
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
                StrangeObject.Pool.Add(obj);
                return Redirect("all");
            }
            else
                return ConstructNewAction(error.ToString());
        }
    }
}
