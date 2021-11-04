using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Mvc.Models;
using EmployeeManagement.DataModel;

namespace EmployeeManagement.Mvc.Controllers
{
    public class EmployeeTaskVMsController : Controller
    {
        private readonly EmployeeSystemContext _context;

        public EmployeeTaskVMsController(EmployeeSystemContext context)
        {
            _context = context;
        }

        // GET: EmployeeTaskVMs
        public async Task<IActionResult> Index()
        {
            var list = await _context.EmployeeTasks.ToListAsync();
            var vmList = new List<EmployeeTaskVM>();
            foreach (var x in list)
            {                            
                vmList.Add(BuildVM(x));
            }
            return View(vmList);
        }

        // GET: EmployeeTaskVMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTask = await _context.EmployeeTasks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeTask == null)
            {
                return NotFound();
            }

            return View(BuildVM(employeeTask));
        }

        // GET: EmployeeTaskVMs/Create
        public IActionResult Create()
        {
            var employeeTaskVM = new EmployeeTaskVM();
            employeeTaskVM.Employees = new SelectList(_context.Employees.ToList(),
                        nameof(Employee.ID), nameof(Employee.FullName), null);
            return View(employeeTaskVM);
        }

        // POST: EmployeeTaskVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeID,TaskName,StartTime,Deadline")] EmployeeTaskVM employeeTaskVM)
        {
            if (ModelState.IsValid)
            {
                var entry = _context.Add(new EmployeeTask());
                entry.CurrentValues.SetValues(employeeTaskVM);
                //_context.Add(employeeTaskVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeTaskVM);
        }

        // GET: EmployeeTaskVMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTask = await _context.EmployeeTasks.FindAsync(id);
            if (employeeTask == null)
            {
                return NotFound();
            }
            EmployeeTaskVM vm = BuildVM(employeeTask);
            return View(vm);
        }

        // POST: EmployeeTaskVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeID,TaskName,StartTime,Deadline")] EmployeeTaskVM employeeTaskVM)
        {
            if (id != employeeTaskVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employeeTask = await _context.EmployeeTasks
                   .FirstOrDefaultAsync(m => m.ID == id);
                    _context.Update(employeeTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTaskExists(employeeTaskVM.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeTaskVM);
        }

        // GET: EmployeeTaskVMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTask = await _context.EmployeeTasks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeTask == null)
            {
                return NotFound();
            }

            return View(BuildVM(employeeTask));
        }

        // POST: EmployeeTaskVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);
            _context.EmployeeTasks.Remove(employeeTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTaskExists(int id)
        {
            return _context.EmployeeTasks.Any(e => e.ID == id);
        }
        private EmployeeTaskVM BuildVM(EmployeeTask entry)
        {
            var emp = _context.Employees.Find(entry.EmployeeID);
            return new EmployeeTaskVM()
            {
                EmployeeID = entry.EmployeeID,
                Deadline = entry.Deadline,
                EmployeeName = emp?.FullName,
                ID = entry.ID,
                StartTime = entry.StartTime,
                TaskName = entry.TaskName,
                Employees = new SelectList(_context.Employees.ToList(),
                        nameof(Employee.ID), nameof(Employee.FullName), entry.EmployeeID)
            };
        }
    }
}
