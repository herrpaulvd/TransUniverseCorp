using Azure.Core;
using BL.Repos;
using Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnviersalMV;

namespace BL.ReposImpl
{
    internal abstract class UniversalWebRepoImpl<TBLEntity>
        : BaseUniversalRepoImpl, IUniversalRepo<TBLEntity>
        where TBLEntity : class, IBLEntity, new()
    {
        private readonly string url;

        protected UniversalWebRepoImpl(string url)
        {
            if (url.Length == 0 || url[^1] != '/') url += "/";
            this.url = url;
        }

        protected string? PushString(string s, string suburl)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new(HttpMethod.Post, url + suburl)
                {
                    Content = new StringContent(s)
                };
                var response = client.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                    return null;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        protected string? Push(object? obj, string suburl)
        {
            if (obj is null) return PushString("", suburl);
            CommonModel model = new(obj);
            JObject json = [];
            try
            {
                foreach (var p in model.GetSettableValues())
                    json.Add(p.Name, p.Value);
            }
            catch(Exception)
            {
                return null;
            }
            return PushString(json.ToString(), suburl);
        }

        protected string? PullString(string suburl)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new(HttpMethod.Get, url + suburl);
                var response = client.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                    return null;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        protected bool Pull(object obj, string suburl)
        {
            CommonModel model = new(obj);
            var response = PullString(suburl);
            if(response is null) return false;

            if(obj is ScheduleElement)
            {
                Console.WriteLine("here");
            }

            JObject json = JObject.Parse(response);
            try
            {
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

        protected List<T>? PullArray<T>(string suburl) where T : new()
        {
            var response = PullString(suburl);
            if (response is null) return null;
            JArray array = JArray.Parse(response);
            List<T> result = [];
            foreach(var tk in array)
            {
                JObject json = (JObject)tk;
                T t = new();
                CommonModel model = new(t);
                try
                {
                    foreach (var p in model.GetDefaultSettableValues())
                    {
                        json.TryGetValue(p.Name, out var value);
                        model.SetValue(p.Name, ((JValue)value!).Value!.ToString()!);
                    }
                }
                catch(Exception)
                {
                    return null;
                }
                result.Add(t);
            }
            return result;
        }

        public void Delete(TBLEntity entity)
        {
            Push(null, $"delete/{entity.Id}");
        }

        protected const int INVALID_ID = -1;

        public TBLEntity? Get(int id)
        {
            TBLEntity result = new() { Id = INVALID_ID };
            if (!Pull(result, $"get/{id}"))
                return null;
            if(result.Id == INVALID_ID) return null;
            return result;
        }

        public IList<TBLEntity?> GetAll()
        {
            return PullArray<TBLEntity?>("all")!;
        }

        public void Update(TBLEntity entity)
        {
            Push(entity, $"update/{entity.Id}");
        }

        public int Add(TBLEntity entity)
        {
            return int.Parse(Push(entity, "add")!);
        }
    }
}
