namespace Test4Create.API.Entities
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
