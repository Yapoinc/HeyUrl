using System.Collections.Generic;
using App.Domain.Models;

namespace App.Domain.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Url> Urls { get; set; }
        public Url NewUrl { get; set; }
    }
}
