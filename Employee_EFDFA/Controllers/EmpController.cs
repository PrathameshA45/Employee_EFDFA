using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Employee_EFDFA.Models;

namespace Employee_EFDFA.Controllers
{
    public class EmpController : Controller
    {
        MVCDBEntities dc = new MVCDBEntities();

        // ===============================
        // DISPLAY ALL EMPLOYEES
        // ===============================
        public ViewResult DisplayEmployees()
        {
            var Emps = dc.Employees
                         .Include(e => e.Department)
                         .Where(e => e.Status == true)
                         .ToList();

            return View(Emps);
        }

        // ===============================
        // LIVE SEARCH (AJAX)
        // ===============================
        public PartialViewResult SearchEmployees(string search)
        {
            var Emps = dc.Employees
                         .Include(e => e.Department)
                         .Where(e => e.Status == true);

            if (!string.IsNullOrWhiteSpace(search))
            {
                Emps = Emps.Where(e =>
                    e.Ename.StartsWith(search) ||
                    e.Department.Location.StartsWith(search)
                );
            }

            return PartialView("_EmployeeTable", Emps.ToList());
        }

        // ===============================
        // VIEW SINGLE EMPLOYEE
        // ===============================
        public ViewResult DisplayEmployee(int Eid)
        {
            var Emp = dc.Employees.Find(Eid);
            return View(Emp);
        }

        // ===============================
        // ADD EMPLOYEE
        // ===============================
        [HttpGet]
        public ViewResult AddEmployee()
        {
            ViewBag.Did = new SelectList(dc.Departments, "Did", "Dname");
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

        // ===============================
        // EDIT EMPLOYEE
        // ===============================
        [HttpGet]
        public ViewResult EditEmployee(int Eid)
        {
            Employee Emp = dc.Employees.Find(Eid);
            ViewBag.Did = new SelectList(dc.Departments, "Did", "Dname", Emp.Did);
            return View(Emp);
        }

        [HttpPost]
        public RedirectToRouteResult UpdateEmployee(Employee Emp)
        {
            Emp.Status = true;
            dc.Entry(Emp).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayEmployees");
        }

        // ===============================
        // DELETE (SOFT DELETE)
        // ===============================
        [HttpGet]
        public ViewResult DeleteEmployee(int Eid)
        {
            Employee Emp = dc.Employees.Find(Eid);
            return View(Emp);
        }

        [HttpPost]
        public RedirectToRouteResult DeleteEmployee(Employee Emp)
        {
            Emp.Status = false;
            dc.Entry(Emp).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayEmployees");
        }
    }
}
