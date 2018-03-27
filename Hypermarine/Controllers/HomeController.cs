using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hypermarine.Data;
using Hypermarine.Models;

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
				.Include(p => p.Comments)
				.OrderByDescending(p => p.Score)
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
			string username = Request.Form["username"];

			if (context.Users.Any(u => u.Name == username))
			{
				ViewBag.ErrorText = "A user with name " + username + " already exists.";
				return View("Login");
			}
			else
			{
				context.Users.Add(new User()
				{
					Name = username
				});
				context.SaveChanges();

				LoginAs(username);
				return RedirectToAction("Index");
			}
		}

		public ActionResult NewPost()
		{
			if (Request.Cookies["username"] == null)
			{
				return RedirectToAction("Login");
			}

			return View();
		}

		[HttpPost]
		public ActionResult NewPost(Post post)
		{
			var username = Request.Cookies["username"].Value;

			var dbPost = new Post()
			{
				Title = post.Title,
				Link = post.Link,
				PostedOn = DateTime.Now,
				UserId = context.Users.SingleOrDefault(u => u.Name == username).Id
			};

			context.Posts.Add(dbPost);
			context.SaveChanges();

			return Redirect("~/Comments/Detail/" + dbPost.Id.ToString());
		}

		public ActionResult EditPost(int? id)
		{
			if (Request.Cookies["username"] == null)
			{
				return RedirectToAction("Login");
			}
			else if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var post = context.Posts.Single(p => p.Id == id);
			return View(post);
		}

		[HttpPost]
		public ActionResult EditPost(Post post)
		{
			var dbPost = context.Posts.Single(p => p.Id == post.Id);
			dbPost.Title = post.Title;
			dbPost.Link = post.Link;
			context.SaveChanges();

			return Redirect("~/Comments/Detail/" + dbPost.Id.ToString());
		}

		public ActionResult DeletePost(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var post = context.Posts.Single(p => p.Id == id);

			var postComments = context.Comments.Where(c => c.PostId == id);
			foreach (var postComment in postComments)
			{
				context.Entry(postComment).State = EntityState.Deleted;
			}

			context.Entry(post).State = EntityState.Deleted;
			context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult EditComment(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var dbComment = context.Comments.Single(c => c.Id == id.Value);

			return View(dbComment);
		}

		[HttpPost]
		public ActionResult EditComment(int? id, Comment comment)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var dbComment = context.Comments.Single(c => c.Id == id.Value);
			dbComment.Text = comment.Text;
			context.SaveChanges();

			return Redirect("~/Comments/Detail/" + dbComment.PostId.ToString());
		}

		public ActionResult DeleteComment(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var dbComment = context.Comments.Single(c => c.Id == id.Value);
			context.Entry(dbComment).State = EntityState.Deleted;
			context.SaveChanges();

			return Redirect("~/Comments/Detail/" + dbComment.PostId.ToString());
		}

		public ActionResult Vote(int? id, string direction)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var dbPost = context.Posts.Single(p => p.Id == id.Value);
			if (direction == "p") { dbPost.Score++; }
			else { dbPost.Score--; }
			context.SaveChanges();

			return Content(dbPost.Score.ToString());
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