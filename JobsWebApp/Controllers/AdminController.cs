using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;
using DataLayer.CombinedCrud;
using DataLayer.Crud;
using Ganss.XSS;
using JobsWebApp.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly DepartmentCrud _departmentCrud;
        private readonly SalaryRangeCrud _salaryRangeCrud;
        private readonly VacancyCrud _vacancyCrud;
        private readonly ApplicationCrud _applicationCrud;
        private readonly FullVacancyApplicantCrud _fullVacancyApplicantCrud;

        private readonly HtmlSanitizer _htmlSanitizer;
        
        public AdminController()
        {
            _departmentCrud = new DepartmentCrud();
            _salaryRangeCrud = new SalaryRangeCrud();
            _vacancyCrud = new VacancyCrud();
            _applicationCrud = new ApplicationCrud();
            _fullVacancyApplicantCrud = new FullVacancyApplicantCrud();

            _htmlSanitizer = new HtmlSanitizer();
        }
        
        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminControllerIndexViewModel
            {
                PublishedVacancies = await _vacancyCrud.FindAllPublished(),
                DraftVacancies = await _vacancyCrud.FindAllDraft()
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Details(int id)
        {
            var vacancy = await _vacancyCrud.FindById(id);
            
            if (vacancy == null) return VacancyNotFoundPage();

            var viewModel = new DetailsViewModel
            {
                VacancyBase = vacancy,
                DepartmentBase = await _departmentCrud.Find(vacancy.DepartmentId),
                NumberOfApplications = await _applicationCrud.Count(vacancy.Id)
            };

            return View(viewModel);
        }

        private IActionResult VacancyNotFoundPage()
        {
            Response.StatusCode = 404;
            return View("VacancyNotFound");
        }

        [HttpPost]
        public IActionResult Publish(int id, bool published)
        {
            // toggle publish status

            _vacancyCrud.Publish(id, published);

            return RedirectToAction("details", new {id});
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new AdminControllerCreateViewModel
            {
                Departments = await LoadDepartmentSelectList(),
            };

            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(AdminControllerCreateViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                viewModel.Departments = await LoadDepartmentSelectList();

                return View(viewModel);
            }

            // insert application

            var vacancyModel = new VacancyBaseModel
            {
                JobTitle = _htmlSanitizer.Sanitize(viewModel.Vacancy.JobTitle),
                JobDescription = _htmlSanitizer.Sanitize(viewModel.Vacancy.JobDescription),
                SalaryMin = (int)viewModel.Vacancy.SalaryMin,
                SalaryMax = (int)viewModel.Vacancy.SalaryMax,
                DepartmentId = (int) viewModel.Department,
                ContractType = _htmlSanitizer.Sanitize(viewModel.Vacancy.ContractType),
                StartDate = viewModel.Vacancy.StartDate,
                EndDate = viewModel.Vacancy.EndDate,
                Published = viewModel.Vacancy.Published
            };

            var questionModels = new List<VacancyQuestionBaseModel>();

            foreach (var question in viewModel.Questions)
                questionModels.Add(new VacancyQuestionBaseModel
                {
                    Question = _htmlSanitizer.Sanitize(question.Question),
                    IsRequired = question.IsRequired,
                    MinLength = question.MinLength,
                    MaxLength = question.MaxLength,
                    DisplayOrder = question.DisplayOrder
                });
            
            var vacancyId = await _vacancyCrud.Insert(vacancyModel, questionModels);

            return Redirect($"/admin/details/{vacancyId}");
        }

        [Route("Admin/Applications/{vacancyId:int}/{applicantId:int?}")]
        public async Task<IActionResult> Applications(int vacancyId, int applicantId = 0)
        {
            var vacancy = await _vacancyCrud.FindById(vacancyId);
            
            if (vacancy == null) return VacancyNotFoundPage();
            
            var viewModel = new ApplicationsViewModel
            {
                Vacancy = vacancy,
                Applications = await _applicationCrud.FindAll(vacancyId)
            };

            if (applicantId != 0)
            {
                viewModel.Applicant = await _fullVacancyApplicantCrud.Find(vacancyId, applicantId);
            }

            return View(viewModel);
        }
        
        private async Task<List<SelectListItem>> LoadDepartmentSelectList()
        {
            var departments = await _departmentCrud.FindAll();

            return departments.Select(x => new SelectListItem
                {Value = x.Id.ToString(), Text = x.DepartmentName}).ToList();
        }
    }
}