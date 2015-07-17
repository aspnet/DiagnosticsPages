// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Elm;
using Microsoft.AspNet.FeatureModel;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
#if DNX451
using Moq;
#endif
using Xunit;

namespace Microsoft.AspNet.Diagnostics.Tests
{
    public class ElmMiddlewareTest
    {
        private const string DefaultPath = "/Elm";

        [Fact]
        public void DefaultPageOptions_HasDefaultPath()
        {
            // Arrange & act
            var options = new ElmOptions();

            // Assert
            Assert.Equal(DefaultPath, options.Path.Value);
        }

#if DNX451
        [Fact]
        public async void Invoke_WithNonMatchingPath_IgnoresRequest()
        {
            // Arrange
            var elmStore = new ElmStore();
            var factory = new LoggerFactory();
            var optionsMock = new Mock<IOptions<ElmOptions>>();
            optionsMock
                .SetupGet(o => o.Options)
                .Returns(new ElmOptions());
            factory.AddProvider(new ElmLoggerProvider(elmStore, optionsMock.Object.Options));

            RequestDelegate next = _ =>
            {
                return Task.FromResult<object>(null);
            };

            var captureMiddleware = new ElmCaptureMiddleware(
                next,
                factory,
                optionsMock.Object);
            var pageMiddleware = new ElmPageMiddleware(
                next,
                optionsMock.Object,
                elmStore);

            var contextMock = GetMockContext("/nonmatchingpath");

            // Act
            await captureMiddleware.Invoke(contextMock.Object);
            await pageMiddleware.Invoke(contextMock.Object);

            // Assert
            // Request.Query is used by the ElmPageMiddleware to parse the query parameters
            contextMock.VerifyGet(c => c.Request.Query, Times.Never());
        }

        [Fact]
        public async void Invoke_WithMatchingPath_FulfillsRequest()
        {
            // Arrange
            var elmStore = new ElmStore();
            var factory = new LoggerFactory();
            var optionsMock = new Mock<IOptions<ElmOptions>>();
            optionsMock
                .SetupGet(o => o.Options)
                .Returns(new ElmOptions());
            factory.AddProvider(new ElmLoggerProvider(elmStore, optionsMock.Object.Options));

            RequestDelegate next = _ =>
            {
                return Task.FromResult<object>(null);
            };

            var captureMiddleware = new ElmCaptureMiddleware(
                next,
                factory,
                optionsMock.Object);
            var pageMiddleware = new ElmPageMiddleware(
                next,
                optionsMock.Object,
                elmStore);
            var contextMock = GetMockContext("/Elm");

            using (var responseStream = new MemoryStream())
            {
                contextMock
                    .SetupGet(c => c.Response.Body)
                    .Returns(responseStream);
                contextMock
                    .SetupGet(c => c.ApplicationServices)
                    .Returns(() => null);

                // Act
                await captureMiddleware.Invoke(contextMock.Object);
                await pageMiddleware.Invoke(contextMock.Object);

                string response = Encoding.UTF8.GetString(responseStream.ToArray());

                // Assert
                contextMock.VerifyGet(c => c.Request.Query, Times.AtLeastOnce());
                Assert.True(response.Contains("<title>ASP.NET Logs</title>"));
            }
        }

        [Fact]
        public async void Invoke_BadRequestShowsError()
        {
            // Arrange
            var elmStore = new ElmStore();
            var factory = new LoggerFactory();
            var optionsMock = new Mock<IOptions<ElmOptions>>();
            optionsMock
                .SetupGet(o => o.Options)
                .Returns(new ElmOptions());
            factory.AddProvider(new ElmLoggerProvider(elmStore, optionsMock.Object.Options));

            RequestDelegate next = _ =>
            {
                return Task.FromResult<object>(null);
            };

            var captureMiddleware = new ElmCaptureMiddleware(
                next,
                factory,
                optionsMock.Object);
            var pageMiddleware = new ElmPageMiddleware(
                next,
                optionsMock.Object,
                elmStore);
            var contextMock = GetMockContext("/Elm/666");

            using (var responseStream = new MemoryStream())
            {
                contextMock
                    .SetupGet(c => c.Response.Body)
                    .Returns(responseStream);

                // Act
                await captureMiddleware.Invoke(contextMock.Object);
                await pageMiddleware.Invoke(contextMock.Object);

                string response = Encoding.UTF8.GetString(responseStream.ToArray());

                // Assert
                contextMock.VerifyGet(c => c.Request.Query, Times.AtLeastOnce());
                Assert.True(response.Contains("Invalid Id"));
            }
        }

