namespace LWSCSecondProject.Models
{
    public class PagedResultViewModel<T> where T : class
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }

        public List<T> Data { get; set; }
    }
}
