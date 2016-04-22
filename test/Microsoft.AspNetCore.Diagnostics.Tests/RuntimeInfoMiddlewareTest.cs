// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Microsoft.AspNetCore.Diagnostics.Tests
{
    public class RuntimeInfoMiddlewareTest
    {
        private const string DefaultPath = "/runtimeinfo";

        [Fact]
        public void DefaultPageOptions_HasDefaultPath()
        {
            // Arrange & act
            var options = new RuntimeInfoPageOptions();

            // Assert
            Assert.Equal(DefaultPath, options.Path.Value);
        }

        [Fact]
        public void CreateRuntimeInfoModel_GetsTheVersionAndAllPackages()
        {
            // Arrage
            RequestDelegate next = _ =>
            {
                return Task.FromResult<object>(null);
            };

            var middleware = new RuntimeInfoMiddleware(
                next,
                Options.Create(new RuntimeInfoPageOptions()));

            // Act
            var model = middleware.CreateRuntimeInfoModel();

            // Assert
            Assert.Equal(typeof(object).GetTypeInfo().Assembly.GetName().Version.ToString(), model.Version);
            Assert.Equal(RuntimeInformation.OSDescription, model.OperatingSystem);
#if NET451
            Assert.Equal("CLR", model.RuntimeType);
#else
            Assert.Equal("CoreCLR", model.RuntimeType);
#endif
            Assert.Equal("x64", model.RuntimeArchitecture);
        }

        [Fact]
        public async void Invoke_WithNonMatchingPath_IgnoresRequest()
        {
            // Arrange
            RequestDelegate next = _ =>
            {
                return Task.FromResult<object>(null);
            };

            var middleware = new RuntimeInfoMiddleware(
               next,
               Options.Create(new RuntimeInfoPageOptions()));

            var contextMock = new Mock<HttpContext>(MockBehavior.Strict);
            contextMock
                .SetupGet(c => c.Request.Path)
                .Returns(new PathString("/nonmatchingpath"));

            // Act
            await middleware.Invoke(contextMock.Object);

            // Assert
            contextMock.VerifyGet(c => c.Request.Path, Times.Once());
        }

        [Fact]
        public async void Invoke_WithMatchingPath_ReturnsInfoPage()
        {
            // Arrange
            RequestDelegate next = _ =>
            {
                return Task.FromResult<object>(null);
            };

            var middleware = new RuntimeInfoMiddleware(
                next,
                Options.Create(new RuntimeInfoPageOptions()));

            var buffer = new byte[4096];
            using (var responseStream = new MemoryStream(buffer))
            {
                var contextMock = new Mock<HttpContext>(MockBehavior.Strict);
                contextMock
                    .SetupGet(c => c.Request.Path)
                    .Returns(new PathString("/runtimeinfo"));
                contextMock
                    .SetupGet(c => c.Response.Body)
                    .Returns(responseStream);
                contextMock
                    .SetupGet(c => c.RequestServices)
                    .Returns(() => null);

                // Act
                await middleware.Invoke(contextMock.Object);

                // Assert
                string response = Encoding.UTF8.GetString(buffer);

                Assert.Contains($"<p>Operating System: {RuntimeInformation.OSDescription}</p>", response);
                Assert.Contains($"<p>Runtime Architecture: {RuntimeInformation.ProcessArchitecture}</p>", response);
#if NET451
                Assert.Contains($"<p>Runtime Type: CLR</p>", response);
#else
                Assert.Contains($"<p>Runtime Type: CoreCLR</p>", response);
#endif
            }
        }
    }
}
