using System.Reflection.Metadata;
using DYoutube.Core;

namespace DYoutube.Cli;

public class App {
    private readonly Options _options;
    private readonly ILogger _logger;
    private readonly YoutubeHandler _youtube;

    public App(Options options) {
		_options = options;
		_logger = new Logger();
		_youtube = new YoutubeHandler(_logger);
	}

	public async Task RunAsync() {
		ValidateOptions();

		if (_options.Link != null) await HandleLinkAsync();
		else if (_options.Playlist != null) await HandlePlaylistAsync();
		else if (_options.FileInput != null) await HandleFileInputAsync();
	}

	public async Task HandleLinkAsync() {
		try {
			string filePath;
		
			if (_options.Name == null) {
				filePath = await _youtube.GetDefaultPathAsync(_options.Link!, _options.Output!);	
			} else {
				filePath = Path.Combine(_options.Output!, _options.Name);
				filePath += ".mp3";
			}

			await _youtube.DownloadVideoAsync(_options.Link!, filePath);

		} catch (Exception e) {
			_logger.Error(e.Message);
		}
		
	}

	public async Task HandlePlaylistAsync() {
		await _youtube.DownloadPlaylistAsync(_options.Playlist!, _options.Output!);
	}

	public async Task HandleFileInputAsync() {
		var urls = await File.ReadAllLinesAsync(_options.FileInput!);
		
		foreach (var url in urls) {
			if (string.IsNullOrWhiteSpace(url)) continue;

			try {
				var filePath = await _youtube.GetDefaultPathAsync(url, _options.Output!);
				await _youtube.DownloadVideoAsync(url.Trim(), filePath);
			} catch (Exception e) {
				_logger.Error(e.Message);
			}
		}
	} 

	private void ValidateOptions() {
		if (_options.Name != null) {
			_options.Name = FileUtils.ValidateFileName(_options.Name);
		}
		
		if (_options.FileInput != null && !File.Exists(_options.FileInput)) {
			_logger.Error($"Invalid path - {_options.FileInput}");
			TerminateProcess();
		}
		
		if (_options.Output == null) {
			_options.Output = Directory.GetCurrentDirectory();
		} else {
			_options.Output = Path.GetFullPath(_options.Output!);
			
			if (!Directory.Exists(_options.Output)) {
				Directory.CreateDirectory(_options.Output);
			}
		}
	}

	private void TerminateProcess() {
		_logger.Error("Process termination (caused by error)");
		Environment.Exit(-1);
	}
}