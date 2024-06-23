using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
//using System.Web.Mvc;
using EmployeeDataAccess;

namespace MyPracAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        // [BasicAuthentication]
        /*
        [HttpGet]
        public IEnumerable<EmployeeData> FetchListOfEmployees() 
        {
            using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities()) 
            {
                //return entities.EmployeeDatas.ToList();
                //return (IQueryable<EmployeeData>)entities.EmployeeDatas.ToList();
                var empdata = entities.EmployeeDatas.ToList();

                return empdata;
            }
        }
        */

        [HttpGet]
        public IEnumerable<EmployeeData> MyEmployeesByGender(string gender="All")
        {
            using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities())
            {
                if (gender.ToLower() == "all")
                {
                    return entities.EmployeeDatas.ToList();
                }
                else if (gender.ToLower() == "male")
                {
                    return entities.EmployeeDatas.Where(emp => emp.Gender.ToLower() == "male");
                }
                else 
                {
                    return entities.EmployeeDatas.Where(emp => emp.Gender.ToLower() == "female");
                }
            }
        }

        [BasicAuthentication]
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


        public HttpResponseMessage Delete(int id)
        {
            using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities())
            {
                entities.EmployeeDatas.Remove(entities.EmployeeDatas.Where(x => x.Id == id).FirstOrDefault());

                var message = Request.CreateResponse(HttpStatusCode.Accepted, id);

                entities.SaveChanges();

                return message;
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]EmployeeData employee)
        {
            try
            {
                using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities())
                {
                    var entity = entities.EmployeeDatas.FirstOrDefault(e => e.Id == id);

                    //var message = Request.CreateResponse(HttpStatusCode.);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + "not found to update");
                    }

                    entity.EmployeeName = employee.EmployeeName;
                    entity.Salary = employee.Salary;
                    entity.Gender = employee.Gender;
                    entity.ManagerId = employee.ManagerId;

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}