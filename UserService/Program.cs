using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = BL.ServiceAddress.IdentityServer;
        options.IncludeErrorDetails = true;
        //options.UseSecurityTokenValidators = false;
        //options.BackchannelHttpHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true };
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKeyValidator = null,
            IssuerSigningKeyResolverUsingConfiguration = null,
            RequireSignedTokens = false,
            //IssuerSigningKey = null,
            //SignatureValidator = delegate (string token, TokenValidationParameters parameters)
            //{
            //    var jwt = new JwtSecurityToken(token);
            //
            //    return jwt;
            //},
            //ValidateActor = false,
            //ValidIssuer = BL.ServiceAddress.IdentityServer,
            //IssuerSigningKey = new X509SecurityKey(GetSigningCertificate())
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "allapi");
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization("ApiScope"));

app.MapControllers();

app.Run();
