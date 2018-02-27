using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hypermarine.Models
{
	public class User
	{
		public int Id { get; set; }

		[StringLength(25)]
		public string Name { get; set; }

		public ICollection<Post> Posts { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}