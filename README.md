## UnityVulnerableEntryPoint
UnityVulnerableEntryPoint is a tool that uses **[AsmResolver][asmresolver]** for assembly manipulation and for searching an entry point in your favorite game for cheating, because anti-cheats such as BattlEye have hardcoded checks for modules inside of a game, in this case you can easily find entry point - edit this method via dnSpy or other decompiler and load your cheats in Ring3 (user mode). If you have any questions/issues please let me know [there](https://github.com/sunnamed434/UnityVulnerableEntryPoint/issues). You can install the latest version of UnityVulnerableEntryPoint [here](PATH TO THE UNKNOWN CHEATS DOWNLOAD).

![Preview image][preview]

## Warning
If you will find an entry point, be careful because you will be able to play but, it's only 5-6 minutes (tested in BE) and you need to reconnect to the server after being disconnected or find a better way to edit this method.

## How to use

## N00bie way
1. Startup UnityVulnerableEntryPoint.CLI.exe
2. Enter path to the Assembly-CSharp.dll
3. Enter path to the Assembly-CSharp.dll references or just stay it empty

## CLI Commands
```console

  -f, --file          Required. Set target file path.

  -r, --references    Set references of file, stay empty to specify the path from file path.

  --help              Display this help screen.

  --version           Display version information.

```

```console
$ UnityVulnerableEntryPoint.CLI.exe -f <path to the file> -r <path to the file references>
```

Don't specify the references if they're in the same folder as path to the file
```console
$ UnityVulnerableEntryPoint.CLI.exe -f <path to the file>
```

## Credits
For this [post](https://www.unknowncheats.me/forum/anti-cheat-bypass/568556-running-own-mono-code-battleye-game.html) and this [nayrde](https://www.unknowncheats.me/forum/members/3941040.html) specificly on unknowncheats, for the knowledge and motivation of creating this magic tool.

[preview]: https://raw.githubusercontent.com/sunnamed434/UnityVulnerableEntryPoint/master/resources/images/preview.png