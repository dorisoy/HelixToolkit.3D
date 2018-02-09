﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelixToolkit.Logger
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="HelixToolkit.Logger.ILogger" />
    public sealed class DebugLogger : ILogger
    {
        /// <summary>
        /// Logs the specified log level.
        /// </summary>
        /// <typeparam name="MsgType"></typeparam>
        /// <param name="logLevel">The log level.</param>
        /// <param name="msg">The message.</param>
        /// <param name="caller">The caller.</param>
        public void Log<MsgType>(LogLevel logLevel, MsgType msg, [CallerMemberName]string caller = "")
        {
            Debug.WriteLine($"Level: {logLevel}; Caller: {caller}; Text: {msg.ToString()};");   
        }
    }
}