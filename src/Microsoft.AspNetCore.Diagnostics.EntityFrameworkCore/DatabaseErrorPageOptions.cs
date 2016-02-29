// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Options for the <see cref="DatabaseErrorPageMiddleware"/>.
    /// </summary>
    public class DatabaseErrorPageOptions
    {
        /// <summary>
        /// Constructs a new DatabaseErrorPageOptions.
        /// </summary>
        public DatabaseErrorPageOptions() { }

        /// <summary>
        /// Constructs and configures a new DatabaseErrorPageOptions.
        /// </summary>
        /// <param name="configureOptions">The configuration actions that will be applied.</param>
        public DatabaseErrorPageOptions(IEnumerable<IConfigureOptions<DatabaseErrorPageOptions>> configureOptions) : this()
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            foreach (var configure in configureOptions)
            {
                configure.Configure(this);
            }
        }

        /// <summary>
        /// Gets or sets the path that <see cref="MigrationsEndPointMiddleware"/> will listen
        /// for requests to execute migrations commands.
        /// </summary>
        public virtual PathString MigrationsEndPointPath { get; set; } = MigrationsEndPointOptions.DefaultPath;
    }
}
