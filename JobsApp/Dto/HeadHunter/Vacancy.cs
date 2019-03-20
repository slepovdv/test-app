using System;

namespace JobsApp.Dto.HeadHunter
{
    public class Vacancy
    {
        public string Id { get; set; }
        public bool Premium { get; set; }
        public string Name { get; set; }
        public object Department { get; set; }
        public Area Area { get; set; }
        public Salary Salary { get; set; }
        public Type Type { get; set; }
        public Address Address { get; set; }
        public Employer Employer { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Archived { get; set; }
        public Contacts Contacts { get; set; }
        public Employment Employment { get; set; }
        public string Description { get; set; }
        public Snippet Snippet { get; set; }
    }
}
