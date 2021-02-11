using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Results;
using GiphyDotNet.Model.Parameters;

namespace giphy_search.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Giphy _giphy;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
    
        [BindProperty(SupportsGet = true)]
        public int Offset { get; set; }

        public GiphySearchResult Result { get; set; }
        public int Next => Offset + 25;
        public int Previous => Math.Min(0, Offset - 25);
        public bool HasSearch => !string.IsNullOrWhiteSpace(Search);
        public bool HasNext => Next <= Result.Pagination.TotalCount;
        public bool HasPrevious => Offset > 0;

        public IndexModel(ILogger<IndexModel> logger, Giphy giphy)
        {
            _logger = logger;
            _giphy = giphy;
        }

        public async Task OnGet()
        {
            Offset = Math.Max(0, Offset);
        
            if (HasSearch)
            {
                Result = await _giphy.GifSearch(new  SearchParameter
                {
                    Query = Search,
                    Offset = Offset
                });
            }
        }
    }
}
