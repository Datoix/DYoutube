using System.Linq.Expressions;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace DYoutube.Core;

// Only audio
public class YoutubeHandler {
	private readonly YoutubeClient _youtube;
    private readonly ILogger _logger;

    public YoutubeHandler(ILogger logger) {
		_youtube = new();
		_logger = logger;
	}


	public async Task DownloadPlaylistAsync(string playlistUrl, string folderPath) {
		try {
			await foreach (var video in _youtube.Playlists.GetVideosAsync(playlistUrl)) {
				await DownloadVideoAsync(video.Url, DefaultPath(video, folderPath));
			}
		} catch (Exception e) {
			_logger.Error(e.Message);
		}
	}

	public async Task DownloadVideoAsync(string videoUrl, string filePath) {
		try {
			var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoUrl);
			var streamInfo = streamManifest
				.GetAudioOnlyStreams()
				.GetWithHighestBitrate();

			await _youtube.Videos.Streams.DownloadAsync(streamInfo, filePath);
			
			_logger.Info($"Saved {videoUrl} -> {filePath}");
		} catch (Exception e) {
			_logger.Error(e.Message);
		}
	}
	
	public async Task<string> GetDefaultPathAsync(string videoUrl, string folderPath) {
		var video = await _youtube.Videos.GetAsync(videoUrl);
		return DefaultPath(video, folderPath);
	}

	public static string DefaultPath(IVideo video, string folderPath) {
		var validatedTitle = FileUtils.ValidateFileName(video.Title);
		return Path.Combine(folderPath, validatedTitle) + ".mp3"; 
	}
}
