using System;
namespace App.Domain.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public int Count { get; set; }
    }
}
