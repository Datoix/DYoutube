namespace DYoutube.Core;

public static class FileUtils {
	public static string ValidateFileName(string fileName) {
		var invalidCharacters = Path.GetInvalidFileNameChars();
		
		return string.Concat(
			fileName.Where(
				c => !invalidCharacters.Contains(c)
			)
		);
	}
}