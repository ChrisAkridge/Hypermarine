using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypermarine.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Text { get; set; }

		public int UserId { get; set; }
		public int PostId { get; set; }
		public virtual User User { get; set; }
		public virtual Post Post { get; set; }
	}
}