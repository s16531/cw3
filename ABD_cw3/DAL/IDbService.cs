using System;
using System.Collections.Generic;
using ABD_cw3.Models;

namespace ABD_cw3.DAL
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();
    }
}
