using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace MyPracAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<EmployeeData> GetEmployees() 
        {
            using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities()) {
                
            }
        }
    }
}