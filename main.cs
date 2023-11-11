using System;
using System.IO;
using System.Collections;
using System.IO.FileInfo;
using System.Xml.Linq.XElement;
using System.IO.Directory;

namespace main{
    class Program{
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
        
        }
        
        static XDocument CreateReport(IEnumerable<string> files){
        
        }
        
        public static void Main(string[] args){
        
        }
    }
}

