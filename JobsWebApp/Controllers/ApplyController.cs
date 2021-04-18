using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;
using DataLayer.Crud;
using Ganss.XSS;
using JobsWebApp.ViewModels.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Controllers
{
    public class ApplyController : Controller
    {
        private readonly VacancyCrud _vacancyCrud;
        private readonly VacancyQuestionCrud _vacancyQuestionCrud;
        private readonly EducationTypeCrud _educationTypeCrud;
        private readonly ApplicationCrud _applicationCrud;

        private readonly HtmlSanitizer _htmlSanitizer;
        
        public ApplyController()
        {
            _vacancyCrud = new VacancyCrud();
            _vacancyQuestionCrud = new VacancyQuestionCrud(); 
            _educationTypeCrud = new EducationTypeCrud();
            _applicationCrud = new ApplicationCrud();

            _htmlSanitizer = new HtmlSanitizer();
        }
        
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var vacancy = await _vacancyCrud.FindById(id);
            if (vacancy == null) return VacancyNotFound();

            var customQuestions = await _vacancyQuestionCrud.FindAll(id);
            
            return View(new CreateViewModel
            {
                Id = id,
                EducationTypes = await ConvertEducationTypesToSelectList(),
                Questions = customQuestions
            });
        }

        private IActionResult VacancyNotFound()
        {
            Response.StatusCode = 404;
            return View("VacancyNotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateViewModel application)
        {
            var vacancy = await _vacancyCrud.FindById(id);
            if (vacancy == null) return VacancyNotFound();
            
            var customQuestions = await _vacancyQuestionCrud.FindAll(id);

            if (!ModelState.IsValid)
            {
                application.EducationTypes = await ConvertEducationTypesToSelectList();
                application.Questions = customQuestions;
                
                return View(application);
            }

            var vacancyApplication = new VacancyApplicationBaseModel
            {
                FirstName = _htmlSanitizer.Sanitize(application.VacancyApplication.FirstName),
                LastName = _htmlSanitizer.Sanitize(application.VacancyApplication.LastName),
                AddressLine1 = _htmlSanitizer.Sanitize(application.VacancyApplication.AddressLine1),
                AddressLine2 = _htmlSanitizer.Sanitize(application.VacancyApplication.AddressLine2),
                AddressLine3 = _htmlSanitizer.Sanitize(application.VacancyApplication.AddressLine3),
                AddressLine4 = _htmlSanitizer.Sanitize(application.VacancyApplication.AddressLine4),
                PostCode = _htmlSanitizer.Sanitize(application.VacancyApplication.PostCode),
                PhoneNumber = _htmlSanitizer.Sanitize(application.VacancyApplication.PhoneNumber),
                EmailAddress = _htmlSanitizer.Sanitize(application.VacancyApplication.EmailAddress),
                VacancyId = id
            };

            var vacancyEducation = application.Education.Select(educationViewModel => new VacancyEducationBaseModel
            {
                EducationTypeId = educationViewModel.EducationTypeId,
                Description = _htmlSanitizer.Sanitize(educationViewModel.Description),
            }).ToList();

            var vacancyWorkHistory = new List<WorkHistoryBaseModel>();

            foreach (var workHistory in application.WorkHistory)
                vacancyWorkHistory.Add(
                    new WorkHistoryBaseModel
                    {
                        Summary = _htmlSanitizer.Sanitize(workHistory.Summary),
                        EmployerName = _htmlSanitizer.Sanitize(workHistory.EmployerName),
                        JobTitle = _htmlSanitizer.Sanitize(workHistory.JobTitle),
                        StartDate = workHistory.StartDate,
                        EndDate = workHistory.EndDate
                    });

            var questionAnswers = new List<VacancyQuestionAnswerBaseModel>();

            // same order as questions
            for (var i = 0; i < customQuestions.Count; i++)
                questionAnswers.Add(new VacancyQuestionAnswerBaseModel
                {
                    Answer = _htmlSanitizer.Sanitize(application.Answers[i].Answer),
                    VacancyCustomQuestionId = customQuestions[i].Id
                });

            try
            {
                await _applicationCrud.InsertApplication(vacancyApplication, vacancyEducation, vacancyWorkHistory,
                    questionAnswers);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Success");
        }
        
        public IActionResult Success()
        {
            return View();
        }
        
        private async Task<List<SelectListItem>> ConvertEducationTypesToSelectList()
        {
            var educationTypes = await _educationTypeCrud.FindAll();

            return educationTypes.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name}).ToList();
        }
    }
}