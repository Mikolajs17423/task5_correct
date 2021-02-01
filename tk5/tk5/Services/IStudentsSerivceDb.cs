using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tk5.DTO;
using tk5.Models;

namespace tk5.Services
{
    public interface IStudentsSerivceDb
    {
        IEnumerable<Student> GetStudents();

        IEnumerable<string> GetSemester(string id);
      
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest req);

        bool idExists(string id);

        void logIntoFile(string data);
    }
}