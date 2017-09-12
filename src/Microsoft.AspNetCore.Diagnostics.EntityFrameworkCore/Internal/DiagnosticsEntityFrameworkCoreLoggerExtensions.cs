// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore.Internal
{
    internal static class DiagnosticsEntityFrameworkCoreLoggerExtensions
    {
        private static readonly Action<ILogger, Exception> _noContextType;
        private static readonly Action<ILogger, Type, Exception> _invalidContextType;
        private static readonly Action<ILogger, string, Exception> _contextNotRegistered;
        private static readonly Action<ILogger, string, Exception> _requestPathMatched;
        private static readonly Action<ILogger, string, Exception> _applyingMigrations;
        private static readonly Action<ILogger, string, Exception> _migrationsApplied;
        private static readonly Action<ILogger, string, Exception, Exception> _migrationsEndPointMiddlewareException;
        private static readonly Action<ILogger, Type, Exception> _attemptingToMatchException;
        private static readonly Action<ILogger, string, Exception> _contextNotRegisteredDatabaseErrorPageMiddleware;
        private static readonly Action<ILogger, Exception> _notRelationalDatabase;
        private static readonly Action<ILogger, Exception> _noRecordedException;
        private static readonly Action<ILogger, Exception> _noMatch;
        private static readonly Action<ILogger, Exception> _matched;
        private static readonly Action<ILogger, Exception, Exception> _databaseErrorPageMiddlewareException;

        static DiagnosticsEntityFrameworkCoreLoggerExtensions()
        {
            _noContextType = LoggerMessage.Define(
                LogLevel.Error,
                1,
                "No context type was specified. Ensure the form data from the request includes a contextTypeName value, specifying the context to apply migrations for.");

            _invalidContextType = LoggerMessage.Define<Type>(
                LogLevel.Error,
                2,
                "The context type '{ContextType}' could not be loaded. Ensure this is the correct type name for the context you are trying to apply migrations for.");

            _contextNotRegistered = LoggerMessage.Define<string>(
                LogLevel.Error,
                3,
                "The context type '{Context}' was not found in services. This usually means the context was not registered in services during startup. You probably want to call AddScoped<>() inside the UseServices(...) call in your application startup code.");

            _requestPathMatched = LoggerMessage.Define<string>(
                LogLevel.Debug,
                4,
                "Request path matched the path configured for this migrations endpoint({RequestPath}). Attempting to process the migrations request.");

            _applyingMigrations = LoggerMessage.Define<string>(
                LogLevel.Debug,
                5,
                "Request is valid, applying migrations for context '{Context}'");

            _migrationsApplied = LoggerMessage.Define<string>(
                LogLevel.Debug,
                6,
                "Migrations successfully applied for context '{Context}'.");

            _migrationsEndPointMiddlewareException = LoggerMessage.Define<string, Exception>(
                LogLevel.Error,
                7,
                "An error occurred while applying the migrations for '{Context}'. See InnerException for details.: {Exception}");

            _attemptingToMatchException = LoggerMessage.Define<Type>(
                LogLevel.Debug,
                1,
                "{ExceptionType} occurred, checking if Entity Framework recorded this exception as resulting from a failed database operation.");

            _noRecordedException = LoggerMessage.Define(
                LogLevel.Debug,
                2,
                "Entity Framework did not record any exceptions due to failed database operations. This means the current exception is not a failed Entity Framework database operation, or the current exception occurred from a DbContext that was not obtained from request services.");

            _noMatch = LoggerMessage.Define(
                LogLevel.Debug,
                3,
                "The current exception (and its inner exceptions) do not match the last exception Entity Framework recorded due to a failed database operation. This means the database operation exception was handled and another exception occurred later in the request.");

            _matched = LoggerMessage.Define(
                LogLevel.Debug,
                4,
                "Entity Framework recorded that the current exception was due to a failed database operation. Attempting to show database error page.");

            _contextNotRegisteredDatabaseErrorPageMiddleware = LoggerMessage.Define<string>(
                LogLevel.Error,
                5,
                "The context type '{Context}' was not found in services. This usually means the context was not registered in services during startup. You probably want to call AddScoped<>() inside the UseServices(...) call in your application startup code. Skipping display of the database error page.");

            _notRelationalDatabase = LoggerMessage.Define(
                LogLevel.Debug,
                6,
                "The target data store is not a relational database. Skipping the database error page.");

            _databaseErrorPageMiddlewareException = LoggerMessage.Define<Exception>(
                LogLevel.Error,
                7,
                "An exception occurred while calculating the database error page content. Skipping display of the database error page.: {Exception}");
        }

        public static void NoContextType(this ILogger logger)
        {
            _noContextType(logger, null);
        }

        public static void InvalidContextType(this ILogger logger, Type contextType)
        {
            _invalidContextType(logger, contextType, null);
        }

        public static void ContextNotRegistered(this ILogger logger, string context)
        {
            _contextNotRegistered(logger, context, null);
        }

        public static void RequestPathMatched(this ILogger logger, string requestPath)
        {
            _requestPathMatched(logger, requestPath, null);
        }

        public static void ApplyingMigrations(this ILogger logger, string context)
        {
            _applyingMigrations(logger, context, null);
        }

        public static void MigrationsApplied(this ILogger logger, string context)
        {
            _migrationsApplied(logger, context, null);
        }

        public static void MigrationsEndPointMiddlewareException(this ILogger logger, string context, Exception exception)
        {
            _migrationsEndPointMiddlewareException(logger, context, exception, null);
        }

        public static void AttemptingToMatchException(this ILogger logger, Type exceptionType)
        {
            _attemptingToMatchException(logger, exceptionType, null);
        }

        public static void NoRecordedException(this ILogger logger)
        {
            _noRecordedException(logger, null);
        }

        public static void NoMatch(this ILogger logger)
        {
            _noMatch(logger, null);
        }

        public static void Matched(this ILogger logger)
        {
            _matched(logger, null);
        }

        public static void NotRelationalDatabase(this ILogger logger)
        {
            _notRelationalDatabase(logger, null);
        }

        public static void ContextNotRegistereDatabaseErrorPageMiddleware(this ILogger logger, string context)
        {
            _contextNotRegisteredDatabaseErrorPageMiddleware(logger, context, null);
        }

        public static void DatabaseErrorPageMiddlewareException(this ILogger logger, Exception exception)
        {
            _databaseErrorPageMiddlewareException(logger, exception, null);
        }
    }
}
