﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;

namespace RDB_Project.DataWriting
{
    interface IFileReader
    {
        void Read(BlockingCollection<List<string>> output);
    }

    class FileReader : IFileReader
    {
        string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public void Read(BlockingCollection<List<string>> output)
        {
            try
            {
                using (FileStream fs = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    List<string> data = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        data.Add(line);
                        if ((data.Count % 100) == 0)
                        {
                            output.Add(data);
                            data = new List<string>();
                        }
                        //System.Windows.MessageBox.Show("Data reader: "+line);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException(e.Message, e);
            }
            finally
            {
                output.CompleteAdding();
            }
        }
    }
}
