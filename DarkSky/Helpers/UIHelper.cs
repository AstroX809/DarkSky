﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using System.Globalization;
using System.Diagnostics;

namespace DarkSky.Helpers
{
	public class UIHelper
	{
		public static string SimpleDate(DateTime date)
		{
			TimeSpan timeDifference = DateTime.Now - date;

			if (timeDifference.TotalDays >= 365) // More than a year ago
				return date.ToString("MMM d, yyyy");
			else if (timeDifference.TotalDays >= 30) // More than a month ago but less than a year
				return date.ToString("MMM d");
			else if (timeDifference.TotalDays >= 1) // More than a day but less than a month
				return $"{(int)timeDifference.TotalDays} {GetSingularOrPlural((int)timeDifference.TotalDays, "day")} ago";
			else if (timeDifference.TotalHours >= 1) // More than an hour but less than a day
				return $"{(int)timeDifference.TotalHours} {GetSingularOrPlural((int)timeDifference.TotalHours, "hour")} ago";
			else if (timeDifference.TotalMinutes >= 1) // More than a minute but less than an hour
				return $"{(int)timeDifference.TotalMinutes} {GetSingularOrPlural((int)timeDifference.TotalMinutes, "minute")} ago";
			else if (timeDifference.TotalSeconds >= 1) // More than a second but less than a minute
				return $"{(int)timeDifference.TotalSeconds} {GetSingularOrPlural((int)timeDifference.TotalSeconds, "second")} ago";
			else
				return "just now"; // Less than 1 second ago
		}

		private static string GetSingularOrPlural(int value, string singular)
		{
			return value == 1 ? singular : $"{singular}s";
		}

		public static string FormatDate(DateTime date)
		{
			return date.ToString("MMMM dd, yyyy 'at' hh:mm tt", CultureInfo.InvariantCulture);
		}

		public static bool none(object obj) => obj is not null;

		public static ImageSource Img(string uri)
		{
			if (uri == null)
				throw new ArgumentNullException(nameof(uri));

			// Create a BitmapImage and set its UriSource to the provided Uri
			var bitmapImage = new BitmapImage(new Uri(uri));
			return bitmapImage;
		}
	}
}