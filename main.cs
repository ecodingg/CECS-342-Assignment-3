using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace main{
    class Program{
        // sources: ChatGPT and Microsoft Learn
        static IEnumerable<string> EnumerateFilesRecursively(string path){
            // iterate over each file in the path and yield it
            var fileEntries = Directory.GetFiles(path);
            foreach (var fileName in fileEntries){
                yield return fileName;
            }

            // iterate over each subdirectory in the path and enter a recursive call
            var subdirectoryEntries = Directory.GetDirectories(path);
            foreach (var subdirectory in subdirectoryEntries){
                // each subdirectory enters a recursive call and yield the file
                foreach (var file in EnumerateFilesRecursively(subdirectory)){
                    yield return file;
                }
            }
        }
        
        static string FormatByteSize(long byteSize){
            // Sources: learn.microsoft.com, stackoverflow, & ChatGPT
          
            // String of units to be assigned
            string[] sizeSuffixes = { "B", "kB", "MB", "GB", "TB", "PB", "EB", "ZB"};

            // Idex used to index into sizeSuffixes array
            int index = 0;

            // Size initialized to variable byteSize to perform calculations to determine appropriate unite for byte size
            decimal size = byteSize;

            // Loop to divide size by 1000 and increment index until size becomes less than 1000
            while(size >= 1000){
                size /= 1000;
                index++;
            }
            string strSize = size.ToString();
            string strsizeSf = sizeSuffixes[index].ToString();

            // Returns a formatted string that represents byte size in a human-readable form rounded to 2 digits after decimal point
            return strSize + strsizeSf;

        }
        
        static XDocument CreateReport(IEnumerable<string> files){
            /*
            *Sources - C# Corner, Microsoft Documentation, ChatGPT, Stackoverflow, YouTube, Github, VSC
            */ 

            //LINQ Stuff
            var fileGroups = files.GroupBy(file => Path.GetExtension(file).ToLower()).Select(group => new
                {
                    Extension = group.Key,
                    Count = group.Count(),
                    Size = group.Sum(file => file.Length)
                }).OrderByDescending(group => group.Size);

            //Basic HTML construction
            XElement html = new XElement("html");
            XElement body = new XElement("body");
            XElement table = new XElement("table");
            XElement thead = new XElement("thead");
            XElement trHeader = new XElement("tr");
            trHeader.Add(new XElement("th", "Type"), new XElement("th", "Count"), new XElement("th", "Size"));
            thead.Add(trHeader);
            XElement tbody = new XElement("tbody");

            //Creating table with data
            foreach (var group in fileGroups)
            {
                tbody.Add(
                    new XElement("tr",
                    new XElement("td", group.Extension),
                    new XElement("td", group.Count),
                    new XElement("td", FormatByteSize(group.Size))));
            }

            //Combine and save everything
            table.Add(thead, tbody);
            body.Add(table);
            html.Add(body);
            XDocument document = new XDocument(html);
            document.Save("output.html");
            return document;

        }
        
        public static void Main(string[] args){
            // Sources: microsoft learn, chatgpt, and stack overflow
            
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: FileAnalyzer <input_folder_path> <output_html_file_path>");
                return;
            }

            string inputFolderPath = args[0];
            string outputHtmlFilePath = args[1];

            IEnumerable<string> files = EnumerateFilesRecursively(inputFolderPath);
            XDocument report = CreateReport(files);

            report.Save(outputHtmlFilePath);

            Console.WriteLine("The report was generated successfully!");
        }
    }
}

