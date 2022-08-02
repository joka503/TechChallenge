using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TechChallenge.Core.Models
{
    public class Characters
    {
        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("returned")]
        public int Returned { get; set; }
    }

    public class CollectedIssue
    {
        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Collection
    {
        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Creators
    {
        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("returned")]
        public int Returned { get; set; }
    }

    public class Date
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("date")]
        public object DateTime { get; set; }
    }

    public class Events
    {
        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("returned")]
        public int Returned { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("price")]
        public double PriceValue { get; set; }
    }

    public class Comic
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("digitalId")]
        public int DigitalId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("issueNumber")]
        public int IssueNumber { get; set; }

        [JsonPropertyName("variantDescription")]
        public string VariantDescription { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("modified")]
        public object Modified { get; set; }

        [JsonPropertyName("isbn")]
        public string Isbn { get; set; }

        [JsonPropertyName("upc")]
        public string Upc { get; set; }

        [JsonPropertyName("diamondCode")]
        public string DiamondCode { get; set; }

        [JsonPropertyName("ean")]
        public string Ean { get; set; }

        [JsonPropertyName("issn")]
        public string Issn { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("textObjects")]
        public List<TextObject> TextObjects { get; set; }

        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("urls")]
        public List<Url> Urls { get; set; }

        [JsonPropertyName("series")]
        public Series Series { get; set; }

        [JsonPropertyName("variants")]
        public List<object> Variants { get; set; }

        [JsonPropertyName("collections")]
        public List<Collection> Collections { get; set; }

        [JsonPropertyName("collectedIssues")]
        public List<CollectedIssue> CollectedIssues { get; set; }

        [JsonPropertyName("dates")]
        public List<Date> Dates { get; set; }

        [JsonPropertyName("prices")]
        public List<Price> Prices { get; set; }

        [JsonPropertyName("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("creators")]
        public Creators Creators { get; set; }

        [JsonPropertyName("characters")]
        public Characters Characters { get; set; }

        [JsonPropertyName("stories")]
        public Stories Stories { get; set; }

        [JsonPropertyName("events")]
        public Events Events { get; set; }

        public string SubText
        {
            get
            {
                if (!string.IsNullOrEmpty(VariantDescription))
                {
                    return $"Issue Number: {IssueNumber} / Variant Description: {VariantDescription}";
                }

                return $"Issue Number: {IssueNumber}";
            }
        }

        public string ImagePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Thumbnail?.Path) && !string.IsNullOrEmpty(Thumbnail?.Extension))
                {
                    return $"{Thumbnail?.Path}.{Thumbnail?.Extension}";
                }

                return string.Empty;
            }
        }

        public string ComicDescription
        {
            get
            {
                if (!string.IsNullOrEmpty(Description))
                {
                    return Description;
                }

                return "No description.";
            }
        }
    }

    public class Series
    {
        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Stories
    {
        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("returned")]
        public int Returned { get; set; }
    }

    public class TextObject
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Thumbnail
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; }
    }

    public class Url
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string UrlExt { get; set; }
    }


}
