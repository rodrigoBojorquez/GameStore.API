using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new (
            genre.Id,
            genre.Name
        );
    }

    public static Genre ToEntity(this CreateGenreDto genre)
    {
        return new Genre {
            Name = genre.Name
        };
    }


    public static Genre ToEntity(this UpdateGenreDto genre, int id)
    {
        return new Genre()
        {
            Id = id,
            Name = genre.Name
        };
    }
}
