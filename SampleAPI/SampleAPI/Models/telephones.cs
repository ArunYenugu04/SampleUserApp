

using Newtonsoft.Json;

namespace SampleAPI.Models
{
    public class telephones
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public Data? Data { get; set; }
    }
}