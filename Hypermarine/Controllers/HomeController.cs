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

		public ActionResult Login()
		{
			ViewBag.HeaderText = "Login";

			return View();
		}

		[HttpPost]
		[ActionName("Login")]
		public ActionResult LoginPost()
		{
			var username = Request.Form["username"];

			if (context.Users.Any(u => u.Name == username))
			{
				LoginAs(username);
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.ErrorText = "No user is named " + username;
				return View();
			}
		}

		private void LoginAs(string username)
		{
			Response.Cookies["username"].Value = username;
			Response.Cookies["username"].Expires = DateTime.Now.AddDays(1d);
		}

		public ActionResult Logout()
		{
			if (Request.Cookies["username"] != null)
			{
				var myCookie = new HttpCookie("username");
				myCookie.Expires = DateTime.Now.AddDays(-1d);
				Response.Cookies.Add(myCookie);
			}

			return RedirectToAction("Index");
		}

		public ActionResult Signup()
		{
			ViewBag.HeaderText = "Sign Up";

			return View("Login");
		}

		[HttpPost]
		[ActionName("Signup")]
		public ActionResult SignupPost()
		{
			// WYLO: add new post page, plus add comment form for logged in users on comment
			// detail page

			string username = Request.Form["username"];

			if (context.Users.Any(u => u.Name == username))
			{
				ViewBag.ErrorText = "A user with name " + username + " already exists.";
				return View("Login");
			}
			else
			{
				context.Users.Add(new Models.User()
				{
					Name = username
				});
				context.SaveChanges();

				LoginAs(username);
				return RedirectToAction("Index");
			}
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