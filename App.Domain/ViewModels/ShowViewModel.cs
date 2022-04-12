using System.Collections.Generic;
using App.Domain.Models;

namespace App.Domain.ViewModels
{
    public class ShowViewModel
    {
        public Url Url { get; set; }
        public Dictionary<string, int> DailyClicks { get; set; }
        public Dictionary<string, int> BrowseClicks { get; set; }
        public Dictionary<string, int> PlatformClicks { get; set; }
    }
}