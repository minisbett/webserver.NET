using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET
{
  /// <summary>
  /// Attribute for specifying a domain route on a method.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public class DomainRoute : Attribute
  {
    /// <summary>
    /// The route, which the method corresponds to
    /// </summary>
    public string Route { get; }

    /// <summary>
    /// The <seealso cref="MethodInfo"/> object that corresponds to the route
    /// </summary>
    internal MethodInfo Method { get; set; }

    /// <summary>
    /// Create a new <seealso cref="DomainRoute"/>.
    /// </summary>
    /// <param name="route">The route, which the method corresponds to</param>
    public DomainRoute(string route)
    {
      if (string.IsNullOrEmpty(route))
        throw new ArgumentException("The route cannot be empty. If you want to get a route to the main domain, use the route '/'.", route);

      Route = route.TrimEnd('/');
    }

    /// <summary>
    /// Checks whether the specified url matches with the domain route considering variables
    /// </summary>
    /// <param name="url">The url to check the match for</param>
    /// <returns>Bool whether it is a match or not</returns>
    internal bool Match(string url)
    {
      string[] urlSplit = url.Split('/');
      string[] routeSplit = Route.Split('/');

      if (urlSplit.Length != routeSplit.Length)
        return false;

      for(int i = 0; i < urlSplit.Length; i++)
      {
        if (routeSplit[i].StartsWith("$"))
          continue;

        if (urlSplit[i] != routeSplit[i])
          return false;
      }

      return true;
    }
  }
}
