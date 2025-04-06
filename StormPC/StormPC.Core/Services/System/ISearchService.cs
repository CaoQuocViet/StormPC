using StormPC.Core.Models.System.Search;

namespace StormPC.Core.Services.System;

public interface ISearchService
{
    Task<SearchResults> SearchAsync(string query, int page = 1, int pageSize = 20);
    Task<SearchResults> SearchByTypeAsync(string query, string type, int page = 1, int pageSize = 20);
} 