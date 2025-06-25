using DomanLayer.Contracts;
using API.CustomeMiddleWares;

namespace API.Extensions
{
    public static class WebApplicationRegistration
    {

        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateScope();
            var DataSeedingObject = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedingObject.IdentityDataSeedAsync();    
        }

        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWares>();

            return app;
        }

        public static IApplicationBuilder UseSwaggernMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
