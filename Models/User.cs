using System;
using Newtonsoft.Json;

namespace DotNet_SSE.Models
{
    [Serializable]
    public class User
    {
        [JsonProperty("uid")]
        public string UserId{get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
        [JsonProperty("gender")]
        public string Gender{get;set;}
    }
}