using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET.Responses
{
  /// <summary>
  /// Represents a response that is being sent back to the client.
  /// </summary>
  public class Response
  {
    /// <summary>
    /// The content of the response.
    /// </summary>
    public byte[] Content { get; set; } = new byte[0];

    /// <summary>
    /// The encoding of the response's content.
    /// </summary>
    public Encoding Encoding { get; set; } = DefaultEncoding;

    /// <summary>
    /// MIME-Type of the content.
    /// </summary>
    public string ContentType { get; set; } = DefaultContentType;

    /// <summary>
    /// Collection of cookies that are being sent with the response.
    /// </summary>
    public CookieCollection Cookies { get; } = new CookieCollection();

    /// <summary>
    /// The header key-value pairs that are being sent with the response.
    /// </summary>
    public WebHeaderCollection Headers { get; } = new WebHeaderCollection();

    /// <summary>
    /// The header key-value pairs that are being sent with the response.
    /// </summary>
    public string RedirectLocation { get; set; } = "";

    /// <summary>
    /// Status code that is being reported to the client
    /// </summary>
    public int StatusCode { get; set; } = DefaultStatusCode;

    /// <summary>
    /// An optional description of the status code that is being reported to the client
    /// </summary>
    public string StatusDescription { get; set; } = "";

    /// <summary>
    /// The default encoding.
    /// </summary>
    public static Encoding DefaultEncoding => Encoding.UTF8;

    /// <summary>
    /// The default content type.
    /// </summary>
    public static string DefaultContentType => "application/octet-stream";

    /// <summary>
    /// The default status code.
    /// </summary>
    public static int DefaultStatusCode => (int)HttpStatusCode.OK;

    /// <summary>
    /// Create a new <seealso cref="Response"/> instance with all default values.
    /// </summary>
    public Response()
    {

    }

    /// <summary>
    /// Create a new <seealso cref="Response"/> instance with the specified content.
    /// </summary>
    /// <param name="content">The response content</param>
    public Response(byte[] content)
    {
      Content = content;
    }

    /// <summary>
    /// Create a new <seealso cref="Response"/> instance with the specified content and type.
    /// </summary>
    /// <param name="content">The response content</param>
    /// <param name="type">The content type</param>
    public Response(byte[] content, string type)
    {
      Content = content;
      ContentType = type;
    }
  }
}
