using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EzWebForms.Linq
{
	public static class StringExtensions
	{
		public static string ToMd5(this string input, Encoding encoding)
		{
			var md5 = MD5.Create();
			var inputBytes = encoding.GetBytes(input);
			var hash = md5.ComputeHash(inputBytes);
			var stringBuilder = new StringBuilder();

			foreach (var @byte in hash)
			{
				stringBuilder.Append(@byte.ToString("X2"));
			}

			return stringBuilder.ToString();
		}

		public static string ToMd5(this string input)
		{
			return ToMd5(input, Encoding.UTF8);
		}

		public static string ToBase64(this string input, Encoding encoding)
		{
			return Convert.ToBase64String(encoding.GetBytes(input));
		}

		public static string ToBase64(this string input)
		{
			return ToBase64(input, Encoding.UTF8);
		}

		public static string FromBase64(this string input, Encoding encoding)
		{
			return encoding.GetString(Convert.FromBase64String(input));
		}

		public static string FromBase64(this string input)
		{
			return FromBase64(input, Encoding.UTF8);
		}

		public static string HtmlEncode(this string input)
		{
			return HttpUtility.HtmlEncode(input);
		}

		public static string HtmlDecode(this string input)
		{
			return HttpUtility.HtmlDecode(input);
		}

		public static string UrlEncode(this string input)
		{
			return HttpUtility.UrlEncode(input);
		}

		public static string UrlDecode(this string input)
		{
			return HttpUtility.UrlDecode(input);
		}

		public static T ConvertTo<T>(this string input, T defaultValue = default(T))
			where T : IConvertible
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			if (typeof (T) == typeof (bool))
			{
				switch (input.Trim().ToLower())
				{
					default:
						return (T) (object) false;
					case "1":
					case "t":
					case "true":
					case "y":
					case "yes":
						return (T) (object) true;
				}
			}

			try
			{
				return (T) Convert.ChangeType(input.Trim(), typeof (T));
			}
			catch
			{
				return default(T);
			}
		}
	}
}