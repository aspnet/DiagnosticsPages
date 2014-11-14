// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    /// <summary>
    /// Interface for log storage to use with <see cref="ElmMiddleware"/>
    /// </summary>
    public interface IElmStore
    {
        /// <summary>
        /// Adds a log to the store
        /// </summary>
        /// <param name="info">The <see cref="LogInfo"/> to be added</param>
        void Add(LogInfo info);

        /// <summary>
        /// Returns an IEnumberable of the logs
        /// </summary>
        /// <returns>An IEnumerable of <see cref="LogInfo"/>'s</returns>
        IEnumerable<LogInfo> GetLogs();

        /// <summary>
        /// Returns logs logged with a <see cref="LogLevel"/> of minLevel or higher
        /// </summary>
        /// <param name="minLevel">The minimum LogLevel that should be included in the results</param>
        /// <returns>An IEnumerabe of <see cref="LogInfo"/>'s that have a LogLevel greater than or equal to minLevel</returns>
        IEnumerable<LogInfo> GetLogs(LogLevel minLevel);

        /// <summary>
        /// Adds a new <see cref="ActivityContext"/> to the store
        /// </summary>
        /// <param name="context">The context to be added to the store</param>
        void AddActivity(ActivityContext context);

        /// <summary>
        /// Returns an IEnumerable of the contexts of the logs
        /// </summary>
        /// <returns>An IEnumerable of <see cref="ActivityContext"/>s where each context stores 
        /// information about a top level scope</returns>
        IEnumerable<ActivityContext> GetActivities();
    }
}