using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using JobsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Controllers
{
    public class VacancyController : Controller
    {
        /*
         * Index()
        Details()
        Create()
        Edit()
        Delete()
        DeleteConfirmed()
         * 
         */

        public IActionResult Index(int page = 1, int? SalaryRange = null, int? Department = null)
        {
            var vacancyCrud = new Vacancy();
            var departmentCrud = new Department();
            var salaryRangeCrud = new SalaryRange();

            var departments = departmentCrud.FindAll();
            var salaryRange = salaryRangeCrud.FindAll();
            var vacancies = vacancyCrud.FindWithFilters(page, SalaryRange, Department);


            var viewModel = new HomepageViewModel
            {
                Department = Department,
                SalaryRange = SalaryRange,
                Departments = departments.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DepartmentName }).ToList(),
                SalaryRanges = salaryRange.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DisplayValue }).ToList(),
                Vacancies = vacancies
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vacancyCrud = new Vacancy();

            var vacancyModel = vacancyCrud.FindById((int) id);
            if (vacancyModel == null) return NotFound();


            return View(vacancyModel);
        }
    }
}