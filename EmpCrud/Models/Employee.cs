using System.ComponentModel.DataAnnotations;

namespace EmpCrud.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string HobbyName { get; set; }
        //public Hobby Hobby { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }



    }
}
