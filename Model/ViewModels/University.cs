using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Model.ViewModels
{
    [JsonSerializable(typeof(University))]
    public class University
    {
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("alpha_two_code")]
        public string? Alpha_Two_Code { get; set; }
        [JsonProperty("state-province")]
        public string? State_Province { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("domains"), NotMapped]
        public string[] Domains { get => (Domain??"").Split(','); set => Domain = string.Join(",", value); }
        [JsonProperty("web_pages"), NotMapped]
        public string[] Web_Pages { get => (WebPage??"").Split(','); set => WebPage = string.Join(",", value); }

        public string? WebPage { get; set; }
        public string? Domain { get; set; }
    }
}
