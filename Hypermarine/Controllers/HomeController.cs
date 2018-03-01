using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypermarine.Data;

namespace Hypermarine.Controllers
{
	public class HomeController : Controller
	{
		private Context context;
		private bool disposed = false;

		public HomeController()
		{
			context = new Context();
		}

		public ActionResult Index()
		{
			var posts = context.Posts
				.Include(p => p.User)
				.OrderBy(p => p.Score)
				.ToList();

			return View(posts);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposed) { return; }
			if (disposing) { context.Dispose();  }
			disposed = true;
			base.Dispose(disposing);
		}
	}
}