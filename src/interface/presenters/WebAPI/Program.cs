using System.Reflection;
using System.Text.Json.Serialization;
using AwsServices;
using CloudGateway;
using DbGateway;
using Gateway;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoRepository;
using MongoRepository.Config;
using MongoRepository.Context;
using MongoRepository.Repositories;
using UserCase;
using UserCase.Interfaces;
using UserCase.Interfaces.Gateways;
using UserCase.UserCases;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddScoped<AppDbContext>();

var appDbContext = (AppDbContext)builder.Services.BuildServiceProvider().GetService(typeof(AppDbContext));
appDbContext.Map();
var loadDataInDatabase = new CreateData(appDbContext);

builder.Services.AddTransient<IAuthGateway,CognitoManager>();

builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IClienteGateway,ClienteGateway>();
builder.Services.AddTransient<IClienteUserCase,ClienteUserCase>();

builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IProdutoGateway,ProdutoGateway>();
builder.Services.AddTransient<IProdutoUserCase,ProdutoUserCase>();

builder.Services.AddTransient<IMeioPagamentoGateway, MeioPagamentoGateway.MeioPagamentoGateway>();
builder.Services.AddTransient<IOrdemPagamentoRepository, OrdemPagamentoRepository>();
builder.Services.AddTransient<IOrdemPagamentoGateway,OrdemPagamentoGateway>();
builder.Services.AddTransient<IOrdemPagamentoUserCase,OrdemPagamentoUserCase>();

builder.Services.AddTransient<ICarrinhoDeComprasRepository, CarrinhoDeComprasRepository>();
builder.Services.AddTransient<ICarrinhoDeComprasGateway,CarrinhoDeComprasGateway>();
builder.Services.AddTransient<ICarrinhoDeComprasUserCase,CarrinhoDeComprasUserCase>();

builder.Services.AddTransient<IFilaPedidosRepository, FilaPedidosRepository>();
builder.Services.AddTransient<IFilaPedidosGateway,FilaPedidosGateway>();

builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddTransient<IPedidoGateway,PedidoGateway>();
builder.Services.AddTransient<IPedidoUserCase,PedidoUserCase>();



builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v 1.0.0",
        Title = "FIAP - SOFTWARE ARCHITECTURE  - TECH CHALLENGE",
        Description = "Projeto de gestão de pedidos para entrega do Tech Challenge 02",
        Contact = new OpenApiContact
        {
            Name = "Claudio Parra",
            Url = new Uri("https://github.com/parraclaudio")
        } 
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCognitoIdentity();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{   
    options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_S1n4r7ixe";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false
    };
});


//inject automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<StartupBackgroundService>();
builder.Services.AddSingleton<StartupHealthCheck>();

builder.Services.AddHealthChecks()
    .AddCheck<StartupHealthCheck>(
        "Startup",
        tags: new[] { "ready" });
        

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "FIAP - Tech Challenge";
        c.SpecUrl = "/swagger/v1/swagger.json";
        c.RoutePrefix ="docs";
        c.HideHostname();
        c.HideDownloadButton();
        c.ExpandResponses("all");
    });
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});


app.Run();