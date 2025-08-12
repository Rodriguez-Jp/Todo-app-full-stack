namespace TodoAppFS.DTOs
{
    public record class CreateTaskDTO (
        string Name,
        bool IsDone = false
        );
}
