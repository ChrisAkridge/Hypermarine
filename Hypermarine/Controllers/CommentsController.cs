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
    public class CommentsController : Controller
    {
		private Context context = new Context();

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Detail(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Post post = GetPostById(id.Value);
			ViewBag.Title = post.Title + " - Hypermarine";
			ViewBag.Post = post;

			return View();
		}

		private Post GetPostById(int id)
		{
			return context.Posts
				.Include(p => p.User)
				.Include(p => p.Comments)
				.Include(p => p.Comments.Select(c => c.User))
				.SingleOrDefault(p => p.Id == id);
		}

		[HttpPost]
		public ActionResult Detail(int? id, Comment comment)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			string username = Request.Cookies["username"].Value;
			int userId = context.Users.SingleOrDefault(u => u.Name == username).Id;

			var dbComment = new Comment()
			{
				UserId = userId,
				PostId = id.Value,
				Text = comment.Text
			};

			context.Comments.Add(dbComment);
			context.SaveChanges();

			Post post = GetPostById(id.Value);
			ViewBag.Title = post.Title + " - Hypermarine";
			ViewBag.Post = post;

			return View();
		}
    }
}