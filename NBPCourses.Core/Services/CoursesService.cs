using NBPCourses.Core.Integrations.Interfaces;
using NBPCourses.Core.Interfaces;
using NPBCourses.Persistence.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NBPCourses.Core.Services
{
    public class CoursesService : ICoursesService
    {
        private IIntegrationNBPService _integrationNBPService;

        public CoursesService(IIntegrationNBPService integrationNBPService)
        {
            _integrationNBPService = integrationNBPService;
        }

        public async Task AddCourses()
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
