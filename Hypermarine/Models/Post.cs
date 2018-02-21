using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypermarine.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
		public int Score { get; set; }
		public DateTime PostedOn { get; set; }

		public ICollection<Comment> Comments { get; set; }

		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}