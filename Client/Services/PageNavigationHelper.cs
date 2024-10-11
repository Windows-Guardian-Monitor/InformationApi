namespace ClientServer.Client.Services
{
	public class PageNavigationHelper
	{
		private readonly Dictionary<string, object> ValueDictionary;

		public string UserName { get; private set; }

        public PageNavigationHelper()
        {
			ValueDictionary = new();
		}

		public void SetUserName(string userName)
		{
			UserName = userName;
		}

        public string AddValue<T>(T value) where T : class
		{
			if (value is null)
			{
				throw new InvalidOperationException($"The stored object can't be null");
			}

			var id = Guid.NewGuid().ToString();

			if (ValueDictionary.TryAdd(id, value))
			{
				return id;

			}


			throw new InvalidOperationException("Could not store object");
		}

		public string AddIntegerValue(int value)
		{
			if (value is 0)
			{
				throw new InvalidOperationException($"The value can't be zero");
			}

			var id = Guid.NewGuid().ToString();

			if (ValueDictionary.TryAdd(id, value))
			{
				return id;

			}

			throw new InvalidOperationException("Could not store object");
		}

		public int GetIntegerValue(string id)
		{
			if (ValueDictionary.TryGetValue(id, out var value) is false)
			{
				throw new InvalidOperationException($"{id} not found");
			}

			ValueDictionary.Remove(id);

			return value is not int r ? throw new InvalidOperationException($"Incorret type happened") : r;
		}

		public T GetValue<T>(string id) where T : class
		{
			if (ValueDictionary.TryGetValue(id, out var value) is false)
			{
				throw new InvalidOperationException($"{id} not found");
			}

			ValueDictionary.Remove(id);

			return value is not T r ? throw new InvalidOperationException($"Something happened") : r;
		}
	}
}
