using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DectalkNET;

namespace ExampleTest
{
    class Program
    {

        static void Main(string[] args)
        {
            Dectalk.Startup(0);
            if (Dectalk.status != DectalkNET.invokes.MMRESULT.MMSYSERR_NOERROR)
            {
                Console.WriteLine("Error starting Dectalk");
            }

            string outp = "Hello World";
            process(outp);

            Dectalk.Shutdown();
        }

        static void process(string output)
        {
            if (output.StartsWith("vol:")) Dectalk.SetVolume(int.Parse(output.Substring(4)));
            else if (output.StartsWith("log:"))
            {
                //Dectalk.Say($"[:log phoneme on]{output.Substring(4)}[:log phoneme off]");
                Dectalk.WaveOut(output, "./output.wav");
                Dectalk.WaitForSpeech();
                string temp = File.ReadAllText("log.txt");
                Console.WriteLine(temp);
                File.Delete("log.txt");
            }
            else
            {
                //Dectalk.Say(output);
                Dectalk.WaveOut(output, "./output.wav");
                Dectalk.WaitForSpeech();
            }
        }
    }
}
