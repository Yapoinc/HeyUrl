using System;

namespace App.Domain.Models
{
    public class UrlMetric
    {
        public int Id { get; set; }
        public DateTime DateClicked { get; set; }
        public Url Url { get; set; }
        public Guid UrlId { get; set; }

        public Browser Browser { get; set; }
        public int BrowserId { get; set; }

        public Plataform Plataform { get; set; }
        public int PlataformId { get; set; }

    }
}
