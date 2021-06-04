using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET
{
  /// <summary>
  /// A dictionary of string pairs that holds the parameters specified in the url
  /// </summary>
  public class Parameters : Dictionary<string, string>
  {
    /// <summary>
    /// Creates a new <seealso cref="Parameters"/> instance and parses the parameters with the specified route and url
    /// </summary>
    /// <param name="url">The url from the request</param>
    /// <param name="route">The domain route with parameter placeholders</param>
    public Parameters(DomainRoute route, string url)
    {
      if (!route.Match(url))
        throw new InvalidOperationException("The route does not match with the specified url.");

      string[] urlSplit = url.Split('/');
      string[] routeSplit = route.Route.Split('/');

      for(int i = 0; i < urlSplit.Length; i++)
      {
        if(routeSplit[i].StartsWith("$"))
        {
          string key = routeSplit[i].Substring(1);
          string value = urlSplit[i];
          Add(key, value);
        }
      }
    }
  }
}
