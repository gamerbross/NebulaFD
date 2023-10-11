﻿using SapphireD.Core.Data.Chunks.BankChunks.Sounds;
using SapphireD.Core.Utilities;
using Spectre.Console;

namespace SapphireD.Plugins.GameDumper
{
    public class SoundDump : SapDPlugin
    {
        public string Name => "Sound Dumper";

        public void Execute()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(SapDCore.ConsoleFiglet);
            AnsiConsole.Write(SapDCore.ConsoleRule);

            AnsiConsole.Progress().Start(ctx =>
            {
                ProgressTask? task = ctx.AddTask("[DeepSkyBlue3]Dumping sounds[/]", false);

                int progress = 0;
                string path = "Dumps\\" + Utilities.ClearName(SapDCore.PackageData.AppName) + "\\Sounds";
                while (!task.IsFinished)
                {
                    if (SapDCore.PackageData.SoundBank != null)
                    {
                        if (SapDCore.PackageData.SoundBank.Sounds.Count == 0)
                            return;

                        if (!task.IsStarted)
                            task.StartTask();

                        task.Value = progress;
                        task.MaxValue = SapDCore.PackageData.SoundBank.Sounds.Count;

                        Sound[] sounds = SapDCore.PackageData.SoundBank.Sounds.Values.ToArray();
                        for (int i = 0; i < sounds.Length; i++)
                        {
                            Directory.CreateDirectory(path);
                            File.WriteAllBytes(path + "\\" + sounds[i].Name + GetExtension(sounds[i].Data), sounds[i].Data);
                            task.Value = ++progress;
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[Red]Could not find the sound bank.[/]");
                        Console.ReadKey();
                    }
                }
            });
        }

        public static string GetExtension(byte[] data)
        {
            if (data[0] == 'R' && data[1] == 'I' && data[2] == 'F' && data[3] == 'F')
                return ".wav";
            if (data[0] == 'O' && data[1] == 'g' && data[2] == 'g' && data[3] == 'S')
                return ".ogg";
            if (data[0] == 'F' && data[1] == 'O' && data[2] == 'R' && data[3] == 'M')
                return ".aiff";
            if (data[0] == 'I' && data[1] == 'D' && data[2] == '3')
                return ".mp3";

            // Because of Clickteam stole the MOD replayer from open-source OpenMPT library,
            // there's more file formats that can be supported by modflt.sft.
            return string.Empty;
        }
    }
}