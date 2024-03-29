﻿using Netcraft.Modules;
using System.Net;
using System.Net.Http;

namespace Netcraft
{
    /// <summary>
    /// The primary class for interacting with the Netcraft API.
    /// </summary>
    public class NetcraftClient
    {
        private static readonly HttpClientHandler HttpHandler = new()
        {
            AutomaticDecompression = DecompressionMethods.All,
            AllowAutoRedirect = false
        };

        private readonly HttpClient Client = new(HttpHandler)
        {
            BaseAddress = Constants.BaseUri,
            DefaultRequestVersion = Constants.HttpVersion
        };

        /// <summary>
        /// Create a new instance of the API client.
        /// </summary>
        public NetcraftClient()
        {
            Client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            Client.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
            Client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            Client.DefaultRequestHeaders.Accept.ParseAdd("*/*");

            Report = new(Client);
            Submission = new(Client);
            Misc = new(Client);
        }

        /// <summary>
        /// Report malicious URLs, emails or mistakes.
        /// </summary>
        public readonly ReportModule Report;

        /// <summary>
        /// Get details about submissions, analysis, fetch metadata or report classificiaton issues.
        /// </summary>
        public readonly SubmissionModule Submission;

        /// <summary>
        /// Miscellaneous features that don't belong to any other module.
        /// </summary>
        public readonly MiscModule Misc;
    }
}