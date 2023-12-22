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

        protected HttpResponseMessage MakeResponse(string content, bool successful)
        {
            HttpResponseMessage response = new(successful ? HttpStatusCode.OK : HttpStatusCode.NotFound)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            return response;
        }

        protected HttpResponseMessage Push(object? obj)
        {
            if (obj is null) return MakeResponse("", false);
            CommonModel model = new(obj);
            JObject json = [];
            try
            {
                foreach (var p in model.GetSettableValues())
                    json.Add(p.Name, p.Value);
            }
            catch (Exception)
            {
                return MakeResponse("", false);
            }
            return MakeResponse(json.ToString(), true);
        }

        protected HttpResponseMessage PushArray<TItem>(IList<TItem> array)
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
                    return MakeResponse("", false);
                }
                result.Add(json);
            }
            return MakeResponse(result.ToString(), true);
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
                return reader.ReadToEnd();
            }
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int? id)
        {
            try
            {
                Repo.Delete(Repo.Get(id!.Value!)!);
                return MakeResponse("", true);
            }
            catch(Exception)
            {
                return MakeResponse("", false);
            }
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public HttpResponseMessage Get(int? id)
        {
            try
            {
                return Push(Repo.Get(id!.Value));
            }
            catch(Exception)
            {
                return MakeResponse("", false);
            }
        }

        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                return PushArray(Repo.GetAll());
            }
            catch(Exception)
            {
                return MakeResponse("", false);
            }
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public HttpResponseMessage Update(int? id)
        {
            try
            {
                T entity = new();
                Pull(entity, ReadRequest());
                Repo.Update(entity);
                return MakeResponse("", true);
            }
            catch(Exception)
            {
                return MakeResponse("", false);
            }
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add()
        {
            try
            {
                T entity = new();
                using (var reader = new StreamReader(Request.Body))
                {
                    Pull(entity, reader.ReadToEnd());
                }
                return MakeResponse(Repo.Add(entity).ToString(), true);
            }
            catch(Exception)
            {
                return MakeResponse("", false);
            }
        }
    }
}
