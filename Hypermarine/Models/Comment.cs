using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hypermarine.Models
{
	public class Comment
	{
		public int Id { get; set; }

		[StringLength(10000)]
		public string Text { get; set; }

		public int UserId { get; set; }
		public int PostId { get; set; }
		public User User { get; set; }
		public Post Post { get; set; }
	}
}