using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypermarine
{
	public static class Helpers
	{
		public static string DateTimeToTZQualifiedString(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd hh:mm:ss tt zzz");
		}

		public static string DateTimeToTimeAgo(DateTime dateTime)
		{
			TimeSpan difference = DateTime.Now - dateTime;
			
			if (difference <= TimeSpan.FromSeconds(1d)) { return "just now"; }
			else if (difference <= TimeSpan.FromSeconds(60d))
			{ 
				return $"{(int)difference.TotalSeconds} seconds ago"; 
			}
			else if (difference <= TimeSpan.FromMinutes(60d)) 
			{ 
				return $"{(int)difference.TotalMinutes} minutes ago"; 
			}
			else if (difference <= TimeSpan.FromHours(24d))
			{
				return $"{(int)difference.TotalHours} hours ago";
			}
			else if (difference <= TimeSpan.FromDays(7d))
			{
				return $"{(int)difference.TotalDays} days ago";
			}
			else if (difference <= TimeSpan.FromDays(365d))
			{
				var weeks = (int)(difference.TotalDays / 7d);
				return $"{weeks} weeks ago";
			}
			else
			{
				var years = (int)(difference.TotalDays / 365d);
				return $"{years} years ago";
			}
		}
	}
}