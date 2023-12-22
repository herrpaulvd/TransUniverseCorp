using BL;
using BL.Repos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using UnviersalMV;

namespace BaseAPI
{
    public abstract class UniversalBaseAPIController<T> : Controller where T : class, IBLEntity, new()
    {
        private Func<RepoKeeper, IUniversalRepo<T>> getRepo;
        protected IUniversalRepo<T> Repo => getRepo(RepoKeeper.Instance);

        public UniversalBaseAPIController(Func<RepoKeeper, IUniversalRepo<T>> getRepo)
        {
            this.getRepo = getRepo;
        }

        protected IActionResult Push(object? obj)
        {
            if (obj is null) return NotFound();
            CommonModel model = new(obj);
            JObject json = [];
            try
            {
                foreach (var p in model.GetSettableValues())
                    json.Add(p.Name, p.Value);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Content(json.ToString());
        }

        protected IActionResult PushArray<TItem>(IList<TItem> array)
        {
            JArray result = new();
            foreach(var obj in array)
            {
                CommonModel model = new(obj!);
                JObject json = [];
                try
                {
                    foreach (var p in model.GetSettableValues())
                        json.Add(p.Name, p.Value);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                result.Add(json);
            }
            return Content(result.ToString());
        }

        protected bool Pull(object obj, string src)
        {
            CommonModel model = new(obj);
            try
            {
                JObject json = JObject.Parse(src);
                foreach (var p in model.GetDefaultSettableValues())
                {
                    json.TryGetValue(p.Name, out var value);
                    model.SetValue(p.Name, ((JValue)value!).Value!.ToString()!);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        protected string ReadRequest()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var result = reader.ReadToEndAsync().Result;
                Console.WriteLine("Request body:\n" + result + "\n\n\n");
                return result;
            }
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public IActionResult Delete(int? id)
        {
            try
            {
                Repo.Delete(Repo.Get(id!.Value!)!);
                return Ok();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public IActionResult Get(int? id)
        {
            try
            {
                return Push(Repo.Get(id!.Value));
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                return PushArray(Repo.GetAll());
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public IActionResult Update(int? id)
        {
            try
            {
                T entity = new();
                Pull(entity, ReadRequest());
                Repo.Update(entity);
                return Ok();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add()
        {
            try
            {
                T entity = new();
                Pull(entity, ReadRequest());
                return Content(Repo.Add(entity).ToString());
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
    }
}
