using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tk5.DTO;
using tk5.Models;

namespace tk5.Services
{
    public class SqlServerStudentDbService : IStudentsSerivceDb
    {
        private string _connString = "Data Source=POL1-1028444LT;Initial Catalog=baza;Integrated Security=True;Pooling=False";
        private int _countSt;


        public IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>();
            using var sqlConnection = new SqlConnection(_connString);
            using var command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "select s.IndexNumber, s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                "from Student s " +
                "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                "join Studies st on st.IdStudy = e.IdStudy; ";
            sqlConnection.Open();
            SqlDataReader response = command.ExecuteReader();
            while (response.Read())
            {
                var st = new Student
                {
                    IndexNumber = response["IndexNumber"].ToString(),
                    FirstName = response["FirstName"].ToString(),
                    LastName = response["LastName"].ToString(),
                    Studies = response["Studies"].ToString(),
                    BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                    Semester = int.Parse(response["Semester"].ToString())

                };

                students.Add(st);
            }

            return students;
        }

        public IEnumerable<string> GetSemester(string id)
        {
            using var sqlConnection = new SqlConnection(_connString);
            using var command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "select e.Semester " +
                "from Student s " +
                "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                "where IndexNumber like @index;";
            SqlParameter par = new SqlParameter();
            par.ParameterName = "index";
            par.Value = id;
            command.Parameters.Add(par);
            sqlConnection.Open();
            SqlDataReader response = command.ExecuteReader();
            var entriesList = new List<string>();
            while (response.Read())
                entriesList.Add(response["Semester"].ToString());

            if (entriesList.Count > 0)
            {
                return entriesList;
            }
            else
            {
                return null;
            }
        }

        public bool idExists(string id)
        {

            using (SqlConnection con = new SqlConnection(_connString))
            using (SqlCommand com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT COUNT(1) countSt FROM Student WHERE IndexNumber = @index";
                com.Parameters.AddWithValue("index", id);
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    _countSt = int.Parse(dr["countSt"].ToString());
                }

                if (_countSt > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        public void logIntoFile(string data)
        {
            var sw = new StreamWriter(@"requestsLog.txt");
            sw.WriteLine(data);
            sw.Close();
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest req)
        {
            throw new NotImplementedException();
        }
    }
}