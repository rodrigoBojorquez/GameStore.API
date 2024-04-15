using System.ComponentModel.DataAnnotations;

namespace GameStore;

public record class CreateGenreDto(
    [Required] [MaxLength(50)]
    string Name
);