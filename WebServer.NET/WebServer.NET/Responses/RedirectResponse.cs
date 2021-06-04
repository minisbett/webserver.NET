using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET.Responses
{
  /// <summary>
  /// A response that tells the client to redirect the user to another location.
  /// </summary>
  public class RedirectResponse : Response
  {
    /// <summary>
    /// Create a new <seealso cref="RedirectResponse"/> instance with the specified location.
    /// </summary>
    /// <param name="file">The file path</param>
    public RedirectResponse(string location)
    {
      // https://stackoverflow.com/a/2068407
      RedirectLocation = location;
      StatusCode = 302;
    }
  }
}
