using Newtonsoft.Json;

namespace Codecool.HackerNewsClient.Models
{
    public class News
    {
        public string Title { get; set; }
        [JsonProperty("Time_Ago")]
        public string TimeAgo { get; set; }
        public string User { get; set; }
        public string Url { get; set; }
    }
}
