using System.Collections.Generic;

namespace SampleAPI.Models
{
    public class SeedDataRoot
    {
        public List<Data> Users { get; set; }
        public List<Address> Addresses { get; set; }
        public List<telephones> telephones { get; set; }
        public List<Institutions> Institutions { get; set; }
        public List<UserInstitutions> UserInstitutions { get; set; }
    }
}