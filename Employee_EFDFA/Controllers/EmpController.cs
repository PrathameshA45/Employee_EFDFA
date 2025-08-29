    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Employee_EFDFA.Models;
    namespace Employee_EFDFA.Controllers
    {
        public class EmpController : Controller
        {
          MVCDBEntities dc = new MVCDBEntities();
            public ViewResult DisplayEmployees(string search)
            {

            var Emps = dc.Employees.Where(e => e.Status == true);

            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();

                Emps = Emps.Where(e =>
                    e.Ename.ToLower().Contains(lowerSearch) ||
                    e.Department.Location.ToLower().Contains(lowerSearch)
                );
            }
            return View(Emps.ToList());
        }
            public ViewResult DisplayEmployee(int Eid)
            {
                var Emp = dc.Employees.Find(Eid);
                return View(Emp);
            }
            [HttpGet]
            public ViewResult AddEmployee()
            {
                ViewBag.Did = new SelectList(dc.Departments, "Did", "Dname", "Select Departrment");
                return View();
            }
            [HttpPost]
            public RedirectToRouteResult AddEmployee(Employee Emp)
            {
                Emp.Status = true;
                dc.Employees.Add(Emp);
                dc.SaveChanges();
                return RedirectToAction("DisplayEmployees");
            }
            [HttpGet]
            public ViewResult EditEmployee(int Eid)
            {
                Employee Emp = dc.Employees.Find(Eid);
                ViewBag.Did = new SelectList(dc.Departments, "Did", "Dname", Emp.Did);
                return View(Emp);
            }
            [HttpPost]
            public RedirectToRouteResult UpdateEmployee (Employee Emp)
            {
                Emp.Status = true;
                dc.Entry(Emp).State = EntityState.Modified;
                dc.SaveChanges();
                return RedirectToAction("DisplayEmployees");
            }
            public ViewResult DeleteEmployee(int Eid)
            {
                Employee Emp = dc.Employees.Find(Eid);
                return View(Emp);
            }
            [HttpPost]
            public RedirectToRouteResult DeleteEmployee(Employee Emp)
            {
                //If we want to update the status of employee use the below code:
                dc.Entry(Emp).State = EntityState.Modified;
                //If we want to delete the record permanently comment the above statement and un-comment the below:
                //dc.Entry(Emp).State = EntityState.Deleted;
                dc.SaveChanges();
                return RedirectToAction("DisplayEmployees");
            }


        }
    }