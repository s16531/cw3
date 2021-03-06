﻿using System;
using System.Collections.Generic;
using ABD_cw3.Models;

namespace ABD_cw3.DAL
{
    public class MockDbService : IStudentsDbSerivce
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski"},
                new Student{IdStudent=2, FirstName="Anna", LastName="Malewski"},
                new Student{IdStudent=3, FirstName="Krzysztof", LastName="Andrzejewicz"}
            };

        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
