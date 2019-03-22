using System;
using System.IO;

namespace DocumentMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputFiles = new string[args.Length - 1];
            Array.Copy(args, 0, inputFiles, 0, args.Length - 1);
            var outputFile = args[args.Length - 1];
            StreamWriter writer = null;
            ulong characterCount = 0;
            StreamReader reader = null;
            string currentFile = null;

            Console.WriteLine("Document Merger\n");

            if (args.Length < 3)
            {
                Console.WriteLine("DocumentMerger2 <input_file_1> <input_file_2> ... <input_file_n> <output_file>");
                Console.WriteLine("Supply a list of text files to merge followed by the name of the resulting merged text file as command line arguments.");
                System.Environment.Exit(1);
            }

            try
            {
                writer = new StreamWriter(outputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to file: {0}", e.Message);
                System.Environment.Exit(1);
            }

            try
            {
                foreach (string file in inputFiles)
                {
                    currentFile = file;
                    reader = new StreamReader(currentFile);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(line);
                        characterCount = characterCount + (ulong)line.Length;
                    }
                    reader.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with file {0}\nMessage: {0}", currentFile, e.Message);
                System.Environment.Exit(1);
            }

            Console.WriteLine("{0} was successfully saved. Document contains {1} characters", outputFile, characterCount);
            
        }
    }
}
