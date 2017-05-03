using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Part_1.Models;

namespace Part_1.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        // GET: Employees
        public ActionResult Index(string sortBy, string searchTerm, string searchBy)
        {
            var employees = from employee in db.Employees
                            select employee;

            if (!String.IsNullOrEmpty(searchBy))
            {
                switch(searchBy)
                {
                    case "LastName":
                        employees = employees.Where(employee => employee.LastName.Contains(searchTerm));
                        break;
                    case "FirstName":
                        employees = employees.Where(employee => employee.FirstName.Contains(searchTerm));
                        break;
                    case "Department":
                        employees = employees.Where(employee => employee.Department.Contains(searchTerm));
                        break;
                    case "Location":
                        employees = employees.Where(employee => employee.Location.Contains(searchTerm));
                        break;
                }
            }

            switch(sortBy)
            {
                case "LastName":
                    employees = employees.OrderBy(employee => employee.LastName);
                    break;
                case "FirstName":
                    employees = employees.OrderBy(employee => employee.FirstName);
                    break;
                case "Department":
                    employees = employees.OrderBy(employee => employee.Department);
                    break;
                case "Performance":
                    employees = employees.OrderBy(employee => employee.Performance);
                    break;
                default:
                    employees = employees.OrderBy(employee => employee.LastName);
                    break;
            }
            return View(employees.ToList());
        }

        public ActionResult AllEmployees()
        {
            var employeeNames = from employee in db.Employees
                                select employee.LastName + ", " + employee.FirstName;
            ViewBag.employeeNames = new SelectList((from employee in db.Employees
                                                    select new
                                                    {
                                                        EmployeeId = employee.EmployeeId,
                                                        EmployeeName = employee.LastName + ", " + employee.FirstName
                                                    }),
                                                    "EmployeeId",
                                                    "EmployeeName");
            return View(employeeNames.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,LastName,FirstName,Salary,Gender,Department,Location,Performance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,LastName,FirstName,Salary,Gender,Department,Location,Performance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
