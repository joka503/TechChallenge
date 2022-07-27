using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace TechChallenge.Core.Models
{
    public  class QueryParams
    {
        //[AliasAs("orderBy")]
        //public string OrderBy { get; set; }

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

        [AliasAs("title")]
        public string Title { get; set; }
    }
}
