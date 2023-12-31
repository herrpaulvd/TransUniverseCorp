﻿using BL.Repos;
using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransUniverseCorp.Models;
using System.Security.Claims;
using IdentityModel.Client;

namespace TransUniverseCorp.Controllers
{
    [Authorize(Roles = "driver")]
    [Route("/drvst")]
    public class DriverStateController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private IScheduleElementRepo ScheduleElementRepo => RepoKeeper.Instance.ScheduleElementRepo;
        private IOrderRepo OrderRepo => RepoKeeper.Instance.OrderRepo;
        private ISpacePortRepo SpacePortRepo => RepoKeeper.Instance.SpacePortRepo;
        private ISpaceshipRepo SpaceshipRepo => RepoKeeper.Instance.SpaceshipRepo;

        private int? GetDriverId()
        {
            return UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!.Driver;
        }

        private Driver? GetDriverData()
        {
            var id = GetDriverId();
            if (id is null) return null;
            return DriverRepo.Get(id.Value)!;
        }

        private ScheduleElement? GetScheduleElement(Driver driver)
            => driver.CurrentState.HasValue ? ScheduleElementRepo.Get(driver.CurrentState.Value) : null;

        private DriverFormData ConstructData(string? error)
        {
            var driver = GetDriverData()!;
            var currScheduleElement = GetScheduleElement(driver);
            return new()
            {
                CurrentScheduleElement = currScheduleElement,
                HasOrder = currScheduleElement?.Order is not null,
                Error = error
            };
        }

        [Route("")]
        public IActionResult Index(string? error)
        {
            return View(ConstructData(error));
        }

        private static HttpClientHandler MakeHandler()
            => new() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true };

        private bool SetAccessToken(HttpClient mainClient)
        {
            using HttpClient client = new(MakeHandler());
            var disco = client.GetDiscoveryDocumentAsync(ServiceAddress.IdentityServer).Result;
            if (disco.IsError) return false;
            var tokenResponse = client.RequestClientCredentialsTokenAsync(new()
            {
                Address = disco.TokenEndpoint,
                ClientId = "mainAPP",
                ClientSecret = "superpupersecurepassword228",
                Scope = "allapi"
            }).Result;
            if (tokenResponse.IsError) return false;
            mainClient.SetBearerToken(tokenResponse.AccessToken!);
            return true;
        }

        [HttpPost]
        [Route("leave")]
        public IActionResult Leave()
        {
            int? id = GetDriverId();
            if(id is null) return Redirect("/drvst?error=NOT%20A%20DRIVER");
            string port = Request.Form["port"]!;
            port = "p" + (port ?? "");
            using(HttpClient client = new(MakeHandler()))
            {
                if(!SetAccessToken(client)) return Redirect("/drvst?error=SERVICE%20UNAVAIlABLE");
                HttpRequestMessage request = new(HttpMethod.Post, $"{ServiceAddress.Driver}/drvst/leave/{id.Value}/{port}");
                var response = client.SendAsync(request).Result;
                if(response is null)
                    return Redirect("/drvst?error=INVALID%20PORT");
                else
                    return Redirect("/drvst");
            }
        }

        [HttpPost]
        [Route("next")]
        public IActionResult Next()
        {
            int? id = GetDriverId();
            if (id is null) return Redirect("/drvst?error=INVALID%20OPERATION");
            using (HttpClient client = new(MakeHandler()))
            {
                if (!SetAccessToken(client)) return Redirect("/drvst?error=SERVICE%20UNAVAIlABLE");
                HttpRequestMessage request = new(HttpMethod.Post, $"{ServiceAddress.Driver}/drvst/next/{id.Value}");
                var response = client.SendAsync(request).Result;
                if (response is null)
                    return Redirect("/drvst?error=INVALID%20OPERATION");
                else
                    return Redirect("/drvst");
            }
        }
    }
}
