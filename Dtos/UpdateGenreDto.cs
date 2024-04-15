using System.ComponentModel.DataAnnotations;

namespace GameStore;

public record class UpdateGenreDto(
    [Required] [MaxLength(50)]
    string Name
);