﻿

using Newtonsoft.Json;

namespace SampleAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        [JsonIgnore]
        public Data? Data { get; set; }
    }
}
