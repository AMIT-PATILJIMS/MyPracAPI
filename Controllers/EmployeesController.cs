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
                //return (IQueryable<EmployeeData>)entities.EmployeeDatas.ToList();
                var empdata = entities.EmployeeDatas.ToList();

                return empdata;
            }
        }

        //[BasicAuthentication]
        public HttpResponseMessage InsertEmployee([FromBody] EmployeeData employee) 
        {
            try
            {
                using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities())
                {
                    entities.EmployeeDatas.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);

                    message.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());

                    return message;
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }
    }
}