namespace TodoAppFS.DTOs
{
    public record class UpdateTaskDTO(
        string Name,
        bool IsDone = false
        );
}
