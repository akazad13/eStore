using eStore.API;
using eStore.Application;
using eStore.Persistence;
using eStore.Persistence.SeedDatabase;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<Seed>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        try
        {
            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (errorFeature != null)
            {
                var exception = errorFeature.Error;

                if (exception is not ValidationException validationException)
                {
                    throw exception;
                }

                var errors = validationException.Errors.Select(err => err.ErrorMessage);

                var ret = new { message = "Validation errors occur!", errors = errors, };

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(ret), Encoding.UTF8);
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    new
                    {
                        message = "Error Occured!",
                        errors = new string[] { ex?.InnerException?.Message ?? ex?.Message ?? "" }
                    }
                ),
                Encoding.UTF8
            );
        }
    });
});

app.UseCors("_myAllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
