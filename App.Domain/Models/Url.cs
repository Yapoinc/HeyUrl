using System;
using System.Collections.Generic;

namespace App.Domain.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public int Count { get; set; }
        public List<UrlMetric> UrlMetrics { get; set; }
    }


}
