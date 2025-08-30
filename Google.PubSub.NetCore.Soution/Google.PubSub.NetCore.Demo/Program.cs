using System;
using Google.PubSub.Subscriber;
using Google.PubSub.Publisher;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Models;

namespace Google.PubSub.NetCore.Demo
{
    public class Program
    {
        private static string _setting_path = @"/Users/baraahaljabali/Desktop/google-pub-sub-dotnet-core-main/Google.PubSub.NetCore.Soution/gcp_pubsubsetting.json";

        public static void Main(string[] args)
        {
            Console.WriteLine($"HERE!!! {_setting_path}");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _setting_path);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Console.WriteLine("In ConfigureServices");
            builder.Services.AddControllers();
            builder.Services.AddGooglePubSubSubscriberService(builder.Configuration);
            builder.Services.AddGooglePubSubPublisherService(builder.Configuration);
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Google.PubSub.NetCore.Demo", Version = "v1" });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Google.PubSub.NetCore.Demo v1"));
            }

            // allowing any origin:
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            Console.WriteLine("Done, Mapping controllers!");

            app.Run();
        }
    }
}
