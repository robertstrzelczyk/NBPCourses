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
        /// Pobranie kursów z bazy danych (jest to opcjonalny endpoint, gdybyśmy chcieli pobrać dane z bazy danych bezpośrednio)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetCourses()
        {
            var currencyList = await _coursesService.GetCoursesAsync();

            return Json(new { iTotalRecords = currencyList.TotalCount, iTotalDisplayRecords = currencyList.TotalCount, aaData = currencyList.Items });
        }

        /// <summary>
        /// Pobranie kursów ze strony NBP
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetCurrencyList()
        {
            var currencyList = await
                _integrationNBPService.GetCoursesAsync();

            await Task.Run(() =>
            {
                _coursesService.AddCoursesAsync();
            });

            return Json(new { iTotalRecords = currencyList.TotalCount, iTotalDisplayRecords = currencyList.TotalCount, aaData = currencyList.Items });
        }

        /// <summary>
        /// Dodawanie kursów do bazy danych (opcjonalna opcja, aby samemu można strzelić do api i pobrać dane)
        /// </summary>
        /// <returns></returns>
        [HttpPost("/add-courses")]
        public async Task<IActionResult> AddAsync()
        {
            await _coursesService.AddCoursesAsync();

            return Ok();
        }
    }
}
