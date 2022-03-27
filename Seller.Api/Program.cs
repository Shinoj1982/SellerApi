using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Seller.API.Data;
using Seller.API.Models;
using Seller.API.Queue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.Configure<SellerDatabaseSettings>(
    builder.Configuration.GetSection("SellerDatabaseSettings"));
builder.Services.Configure<AzureServiceBusSettings>(
    builder.Configuration.GetSection("AzureServiceBusSettings"));
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IMessageQueueClient, AzureMessageClient>();
builder.Services.AddCors(
    options => options.AddPolicy("corspolicy",
    builder => { builder.AllowAnyOrigin(); builder.AllowAnyHeader(); builder.AllowAnyMethod(); }
    ));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corspolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
