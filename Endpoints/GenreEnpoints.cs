using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GenreEnpoints
{
    const string GetGenreEndpointName = "GetGenre";

    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres")
            .WithParameterValidation();

        // GET genres
        group.MapGet("/", async (GameStoreContext dbContext) => {
            return await dbContext.Genres
                .Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync();
        });

        // GET one genre
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
            Genre? genre = await dbContext.Genres.FindAsync(id);

            return genre is null ? Results.NotFound() : Results.Ok(genre.ToDto());
        })
        .WithName(GetGenreEndpointName);

        group.MapPost("/", async (CreateGenreDto newGenre, GameStoreContext dbContext) => {
            Genre genre = newGenre.ToEntity();

            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetGenreEndpointName,
                new { id = genre.Id },
                genre.ToDto()
            );
        });

        group.MapPut("/{id}", async (int id, UpdateGenreDto updatedGenre, GameStoreContext dbContext) => {
            var existingGenre = await dbContext.Genres.FindAsync(id);

            if (existingGenre is null) {
                return Results.NotFound();
            }

            dbContext.Entry(existingGenre)
                .CurrentValues
                .SetValues(updatedGenre.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => {
            await dbContext.Genres
                .Where(genre => genre.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
