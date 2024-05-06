# DYoutube

Youtube audio-only (`mp3`) downloader

Either video, file or playlist

```
-l, --link        Required. Link to youtube video
-n, --name        File name used to save video
-f, --file        Required. Load from file (new line as separator)
-p, --playlist    Required. Link to playlist
-o, --output      Output directory
```

- **Example video**:

```
dyoutube.cli.exe -l https://youtu.be/dQw4w9WgXcQ?si=5Mc-p0Gl9yCZk38t
```

- **Example file**

`file.txt` (videos separated by `\n`)
```
https://www.youtube.com/watch?v=dQw4w9WgXcQ
https://www.youtube.com/watch?v=dQw4w9WgXcQ
https://www.youtube.com/watch?v=dQw4w9WgXcQ
https://www.youtube.com/watch?v=dQw4w9WgXcQ
```

```
dyoutube.cli.exe -f path/to/file.txt -o path/to/output/folder
```

- **Example playlist**

dyoutube.cli.exe -p https://link_to_playlist -o path/to/output/folder
