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
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var vacancyCrud = new VacancyCrud();
            var vacancy = await vacancyCrud.FindById(id);
            if (vacancy == null)
            {
                Response.StatusCode = 404;
                return View("VacancyNotFound");
            }
            
            
            var educationTypes = await GetAllEducationTypes();

            var vacancyQuestionsCrud = new VacancyQuestionCrud();

            var customQuestions = await vacancyQuestionsCrud.FindAll(id);


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
            var vacancyCrud = new VacancyCrud();
            var vacancy = await vacancyCrud.FindById(id);
            if (vacancy == null)
            {
                Response.StatusCode = 404;
                return View("VacancyNotFound");

            }
            
            var vacancyQuestionsCrud = new VacancyQuestionCrud();

            var customQuestions = await vacancyQuestionsCrud.FindAll(id);

            if (!ModelState.IsValid)
            {
                var educationTypes = await GetAllEducationTypes();

                application.EducationTypes = ConvertEducationTypesToSelectList(educationTypes);


                application.Questions = customQuestions;


                return View(application);
            }

            var applicationCrud = new ApplicationCrud();

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
                await applicationCrud.InsertApplication(vacancyApplication, vacancyEducation, vacancyWorkHistory,
                    questionAnswers);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Success");
            //return NotFound();
        }


        public IActionResult Success()
        {
            return View();
        }

        private static async Task<List<EducationTypeBaseModel>> GetAllEducationTypes()
        {
            var educationTypeCrud = new EducationTypeCrud();

            var educationTypes = await educationTypeCrud.FindAll();

            return educationTypes;
        }

        private static List<SelectListItem> ConvertEducationTypesToSelectList(
            List<EducationTypeBaseModel> educationTypes)
        {
            return educationTypes.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name}).ToList();
        }
    }
}