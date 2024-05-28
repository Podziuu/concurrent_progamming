using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger
    {
        private readonly ConcurrentQueue<JObject> _queue = new ConcurrentQueue<JObject>();
        private readonly JArray _logArray;
        private readonly string _logFilePath;
        private Task _loggingTask;

        internal Logger()
        {
            string PathToSave = Path.GetTempPath();
            _logFilePath = Path.Combine(PathToSave, "log.json");

            if (File.Exists(_logFilePath))
            {
                try
                {
                    string input = File.ReadAllText(_logFilePath);
                    _logArray = JArray.Parse(input);
                }
                catch (JsonReaderException)
                {
                    _logArray = new JArray();
                }

            }
            else
            {
                _logArray = new JArray();
                FileStream file = File.Create(_logFilePath);
                file.Close();
            }
        }

    }
}
