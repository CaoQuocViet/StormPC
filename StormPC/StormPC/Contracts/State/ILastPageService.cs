using StormPC.ViewModels.Shell;

namespace StormPC.Contracts;

public interface ILastPageService
{
    Task<string?> GetLastPageAsync();
    Task SaveLastPageAsync(string pageType);
    Task ClearLastPageAsync();
} 