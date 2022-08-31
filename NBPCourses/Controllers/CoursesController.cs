using Microsoft.AspNetCore.Mvc;
using NBPCourses.Core.Integrations.Interfaces;
using System.Collections.Generic;
using System;
using NBPCourses.Core.Integrations.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;
using NBPCourses.Core.Interfaces;

namespace NBPCourses.Api.Controllers
{    
    public class CoursesController : Controller
    {
        public IIntegrationNBPService _integrationNBPService;
        public ICoursesService _coursesService;

        public CoursesController(IIntegrationNBPService integrationNBPService, ICoursesService coursesService)
        {
            _integrationNBPService = integrationNBPService;
            _coursesService = coursesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Pobranie kursów ze strony NBP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetCurrencyList()
        {
            var currencyList = await
                _integrationNBPService.GetCoursesAsync();

            return Json(new { iTotalRecords = currencyList.TotalCount, iTotalDisplayRecords = currencyList.TotalCount, aaData = currencyList.Items });
        }

        /// <summary>
        /// Dodawanie kursów do bazy danych
        /// </summary>
        /// <returns></returns>
        [HttpPost("/add-courses")]
        public async Task<IActionResult> AddAsync()
        {
            await _coursesService.AddCourses();

            return Ok();
        }
    }
}
