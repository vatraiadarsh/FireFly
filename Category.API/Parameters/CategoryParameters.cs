using Utils;

namespace Category.API.Parameters
{
    public class CategoryParameters : RequestParameters
    {
        public CategoryParameters()
        {
            OrderBy = "name";
        }
        public string SearchTerm { get; set; }
    }
}