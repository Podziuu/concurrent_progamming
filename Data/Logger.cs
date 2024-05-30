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
    internal class Logger : LoggerAPI
    {
        private class BallToLog
        {
            public int BallId { get; }
            public Vector2 Position { get; }
            public Vector2 Velocity { get; }
            public string Date { get; }
            

            public BallToLog(int ballID, Vector2 pos, Vector2 vel, string date)
            {
                BallId = ballID;
                Position = pos;
                Velocity = vel;
                Date = date;
            }

        }

        private readonly ConcurrentQueue<BallToLog> _queue = new ConcurrentQueue<BallToLog>();
        private readonly string _logFilePath;
        private readonly int _cacheSize = 1000;
        private bool _isOverCacheSize = false;

        internal Logger()
        {
            string PathToSave = Path.GetTempPath();
            _logFilePath = Path.Combine(PathToSave, "log.json");
            WriteToFile();


        }

        public override void Log(IBall ball, string date)
        {
            _queue.Enqueue(new BallToLog(ball.BallId, ball.Position, ball.Velocity, date));

            if (_queue.Count > _cacheSize && !_isOverCacheSize)
            {
                _isOverCacheSize = true;
            }
        }

        private void WriteToFile()
        {
            Task.Run(async () =>
            {
                using StreamWriter _streamWriter = new StreamWriter(_logFilePath);
                while (true)
                {
                    while (_queue.TryDequeue(out BallToLog ball))
                    {
                        string jsonString = JsonConvert.SerializeObject(ball);
                        _streamWriter.WriteLine(jsonString);

                        if(_isOverCacheSize)
                        {
                            _streamWriter.WriteLine("Cache is over size.");
                            _isOverCacheSize = false;
                        }
                    }
                    await _streamWriter.FlushAsync();
                }
            });
        }

    }
}
