using Dapper;
using Microsoft.AspNetCore.Mvc;
using StudentRegistrationProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace StudentRegistrationProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly string _connectionString;
        public StudentController(ConnectionHelper connection)
        {
            _connectionString = connection.Default;
        }
        public IActionResult RegistrationForm(int id)
        {
            var res = new Student();
            using (var con = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT * FROM tbl_Student WHERE Id = @id";
                var students = con.Query<Student>(sqlQuery, new { id }, commandType: CommandType.Text);
                res = students.FirstOrDefault();
            }
            return PartialView(res);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(Student student)
        {
            var res = "Something went wrong";
            using (var con = new SqlConnection(_connectionString))
            {
                var sqlQuery = string.Empty;
                if (student.Id==0)
                {
                    sqlQuery = @"INSERT into tbl_Student(Name ,Father,DOB ,Mobile ,Email ,State,District, Address,Class)
                                 values(@Name ,@Father,@DOB ,@Mobile ,@Email ,@State,@District, @Address,@Class)
                                 Select 'Add Successfully'";
                }
                else
                {
                    sqlQuery = @"UPDATE tbl_Student Set Name = @Name, Father = @Father , DOB = @DOB,Mobile = @Mobile , Email = @Email , 
                                 State = @State , District = @District , Address = @Address, Class = @Class
                                 Select 'Update Successfully'";
                }
                var stu = con.Query<string>(sqlQuery, new
                {
                    student.Id,
                    student.Name,
                    student.Email,
                    student.DOB,
                    student.Mobile,
                    student.Address,
                    student.Class,
                    student.District,
                    student.Father,
                    student.State

                }, commandType: CommandType.Text);
                res = stu.FirstOrDefault() ?? res;
            }
            return Json(res);
        }
        public IActionResult Get(string mobileNo)
        {
            var res = new List<Student>();
            using (var con = new SqlConnection(_connectionString))
            {
                var sqlQuery = "Select * from tbl_Student WHERE mobile = @mobileNo OR @mobileNo = ''";
                var lists = con.Query<Student>(sqlQuery, new
                {
                    mobileNo = mobileNo ?? string.Empty
                }, commandType: CommandType.Text);
                res = lists.ToList();
            }
            return PartialView(res);
        }
        public IActionResult Delete(int Id)
        {
            var res = new Student();
            using (var con = new SqlConnection(_connectionString))
            {
                var sqlQuery = "Delete from tbl_Student where Id = @Id";
                var del = con.Query<Student>(sqlQuery, new { Id },
                 commandType: CommandType.Text);
            }
            return Json(res);
        }
    }
}
