using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hypermarine.Models;

namespace Hypermarine.Data
{
	public sealed class ModelInitializer : DropCreateDatabaseAlways<Context>
	{
		public static void Initialize()
		{
			Database.SetInitializer(new ModelInitializer());
		}

		protected override void Seed(Context context)
		{
			context.Users.Add(new User() { Id = 1, Name = "Admin" });
			context.Users.Add(new User() { Id = 2, Name = "johndoe" });
			context.Users.Add(new User() { Id = 3, Name = "xXxMETAL_FAN_1986xXx" });

			context.Posts.Add(new Post() { Id = 1, Title = "Hello, world!", Link = "http://example.com", UserId = 1, Score = 1200, PostedOn = DateTime.Now });
			context.Posts.Add(new Post() { Id = 2, Title = "Social Security", Link = "https://www.ssa.gov", UserId = 2, Score = 2, PostedOn = DateTime.Now - TimeSpan.FromHours(1d) });
			context.Posts.Add(new Post() { Id = 3, Title = "LISTEN TO MORE METAL!!!!!1111!!111!1", Link = "https://en.wikipedia.org/wiki/Heavy_metal_music", UserId = 3, Score = int.MinValue, PostedOn = DateTime.Now - TimeSpan.FromDays(365d * 1000d) });

			context.Comments.Add(new Comment()
			{
				Id = 1, UserId = 1, PostId = 1,
				Text = "This is the most important website ever."
			});

			context.Comments.Add(new Comment()
			{
				Id = 2,
				UserId = 2,
				PostId = 2,
				Text = "I get over $700 septillion from SSI a month!"
			});

			context.Comments.Add(new Comment()
			{
				Id = 3,
				UserId = 3,
				PostId = 3,
				Text = "Heavy metal is objectively inferior to Peruvian death Gregorian chants."
			});
		}
	}
}