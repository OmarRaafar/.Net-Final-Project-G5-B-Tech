using ApplicationB.Services_B.General;
using ApplicationB.Services_B.Product;
using DTOsB.Product;
using DTOsB.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DTOsB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService productService;
        private readonly ILanguageService languageService;

        public ProductController(IProductService _productService, IWebHostEnvironment _webHostEnvironment,
            ILanguageService _languageService)
        {
            productService = _productService;
            webHostEnvironment = _webHostEnvironment;
            languageService = _languageService;
        }


        public async Task<IActionResult> Index(int selectedLanguageId=2)
        {
           
            var availableLanguages = await languageService.GetAllLanguagesAsync();
            ViewBag.AvailableLanguages = new SelectList(availableLanguages, "Id", "Code");

       
           await languageService.SetUserSelectedLanguageAsync(selectedLanguageId);
            ViewBag.SelectedLanguageId = selectedLanguageId;


            var products = await productService.GetAllProductsAsync();
            if (products == null)
            {
                // Handle null case for products
                return View("Error", "Products not available.");
            }
            return View(products);
        }


        //public async Task<IActionResult> Index(int pageNumber = 1, int count = 10, int selectedLanguageId = 2)
        //{
        //    var products = await productService.GetAllPaginatedByLanguageAsync(pageNumber, count, selectedLanguageId);
        //    var availableLanguages = await languageService.GetAllLanguagesAsync();

        //    var viewModel = new ProductViewModelWithLang
        //    {
        //        Products = products.Data,
        //        AvailableLanguages = availableLanguages,
        //        SelectedLanguageId = selectedLanguageId
        //    };

        //    return View(viewModel);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var products = await productService.GetAllProductsAsync();
        //    return View(products); // Assuming Data contains IEnumerable<ProductViewModel>
        //}

        // GET: Products/Create
        public async Task<IActionResult> Create(int selectedLanguageId = 2)
        {
            var availableLanguages = await languageService.GetAllLanguagesAsync();
            ViewBag.AvailableLanguages = new SelectList(availableLanguages, "Id", "Code");


            languageService.SetUserSelectedLanguageAsync(selectedLanguageId);
            ViewBag.SelectedLanguageId = selectedLanguageId;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateOrUpdateDto productDto)
        {
            //if (ModelState.IsValid)
            //{
                var result = await productService.CreateProductAsync(productDto);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Msg);
            //}
            return View(productDto);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await productService.UpdateProductAsync(productDto);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Msg);
            }
            return View(productDto);
        }

        // GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await productService.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(result.Msg);
        }









        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productB = await productService.GetProductByIdAsync(id);

            return View(productB);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var products = await productService.SearchProductsByNameAsync(searchString);
            ViewBag.SearchString = searchString;

            return View("Index", products);
        }
    }
}
