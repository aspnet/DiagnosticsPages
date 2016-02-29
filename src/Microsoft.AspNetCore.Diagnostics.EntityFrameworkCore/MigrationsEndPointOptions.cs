// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Options for the <see cref="MigrationsEndPointMiddleware"/>.
    /// </summary>
    public class MigrationsEndPointOptions
    {
        /// <summary>
        /// Constructs a new MigrationsEndPointOptions.
        /// </summary>
        public MigrationsEndPointOptions() { }

        /// <summary>
        /// Constructs and configures a new MigrationsEndPointOptions.
        /// </summary>
        /// <param name="configureOptions">The configuration actions that will be applied.</param>
        public MigrationsEndPointOptions(IEnumerable<IConfigureOptions<MigrationsEndPointOptions>> configureOptions) : this()
        {
            if (configureOptions != null)
            {
                foreach (var configure in configureOptions)
                {
                    configure.Configure(this);
                }
            }
        }

        /// <summary>
        /// The default value for <see cref="Path"/>.
        /// </summary>
        public static PathString DefaultPath = new PathString("/ApplyDatabaseMigrations");

        /// <summary>
        /// Gets or sets the path that the <see cref="MigrationsEndPointMiddleware"/> will listen
        /// for requests to execute migrations commands.
        /// </summary>
        public virtual PathString Path { get; set; } = DefaultPath;
    }
}
