using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tk5.DTO;
using tk5.Services;

namespace tk5.Controllers

{

    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentsSerivceDb _service;

        public EnrollmentsController(IStudentsSerivceDb serivice)
        {
            _service = serivice;
        }

        [HttpPost(Name = nameof(EnrollStudent))]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var response = _service.EnrollStudent(request);
            if (response == null) return BadRequest("Cannot create such a student");
            else return CreatedAtAction(nameof(EnrollStudent), response);
        }

      
    }
}
