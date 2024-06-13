using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using EmployeeDataAccess;

namespace MyPracAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        [BasicAuthentication]
        public IEnumerable<EmployeeData> GetEmployees() 
        {
            using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities()) 
            {
                //return entities.EmployeeDatas.ToList();
                return entities.EmployeeDatas.ToList();
            }
        }
    }
}