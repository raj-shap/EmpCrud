using EmpCrud.Data;
using EmpCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmpCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var empData = await _context.Employees.ToListAsync();
            return View(empData);
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new EmployeeViewModel
            {
                Hobbies = _context.Hobbies.Select(h => new SelectListItem { Value = h.HobbyId.ToString(), Text = h.HobbyName }).ToList(),
                Countries = _context.Countries.Select(c => new SelectListItem { Value = c.CountryId.ToString(), Text = c.CountryName }).ToList(),
                States = new List<SelectListItem>(),
                Cities = new List<SelectListItem>(),
                HobbiesCB = _context.Hobbies.ToList(),
                //HobbiesCB = _context.Hobbies.Select(h => new Hobby
                //{
                //    HobbyId = h.HobbyId,
                //    HobbyName = h.HobbyName,
                //    HobbyValue = h.HobbyValue,
                //    IsChecked = false
                //}).ToList()


                //HobbiesCB = new List<Hobby>
                //{
                //    new Hobby()
                //    {
                //        IsChecked = true,
                //        HobbyName = "Reading",
                //        HobbyValue = "Reading"
                //    },
                //    new Hobby()
                //    {
                //        IsChecked = false,
                //        HobbyName = "Writting",
                //        HobbyValue = "Writting",
                //    },
                //    new Hobby()
                //    {
                //        IsChecked = false,
                //        HobbyName = "Cricket",
                //        HobbyValue = "Cricket",
                //    },
                //}
                //SelectedHobbies = new List<int>()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
                var employee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    //HobbyId = model.Hobby.HobbyId,
                    HobbyName = string.Join(" | ",model.EmpHobbies),
                    CountryId = model.CountryId,
                    StateId = model.StateId,
                    CityId = model.CityId,
                };
                //if (model.SelectedHobbies != null && model.SelectedHobbies.Any())
                //{
                //    var employeeHobbies = model.SelectedHobbies.Select(hobbyId => new Hobby
                //    {

                //        HobbyId = hobbyId
                //    }).ToList();

                //    _context.Hobbies.AddRange(employeeHobbies);
                //    await _context.SaveChangesAsync();
                //}
                _context.Add(employee);
                await _context.SaveChangesAsync();
            
            //model.Hobbies = _context.Hobbies
            //    .Select(h=> new SelectListItem { Value = h.HobbyId.ToString(), Text = h.HobbyName})
            //    .ToList();
            model.Hobbies = _context.Hobbies
                .Select(h => new SelectListItem { Value = h.HobbyId.ToString(), Text = h.HobbyName }).ToList();
            model.HobbiesCB = _context.Hobbies.ToList();
            //model.HobbiesCB = _context.Hobbies
            //    .Select(h => new Hobby { HobbyId = h.HobbyId, HobbyName = h.HobbyName })
            //    .ToList();
            //return View();

            model.Countries = _context.Countries
                .Select(c => new SelectListItem { Value = c.CountryId.ToString(), Text = c.CountryName })
                .ToList();
            model.States = _context.States
                .Where(s => s.CountryId == model.CountryId)
                .Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.StateName })
                .ToList();
            model.Cities = _context.Cities
                .Where(c => c.StateId == model.StateId)
                .Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName })
                .ToList();
            return RedirectToAction("Index");
        }
        public IActionResult GetStatesByCountry(int CountryId)
        {
            var states = _context.States
                .Where(s => s.CountryId == CountryId)
                .Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.StateName })
                .ToList();
            return Json(states);
        }
        public IActionResult GetCitiesByState(int StateId)
        {
            var cities = _context.Cities
                .Where(c => c.StateId == StateId)
                .Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName })
                .ToList();
            return Json(cities);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var emp = await _context.Employees.Include(e => e.Country).Include(e => e.State).Include(e => e.City).FirstOrDefaultAsync(e => e.ID == id);
            //var emp = _context.Employees.Where(e=> e.ID == id).FirstOrDefault();
            return View(emp);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var emp = await _context.Employees.Include(e=>e.Country).Include(e=>e.State).Include(e=>e.City).FirstOrDefaultAsync(e=>e.ID == id);
            return View(emp);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var emp = await _context.Employees.FindAsync(id);
            _context.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

