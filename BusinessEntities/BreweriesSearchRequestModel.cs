

namespace BusinessEntities
{
    public class BreweriesSearchRequestModel
    {
        public string search {  get; set; }
        public string sortColumn { get; set; } = string.Empty;
        public string sortOrder { get; set; } = string.Empty;
    }
}
