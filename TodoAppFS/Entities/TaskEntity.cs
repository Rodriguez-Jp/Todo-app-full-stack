namespace TodoAppFS.Entities
{
    public class TaskEntity
    {

        public int Id { get; set; }
        public required string Name { get; set; }

        public bool IsDone { get; set; }

    }
}
