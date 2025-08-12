namespace TodoAppFS.DTOs
{
    public record class TaskDTO(
        int Id, 
        string Name, 
        bool IsDone = false 
        );
}
