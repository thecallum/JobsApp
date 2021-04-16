using DataLayer;
using DataLayer.Models;
using JobsWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace JobsWebApp.Controllers
{
    public class AdminController : Controller
    { 
        public IActionResult Index()
        {
            var vacancyCrud = new Vacancy();

            

            var viewModel = new AdminControllerIndexViewMovel
            {
                Vacancies = vacancyCrud.FindAll()
            };

            return View(viewModel);
        }
        

        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departmentCrud = new Department();
            var salaryRangeCrud = new SalaryRange();



            var departments = departmentCrud.FindAll();
            var salaryRange = salaryRangeCrud.FindAll();


            var viewModel = new AdminControllerCreateViewModel
            {
                Departments = departments.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DepartmentName }).ToList(),
                SalaryRanges = salaryRange.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DisplayValue }).ToList(),

            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AdminControllerCreateViewModel viewModel)
        {

            if (ModelState.IsValid == false)
            {
                var departmentCrud = new Department();
                var salaryRangeCrud = new SalaryRange();

                var departments = departmentCrud.FindAll();
                var salaryRange = salaryRangeCrud.FindAll();

                viewModel.Departments = departments.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DepartmentName }).ToList();
                viewModel.SalaryRanges = salaryRange.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DisplayValue }).ToList();

                return View(viewModel);
            }

            // insert application

            var vacancyModel = new VacancyModel
            {
                JobTitle = viewModel.Vacancy.JobTitle,
                JobDescription = viewModel.Vacancy.JobDescription,
                SalaryMin = viewModel.Vacancy.SalaryMin,
                SalaryMax = viewModel.Vacancy.SalaryMax,
                SalaryRangeId = (int)viewModel.SalaryRange,
                DepartmentId = (int)viewModel.Department,
                ContractType = viewModel.Vacancy.ContractType,
                StartDate = viewModel.Vacancy.StartDate,
                EndDate = viewModel.Vacancy.EndDate,
                Published = viewModel.Vacancy.Published

            };

            var questionModels = new List<VacancyCustomQuestionModel>();

            foreach(var question in viewModel.Questions)
            {
                questionModels.Add(new VacancyCustomQuestionModel {
                    Question = question.Question,
                    IsRequired = question.IsRequired,
                    MinLength = question.MinLength,
                    MaxLength = question.MaxLength,
                    DisplayOrder = question.DisplayOrder
                });
            }

            var vacancyCrud = new Vacancy();

            var vacancyId = vacancyCrud.Insert(vacancyModel, questionModels);

            return Redirect($"/admin/details/{vacancyId}");
        }
    }
}