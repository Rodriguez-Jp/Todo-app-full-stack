using System.ComponentModel.DataAnnotations;

namespace TodoAppFS.DTOs
{
    public record class CreateTaskDTO (
        [Required] [StringLength(30)] string Name,
        bool IsDone = false
        );
}