        private Mock<HttpContext> GetMockContext(string path)
        {
            var contextMock = new Mock<HttpContext>(MockBehavior.Strict);
            contextMock
                .SetupGet(c => c.Request.Path)
                .Returns(new PathString(path));
            contextMock
                .SetupGet(c => c.Request.Host)
                .Returns(new HostString("localhost"));
            contextMock
                .SetupGet(c => c.Request.ContentType)
                .Returns("");
            contextMock
                .SetupGet(c => c.Request.Scheme)
                .Returns("http");
            contextMock
                .SetupGet(c => c.Request.Scheme)
                .Returns("http");
            contextMock
                .SetupGet(c => c.Response.StatusCode)
                .Returns(200);
            contextMock
                .SetupGet(c => c.Response.Body)
                .Returns(new Mock<Stream>().Object);
            contextMock
                .SetupGet(c => c.User)
                .Returns(new ClaimsPrincipal());
            contextMock
                .SetupGet(c => c.Request.Method)
                .Returns("GET");
            contextMock
                .SetupGet(c => c.Request.Protocol)
                .Returns("HTTP/1.1");
            contextMock
                .SetupGet(c => c.Request.Headers)
                .Returns(new Mock<IHeaderDictionary>().Object);
            contextMock
                .SetupGet(c => c.Request.QueryString)
                .Returns(new QueryString());
            contextMock
                .SetupGet(c => c.Request.Query)
                .Returns(new Mock<IReadableStringCollection>().Object);
            contextMock
                .SetupGet(c => c.Request.Cookies)
                .Returns(new Mock<IReadableStringCollection>().Object);
            contextMock
                .Setup(c => c.Request.ReadFormAsync(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(Task.FromResult(new Mock<IFormCollection>().Object));
            contextMock
                .Setup(c => c.Request.HasFormContentType)
                .Returns(true);
            var requestIdentifier = new Mock<IHttpRequestIdentifierFeature>();
            requestIdentifier.Setup(f => f.TraceIdentifier).Returns(Guid.NewGuid().ToString());
            contextMock.Setup(c => c.GetFeature<IHttpRequestIdentifierFeature>())
                .Returns(requestIdentifier.Object);
            return contextMock;
        }
#endif

        [Fact]
        public async Task SetsNewIdentifierFeature_IfNotPresentOnContext()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new ElmLoggerProvider(new ElmStore(), new ElmOptions()));

            // Act & Assert
            var errorPageMiddleware = new ElmCaptureMiddleware((innerContext) =>
            {
                var feature = innerContext.GetFeature<IHttpRequestIdentifierFeature>();
                Assert.NotNull(feature);
                Assert.False(string.IsNullOrEmpty(feature.TraceIdentifier));
                return Task.FromResult(0);
            }, loggerFactory, new TestElmOptions());

            await errorPageMiddleware.Invoke(context);

            Assert.Null(context.GetFeature<IHttpRequestIdentifierFeature>());
        }

        [Fact]
        public async Task UsesIdentifierFeature_IfAlreadyPresentOnContext()
        {
            // Arrange
            var requestIdentifierFeature = new HttpRequestIdentifierFeature()
            {
                TraceIdentifier = Guid.NewGuid().ToString()
            };
            var features = new FeatureCollection();
            features.Add(typeof(IHttpRequestFeature), new HttpRequestFeature());
            features.Add(typeof(IHttpRequestIdentifierFeature), requestIdentifierFeature);
            features.Add(typeof(IHttpResponseFeature), new HttpResponseFeature());
            var context = new DefaultHttpContext(features);
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new ElmLoggerProvider(new ElmStore(), new ElmOptions()));

            // Act & Assert
            var errorPageMiddleware = new ElmCaptureMiddleware((innerContext) =>
            {
                Assert.Same(requestIdentifierFeature, innerContext.GetFeature<IHttpRequestIdentifierFeature>());
                return Task.FromResult(0);
            }, loggerFactory, new TestElmOptions());

            await errorPageMiddleware.Invoke(context);

            Assert.Same(requestIdentifierFeature, context.GetFeature<IHttpRequestIdentifierFeature>());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task UpdatesTraceIdentifier_IfNullOrEmpty(string requestId)
        {
            // Arrange
            var requestIdentifierFeature = new HttpRequestIdentifierFeature() { TraceIdentifier = requestId };
            var features = new FeatureCollection();
            features.Add(typeof(IHttpRequestIdentifierFeature), requestIdentifierFeature);
            features.Add(typeof(IHttpRequestFeature), new HttpRequestFeature());
            features.Add(typeof(IHttpResponseFeature), new HttpResponseFeature());
            var context = new DefaultHttpContext(features);
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new ElmLoggerProvider(new ElmStore(), new ElmOptions()));

            // Act & Assert
            var errorPageMiddleware = new ElmCaptureMiddleware((innerContext) =>
            {
                var feature = innerContext.GetFeature<IHttpRequestIdentifierFeature>();
                Assert.NotNull(feature);
                Assert.False(string.IsNullOrEmpty(feature.TraceIdentifier));
                return Task.FromResult(0);
            }, loggerFactory, new TestElmOptions());

            await errorPageMiddleware.Invoke(context);

            Assert.Equal(requestId, context.GetFeature<IHttpRequestIdentifierFeature>().TraceIdentifier);
        }

        private class TestElmOptions : IOptions<ElmOptions>
        {
            private readonly ElmOptions _innerOptions;

            public TestElmOptions() :
                this(new ElmOptions())
            {
            }

            public TestElmOptions(ElmOptions innerOptions)
            {
                _innerOptions = innerOptions;
            }

            public ElmOptions Options
            {
                get
                {
                    return _innerOptions;
                }
            }

            public ElmOptions GetNamedOptions(string name)
            {
                return _innerOptions;
            }
        }
    }
}
