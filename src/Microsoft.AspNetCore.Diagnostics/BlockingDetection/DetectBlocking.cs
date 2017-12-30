// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Diagnostics.Internal;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics
{
    // Tips of the Toub
    internal sealed class DetectBlocking : SynchronizationContext
    {
        [ThreadStatic]
        private static int t_recursionCount;

        private readonly ILogger _logger;

        public DetectBlocking(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DetectBlocking>();

            SetWaitNotificationRequired();
        } 

        public override int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
        {
            t_recursionCount++;

            try
            {
                if (t_recursionCount == 1 && Thread.CurrentThread.IsThreadPoolThread)
                {
                    _logger.BlockingMethodCalled(new StackTrace());
                }

                return base.Wait(waitHandles, waitAll, millisecondsTimeout);
            }
            finally
            {
                t_recursionCount--;
            }
        }
    }
}
