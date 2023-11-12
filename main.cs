using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO.FileInfo;
using System.Xml.Linq.XElement;
using System.IO.Directory;

namespace main{
    class Program{
        // used ChatGPT and Microsoft Learn
        static IEnumerable<string> EnumerateFilesRecursively(string path){
            // iterate over each file in the path and yield it
            foreach (var file in Directory.GetFiles(path)){
                yield return file;
            }

            // iterate over each subdirectory in the path and enter a recursive call
            foreach (var subdirectory in Directory.GetDirectories(path)){
                // each subdirectory enters a recursive call and yield the file
                foreach (var file in EnumerateFilesRecursively(subdirectory)){
                    yield return file;
                }
            }
        }
        
        static string FormatByteSize(long byteSize){
            // Sources: learn.microsoft.com, stackoverflow, & ChatGPT
            
            // Validation if byteSize < 0 || byteSize >= 1000
            if(byteSize < 0 || byteSize >= 1000){
                throw new ArgumentOutOfRangeException(nameof(byteSize), "Byte size should be >= 0 and < 1000.");
            }
            
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

            // Returns a formatted string that represents byte size in a human-readable form rounded to 2 digits after decimal point
            return $"{size:0.00}{sizeSuffixes[index]}";

        }
        
        static XDocument CreateReport(IEnumerable<string> files){
        
        }
        
        public static void Main(string[] args){
        
        }
    }
}

