using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;

namespace StudentRegistrationProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string DOB { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Class { get; set; }
    }
}

