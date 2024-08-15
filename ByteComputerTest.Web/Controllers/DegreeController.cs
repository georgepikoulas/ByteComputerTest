using ByteComputerTest.Core;
using ByteComputerTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Signing.DerEncoding;
using static System.Net.Mime.MediaTypeNames;

namespace ByteComputerTest.Web.Controllers
{
    public class DegreeController : Controller
    {
        private readonly ByteComputerTestDbContext _byteComputerTestDbContext;
        public List<SelectListItem> candidates { get; set; }
        public DegreeController(ByteComputerTestDbContext byteComputerTestDbContext)
        {
            this._byteComputerTestDbContext = byteComputerTestDbContext;
        }
        // GET: DegreeController
        public async Task<ActionResult> Index()
        {
            return View(await _byteComputerTestDbContext.Degrees.Include(p => p.Candidate).ToListAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            var selectedDegree = await _byteComputerTestDbContext.Degrees.FirstOrDefaultAsync(p => p.Id == id);

            return View(selectedDegree);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Candidates"] = AddViewData();
            ModelState.Remove("Candidate");


            if (id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the degree failed, invalid ID!";
                return View();
            }
            var selectedDegree = await _byteComputerTestDbContext.Degrees.Include(p => p.Candidate).FirstOrDefaultAsync(p => p.Id == id);

            try
            {
                //await _categoryRepository.DeleteCategoryAsync(CategoryId.Value);
                    _byteComputerTestDbContext.Remove(selectedDegree);
                    await _byteComputerTestDbContext.SaveChangesAsync();
                    TempData["CandidateDeleted"] = "candidate deleted successfully!";

                    return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the candidate failed, please try again! Error: {ex.Message}";
            }


            return View(selectedDegree);

        }
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["Candidates"] = AddViewData();

            if (id == null)
            {
                return NotFound();
            }

            var selectedDegree = await _byteComputerTestDbContext.Degrees.AsNoTracking().Include(p => p.Candidate).FirstOrDefaultAsync(p => p.Id == id);


            return View(selectedDegree);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Degree degree)
        {
            try
            {
                ViewData["Candidates"] = AddViewData();
                ModelState.Remove("Candidate");

                if (ModelState.IsValid)
                {
                    degree.CreationTime = DateTime.Now;
                    _byteComputerTestDbContext.Degrees.Update(degree);
                    await _byteComputerTestDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the degree failed, please try again! Error: {ex.Message}");
            }

            return View(degree);
        }

        public async Task<IActionResult> Add(int? id)
        {

            ViewData["Candidates"] = AddViewData();

            return View();
        }

        private List<SelectListItem> AddViewData()
        {
         return   candidates = (from p in _byteComputerTestDbContext.Candidates.AsEnumerable()
                          select new SelectListItem
                          {
                              Text = p.FirstName + " " + p.LastName,
                              Value = p.Id.ToString()
                          }).ToList();

        }

        [HttpPost]

        public async Task<IActionResult> Add([Bind("Name,Candidate,CandidateId")] Degree degree)
        {
            try
            {

               
                ViewData["Candidates"] = AddViewData();

                ModelState.Remove("Candidate");
                if (ModelState.IsValid)
                {
                    
                    degree.CreationTime = DateTime.Now;
                    _byteComputerTestDbContext.Add(degree);
                    await _byteComputerTestDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Adding the degree failed, please try again! Error: {ex.Message}");
            }

            return View(degree);

        }
    }
}

