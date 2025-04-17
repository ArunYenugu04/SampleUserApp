using System.Net;

namespace SampleAPI.Models
{
    public class Data
    {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        public Address Address { get; set; }
        public telephones telephones { get; set; }
        public List<UserInstitutions> UserInstitutions { get; set; }
    }
}
