// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Microsoft.AspNet.Diagnostics.Views
{
    /// <summary>
    /// Infrastructure
    /// </summary>
    public abstract class BaseView
    {
        /// <summary>
        /// The request context
        /// </summary>
        protected HttpContext Context { get; private set; }

        /// <summary>
        /// The request
        /// </summary>
        protected HttpRequest Request { get; private set; }

        /// <summary>
        /// The response
        /// </summary>
        protected HttpResponse Response { get; private set; }

        /// <summary>
        /// The output stream
        /// </summary>
        protected StreamWriter Output { get; private set; }

        /// <summary>
        /// Execute an individual request
        /// </summary>
        /// <param name="context"></param>
        public async Task ExecuteAsync(HttpContext context)
        {
            Context = context;
            Request = Context.Request;
            Response = Context.Response;
            Output = new StreamWriter(Response.Body);
            await ExecuteAsync();
            Output.Dispose();
        }

        /// <summary>
        /// Execute an individual request
        /// </summary>
        public abstract Task ExecuteAsync();

        /// <summary>
        /// Write the given value directly to the output
        /// </summary>
        /// <param name="value"></param>
        protected void WriteLiteral(string value)
        {
            WriteLiteralTo(Output, value);
        }

        protected void WriteAttribute(
            [NotNull] string name,
            [NotNull] Tuple<string, int> leader,
            [NotNull] Tuple<string, int> trailer,
            params AttributeValue[] values)
        {
            WriteAttributeTo(
                Output,
                name,
                leader,
                trailer,
                values);
        }

        protected void WriteAttributeTo(
            [NotNull] TextWriter writer,
            [NotNull] string name,
            [NotNull] Tuple<string, int> leader,
            [NotNull] Tuple<string, int> trailer,
            params AttributeValue[] values)
        {
        	
            WriteLiteralTo(writer, leader.Item1);
            foreach (var value in values)
            {
                WriteLiteralTo(writer, value.Prefix.Item1);
                WriteTo(writer, value.Value.Item1);
            }
            WriteLiteralTo(writer, trailer.Item1);
        }

        /// <summary>
        /// Convert to string and html encode
        /// </summary>
        /// <param name="value"></param>
        protected void Write(object value)
        {
            WriteTo(Output, Convert.ToString(value, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Html encode and write
        /// </summary>
        /// <param name="value"></param>
        protected void Write(string value)
        {
            WriteTo(Output, value);
        }

        /// <summary>
        /// <see cref="HelperResult.WriteTo(TextWriter)"/> is invoked
        /// </summary>
        /// <param name="result">The <see cref="HelperResult"/> to invoke</param>
        protected void Write(HelperResult result)
        {
            WriteTo(Output, result);
        }

        /// <summary>
        /// Writes the specified <paramref name="value"/> to <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> instance to write to.</param>
        /// <param name="value">The <see cref="object"/> to write.</param>
        /// <remarks>
        /// <see cref="HelperResult.WriteTo(TextWriter)"/> is invoked for <see cref="HelperResult"/> types.
        /// For all other types, the encoded result of <see cref="object.ToString"/> is written to the 
        /// <paramref name="writer"/>.
        /// </remarks>
        protected void WriteTo(TextWriter writer, object value)
        {
            if (value != null)
            {
                var helperResult = value as HelperResult;
                if (helperResult != null)
                {
                    helperResult.WriteTo(writer);
                }
                else
                {
                    WriteTo(writer, value.ToString());
                }
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="value"/> with HTML encoding to <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> instance to write to.</param>
        /// <param name="value">The <see cref="string"/> to write.</param>
        protected void WriteTo(TextWriter writer, string value)
        {
            WriteLiteralTo(writer, WebUtility.HtmlEncode(value));
        }

        /// <summary>
        /// Writes the specified <paramref name="value"/> without HTML encoding to the <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> instance to write to.</param>
        /// <param name="value">The <see cref="object"/> to write.</param>
        protected void WriteLiteralTo(TextWriter writer, object value)
        {
            WriteLiteralTo(writer, Convert.ToString(value, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes the specified <paramref name="value"/> without HTML encoding to <see cref="Output"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to write.</param>
        protected void WriteLiteralTo(TextWriter writer, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.Write(value);
            }
        }
    }
}
