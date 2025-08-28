using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using Employee_EFDFA.Models;

namespace Employee_EFDFA.Controllers
{
    public class DeptController : Controller
    {
        MVCDBEntities dc = new MVCDBEntities();

        public ViewResult DisplayDept()
        {
            var department = dc.Departments.ToList();
            return View(department);
        }

        public ViewResult ViewDept(int Did)
        {
            var Dept = dc.Departments.Find(Did);
            return View(Dept);
        }
        [HttpGet]
        public ViewResult AddDept()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AddDept(Department Dept)
        {
            dc.Departments.Add(Dept);
            dc.SaveChanges();
            return RedirectToAction("DisplayDept");
        }

        [HttpGet]
        public ViewResult EditDept (int Did)
        {
            Department department = dc.Departments.Find(Did);
            return View(department);
        }
        [HttpPost]
        public RedirectToRouteResult EditDept (Department Dept)
        {
            dc.Entry(Dept).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayDept");
        }
        [HttpGet]
        public ViewResult DeleteDept(int Did)
        {
            var dept = dc.Departments.Find(Did);
            return View(dept); 
        }
        public RedirectToRouteResult DeleteDept (Department Dept)
        {
            var dept = dc.Departments.Find(Dept.Did);
            dc.Departments.Remove(dept);
            dc.SaveChanges();
            return RedirectToAction("DisplayDept");
        }
    }
}