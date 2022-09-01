using NBPCourses.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPCourses.Core.Interfaces
{
    public interface ICoursesService
    {
        Task<CoursesDataTable> GetCoursesAsync();
        Task AddCoursesAsync();
    }
}
