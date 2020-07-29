using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkExample_WebProject.EF;

namespace UnitOfWorkExample_WebProject.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PeopleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.ContactRepository.GetPeopleWithAddress());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await unitOfWork.ContactRepository.GetPersonById(id.GetValueOrDefault());

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                var address = new Address { FullAddress = "A default address" };

                //we are starting a unit of work here (with out any transaction)
                //unitOfWork.ContactRepository.AddPerson(person);
                //unitOfWork.ContactRepository.AddAddress(address);
                unitOfWork.ContactRepository.AddAddressWithPerson(new PersonToAddress { Person = person, Address = address });
                await unitOfWork.SaveAsync();
                //Unit of work end with this save call.

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await unitOfWork.ContactRepository.GetPersonById(id.GetValueOrDefault());
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        ////// POST: People/Edit/5
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Person person)
        ////{
        ////    if (id != person.Id)
        ////    {
        ////        return NotFound();
        ////    }

        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            _context.Update(person);
        ////            await _context.SaveChangesAsync();
        ////        }
        ////        catch (DbUpdateConcurrencyException)
        ////        {
        ////            if (!PersonExists(person.Id))
        ////            {
        ////                return NotFound();
        ////            }
        ////            else
        ////            {
        ////                throw;
        ////            }
        ////        }
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    return View(person);
        ////}

        ////// GET: People/Delete/5
        ////public async Task<IActionResult> Delete(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var person = await _context.People
        ////        .FirstOrDefaultAsync(m => m.Id == id);
        ////    if (person == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return View(person);
        ////}

        ////// POST: People/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> DeleteConfirmed(int id)
        ////{
        ////    var person = await _context.People.FindAsync(id);
        ////    _context.People.Remove(person);
        ////    await _context.SaveChangesAsync();
        ////    return RedirectToAction(nameof(Index));
        ////}

        ////private bool PersonExists(int id)
        ////{
        ////    return _context.People.Any(e => e.Id == id);
        ////}
    }
}
