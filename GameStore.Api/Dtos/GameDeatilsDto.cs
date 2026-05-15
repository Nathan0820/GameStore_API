namespace GameStore.Api.Dtos;

// A DTO is a contract between the API and its clients. 
//It defines the shape of the data that the API will send and receive.
public record GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
