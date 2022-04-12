using App.Data;
using App.Domain.Models;
using App.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library
{
    public class UrlAction
    {
        private Random rnd = new Random();
        private readonly AppDbContext context;
        private string ErrorMessage { set; get; } = "";

        public UrlAction(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<HomeViewModel> GetUrlView()
        {
            HomeViewModel result = new HomeViewModel
            {
                UrlView = await context.UrlView.ToListAsync(),
                NewUrl = new Domain.Models.Url()
            };
            return result;
        }

        public string GetError()
        {
            return ErrorMessage;
        }

        public async Task<Url> RegisterVisit(string shortUrl, string browserName, string plataformName)
        {
            var url = await GetUrlByShortName(shortUrl);
            var browserId = await GetBrowserId(browserName);
            var plataformId = await GetPlataformId(plataformName);
            UrlMetric urlMetric = new UrlMetric
            {
                UrlId = url.Id,
                BrowserId = browserId,
                PlataformId = plataformId,
                DateClicked = DateTime.UtcNow
            };
            context.UrlMetrics.Add(urlMetric);
            await context.SaveChangesAsync();
            return url;
        }

        public async Task<Url> GetUrlByShortName(string shortUrl)
        {
            var url = await context.Urls.FirstOrDefaultAsync(e => e.ShortUrl == shortUrl);
            return url;
        }

        private async Task<int> GetBrowserId(string browserName)
        {
            var browser = await context.Browsers.FirstOrDefaultAsync(e => e.Name == browserName);
            if (browser is null)
            {
                browser = new Browser { Name = browserName };
                context.Add(browser);
                await context.SaveChangesAsync();
            }
            return browser.Id;


        }

        private async Task<int> GetPlataformId(string plataformName)
        {
            var plataform = await context.Plataforms.FirstOrDefaultAsync(e => e.Name == plataformName);
            if (plataform is null)
            {
                plataform = new Plataform { Name = plataformName };
                context.Add(plataform);
                await context.SaveChangesAsync();
            }
            return plataform.Id;
        }

        private bool IsValidUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        public async Task<Url> RegisterUrl(string originalUrl)
        {
            ErrorMessage = "";
            if(string.IsNullOrEmpty(originalUrl))
            {
                ErrorMessage = "Url cannot be empty";
                return null;
            }
            if(!IsValidUrl(originalUrl))
            {
                ErrorMessage = "Url is not valid";
                return null;

            }

            var url = new Url();
            for (int i = 0; i < 100; i++)
            {
                url = null;
                var shortUrl = GetRandomShortUrl();
                url = await context.Urls.FirstOrDefaultAsync(e => e.ShortUrl == shortUrl);
                if (url is null)
                {
                    url = new Url
                    {
                        ShortUrl = shortUrl,
                        DateCreated = DateTime.UtcNow,
                        OriginalUrl = originalUrl,
                    };
                    context.Urls.Add(url);
                    await context.SaveChangesAsync();
                    break;
                }
            }
            return url;
        }

        private string GetRandomShortUrl()
        {

            string result = "";
            for (int i = 0; i < 5; i++)
            {
                result += (char)rnd.Next(65, 91);
            }
            return result;
        }

        private async Task<Dictionary<string, int>> GetClickDays(string shortUrl)
        {
            var month = DateTime.UtcNow.Month;
            var urlMetrics = await this.context.UrlMetrics
               .Where(e => e.Url.ShortUrl == shortUrl && e.DateClicked.Month == month)
               .ToListAsync();

            var gMetric = urlMetrics
                .GroupBy(e => e.DateClicked.Day)
                .OrderBy(e => e.Key)
                .ToList();
            Dictionary<string, int> result = new Dictionary<string, int>();
            gMetric.ForEach(metricItem =>
            {
                result.Add(metricItem.Key.ToString(), metricItem.Count());
            });
            return result;
        }

        private async Task<Dictionary<string, int>> GetBrowserUsage(string shortUrl)
        {
            var browsers = context.Browsers.ToList();
            var urlMetrics = await this.context.UrlMetrics
               .Where(e => e.Url.ShortUrl == shortUrl)
               .ToListAsync();

            var gMetric = urlMetrics
                .GroupBy(e => e.BrowserId)
                .ToList();

            Dictionary<string, int> result = new Dictionary<string, int>();
            gMetric.ForEach(metricItem =>
            {
                var name = browsers.FirstOrDefault(b => b.Id == metricItem.Key).Name;
                result.Add(name, metricItem.Count());
            });
            return result;
        }

        private async Task<Dictionary<string, int>> GetPlataformUsage(string shortUrl)
        {
            var plataforms = context.Plataforms.ToList();
            var urlMetrics = await this.context.UrlMetrics
               .Where(e => e.Url.ShortUrl == shortUrl)
               .ToListAsync();

            var gMetric = urlMetrics
                .GroupBy(e => e.PlataformId)
                .ToList();

            Dictionary<string, int> result = new Dictionary<string, int>();
            gMetric.ForEach(metricItem =>
            {
                var name = plataforms.FirstOrDefault(b => b.Id == metricItem.Key).Name;
                result.Add(name, metricItem.Count());
            });
            return result;
        }


        public async Task<ShowViewModel> GetData(string shortUrl)
        {
            Url url = await GetUrlByShortName(shortUrl);
            if (url is null)
                return null;

            ShowViewModel showViewModel = new ShowViewModel();
            showViewModel.DailyClicks = await GetClickDays(shortUrl);
            showViewModel.BrowseClicks = await GetBrowserUsage(shortUrl);
            showViewModel.PlatformClicks = await GetPlataformUsage(shortUrl);
            url.Count = showViewModel.DailyClicks.Sum(e => e.Value);
            showViewModel.Url = url;
            return showViewModel;
        }



    }
}
