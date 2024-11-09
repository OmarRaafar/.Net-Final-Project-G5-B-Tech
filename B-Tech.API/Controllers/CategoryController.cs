using ApplicationB.Services_B.Category;
using ApplicationB.Services_B.General;
using ApplicationB.Services_B.Product;
using AutoMapper;
using DTOsB.Category;
using DTOsB.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B_Tech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductService productService, ICategoryService categoryService, ILanguageService languageService, IMapper mapper, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _languageService = languageService;
            _mapper = mapper;
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Category/Search?name=categoryName
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            var result = await _categoryService.GetCategoryByNameAsync(name);

            if (!result.IsSuccess)
            {
                return NotFound(result.Msg);
            }

            return Ok(result.Entity);
        }

        // GET: api/Category/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                if (category.Entity == null)
                {
                    return NotFound($"Category with id {id} not found");
                }

                var categoryDto = _mapper.Map<GetAllCategoriesDTO>(category.Entity);

                return Ok(categoryDto);
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        public async Task<IActionResult> GetProductsByCategoryName(string categoryName)
        {
            try
            {
                var products = await _categoryService.GetProductsByCategoryNameAsync(categoryName);
                if (products == null || !products.Any())
                {
                    return NotFound($"No products found for category: {categoryName}");
                }
                return Ok(products);
            }
            catch (Exception ex)
                result = await _categoryService.GetAllCategoriesAsync();
            }

            if (result.IsSuccess)
            {
                return Ok(result.Entity);
                return BadRequest(new { message = ex.Message });
            }
            return BadRequest(result.Msg);
        //GET: api/Category/FilterByLanguage?languageId=2
        [HttpGet("FilterByLanguage")]
        public async Task<IActionResult> FilterByLanguage(int languageId)
        {
            ResultView<IEnumerable<GetAllCategoriesDTO>> result;

            if (languageId > 0)
            {

        // GET: api/Category/GetProductsByCategoryName/{categoryName}
        [HttpGet("GetProductsByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategoryName(string categoryName)
        {
            var result = await _productCategoryService.GetProductsByCategoryNameAsync(categoryName);

            if (!result.IsSuccess)
            {
                return NotFound(result.Msg);
            }

            return Ok(result.Entity);
        }
            }
        // GET: api/Category/GetProductsByCategoryId/{categoryId}
        [HttpGet("GetProductsByCategoryId/{categoryId}")]
            {
                categories = await _categoryService.GetAllCategoriesAsync();
            }

            return Ok(categories); 
        }
        //GET: api/Category/GetLanguages
        [HttpGet("GetLanguages")]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _languageService.GetAllLanguagesAsync();
            return Ok(languages);
        }
    

         [HttpGet("GetProductsByCategoryId/{categoryId}")]
            var result = await _productCategoryService.GetMainCategoriesAsync();

            if (!result.IsSuccess)
            {
                return NotFound(result.Msg);
            }

            return Ok(result.Entity);
        }

        // GET: api/Category/GetSubCategories
        [HttpGet("GetSubCategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            var result = await _productCategoryService.GetSubCategoriesAsync();

            if (!result.IsSuccess)
        // GET: api/Category/GetMainCategories
                return NotFound(result.Msg);
            }

            return Ok(result.Entity);
        }
            try
        //api/Category/subcategories
        [HttpGet("subcategories/{id}")]
        public async Task<IActionResult> GetSubCategoriesById(int id)
        {
            var result = await _productCategoryService.GetSubCategoriesByMainCategoryIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Msg); // Return a 404 if not found
            }
                var mainCategories = await _categoryService.GetMainCategoriesAsync();
            return Ok(result.Entity); // Return a 200 with the entity
        }
    }
}
            {
                return NotFound(result.Msg);
            }

                return Ok(mainCategories); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
