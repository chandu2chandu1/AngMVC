using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngMVC.Models;

namespace AngMVC.api
{
    public class SchoolController : ApiController
    {
        SchoolModel context = new SchoolModel();

        public IHttpActionResult GetSchools()
        {
            var schools = context.Schools.AsEnumerable();
            return Ok(schools);
        }

        [HttpPut]
        public IHttpActionResult UpdateSchool(int id, School school)
        {
            School selectedSchool = context.Schools.First(s => s.ID == school.ID);
            if (selectedSchool == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            selectedSchool.SchoolName = school.SchoolName;
            selectedSchool.City = school.City;

            context.SaveChanges();
            return Ok(selectedSchool);
        }

        [HttpPost]
        //[Route(Name = "~/api/School/Create")]
        public IHttpActionResult CreateSchool(School school)
        {
            if (ModelState.IsValid)
            {
                context.Schools.Add(school);
                context.SaveChanges();
                return Ok(school);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
