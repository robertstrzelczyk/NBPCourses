using NBPCourses.Core.Integrations.Models;
using NBPCourses.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPCourses.Core.Integrations.Interfaces
{
    public interface IIntegrationNBPService
    {
        Task <CoursesDataTable> GetCoursesAsync();
    }
}
