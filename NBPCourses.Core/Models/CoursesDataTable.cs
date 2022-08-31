using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPCourses.Core.Models
{
    public class CoursesDataTable
    {
        public IEnumerable<Course> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
