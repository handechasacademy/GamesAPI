using  GamesAPI.Data;
using GamesAPI.Exceptions;
using GamesAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("GamesDB"));

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IGameLibraryService, GameLibraryService>();

builder.Services.AddProblemDetails(options => {
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = context.HttpContext.Request.Path;
    };
});

#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache();
#pragma warning restore EXTEXP0018

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        var problemDetails = exception switch
        {
            NotFoundException ex => new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = 404,
                Title = "Not Found",
                Detail = ex.Message
            },
            BadRequestException ex => new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = 400,
                Title = "Bad Request",
                Detail = ex.Message
            },
            _ => new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "Ett oväntat fel inträffade. Försök igen senare."
            }
        };

        context.Response.StatusCode = problemDetails.Status ?? 500;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
