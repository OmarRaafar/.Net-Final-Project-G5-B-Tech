using DTOsB.Category;

namespace DTOsB.Controllers
{
    internal class CustomPagedList<T>
    {
        private IEnumerable<GetAllCategoriesDTO> categories;
        private int v;
        private int pageSize;

        public CustomPagedList(IEnumerable<GetAllCategoriesDTO> categories, int v, int pageSize)
        {
            this.categories = categories;
            this.v = v;
            this.pageSize = pageSize;
        }
    }
}