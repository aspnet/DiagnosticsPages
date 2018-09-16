// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides basic extension methods for registering <see cref="IHealthCheck"/> instances in an <see cref="IHealthChecksBuilder"/>.
    /// </summary>
    public static class HealthChecksBuilderAddCheckExtensions
    {
        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="instance">An <see cref="IHealthCheck"/> instance.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string name, IHealthCheck instance)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return AddCheck(builder, name, failureStatus: null, tags: null, instance);
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="failureStatus">
        /// The <see cref="HealthStatus"/> that should be reported when the health check reports a failure. If the provided value
        /// is <c>null</c>, then <see cref="HealthStatus.Unhealthy"/> will be reported.
        /// </param>
        /// <param name="instance">An <see cref="IHealthCheck"/> instance.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus, IHealthCheck instance)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return AddCheck(builder, name, failureStatus, tags: null, instance);
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="failureStatus">
        /// The <see cref="HealthStatus"/> that should be reported when the health check reports a failure. If the provided value
        /// is <c>null</c>, then <see cref="HealthStatus.Unhealthy"/> will be reported.
        /// </param>
        /// <param name="tags">A list of tags that can be used to filter health checks.</param>
        /// <param name="instance">An <see cref="IHealthCheck"/> instance.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddCheck(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus,
            IEnumerable<string> tags,
            IHealthCheck instance)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return builder.Add(new HealthCheckRegistration(name, failureStatus, tags, instance));
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <typeparam name="T">The health check implementation type.</typeparam>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        /// <remarks>
        /// This method will use <see cref="ActivatorUtilities.GetServiceOrCreateInstance{T}(IServiceProvider)"/> to create the health check
        /// instance when needed. If a service of type <typeparamref name="T"/> is registred in the dependency injection container
        /// with any liftime it will be used. Otherwise an instance of type <typeparamref name="T"/> will be constructed with
        /// access to services from the dependency injection container.
        /// </remarks>
        public static IHealthChecksBuilder AddCheck<T>(this IHealthChecksBuilder builder, string name) where T : class, IHealthCheck
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return AddCheck<T>(builder, name, failureStatus: null, tags: null);
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <typeparam name="T">The health check implementation type.</typeparam>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="failureStatus">
        /// The <see cref="HealthStatus"/> that should be reported when the health check reports a failure. If the provided value
        /// is <c>null</c>, then <see cref="HealthStatus.Unhealthy"/> will be reported.
        /// </param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        /// <remarks>
        /// This method will use <see cref="ActivatorUtilities.GetServiceOrCreateInstance{T}(IServiceProvider)"/> to create the health check
        /// instance when needed. If a service of type <typeparamref name="T"/> is registred in the dependency injection container
        /// with any liftime it will be used. Otherwise an instance of type <typeparamref name="T"/> will be constructed with
        /// access to services from the dependency injection container.
        /// </remarks>
        public static IHealthChecksBuilder AddCheck<T>(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus) where T : class, IHealthCheck
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return AddCheck<T>(builder, name, failureStatus, tags: null);
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <typeparam name="T">The health check implementation type.</typeparam>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="failureStatus">
        /// The <see cref="HealthStatus"/> that should be reported when the health check reports a failure. If the provided value
        /// is <c>null</c>, then <see cref="HealthStatus.Unhealthy"/> will be reported.
        /// </param>
        /// <param name="tags">A list of tags that can be used to filter health checks.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        /// <remarks>
        /// This method will use <see cref="ActivatorUtilities.GetServiceOrCreateInstance{T}(IServiceProvider)"/> to create the health check
        /// instance when needed. If a service of type <typeparamref name="T"/> is registred in the dependency injection container
        /// with any liftime it will be used. Otherwise an instance of type <typeparamref name="T"/> will be constructed with
        /// access to services from the dependency injection container.
        /// </remarks>
        public static IHealthChecksBuilder AddCheck<T>(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus,
            IEnumerable<string> tags) where T : class, IHealthCheck
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return builder.Add(new HealthCheckRegistration(name, failureStatus, tags, s => ActivatorUtilities.GetServiceOrCreateInstance<T>(s)));
        }

        /// <summary>
        /// Adds a new type activated health check with the specified name and implementation.
        /// </summary>
        /// <typeparam name="T">The health check implementation type.</typeparam>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="args">Additional arguments to provide to the constructor.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        /// <remarks>
        /// This method will use <see cref="ActivatorUtilities.CreateInstance{T}(IServiceProvider, object[])"/> to create the health check
        /// instance when needed. Additional arguments can be provided to the constructor via <paramref name="args"/>.
        /// </remarks>
        public static IHealthChecksBuilder AddTypeActivatedCheck<T>(this IHealthChecksBuilder builder, string name, params object[] args) where T : class, IHealthCheck
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return AddTypeActivatedCheck<T>(builder, name, failureStatus: null, tags: null);
        }

        /// <summary>
        /// Adds a new type activated health check with the specified name and implementation.
        /// </summary>
        /// <typeparam name="T">The health check implementation type.</typeparam>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="failureStatus">
        /// The <see cref="HealthStatus"/> that should be reported when the health check reports a failure. If the provided value
        /// is <c>null</c>, then <see cref="HealthStatus.Unhealthy"/> will be reported.
        /// </param>
        /// <param name="args">Additional arguments to provide to the constructor.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        /// <remarks>
        /// This method will use <see cref="ActivatorUtilities.CreateInstance{T}(IServiceProvider, object[])"/> to create the health check
        /// instance when needed. Additional arguments can be provided to the constructor via <paramref name="args"/>.
        /// </remarks>
        public static IHealthChecksBuilder AddTypeActivatedCheck<T>(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus,
            params object[] args) where T : class, IHealthCheck
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return AddTypeActivatedCheck<T>(builder, name, failureStatus, tags: null);
        }

        /// <summary>
        /// Adds a new type activated health check with the specified name and implementation.
        /// </summary>
        /// <typeparam name="T">The health check implementation type.</typeparam>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="failureStatus">
        /// The <see cref="HealthStatus"/> that should be reported when the health check reports a failure. If the provided value
        /// is <c>null</c>, then <see cref="HealthStatus.Unhealthy"/> will be reported.
        /// </param>
        /// <param name="tags">A list of tags that can be used to filter health checks.</param>
        /// <param name="args">Additional arguments to provide to the constructor.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        /// <remarks>
        /// This method will use <see cref="ActivatorUtilities.CreateInstance{T}(IServiceProvider, object[])"/> to create the health check
        /// instance when needed. Additional arguments can be provided to the constructor via <paramref name="args"/>.
        /// </remarks>
        public static IHealthChecksBuilder AddTypeActivatedCheck<T>(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus,
            IEnumerable<string> tags,
            params object[] args) where T : class, IHealthCheck
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return builder.Add(new HealthCheckRegistration(name, failureStatus, tags, s => ActivatorUtilities.CreateInstance<T>(s, args)));
        }
    }
}
