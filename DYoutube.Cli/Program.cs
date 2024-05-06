// See https://aka.ms/new-console-template for more information
using CommandLine;
using DYoutube.Cli;

await Parser.Default.ParseArguments<Options>(args)
	.WithParsedAsync(o => new App(o).RunAsync());