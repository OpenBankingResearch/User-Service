using System;

namespace UserAPI.Audit
{
    public class Audit
    {
        public string Severity { get; set; }
        public string User { get; set; }
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int DescriptionId { get; set; }
        public string FullyQualifiedClassName { get; set; }
        public string MethodName { get; set; }
        public object Data { get; set; }
    }
}
