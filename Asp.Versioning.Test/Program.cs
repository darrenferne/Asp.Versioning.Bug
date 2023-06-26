
using Asp.Versioning;
using Microsoft.AspNetCore.OData;
using Asp.Versioning.Test.LetterApi;
using Asp.Versioning.Test.WordApi;
using Asp.Versioning.Test.MetadataApi;
using Microsoft.OData.ModelBuilder;

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
                    options.AddRouteComponents("metadata");
                    options.AddRouteComponents("lettersapi");
                    options.AddRouteComponents("lettersapi/metadata");
                    options.AddRouteComponents("wordsapi");
                    options.AddRouteComponents("wordsapi/metadata");
                    
                    //This works 
                    //options.ModelBuilder.ModelBuilderFactory = () => new ODataConventionModelBuilder();
                    //But this doesn't
                    options.ModelBuilder.ModelBuilderFactory = () => new ODataModelBuilder();
                    options.ModelBuilder.DefaultModelConfiguration = (builder, version, route) =>
                    {
                        if (route == "metadata")
                        {
                            builder.EntitySet<Metadata>("Metadata");
                            builder.EntityType<Metadata>()
                                   .HasKey(e => e.TypeName);

                        }
                        if (route == "lettersapi")
                        {
                            builder.EntitySet<LetterType>("Letter");
                            builder.EntityType<LetterType>()
                                   .HasKey(e => e.Id);

                        }
                        else if (route == "lettersapi/metadata")
                        {
                            builder.EntitySet<Metadata>("Metadata");
                            builder.EntityType<Metadata>()
                                   .HasKey(e => e.TypeName);

                        }
                        else if (route == "wordsapi")
                        {
                            builder.EntitySet<WordType>("Word");
                            builder.EntityType<WordType>()
                                   .HasKey(e => e.Id);
                        }
                        else if(route == "wordsapi/metadata")
                        {
                            builder.EntitySet<Metadata>("Metadata");
                            builder.EntityType<Metadata>()
                                   .HasKey(e => e.TypeName);

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