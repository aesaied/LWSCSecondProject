namespace LWSCSecondProject.Models
{
#nullable disable
    public class DataTableFilter
    {

        public string Draw { get; set; }

        public string SortColumn { get; set; }

        public string SortDirection { get; set; }


        public int PageSize { get; set; } = 10;

        public int SkipCount { get; set; } = 0;

        public string SearchText { get; set; }
    }
}
