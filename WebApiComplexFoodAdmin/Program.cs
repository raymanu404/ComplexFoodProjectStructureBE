using Microsoft.OpenApi.Models;
using WebApiComplexFoodAdmin.Extensions;

const string CORS_POLICY = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiComplexFoodAdmin.Api", Version = "v1" });
});
builder.Services.AddCors(options => options.AddPolicy(CORS_POLICY, builder => builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUiExtension();
}

app.UseCors(CORS_POLICY);

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}).ApplyMigrations();

app.Run();
