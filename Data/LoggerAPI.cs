using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class LoggerAPI
    {
        public static LoggerAPI CreateLogger()
        {
            return new Logger();
        }
        public abstract void Log(IBall ball, string date);
    }
}
