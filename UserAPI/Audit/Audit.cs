using System;

namespace UserAPI.Audit
{
    public class Audit
    {
        public string severity { get; set; }
        public string user { get; set; }
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string descriptionId { get; set; }
        public string fullyQualifiedClassName { get; set; }
        public string methodName { get; set; }
        public object data { get; set; }
    }
}
