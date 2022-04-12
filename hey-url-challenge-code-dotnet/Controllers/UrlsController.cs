using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Data;
using App.Domain.Models;
using App.Domain.ViewModels;
using App.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly UrlAction urlAction;

        public UrlsController(
            ILogger<UrlsController> logger,
            IBrowserDetector browserDetector, AppDbContext context)
        {
            urlAction = new UrlAction(context);
            this.browserDetector = browserDetector;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var model = await urlAction.GetUrlView();
            return View(model);
        }

        [Route("/{shortUrl}")]
        public async Task<IActionResult> Visit(string shortUrl)
        {
            var url = await urlAction.RegisterVisit(shortUrl, this.browserDetector.Browser.Name, this.browserDetector.Browser.OS);
            return Redirect(url.OriginalUrl);
        }

        [HttpPost]
        [Route("urls/{CreateUrl}")]
        public async Task<IActionResult> CreateUrl(Url newUrl)
        {
            var url = await urlAction.RegisterUrl(newUrl.OriginalUrl);
            if (url is not null)
                TempData["Notice"] = $"{newUrl.OriginalUrl} has been created!";
            else
                TempData["Notice"] = urlAction.GetError();
            return Redirect("/");
        }


        [Route("urls/{url}")]
        public async Task<IActionResult> Show(string url)
        {
            ShowViewModel showViewModel = await urlAction.GetData(url);
            return View(showViewModel);


        }
       
    }
}