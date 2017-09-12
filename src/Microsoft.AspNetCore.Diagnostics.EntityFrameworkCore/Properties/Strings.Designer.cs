// <auto-generated />
namespace Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
{
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    internal static class Strings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore.Strings", typeof(Strings).GetTypeInfo().Assembly);

        /// <summary>
        /// The context type '{0}' was not found in services. This usually means the context was not registered in services during startup. You probably want to call AddScoped&lt;&gt;() inside the UseServices(...) call in your application startup code. Skipping display of the database error page.
        /// </summary>
        internal static string DatabaseErrorPageMiddleware_ContextNotRegistered
        {
            get => GetString("DatabaseErrorPageMiddleware_ContextNotRegistered");
        }

        /// <summary>
        /// The context type '{0}' was not found in services. This usually means the context was not registered in services during startup. You probably want to call AddScoped&lt;&gt;() inside the UseServices(...) call in your application startup code. Skipping display of the database error page.
        /// </summary>
        internal static string FormatDatabaseErrorPageMiddleware_ContextNotRegistered(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DatabaseErrorPageMiddleware_ContextNotRegistered"), p0);

        /// <summary>
        /// An exception occurred while calculating the database error page content. Skipping display of the database error page.
        /// </summary>
        internal static string DatabaseErrorPageMiddleware_Exception
        {
            get => GetString("DatabaseErrorPageMiddleware_Exception");
        }

        /// <summary>
        /// An exception occurred while calculating the database error page content. Skipping display of the database error page.
        /// </summary>
        internal static string FormatDatabaseErrorPageMiddleware_Exception()
            => GetString("DatabaseErrorPageMiddleware_Exception");

        /// <summary>
        /// &gt; dotnet ef migrations add [migration name]
        /// </summary>
        internal static string DatabaseErrorPage_AddMigrationCommandCLI
        {
            get => GetString("DatabaseErrorPage_AddMigrationCommandCLI");
        }

        /// <summary>
        /// &gt; dotnet ef migrations add [migration name]
        /// </summary>
        internal static string FormatDatabaseErrorPage_AddMigrationCommandCLI()
            => GetString("DatabaseErrorPage_AddMigrationCommandCLI");

        /// <summary>
        /// Apply Migrations
        /// </summary>
        internal static string DatabaseErrorPage_ApplyMigrationsButton
        {
            get => GetString("DatabaseErrorPage_ApplyMigrationsButton");
        }

        /// <summary>
        /// Apply Migrations
        /// </summary>
        internal static string FormatDatabaseErrorPage_ApplyMigrationsButton()
            => GetString("DatabaseErrorPage_ApplyMigrationsButton");

        /// <summary>
        /// Migrations Applied
        /// </summary>
        internal static string DatabaseErrorPage_ApplyMigrationsButtonDone
        {
            get => GetString("DatabaseErrorPage_ApplyMigrationsButtonDone");
        }

        /// <summary>
        /// Migrations Applied
        /// </summary>
        internal static string FormatDatabaseErrorPage_ApplyMigrationsButtonDone()
            => GetString("DatabaseErrorPage_ApplyMigrationsButtonDone");

        /// <summary>
        /// Applying Migrations...
        /// </summary>
        internal static string DatabaseErrorPage_ApplyMigrationsButtonRunning
        {
            get => GetString("DatabaseErrorPage_ApplyMigrationsButtonRunning");
        }

        /// <summary>
        /// Applying Migrations...
        /// </summary>
        internal static string FormatDatabaseErrorPage_ApplyMigrationsButtonRunning()
            => GetString("DatabaseErrorPage_ApplyMigrationsButtonRunning");

        /// <summary>
        /// An error occurred applying migrations, try applying them from the command line
        /// </summary>
        internal static string DatabaseErrorPage_ApplyMigrationsFailed
        {
            get => GetString("DatabaseErrorPage_ApplyMigrationsFailed");
        }

        /// <summary>
        /// An error occurred applying migrations, try applying them from the command line
        /// </summary>
        internal static string FormatDatabaseErrorPage_ApplyMigrationsFailed()
            => GetString("DatabaseErrorPage_ApplyMigrationsFailed");

        /// <summary>
        /// In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:
        /// </summary>
        internal static string DatabaseErrorPage_HowToApplyFromPMC
        {
            get => GetString("DatabaseErrorPage_HowToApplyFromPMC");
        }

        /// <summary>
        /// In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:
        /// </summary>
        internal static string FormatDatabaseErrorPage_HowToApplyFromPMC()
            => GetString("DatabaseErrorPage_HowToApplyFromPMC");

        /// <summary>
        /// Try refreshing the page
        /// </summary>
        internal static string DatabaseErrorPage_MigrationsAppliedRefresh
        {
            get => GetString("DatabaseErrorPage_MigrationsAppliedRefresh");
        }

        /// <summary>
        /// Try refreshing the page
        /// </summary>
        internal static string FormatDatabaseErrorPage_MigrationsAppliedRefresh()
            => GetString("DatabaseErrorPage_MigrationsAppliedRefresh");

        /// <summary>
        /// In Visual Studio, use the Package Manager Console to scaffold a new migration and apply it to the database:
        /// </summary>
        internal static string DatabaseErrorPage_NoDbOrMigrationsInfoPMC
        {
            get => GetString("DatabaseErrorPage_NoDbOrMigrationsInfoPMC");
        }

        /// <summary>
        /// In Visual Studio, use the Package Manager Console to scaffold a new migration and apply it to the database:
        /// </summary>
        internal static string FormatDatabaseErrorPage_NoDbOrMigrationsInfoPMC()
            => GetString("DatabaseErrorPage_NoDbOrMigrationsInfoPMC");

        /// <summary>
        /// Use migrations to create the database for {0}
        /// </summary>
        internal static string DatabaseErrorPage_NoDbOrMigrationsTitle
        {
            get => GetString("DatabaseErrorPage_NoDbOrMigrationsTitle");
        }

        /// <summary>
        /// Use migrations to create the database for {0}
        /// </summary>
        internal static string FormatDatabaseErrorPage_NoDbOrMigrationsTitle(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DatabaseErrorPage_NoDbOrMigrationsTitle"), p0);

        /// <summary>
        /// In Visual Studio, use the Package Manager Console to scaffold a new migration for these changes and apply them to the database:
        /// </summary>
        internal static string DatabaseErrorPage_PendingChangesInfoPMC
        {
            get => GetString("DatabaseErrorPage_PendingChangesInfoPMC");
        }

        /// <summary>
        /// In Visual Studio, use the Package Manager Console to scaffold a new migration for these changes and apply them to the database:
        /// </summary>
        internal static string FormatDatabaseErrorPage_PendingChangesInfoPMC()
            => GetString("DatabaseErrorPage_PendingChangesInfoPMC");

        /// <summary>
        /// There are pending model changes for {0}
        /// </summary>
        internal static string DatabaseErrorPage_PendingChangesTitle
        {
            get => GetString("DatabaseErrorPage_PendingChangesTitle");
        }

        /// <summary>
        /// There are pending model changes for {0}
        /// </summary>
        internal static string FormatDatabaseErrorPage_PendingChangesTitle(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DatabaseErrorPage_PendingChangesTitle"), p0);

        /// <summary>
        /// There are migrations for {0} that have not been applied to the database
        /// </summary>
        internal static string DatabaseErrorPage_PendingMigrationsInfo
        {
            get => GetString("DatabaseErrorPage_PendingMigrationsInfo");
        }

        /// <summary>
        /// There are migrations for {0} that have not been applied to the database
        /// </summary>
        internal static string FormatDatabaseErrorPage_PendingMigrationsInfo(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DatabaseErrorPage_PendingMigrationsInfo"), p0);

        /// <summary>
        /// Applying existing migrations for {0} may resolve this issue
        /// </summary>
        internal static string DatabaseErrorPage_PendingMigrationsTitle
        {
            get => GetString("DatabaseErrorPage_PendingMigrationsTitle");
        }

        /// <summary>
        /// Applying existing migrations for {0} may resolve this issue
        /// </summary>
        internal static string FormatDatabaseErrorPage_PendingMigrationsTitle(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DatabaseErrorPage_PendingMigrationsTitle"), p0);

        /// <summary>
        /// &gt; dotnet ef database update
        /// </summary>
        internal static string DatabaseErrorPage_ApplyMigrationsCommandCLI
        {
            get => GetString("DatabaseErrorPage_ApplyMigrationsCommandCLI");
        }

        /// <summary>
        /// &gt; dotnet ef database update
        /// </summary>
        internal static string FormatDatabaseErrorPage_ApplyMigrationsCommandCLI()
            => GetString("DatabaseErrorPage_ApplyMigrationsCommandCLI");

        /// <summary>
        /// Migrations successfully applied for context '{0}'.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_Applied
        {
            get => GetString("MigrationsEndPointMiddleware_Applied");
        }

        /// <summary>
        /// Migrations successfully applied for context '{0}'.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_Applied(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("MigrationsEndPointMiddleware_Applied"), p0);

        /// <summary>
        /// Request is valid, applying migrations for context '{0}'.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_ApplyingMigrations
        {
            get => GetString("MigrationsEndPointMiddleware_ApplyingMigrations");
        }

        /// <summary>
        /// Request is valid, applying migrations for context '{0}'.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_ApplyingMigrations(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("MigrationsEndPointMiddleware_ApplyingMigrations"), p0);

        /// <summary>
        /// The context type '{0}' was not found in services. This usually means the context was not registered in services during startup. You probably want to call AddScoped&lt;{0}&gt;() inside the UseServices(...) call in your application startup code.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_ContextNotRegistered
        {
            get => GetString("MigrationsEndPointMiddleware_ContextNotRegistered");
        }

        /// <summary>
        /// The context type '{0}' was not found in services. This usually means the context was not registered in services during startup. You probably want to call AddScoped&lt;{0}&gt;() inside the UseServices(...) call in your application startup code.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_ContextNotRegistered(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("MigrationsEndPointMiddleware_ContextNotRegistered"), p0);

        /// <summary>
        /// An error occurred while applying the migrations for '{0}'. See InnerException for details.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_Exception
        {
            get => GetString("MigrationsEndPointMiddleware_Exception");
        }

        /// <summary>
        /// An error occurred while applying the migrations for '{0}'. See InnerException for details.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_Exception(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("MigrationsEndPointMiddleware_Exception"), p0);

        /// <summary>
        /// The context type '{0}' could not be loaded. Ensure this is the correct type name for the context you are trying to apply migrations for.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_InvalidContextType
        {
            get => GetString("MigrationsEndPointMiddleware_InvalidContextType");
        }

        /// <summary>
        /// The context type '{0}' could not be loaded. Ensure this is the correct type name for the context you are trying to apply migrations for.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_InvalidContextType(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("MigrationsEndPointMiddleware_InvalidContextType"), p0);

        /// <summary>
        /// No context type was specified. Ensure the form data from the request includes a contextTypeName value, specifying the context to apply migrations for.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_NoContextType
        {
            get => GetString("MigrationsEndPointMiddleware_NoContextType");
        }

        /// <summary>
        /// No context type was specified. Ensure the form data from the request includes a contextTypeName value, specifying the context to apply migrations for.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_NoContextType()
            => GetString("MigrationsEndPointMiddleware_NoContextType");

        /// <summary>
        /// Request path matched the path configured for this migrations endpoint ({0}). Attempting to process the migrations request.
        /// </summary>
        internal static string MigrationsEndPointMiddleware_RequestPathMatched
        {
            get => GetString("MigrationsEndPointMiddleware_RequestPathMatched");
        }

        /// <summary>
        /// Request path matched the path configured for this migrations endpoint ({0}). Attempting to process the migrations request.
        /// </summary>
        internal static string FormatMigrationsEndPointMiddleware_RequestPathMatched(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("MigrationsEndPointMiddleware_RequestPathMatched"), p0);

        /// <summary>
        /// A database operation failed while processing the request.
        /// </summary>
        internal static string DatabaseErrorPage_Title
        {
            get => GetString("DatabaseErrorPage_Title");
        }

        /// <summary>
        /// A database operation failed while processing the request.
        /// </summary>
        internal static string FormatDatabaseErrorPage_Title()
            => GetString("DatabaseErrorPage_Title");

        /// <summary>
        /// {0} occurred, checking if Entity Framework recorded this exception as resulting from a failed database operation.
        /// </summary>
        internal static string DatabaseErrorPage_AttemptingToMatchException
        {
            get => GetString("DatabaseErrorPage_AttemptingToMatchException");
        }

        /// <summary>
        /// {0} occurred, checking if Entity Framework recorded this exception as resulting from a failed database operation.
        /// </summary>
        internal static string FormatDatabaseErrorPage_AttemptingToMatchException(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DatabaseErrorPage_AttemptingToMatchException"), p0);

        /// <summary>
        /// Entity Framework recorded that the current exception was due to a failed database operation. Attempting to show database error page.
        /// </summary>
        internal static string DatabaseErrorPage_Matched
        {
            get => GetString("DatabaseErrorPage_Matched");
        }

        /// <summary>
        /// Entity Framework recorded that the current exception was due to a failed database operation. Attempting to show database error page.
        /// </summary>
        internal static string FormatDatabaseErrorPage_Matched()
            => GetString("DatabaseErrorPage_Matched");

        /// <summary>
        /// Entity Framework did not record any exceptions due to failed database operations. This means the current exception is not a failed Entity Framework database operation, or the current exception occurred from a DbContext that was not obtained from request services.
        /// </summary>
        internal static string DatabaseErrorPage_NoRecordedException
        {
            get => GetString("DatabaseErrorPage_NoRecordedException");
        }

        /// <summary>
        /// Entity Framework did not record any exceptions due to failed database operations. This means the current exception is not a failed Entity Framework database operation, or the current exception occurred from a DbContext that was not obtained from request services.
        /// </summary>
        internal static string FormatDatabaseErrorPage_NoRecordedException()
            => GetString("DatabaseErrorPage_NoRecordedException");

        /// <summary>
        /// The target data store is not a relational database. Skipping the database error page.
        /// </summary>
        internal static string DatabaseErrorPage_NotRelationalDatabase
        {
            get => GetString("DatabaseErrorPage_NotRelationalDatabase");
        }

        /// <summary>
        /// The target data store is not a relational database. Skipping the database error page.
        /// </summary>
        internal static string FormatDatabaseErrorPage_NotRelationalDatabase()
            => GetString("DatabaseErrorPage_NotRelationalDatabase");

        /// <summary>
        /// The current exception (and its inner exceptions) do not match the last exception Entity Framework recorded due to a failed database operation. This means the database operation exception was handled and another exception occurred later in the request.
        /// </summary>
        internal static string DatabaseErrorPage_NoMatch
        {
            get => GetString("DatabaseErrorPage_NoMatch");
        }

        /// <summary>
        /// The current exception (and its inner exceptions) do not match the last exception Entity Framework recorded due to a failed database operation. This means the database operation exception was handled and another exception occurred later in the request.
        /// </summary>
        internal static string FormatDatabaseErrorPage_NoMatch()
            => GetString("DatabaseErrorPage_NoMatch");

        /// <summary>
        /// PM&gt; Add-Migration [migration name]
        /// </summary>
        internal static string DatabaseErrorPage_AddMigrationCommandPMC
        {
            get => GetString("DatabaseErrorPage_AddMigrationCommandPMC");
        }

        /// <summary>
        /// PM&gt; Add-Migration [migration name]
        /// </summary>
        internal static string FormatDatabaseErrorPage_AddMigrationCommandPMC()
            => GetString("DatabaseErrorPage_AddMigrationCommandPMC");

        /// <summary>
        /// PM&gt; Update-Database
        /// </summary>
        internal static string DatabaseErrorPage_ApplyMigrationsCommandPMC
        {
            get => GetString("DatabaseErrorPage_ApplyMigrationsCommandPMC");
        }

        /// <summary>
        /// PM&gt; Update-Database
        /// </summary>
        internal static string FormatDatabaseErrorPage_ApplyMigrationsCommandPMC()
            => GetString("DatabaseErrorPage_ApplyMigrationsCommandPMC");

        /// <summary>
        /// Alternatively, you can scaffold a new migration and apply it from a command prompt at your project directory:
        /// </summary>
        internal static string DatabaseErrorPage_NoDbOrMigrationsInfoCLI
        {
            get => GetString("DatabaseErrorPage_NoDbOrMigrationsInfoCLI");
        }

        /// <summary>
        /// Alternatively, you can scaffold a new migration and apply it from a command prompt at your project directory:
        /// </summary>
        internal static string FormatDatabaseErrorPage_NoDbOrMigrationsInfoCLI()
            => GetString("DatabaseErrorPage_NoDbOrMigrationsInfoCLI");

        /// <summary>
        /// Alternatively, you can scaffold a new migration and apply it from a command prompt at your project directory:
        /// </summary>
        internal static string DatabaseErrorPage_PendingChangesInfoCLI
        {
            get => GetString("DatabaseErrorPage_PendingChangesInfoCLI");
        }

        /// <summary>
        /// Alternatively, you can scaffold a new migration and apply it from a command prompt at your project directory:
        /// </summary>
        internal static string FormatDatabaseErrorPage_PendingChangesInfoCLI()
            => GetString("DatabaseErrorPage_PendingChangesInfoCLI");

        /// <summary>
        /// Alternatively, you can apply pending migrations from a command prompt at your project directory:
        /// </summary>
        internal static string DatabaseErrorPage_HowToApplyFromCLI
        {
            get => GetString("DatabaseErrorPage_HowToApplyFromCLI");
        }

        /// <summary>
        /// Alternatively, you can apply pending migrations from a command prompt at your project directory:
        /// </summary>
        internal static string FormatDatabaseErrorPage_HowToApplyFromCLI()
            => GetString("DatabaseErrorPage_HowToApplyFromCLI");

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);

            System.Diagnostics.Debug.Assert(value != null);

            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }

            return value;
        }
    }
}
