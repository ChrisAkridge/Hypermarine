using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hypermarine.Models;

namespace Hypermarine.Data
{
	public class Context : DbContext
	{
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }

		public Context() : base("name=Context")
		{
			
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
	}
}