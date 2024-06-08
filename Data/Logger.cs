using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger
    {
        private class BallToLog
        {
            public int BallId { get; }
            public Vector2 Position { get; }
            public Vector2 Velocity { get; }
            public DateTime Date { get; }

            public BallToLog(int ballID, Vector2 pos, Vector2 vel, DateTime date)
            {
                BallId = ballID;
                Position = pos;
                Velocity = vel;
                Date = date;
            }
        }

        private readonly BlockingCollection<BallToLog> _queue;
        private readonly string _logFilePath;
        private readonly int _cacheSize = 100;
        private static Logger? _logger;
        private bool _isOverCacheSize = false;
        private readonly object _lock = new object();

        internal Logger()
        {
            _queue = new BlockingCollection<BallToLog>(new ConcurrentQueue<BallToLog>(), _cacheSize);
            string PathToSave = Path.GetTempPath();
            _logFilePath = Path.Combine(PathToSave, "log.json");
            WriteToFile();
        }

        public static Logger CreateLogger()
        {
            if (_logger != null) return _logger;
            _logger = new Logger();
            return _logger;
        }

        public void Log(IBall ball, DateTime date)
        {
            bool isAdded = _queue.TryAdd(new BallToLog(ball.BallId, ball.Position, ball.Velocity, date));
            if (isAdded) return;
            lock (_lock)
            {
                _isOverCacheSize = true;
            }
        }

        private void WriteToFile()
        {
            Task.Run(async () =>
            {
                using StreamWriter _streamWriter = new StreamWriter(_logFilePath, false, Encoding.UTF8);
                while(!_queue.IsCompleted)
                {
                    bool isOverflow = false;

                    lock(_lock)
                    {
                        if(_isOverCacheSize)
                        {
                            isOverflow = true;
                            _isOverCacheSize = false;
                        }
                    }

                    if(isOverflow) await _streamWriter.WriteLineAsync("Cache is over size.");

                    BallToLog ball = _queue.Take();
                    string jsonString = JsonConvert.SerializeObject(ball);
                    await _streamWriter.WriteLineAsync(jsonString);
                    await _streamWriter.FlushAsync();
                }
            });
        }
    }
}
