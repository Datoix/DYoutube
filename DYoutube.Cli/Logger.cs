using DYoutube.Core;

namespace DYoutube.Core;

public class Logger : ILogger {
	public void Info(string message) {
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("[Info] {0}", message);
		
		Console.ResetColor();
	}

	public void Warn(string message) {
		Console.ForegroundColor = ConsoleColor.Yellow;		
		Console.WriteLine("[Warning] {0}", message);
		
		Console.ResetColor();
	}

	public void Error(string message) {
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("[Error] {0}", message);
		
		Console.ResetColor();
	}
}