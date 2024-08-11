using BurgerStoreAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BurgerStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         

            builder.Services.AddDbContext<BurgerStoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CartDbConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            }
            );


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

         
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();  
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Ensure this line is present to serve static files
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}