using System.Runtime.CompilerServices;

namespace InformationHandlerApi.Extensions
{
	public static class LongExtensions
	{
		public static DateTime TimestampToDatetime(this long timestamp)
		{
			var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(timestamp);
			return dateTime;
		}
	}
}
