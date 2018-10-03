﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.Diagnostics.HealthChecks
{
    /// <summary>
    /// Options for the default service that executes <see cref="IHealthCheckPublisher"/> instances.
    /// </summary>
    public sealed class HealthCheckPublisherOptions
    {
        /// <summary>
        /// Gets or sets the initial delay applied after the application starts before executing 
        /// <see cref="IHealthCheckPublisher"/> instances. The delay is applied once at startup, and does
        /// not apply to subsequent iterations. The default value is 5 seconds.
        /// </summary>
        public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Gets or sets the period of <see cref="IHealthCheckPublisher"/> execution. The default value is
        /// 30 seconds.
        /// </summary>
        public TimeSpan Period { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Gets or sets a predicate that is used to filter the set of health checks executed.
        /// </summary>
        /// <remarks>
        /// If <see cref="Predicate"/> is <c>null</c>, the health check publisher service will run all
        /// registered health checks - this is the default behavior. To run a subset of health checks,
        /// provide a function that filters the set of checks.
        /// </remarks>
        public Func<HealthCheckRegistration, bool> Predicate { get; set; }

        /// <summary>
        /// Gets or sets the timeout for executing the health checks an all <see cref="IHealthCheckPublisher"/> 
        /// instances. Use <see cref="System.Threading.Timeout.InfiniteTimeSpan"/> to execute with no timeout.
        /// The default value is 30 seconds.
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
