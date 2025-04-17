namespace SampleAPI.Models
{
    public class Institutions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Domain { get; set; }

        public List<UserInstitutions> UserInstitutions { get; set; }
    }
}
