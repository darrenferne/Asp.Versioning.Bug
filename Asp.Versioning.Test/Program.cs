
using Asp.Versioning;
using Microsoft.AspNetCore.OData;
using Asp.Versioning.Test.LetterApi;
using Asp.Versioning.Test.WordApi;

namespace Asp.Versioning.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddMvc()
                .AddOData();

            builder.Services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = ApiVersion.Default;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                .AddOData(options =>
                {
                    options.AddRouteComponents("lettersapi");
                    options.AddRouteComponents("wordsapi");

                    options.ModelBuilder.DefaultModelConfiguration = (builder, version, route) =>
                    {
                        if (route == "lettersapi")
                        {
                            builder.EntitySet<LetterType>("Letter");
                            builder.EntityType<LetterType>()
                                   .HasKey(e => e.Id);
                        }
                        else if (route == "wordsapi")
                        {
                            builder.EntitySet<WordType>("Word");
                            builder.EntityType<WordType>()
                                   .HasKey(e => e.Id);
                        }
                    };
                })
                .AddODataApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}