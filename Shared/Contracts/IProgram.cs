namespace ClientServer.Shared.Contracts
{
	public interface IProgram
	{
		string Hash { get; set; }
		string Name { get; set; }
		string Path { get; set; }
	}
}