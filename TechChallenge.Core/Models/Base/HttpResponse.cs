using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TechChallenge.Core.Models.Base
{
    public class HttpResponse<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("attributionText")]
        public string AttributionText { get; set; }

        [JsonPropertyName("attributionHTML")]
        public string AttributionHTML { get; set; }

        [JsonPropertyName("etag")]
        public string Etag { get; set; }

        [JsonPropertyName("data")]
        public Pagination<T> Data { get; set; }
    }
}
