using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace EmpCrud.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string? HobbyName { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }

    public List<SelectListItem> Countries { get; set; }
    public List<SelectListItem> States {  get; set; }
    public List<SelectListItem> Cities {  get; set; }
    public List<SelectListItem>? Hobbies { get; set; }

    public List<Hobby>? HobbiesCB { get; set; }
    public List<string> EmpHobbies { get; set; }
    //public List<int> SelectedHobbies { get; set; }

    //public Employee Employee { get; set; }
}
