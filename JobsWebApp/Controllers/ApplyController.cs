using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using JobsWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Controllers
{
    public class ApplyController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var educationTypes = GetAllEducationTypes();

            var vacancyQuestionsCrud = new VacancyQuestion();

            var customQuestions = vacancyQuestionsCrud.FindAll(id);


            return View(new CreateViewModel
            {
                Id = id,
                EducationTypes = ConvertEducationTypesToSelectList(educationTypes),
                Questions = customQuestions
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateViewModel application)
        {
            var vacancyQuestionsCrud = new VacancyQuestion();

            var customQuestions = vacancyQuestionsCrud.FindAll(id);

            if (!ModelState.IsValid)
            {
                var educationTypes = GetAllEducationTypes();

                application.EducationTypes = ConvertEducationTypesToSelectList(educationTypes);


                application.Questions = customQuestions;


                return View(application);
            }

            var applicationCrud = new Application();

            var vacancyApplication = new VacancyApplicationModel
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

            var vacancyEducation = application.Education.Select(educationViewModel => new VacancyEdutationModel
                {
                    EducationTypeID = educationViewModel.EducationTypeId, Description = educationViewModel.Description
                })
                .ToList();

            var vacancyWorkHistory = new List<VacancyWorkHistoryModel>();

            foreach (var workHistory in application.WorkHistory)
                vacancyWorkHistory.Add(
                    new VacancyWorkHistoryModel
                    {
                        Summary = workHistory.Summary,
                        EmployerName = workHistory.EmployerName,
                        JobTitle = workHistory.JobTitle,
                        StartDate = workHistory.StartDate,
                        EndDate = workHistory.EndDate
                    });

            var questionAnswers = new List<FullVacancyCustomQuestionAnswerModel>();

            // same order as questions
            for (var i = 0; i < customQuestions.Count; i++)
                questionAnswers.Add(new FullVacancyCustomQuestionAnswerModel
                {
                    Answer = application.Answers[i].Answer,
                    VacancyCustomQuestionId = customQuestions[i].Id
                });

            try
            {
                applicationCrud.InsertApplication(vacancyApplication, vacancyEducation, vacancyWorkHistory,
                    questionAnswers);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return RedirectToAction("Success");
            //return NotFound();
        }


        public async Task<IActionResult> Success()
        {
            return View();
        }

        private static List<EducationTypeModel> GetAllEducationTypes()
        {
            var educationTypeCrud = new EducationType();

            var educationTypes = educationTypeCrud.FindAll();

            return educationTypes;
        }

        private static List<SelectListItem> ConvertEducationTypesToSelectList(List<EducationTypeModel> educationTypes)
        {
            return educationTypes.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name}).ToList();
        }
    }
}