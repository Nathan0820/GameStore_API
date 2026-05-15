namespace GameStore.Api.Dtos;

// A DTO is a contract between the API and its clients. 
//It defines the shape of the data that the API will send and receive.
public record GameSummaryDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
