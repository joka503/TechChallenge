using Refit;

namespace TechChallenge.Core.Models
{
    public class QueryParams
    {
        [AliasAs("apikey")]
        public string ApiKey { get; set; }

        [AliasAs("hash")]
        public string Hash { get; set; }

        [AliasAs("ts")]
        public string TimeStamp { get; set; }

        [AliasAs("limit")]
        public int Limit { get; set; }

        [AliasAs("offset")]
        public int Offset { get; set; }

        [AliasAs("titleStartsWith")]
        public string Title { get; set; }
    }
}
