namespace Test4Create.API.Entities
{
    public class SystemLog : Base
    {
        public ResourceTypeEnum ResourceType { get; set; }
        public EventEnum Event { get; set; }
        public string ResourceAttributes { get; set; }
        public string Comment { get; set; }
    }
}
