﻿using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Diagnostics.Elm;
using Microsoft.Framework.Logging;
using Xunit;

namespace Microsoft.AspNet.Diagnostics.Tests
{
    public class ElmLoggerTest
    {
        private const string _name = "test";
        private const string _state = "This is a test";
        private Func<string, LogLevel, bool> _filter = (_, __) => true;

        private Tuple<ElmLogger, ElmStore> SetUp(Func<string, LogLevel, bool> filter = null, string name = null)
        {
            // Arrange
            var store = new ElmStore();
            var options = new ElmOptions() { Filter = filter ?? _filter };
            var provider = new ElmLoggerProvider(store, options);
            var logger = (ElmLogger)provider.Create(name ?? _name);

            return new Tuple<ElmLogger, ElmStore>(logger, store);
        }

        [Fact]
        public void LogsWhenNullFormatterGiven()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialCount = (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count;

            // Act
            logger.Write(LogLevel.Information, 0, _state, null, null);

            // Assert
            Assert.Equal(1, (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count - initialCount);
        }

        [Fact]
        public void DoesNotLogWithEmptyStateAndException()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialCount = (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count;

            // Act
            logger.Write(LogLevel.Information, 0, null, null, null);

            // Assert
            Assert.Equal(0, (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count - initialCount);
        }

        [Fact]
        public void DefaultLogsForAllLogLevels()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialCount = (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count;

            // Act
            logger.Write(LogLevel.Verbose, 0, _state, null, null);
            logger.Write(LogLevel.Information, 0, _state, null, null);
            logger.Write(LogLevel.Warning, 0, _state, null, null);
            logger.Write(LogLevel.Error, 0, _state, null, null);
            logger.Write(LogLevel.Critical, 0, _state, null, null);

            // Assert
            Assert.Equal(5, (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count - initialCount);
        }

        [Theory]
        [InlineData(LogLevel.Warning, "", 3)]
        [InlineData(LogLevel.Warning, "te", 3)]
        [InlineData(LogLevel.Warning, "bad", 0)]
        [InlineData(LogLevel.Critical, "", 1)]
        [InlineData(LogLevel.Critical, "test", 1)]
        [InlineData(LogLevel.Verbose, "t", 5)]
        public void Filter_LogsWhenAppropriate(LogLevel minLevel, string prefix, int count)
        {
            // Arrange
            var t = SetUp((name, level) => (name.StartsWith(prefix) && level >= minLevel), _name);
            var logger = t.Item1;
            var store = t.Item2;
            var initialCount = (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count;

            // Act
            logger.Write(LogLevel.Verbose, 0, _state, null, null);
            logger.Write(LogLevel.Information, 0, _state, null, null);
            logger.Write(LogLevel.Warning, 0, _state, null, null);
            logger.Write(LogLevel.Error, 0, _state, null, null);
            logger.Write(LogLevel.Critical, 0, _state, null, null);

            // Assert
            Assert.Equal(count, (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count - initialCount);
        }

        [Fact]
        public void ThreadsHaveSeparateActivityContexts()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialCount = (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count;
            var initialActivityCount = store.GetActivities().ToList().Count;

            var testThread = new TestThread(logger);
            Thread workerThread = new Thread(testThread.work);

            // Act
            workerThread.Start();
            using (logger.BeginScope("test1"))
            {
                logger.WriteWarning("hello world");
                Thread.Sleep(1000);
                logger.WriteCritical("goodbye world");
            }
            workerThread.Join();

            // Assert
            Assert.Equal(17, (store.GetActivities().SelectMany(a => a.AllMessages).ToList()).Count - initialCount);
            Assert.Equal(2, store.GetActivities().ToList().Count - initialActivityCount);
        }

        [Fact]
        public void ScopesHaveProperTreeStructure()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;

            var testThread = new TestThread(logger);
            Thread workerThread = new Thread(testThread.work);

            // Act
            workerThread.Start();
            using (logger.BeginScope("test2"))
            {
                logger.WriteWarning("hello world");
                Thread.Sleep(1000);
                logger.WriteCritical("goodbye world");
            }
            workerThread.Join();

            // Assert
            var root1 = (store.GetActivities()).Where(a => a.Root.State?.Equals("test2") ?? false)?.ElementAt(0)?.Root;
            Assert.NotNull(root1);
            var root2 = (store.GetActivities()).Where(a => a.Root.State?.Equals("test12") ?? false)?.ElementAt(0)?.Root;
            Assert.NotNull(root2);

            Assert.Equal(0, root1.Children.Count);
            Assert.Equal(2, root1.Messages.Count);
            Assert.Equal(1, root2.Children.Count);
            Assert.Equal(12, root2.Messages.Count);
            Assert.Equal(0, root2.Children.ElementAt(0).Children.Count);
            Assert.Equal(3, root2.Children.ElementAt(0).Messages.Count);
        }

        [Fact]
        public void CollapseTree_CollapsesWhenNoLogsInSingleScope()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialActivityCount = store.GetActivities().ToList().Count;

            // Act
            using (logger.BeginScope("test3"))
            {
            }

            // Assert
            Assert.Equal(0, store.GetActivities().ToList().Count - initialActivityCount);
        }

        [Fact]
        public void CollapseTree_CollapsesWhenNoLogsInNestedScope()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialActivityCount = store.GetActivities().ToList().Count;

            // Act
            using (logger.BeginScope("test4"))
            {
                using (logger.BeginScope("test5"))
                {
                }
            }

            // Assert
            Assert.Equal(0, store.GetActivities().ToList().Count - initialActivityCount);
        }

        [Fact]
        public void CollapseTree_DoesNotCollapseWhenLogsExist()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialActivityCount = store.GetActivities().ToList().Count;

            // Act
            using (logger.BeginScope("test6"))
            {
                using (logger.BeginScope("test7"))
                {
                    logger.WriteVerbose("hi");
                }
            }

            // Assert
            Assert.Equal(1, store.GetActivities().ToList().Count - initialActivityCount);
        }

        [Fact]
        public void CollapseTree_CollapsesAppropriateNodes()
        {
            // Arrange
            var t = SetUp();
            var logger = t.Item1;
            var store = t.Item2;
            var initialActivityCount = store.GetActivities().ToList().Count;

            // Act
            using (logger.BeginScope("test8"))
            {
                logger.WriteVerbose("hi");
                using (logger.BeginScope("test9"))
                {
                }
            }

            // Assert
            Assert.Equal(1, store.GetActivities().ToList().Count - initialActivityCount);
            var context = store.GetActivities().Where(a => a.Root.State.Equals("test8")).First();
            Assert.Empty(context.Root.Children);
        }

        [Fact]
        public void CollapseTree_WorksWithFilter()
        {
            // Arrange
            var t = SetUp((_, level) => level >= LogLevel.Warning, null);
            var logger = t.Item1;
            var store = t.Item2;
            var initialActivityCount = store.GetActivities().ToList().Count;

            // Act
            using (logger.BeginScope("test10"))
            {
                using (logger.BeginScope("test11"))
                {
                    logger.WriteInformation("hi");
                }
            }

            // Assert
            Assert.Equal(0, store.GetActivities().ToList().Count - initialActivityCount);
        }

        private class TestThread
        {
            private ILogger _logger;

            public TestThread(ILogger logger)
            {
                _logger = logger;
            }

            public void work()
            {
                using (_logger.BeginScope("test12"))
                {
                    for (var i = 0; i < 5; i++)
                    {
                        _logger.WriteVerbose(string.Format("xxx {0}", i));
                        Thread.Sleep(5);
                    }
                    using (_logger.BeginScope("test13"))
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            _logger.WriteVerbose(string.Format("yyy {0}", i));
                            Thread.Sleep(200);
                        }
                    }
                    for (var i = 0; i < 7; i++)
                    {
                        _logger.WriteVerbose(string.Format("zzz {0}", i));
                        Thread.Sleep(40);
                    }
                }
            }
        }
    }
}