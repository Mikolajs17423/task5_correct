using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tk5.Services;

namespace tk5.Controllers

{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IStudentsSerivceDb _dbService;

        public StudentsController(IStudentsSerivceDb db)
        {
            _dbService = db;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("entries/{id}")]
        public IActionResult GetSemester(string id)
        {
            var resp = _dbService.GetSemester(id);
            if (resp != null)
            {
                return Ok(resp);
            }
            else return NotFound("Record has not been found");
        }
    }
}