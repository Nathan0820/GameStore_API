using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";
//     private static readonly List<GameDto> games = new List<GameDto>
// {
//     new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
//     new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
//     new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 59.99m, new DateOnly(2018, 10, 26)),
//     new GameDto(4, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateOnly(2015, 5, 19)),
//     new GameDto(5, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18))
// };
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => new GameSummaryDto(
                    game.Id,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleaseDate
                ))
                .AsNoTracking()
                .ToListAsync();
        });


        // GET /games/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                )
            );
        })
        .WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
        });

        //PUT /games/{id}
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game is null)
            {
                return Results.NotFound();
            }

            game.Name = updatedGame.Name;
            game.GenreId = updatedGame.GenreId;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();
            return Results.Ok();
        });

        //DELETE /games/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

    }
}
