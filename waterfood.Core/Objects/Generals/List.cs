namespace waterfood.Objects.Generals
{
    public class ManagementList
    {
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
    }

    public class SelectContent
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
