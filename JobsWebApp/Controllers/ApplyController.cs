using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;
using DataLayer.Crud;
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
        
        public ApplyController()
        {
            _vacancyCrud = new VacancyCrud();
            _vacancyQuestionCrud = new VacancyQuestionCrud(); 
            _educationTypeCrud = new EducationTypeCrud();
            _applicationCrud = new ApplicationCrud();
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
                FirstName = application.VacancyApplication.FirstName,
                LastName = application.VacancyApplication.LastName,
                AddressLine1 = application.VacancyApplication.AddressLine1,
                AddressLine2 = application.VacancyApplication.AddressLine2,
                AddressLine3 = application.VacancyApplication.AddressLine3,
                AddressLine4 = application.VacancyApplication.AddressLine4,
                PostCode = application.VacancyApplication.PostCode,
                PhoneNumber = application.VacancyApplication.PhoneNumber,
                EmailAddress = application.VacancyApplication.EmailAddress,
                VacancyId = id
            };

            var vacancyEducation = application.Education.Select(educationViewModel => new VacancyEducationBaseModel
                {
                    EducationTypeId = educationViewModel.EducationTypeId, Description = educationViewModel.Description
                })
                .ToList();

            var vacancyWorkHistory = new List<WorkHistoryBaseModel>();

            foreach (var workHistory in application.WorkHistory)
                vacancyWorkHistory.Add(
                    new WorkHistoryBaseModel
                    {
                        Summary = workHistory.Summary,
                        EmployerName = workHistory.EmployerName,
                        JobTitle = workHistory.JobTitle,
                        StartDate = workHistory.StartDate,
                        EndDate = workHistory.EndDate
                    });

            var questionAnswers = new List<VacancyQuestionAnswerBaseModel>();

            // same order as questions
            for (var i = 0; i < customQuestions.Count; i++)
                questionAnswers.Add(new VacancyQuestionAnswerBaseModel
                {
                    Answer = application.Answers[i].Answer,
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