using ByteComputerTest.Core;
using ByteComputerTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteComputerTest.Web.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ByteComputerTestDbContext _byteComputerTestDbContext;

        public CandidateController(ByteComputerTestDbContext byteComputerTestDbContext)
        {
            this._byteComputerTestDbContext = byteComputerTestDbContext;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _byteComputerTestDbContext.Candidates.ToListAsync());
        }
        public async Task<IActionResult> Delete(int id)
        {
            var selectedCandidate = await _byteComputerTestDbContext.Candidates.FirstOrDefaultAsync(p => p.Id == id);

            return View(selectedCandidate);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the candidate failed, invalid ID!";
                return View();
            }
            var selectedCandidate = await _byteComputerTestDbContext.Candidates.Include(p => p.Degrees).FirstOrDefaultAsync(p => p.Id == id);

            try
            {
                //await _categoryRepository.DeleteCategoryAsync(CategoryId.Value);
                if (selectedCandidate != null && selectedCandidate.Degrees.Count == 0)
                {
                    _byteComputerTestDbContext.Remove(selectedCandidate);
                    await _byteComputerTestDbContext.SaveChangesAsync();
                    TempData["CandidateDeleted"] = "candidate deleted successfully!";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["ErrorMessage"] = $"Deleting the candidate failed, because he has Degrees associated. Please delete them first.";
                    return View(selectedCandidate);

                }

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the candidate failed, please try again! Error: {ex.Message}";
            }


            return View(selectedCandidate);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedCandidate = await _byteComputerTestDbContext.Candidates.AsNoTracking().Include(p => p.Degrees).FirstOrDefaultAsync(p => p.Id == id);


            return View(selectedCandidate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Candidate candidate, IFormFile file)
        {
            try
            {
                ModelState.Remove("file");
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (!GetFleExtensions(candidate, file)){
                            return View(candidate);
                        }
                    
                        CreateCVByteArray(candidate, file);

                    }

                    if (CheckEmailExistInAnotherCandidate(candidate))
                    {
                        ModelState.AddModelError(nameof(Candidate.Email), "Adding the candidate failed the Email exits already, please try again! Error: ");
                        return View(candidate);
                    }

                    candidate.CreationTime = DateTime.Now;

                    _byteComputerTestDbContext.Update(candidate);
                    await _byteComputerTestDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the candidate failed, please try again! Error: {ex.Message}");
            }

            return View(candidate);
        }

        public async Task<IActionResult> Add(int? id)
        {

            return View();
        }


        [HttpPost]
        [RequestSizeLimit(10000000)] // Limit to 10 MB
        public async Task<IActionResult> Add([Bind("LastName,FirstName,Email,Mobile,SelectedDegree,CV")] Candidate candidate, IFormFile file)
        {
            try
            {
                ModelState.Remove("file");
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (!GetFleExtensions(candidate, file))
                        {
                            return View(candidate);
                        }

                        CreateCVByteArray(candidate, file);

                    }

                    if (CheckEmailExistInAnotherCandidate(candidate))
                    {
                        ModelState.AddModelError(nameof(Candidate.Email), "Adding the candidate failed the Email exits already, please try again! Error: ");
                        return View(candidate);
                    }
                    candidate.CreationTime = DateTime.Now;

                    _byteComputerTestDbContext.Add(candidate);
                    await _byteComputerTestDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Adding the candidate failed, please try again! Error: {ex.Message}");
            }

            return View(candidate);

        }

        private static void CreateCVByteArray(Candidate candidate, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(memoryStream);
                candidate.CV = memoryStream.ToArray();
                candidate.CVFilename = file.FileName;
                // use filebyteArr for saving in database
            }
        }

        private bool GetFleExtensions(Candidate candidate, IFormFile file)
        {
            var permittedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Invalid file type.");
                return false;
            }
            return true;
        }

        private bool CheckEmailExistInAnotherCandidate(Candidate candidate)
        {
            return _byteComputerTestDbContext.Candidates.Any(p => p.Id != candidate.Id && p.Email == candidate.Email);

        }


        private bool CandidateExists(int candidateId)
        {
            return _byteComputerTestDbContext.Candidates.Any(p => p.Id == candidateId);
        }
    }
}


