using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace KernelMarioPlayer
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        static void Main(string[] args)
        {
            var sounds = ReadSounds();
            foreach (var sound in sounds)
            {
                Beep(sound[0], sound[1]);
                Thread.Sleep(sound[2]);
            }
        }

        private static List<int[]> ReadSounds()
        {
            var result = new List<int[]>();

            int counter = 0;
            string line;

            var frequence = String.Empty;
            var length = String.Empty;
            var delay = String.Empty;

            var numbCounter = 0;
            var wasNumeric = false;

            var file = new StreamReader("Frequences.txt");
            while ((line = file.ReadLine()) != null)
            {
                counter++;

                foreach (var character in line)
                {
                    var isNumeric = int.TryParse(character.ToString(), out var n);
                    if (isNumeric)
                    {
                        if (numbCounter == 0)
                        {
                            frequence += n.ToString();
                        }
                        else if (numbCounter == 1)
                        {
                            length += n.ToString();
                        }
                        else
                        {
                            delay += n.ToString();
                        }
                        wasNumeric = true;
                    }

                    if (wasNumeric && !isNumeric)
                    {
                        numbCounter++;
                        wasNumeric = false;
                    }
                }
                if (delay != String.Empty)
                {
                    numbCounter = 0;
                    Console.WriteLine(frequence);
                    Console.WriteLine(length);
                    Console.WriteLine(delay);
                    result.Add(new int[]
                    {
                        Int32.Parse(frequence), Int32.Parse(length), Int32.Parse(delay)
                    });

                    frequence = String.Empty;
                    length = String.Empty;
                    delay = String.Empty;
                }
            }

            file.Close();

            return result;
        }
    }
}
