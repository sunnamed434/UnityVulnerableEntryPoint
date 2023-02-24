## UnityVulnerableEntryPoint
UnityVulnerableEntryPoint is a tool that uses **[AsmResolver][asmresolver]** for assembly manipulation and for searching an entry point in your favorite game for cheating, because anti-cheats such as BattlEye have hardcoded checks for modules inside of a game, in this case, you can easily find entry point - edit this method via dnSpy or other decompiler and load your cheats in Ring3 (user mode). If you have any questions/issues please let me know **[there][issues]**. You can install the latest version of UnityVulnerableEntryPoint soon.

* More information and discussions can be found there on the **[unknowncheats post][unknowncheats_post]**

![Preview of CLI][cli_preview]

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
For this **[post][post_nayrde_uc]** by **[nayrde][nayrde_profile_uc]**, for the knowledge and motivation of creating this magic tool

[asmresolver]: https://github.com/Washi1337/AsmResolver
[cli_preview]: https://raw.githubusercontent.com/sunnamed434/UnityVulnerableEntryPoint/master/resources/images/preview/CLI.png
[unknowncheats_post]: https://www.unknowncheats.me/forum/anti-cheat-bypass/572479-unityvulnerableentrypoint-tool.html#post3687813
[issues]: https://github.com/sunnamed434/UnityVulnerableEntryPoint/issues
[download]: soon
[post_nayrde_uc]: https://www.unknowncheats.me/forum/anti-cheat-bypass/568556-running-own-mono-code-battleye-game.html
[nayrde_profile_uc]: https://www.unknowncheats.me/forum/members/3941040.html