using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hypermarine.Data;

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

			var post = context.Posts
				.Include(p => p.User)
				.Include(p => p.Comments)
				.SingleOrDefault(p => p.Id == id.Value);

			ViewBag.Title = post.Title + " - Hypermarine";

			return View(post);
		}
    }
}