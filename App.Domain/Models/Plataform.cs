using System.Collections.Generic;

namespace App.Domain.Models
{
    public class Plataform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UrlMetric> UrlMetrics { get; set; }
    }
}
