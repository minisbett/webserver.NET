using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET.Responses
{
  /// <summary>
  /// Attribute for specifying that the method handles 404 connections.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public class DomainRoute404 : Attribute
  {
    /// <summary>
    /// The <seealso cref="MethodInfo"/> object that corresponds to the route
    /// </summary>
    internal MethodInfo Method { get; set; }
  }
}
