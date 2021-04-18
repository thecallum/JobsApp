using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;
using DataLayer.CombinedCrud;
using DataLayer.Crud;
using JobsWebApp.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vacancyCrud = new VacancyCrud();

            var viewModel = new AdminControllerIndexViewModel
            {
                PublishedVacancies = await vacancyCrud.FindAllPublished(),
                DraftVacancies = await vacancyCrud.FindAllDraft()
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Details(int id)
        {
            var vacancyCrud = new VacancyCrud();
            
            var vacancy = await vacancyCrud.FindById(id);
            if (vacancy == null)
            {
                Response.StatusCode = 404;
                return View("VacancyNotFound");
            }
            
            var departmentCrud = new DepartmentCrud();
            var vacancyApplicationCrud = new ApplicationCrud();

            var viewModel = new DetailsViewModel
            {
                VacancyBase = vacancy,
                DepartmentBase = await departmentCrud.Find(vacancy.DepartmentId),
                NumberOfApplications = await vacancyApplicationCrud.Count(vacancy.Id)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Publish(int id, bool published)
        {
            // toggle publish status
            var vacancyCrud = new VacancyCrud();

            vacancyCrud.Publish(id, published);

            return RedirectToAction("details", new {id});
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departmentCrud = new DepartmentCrud();
            var salaryRangeCrud = new SalaryRangeCrud();


            var departments = await departmentCrud.FindAll();
            var salaryRange = await salaryRangeCrud.FindAll();


            var viewModel = new AdminControllerCreateViewModel
            {
                Departments = departments.Select(x => new SelectListItem
                    {Value = x.Id.ToString(), Text = x.DepartmentName}).ToList(),
                SalaryRanges = salaryRange
                    .Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.DisplayValue}).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminControllerCreateViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                var departmentCrud = new DepartmentCrud();
                var salaryRangeCrud = new SalaryRangeCrud();

                var departments = await departmentCrud.FindAll();
                var salaryRange = await salaryRangeCrud.FindAll();

                viewModel.Departments = departments.Select(x => new SelectListItem
                    {Value = x.Id.ToString(), Text = x.DepartmentName}).ToList();
                viewModel.SalaryRanges = salaryRange
                    .Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.DisplayValue}).ToList();

                return View(viewModel);
            }

            // insert application

            var vacancyModel = new VacancyBaseModel
            {
                JobTitle = viewModel.Vacancy.JobTitle,
                JobDescription = viewModel.Vacancy.JobDescription,
                SalaryMin = viewModel.Vacancy.SalaryMin,
                SalaryMax = viewModel.Vacancy.SalaryMax,
                SalaryRangeId = (int) viewModel.SalaryRange,
                DepartmentId = (int) viewModel.Department,
                ContractType = viewModel.Vacancy.ContractType,
                StartDate = viewModel.Vacancy.StartDate,
                EndDate = viewModel.Vacancy.EndDate,
                Published = viewModel.Vacancy.Published
            };

            var questionModels = new List<VacancyQuestionBaseModel>();

            foreach (var question in viewModel.Questions)
                questionModels.Add(new VacancyQuestionBaseModel
                {
                    Question = question.Question,
                    IsRequired = question.IsRequired,
                    MinLength = question.MinLength,
                    MaxLength = question.MaxLength,
                    DisplayOrder = question.DisplayOrder
                });

            var vacancyCrud = new VacancyCrud();

            var vacancyId = await vacancyCrud.Insert(vacancyModel, questionModels);

            return Redirect($"/admin/details/{vacancyId}");
        }

        [Route("Admin/Applications/{vacancyId:int}/{applicantId:int?}")]
        public async Task<IActionResult> Applications(int vacancyId, int applicantId = 0)
        {
            var vacancyCrud = new VacancyCrud();
            var vacancy = await vacancyCrud.FindById(vacancyId);
            if (vacancy == null)
            {
                Response.StatusCode = 404;
                return View("VacancyNotFound");
            }
            
            var applicationCrud = new ApplicationCrud();

            var viewModel = new ApplicationsViewModel
            {
                Vacancy = vacancy,
                Applications = await applicationCrud.FindAll(vacancyId)
            };

            if (applicantId != 0)
            {
                var applicantCrud = new FullVacancyApplicantCrud();


                viewModel.Applicant = await applicantCrud.Find(vacancyId, applicantId);
            }

            return View(viewModel);
        }
    }
}