using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypermarine.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Post> Posts { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}