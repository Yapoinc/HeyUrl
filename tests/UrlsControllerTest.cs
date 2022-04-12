using App.Data;
using App.Library;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace tests
{
    public class UrlsControllerTest
    {
        AppDbContext appContext;
        UrlAction urlActions;
        [SetUp]
        public void Setup()
        {

            var connectionString
                = "Server=.\\SQLEXPRESS2017;Database=HeyUrl;User Id=TestHeyUrl;Password=Admin*123;";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString);
            appContext = new AppDbContext(optionsBuilder.Options);

            urlActions = new UrlAction(appContext);
        }

        [Test]
        public async Task GetUrlSuccess()
        {
            var url = await urlActions.GetUrlByShortName("AAAAA");
            Assert.That(url, Is.Not.Null);
        }

        [Test]
        public async Task GetUrlFail()
        {
            var url = await urlActions.GetUrlByShortName("M");
            Assert.That(url, Is.Null);
        }

        [Test]
        public async Task SaveNewUrl()
        {
            var originalUrl = "https://www.hugedomains.com/domain_profile.cfm?d=testyourdomain.com";
            var url = await urlActions.RegisterUrl(originalUrl);
            var isCreated = false;
            if (url is not null)
            {
                isCreated = true;
                appContext.Urls.Remove(url);
                await appContext.SaveChangesAsync();
            }
            Assert.That(isCreated, Is.EqualTo(true));
        }

        [Test]
        public async Task SaveWrongNewUrl()
        {
            var originalUrl = "alksdj aklsjdlad asdljasdl";
            var url = await urlActions.RegisterUrl(originalUrl);
            var notCreated = (url is null);

            Assert.That(notCreated, Is.EqualTo(true));
        }
    }
}