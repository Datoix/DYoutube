using CommandLine;
using DYoutube.Core;

namespace DYoutube.Cli;

public class Options {
	[Option(
		'l', "link", Required = true,
		SetName = "LINK_GROUP",
		HelpText = "Link to youtube video"
	)]
	public string? Link { get; set; }

	[Option(
		'n', "name", Required = false,
		SetName = "LINK_GROUP",
		HelpText = "File name used to save video"
	)]
	public string? Name { get; set; }
	
	[Option(
		'f', "file", Required = true,
		SetName = "FILE_GROUP",
		HelpText = "Load from file (new line as separator)"
	)]
	public string? FileInput { get; set; }

	[Option(
		'p', "playlist", Required = true,
		SetName = "PLAYLIST_GROUP",
		HelpText = "Link to playlist"
	)]
	public string? Playlist { get; set; }


	[Option(
		'o', "output",
		HelpText = "Output directory"
	)]
	public string? Output { get; set; }
}