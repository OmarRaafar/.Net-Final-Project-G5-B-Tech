using ApplicationB.Services_B.General;
using DTOsB.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService languageService;

        public LanguageController(ILanguageService _languageService)
        {
            languageService = _languageService;
        }

        public async Task<IActionResult> Index()
        {

            var availableLanguages = await languageService.GetAllLanguagesAsync();

            if (availableLanguages == null)
            {
                // Handle null case for products
                return View("Error", "No Languages available.");
            }
            return View(availableLanguages);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LanguageDto langDto)
        {

            var result = await languageService.CreateProductAsync(langDto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Msg);
                return View(langDto);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {

            var language = await languageService.GetLangByIdAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ResultView<LanguageDto> resultView)
        {

            var langDto = resultView.Entity;

            if (id != langDto.Id)
            {
                return BadRequest();
            }

            var result = await languageService.UpdateProductAsync(langDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lang = await languageService.GetLangByIdAsync(id);
            if (lang == null)
            {
                return NotFound();
            }
            return View(lang);
        }

        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await languageService.DeletelangAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(result.Msg);
        }

    }
}
