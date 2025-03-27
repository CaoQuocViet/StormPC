namespace StormPC.ViewModels
{
    public interface IPaginatedViewModel
    {
        int CurrentPage { get; set; }
        int TotalPages { get; }
        int PageSize { get; set; }
        void LoadPage(int page);
    }
} 