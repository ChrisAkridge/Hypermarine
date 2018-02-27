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
			Database.SetInitializer(new ModelInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
				.HasRequired(p => p.User)
				.WithMany(u => u.Posts)
				.HasForeignKey(p => p.UserId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Comment>()
				.HasRequired(c => c.Post)
				.WithMany(p => p.Comments)
				.HasForeignKey(c => c.PostId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.Property(u => u.Name)
				.HasMaxLength(25);

			modelBuilder.Entity<Post>()
				.Property(p => p.Title)
				.HasMaxLength(200);

			modelBuilder.Entity<Post>()
				.Property(p => p.Link)
				.HasMaxLength(400);
		}
	}
}