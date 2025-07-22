namespace ZYHDotNetCore.RestApi.Models.BlogModel
{
    public class RequestModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? ContentData { get; set; }

        public byte DeleteFlag { get; set; }
    }
}
