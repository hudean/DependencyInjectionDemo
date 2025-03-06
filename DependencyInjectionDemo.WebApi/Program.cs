
using System.Reflection;

namespace DependencyInjectionDemo.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #region old code
            //builder.Services.AddConventionalRegistrar(new ConventionalRegistrar());
            ////Assembly.Load("DependencyInjectionDemo.Core");
            //var assembly = Assembly.GetAssembly(typeof(Program));
            //builder.Services.AddAssembly(assembly!);
            #endregion

            #region 
            var assembly = Assembly.GetAssembly(typeof(Program));
            var serviceRegister = new DefaultConventionalRegistrar();
            serviceRegister.AddAssembly(builder.Services, assembly!);
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }

            app.UseAuthorization();

            app.MapGet("/", () => "Hello World!");

            app.MapControllers();

            app.Run();
        }

       
    }   
}
