using System.Collections.Generic;

namespace App.Domain.Models
{
    public class Browser {
        public int Id { get; set; }
        public string   Name { get; set; }
        public List<UrlMetric> UrlMetrics { get; set; }
    }
}
