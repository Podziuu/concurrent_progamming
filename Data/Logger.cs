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
    

    public class Logger
    {
        private class BallToLog
        {
            public int BallId { get; }
            public Vector2 Position { get; }
            public Vector2 Velocity { get; }
            

            public BallToLog(int ballID, Vector2 pos, Vector2 vel)
            {
                BallId = ballID;
                Position = pos;
                Velocity = vel;
            }

        }

        private readonly ConcurrentQueue<BallToLog> _queue = new ConcurrentQueue<BallToLog>();
        private readonly string _logFilePath;

        internal Logger()
        {
            string PathToSave = Path.GetTempPath();
            _logFilePath = Path.Combine(PathToSave, "log.json");
            WriteToFile();


        }

        internal void Log(IBall ball)
        {
            _queue.Enqueue(new BallToLog(ball.BallId, ball.Position, ball.Velocity));
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
                    }
                    await _streamWriter.FlushAsync();
                }
            });
        }

    }
}
