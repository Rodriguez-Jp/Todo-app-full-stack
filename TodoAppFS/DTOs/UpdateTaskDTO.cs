using System.ComponentModel.DataAnnotations;

namespace TodoAppFS.DTOs
{
    public record class UpdateTaskDTO(
        [Required][StringLength(30)] string Name,
        [Required] bool IsDone
        );
}
