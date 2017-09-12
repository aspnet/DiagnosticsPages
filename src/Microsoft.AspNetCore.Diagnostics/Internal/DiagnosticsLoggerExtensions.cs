// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics.Internal
{
    internal static class DiagnosticsLoggerExtensions
    {
        private static readonly Action<ILogger, Exception, Exception> _unhandledException;
        private static readonly Action<ILogger, Exception> _responseStartedErrorHandler;
        private static readonly Action<ILogger, Exception, Exception> _errorHandlerException;
        private static readonly Action<ILogger, Exception> _responseStartedErrorPageMiddleware;
        private static readonly Action<ILogger, Exception, Exception> _displayErrorPageException;

        static DiagnosticsLoggerExtensions()
        {
            _unhandledException = LoggerMessage.Define<Exception>(
                LogLevel.Error,
                1,
                "An unhandled exception has occurred while executing the request: {Exception}");

            _responseStartedErrorHandler = LoggerMessage.Define(
                LogLevel.Warning,
                2,
                "The response has already started, the error handler will not be executed.");

            _errorHandlerException = LoggerMessage.Define<Exception>(
                LogLevel.Error,
                3,
                "An exception was thrown attempting to execute the error handler: {Exception}");

            _responseStartedErrorPageMiddleware = LoggerMessage.Define(
                LogLevel.Warning,
                2,
                "The response has already started, the error page middleware will not be executed.");

            _displayErrorPageException = LoggerMessage.Define<Exception>(
                LogLevel.Error,
                3,
                "An exception was thrown attempting to display the error page: {Exception}");
        }

        public static void UnhandledException(this ILogger logger, Exception exception)
        {
            _unhandledException(logger, exception, null);
        }

        public static void ResponseStartedErrorHandler(this ILogger logger)
        {
            _responseStartedErrorHandler(logger, null);
        }

        public static void ErrorHandlerException(this ILogger logger, Exception exception)
        {
            _errorHandlerException(logger, exception, null);
        }

        public static void ResponseStartedErrorPageMiddleware(this ILogger logger)
        {
            _responseStartedErrorPageMiddleware(logger, null);
        }

        public static void DisplayErrorPageException(this ILogger logger, Exception exception)
        {
            _displayErrorPageException(logger, exception, null);
        }
    }
}
