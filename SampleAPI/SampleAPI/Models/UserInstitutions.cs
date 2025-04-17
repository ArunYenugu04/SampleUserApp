
using Newtonsoft.Json;

namespace SampleAPI.Models
{
    public class UserInstitutions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InstitutionId { get; set; }

        [JsonIgnore]
        public Data? Data { get; set; }
        public Institutions Institutions { get; set; }
    }
}
