using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var cert = new X509Certificate2("/app/example.com.pfx", (string)null, X509KeyStorageFlags.PersistKeySet);
//X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
//store.Open(OpenFlags.ReadWrite);
//store.Add(cert);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServer(i => i.IssuerUri = BL.ServiceAddress.IdentityServer)
    .AddDeveloperSigningCredential(true)
    //.AddSigningCredential(cert)
    //.AddValidationKey(new IdentityServer4.Models.SecurityKeyInfo()
    //{
    //    Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("228"))
    //})
    .AddInMemoryApiScopes(IdentityServer.Config.ApiScopes)
    .AddInMemoryClients(IdentityServer.Config.Clients);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
