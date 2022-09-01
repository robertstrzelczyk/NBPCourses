using Microsoft.EntityFrameworkCore;
using NBPCourses.Core.Integrations.Interfaces;
using NBPCourses.Core.Interfaces;
using NBPCourses.Core.Models;
using NPBCourses.Persistence.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Course = NPBCourses.Persistence.Models.Course;

namespace NBPCourses.Core.Services
{
    public class CoursesService : ICoursesService
    {
        private IIntegrationNBPService _integrationNBPService;

        public CoursesService(IIntegrationNBPService integrationNBPService)
        {
            _integrationNBPService = integrationNBPService;
        }

        public async Task<CoursesDataTable> GetCoursesAsync()
        {
            try
            {
                var output = new CoursesDataTable();
                var context = new CoursesDbContext();

                output.Items = await context.Courses.Select(x => new Models.Course
                {
                    CurrencyName = x.CurrencyName,
                    Code = x.Code,
                    Mid = x.Mid,
                    EffectiveDate = x.EffectiveDate.ToString()
                }).OrderBy(x => x.EffectiveDate).ToListAsync();

                output.TotalCount = output.Items.Count();

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddCoursesAsync()
        {
            try
            {
                var rates = _integrationNBPService.GetCoursesAsync();

                if (rates == null) throw new Exception("The rates is null");

                var context = new CoursesDbContext();

                foreach (var rate in rates.Result.Items)
                {
                    var course = context.Courses.Add(new Course
                    {
                        CurrencyName = rate.CurrencyName,
                        Code = rate.Code,
                        Mid = rate.Mid,
                        EffectiveDate = DateTime.Now
                    });                 
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
