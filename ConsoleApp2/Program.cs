using System;
using System.IO;
using Newtonsoft.Json;
using CommandLine;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)

        {
            
            while (true)
            {
               
                string CommandRead = Console.ReadLine();
                if ("exit" == CommandRead)
                { }
                
                string[] CommandArray = CommandRead.Split();
                

                    if (CommandArray != null && CommandArray.Length > 0)
                    {
                        string CommandArgum = CommandArray[0];

                        switch (CommandArgum)
                        {
                            case "ls":
                                string[] Getdir = Directory.GetDirectories(CommandArray[1]);
                                string[] Getfile = Directory.GetFiles(CommandArray[1]);
                                string D = JsonConvert.SerializeObject((Getdir, Getfile));
                                File.WriteAllText("listDir.json", D);
                            foreach (string a in Getdir)
                                {
                                    Console.WriteLine(a);
                                }
                                foreach (string a in Getfile)
                                {
                                    Console.WriteLine(a);
                                }

                                continue;
                            case "mkdir":
                                {
                                    if (!Directory.Exists(CommandArray[1]))
                                    {
                                        Directory.CreateDirectory(CommandArray[1]);
                                    }
                                }
                            continue;
                            case "cp":
                                {
                                    try
                                    {
                                        if (System.IO.File.Exists(CommandArray[1]))
                                        {
                                            File.Copy(Path.Combine(CommandArray[1]), Path.Combine(CommandArray[2]), true);
                                        }
                                        else if (System.IO.Directory.Exists(CommandArray[1]))
                                        {
                                            DirectoryInfo dirSource = new DirectoryInfo(CommandArray[1]);
                                            DirectoryInfo dirTarget = new DirectoryInfo(CommandArray[2]);

                                            //Перебираем все внутренние папки
                                            foreach (DirectoryInfo dir in dirSource.GetDirectories())
                                            {
                                                if (Directory.Exists(dirTarget + "\\" + dir.Name) != true)
                                                {
                                                    Directory.CreateDirectory(dirTarget + "\\" + dir.Name);
                                                }


                                            }

                                            foreach (string file in Directory.GetFiles(CommandArray[1]))
                                            {
                                                string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf('\\'));
                                                File.Copy(file, dirTarget + "\\" + filik, true);
                                            }




                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                                    }

                                }
                            continue;
                            case "rm":
                                {
                                    if (File.Exists(CommandArray[1]))
                                    {
                                        File.Delete(CommandArray[1]);
                                    }
                                    else if (Directory.Exists(CommandArray[1]))
                                    {
                                        try
                                        {
                                            DirectoryInfo diR = new DirectoryInfo(CommandArray[1]);
                                            DirectoryInfo[] diP = diR.GetDirectories();
                                            FileInfo[] finf = diR.GetFiles();
                                            foreach (FileInfo f in finf)
                                            {
                                                f.Delete();
                                            }
                                            foreach (DirectoryInfo df in diP)
                                            {
                                                Directory.Delete(df.FullName);
                                            }
                                            if (diR.GetDirectories().Length == 0 && diR.GetFiles().Length == 0) diR.Delete();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Произошла ошибка: " + ex.Message);
                                        }
                                    }
                                }
                            continue;
                            case "file":
                                {
                                    FileInfo file = new FileInfo(CommandArray[1]);
                                    Console.WriteLine($"Байт {file.Length} Имя {file.Name} создан {file.CreationTime} расширение {file.Extension}");
                                    Console.WriteLine();


                                }
                            continue;

                        }
                    break;

                }
            }
            
        }
    }
}