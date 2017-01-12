using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebApplicationBasic.Models;
using WebApplicationBasic.Data;
using WebApplicationBasic.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private SchoolContext _context;
        private StudentController _controller;
        public Tests() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
                optionsBuilder.UseInMemoryDatabase();
            _context = new SchoolContext(optionsBuilder.Options);

        }

        private void CreateTestData(SchoolContext context) 
        {
            context.Students.AddRange(
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            );

            context.SaveChanges();
        }

        [Fact]
        public async Task IndexShouldIncludeStudentsData()
        {
            CreateTestData(_context);
            _controller = new StudentController(_context);
            var actionResult = await _controller.Index() as ViewResult;
            Assert.Equal(8, (actionResult.Model as List<Student>).Count);
        }

        [Fact]
        public async Task IndexShouldIncludeStudentsDataWithExactCount()
        {
            try {
            _controller = new StudentController(_context);
            var actionResult = await _controller.Index() as ViewResult;
            Assert.Equal(8, (actionResult.Model as List<Student>).Count);
            } catch(InvalidCastException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}
