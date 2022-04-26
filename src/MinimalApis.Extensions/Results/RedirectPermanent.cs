﻿using MinimalApis.Extensions.Metadata;

namespace MinimalApis.Extensions.Results;

/// <summary>
/// Represents an <see cref="IResult"/> for a <see cref="StatusCodes.Status301MovedPermanently"/> redirect response.
/// </summary>
public class RedirectPermanent : ResultBase, IEndpointMetadataProvider
{
    private const int ResponseStatusCode = StatusCodes.Status301MovedPermanently;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedirectPermanent"/> class.
    /// </summary>
    /// <param name="uri">The URI to redirect to.</param>
    public RedirectPermanent(string uri)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        Uri = uri;
        StatusCode = ResponseStatusCode;
    }

    /// <summary>
    /// The URI to redirect to.
    /// </summary>
    public string Uri { get; init; }

    /// <inheritdoc />
    public override Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));

        httpContext.Response.Headers.Location = Uri;

        return base.ExecuteAsync(httpContext);
    }

    /// <summary>
    /// Provides metadata for parameters to <see cref="Endpoint"/> route handler delegates.
    /// </summary>
    /// <param name="endpoint">The <see cref="Endpoint"/> to provide metadata for.</param>
    /// <param name="services">The <see cref="IServiceProvider"/>.</param>
    /// <returns>The metadata.</returns>
    public static IEnumerable<object> GetMetadata(Endpoint endpoint, IServiceProvider services)
    {
        yield return new Mvc.ProducesResponseTypeAttribute(ResponseStatusCode);
    }
}
