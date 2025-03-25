using StormPC.ViewModels.Shell;

namespace StormPC.Contracts.Services;

public interface ILastPageService
{
    Task<string?> GetLastPageAsync();
    Task SaveLastPageAsync(string pageType);
    Task ClearLastPageAsync();
} 