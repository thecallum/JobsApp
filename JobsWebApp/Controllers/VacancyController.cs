using System.Linq;
using System.Threading.Tasks;
using DataLayer.Crud;
using JobsWebApp.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Controllers
{
    public class VacancyController : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int? SalaryRange = null, int? Department = null)
        {
            var vacancyCrud = new VacancyCrud();
            var departmentCrud = new DepartmentCrud();
            var salaryRangeCrud = new SalaryRangeCrud();

            var departments = await departmentCrud.FindAll();
            var salaryRange = await salaryRangeCrud.FindAll();
            var vacancies = await vacancyCrud.FindWithFilters(page, SalaryRange, Department);

            var viewModel = new HomepageViewModel
            {
                Department = Department,
                SalaryRange = SalaryRange,
                Departments = departments.Select(x => new SelectListItem
                    {Value = x.Id.ToString(), Text = x.DepartmentName}).ToList(),
                SalaryRanges = salaryRange
                    .Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.DisplayValue}).ToList(),
                Vacancies = vacancies
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vacancyCrud = new VacancyCrud();

            var vacancyModel = await vacancyCrud.FindById(id);
            if (vacancyModel == null)
            {
                Response.StatusCode = 404;
                return View("VacancyNotFound");
            }

            return View(vacancyModel);
        }
    }
}