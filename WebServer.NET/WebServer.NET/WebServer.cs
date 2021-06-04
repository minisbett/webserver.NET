using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WebServerNET.Responses;

namespace WebServerNET
{
  public class WebServer
  {

    private int m_port;
    private HttpListener m_listener;

    private DomainRoute404 m_route404 = null;
    private List<DomainRoute> m_routes = new List<DomainRoute>();
    private bool m_running = false;

    /// <summary>
    /// Creates a new <seealso cref="WebServer"/> instance.
    /// </summary>
    /// <param name="port">The port the <seealso cref="WebServer"/> should run on</param>
    public WebServer(int port)
    {
      m_port = port;
    }

    /// <summary>
    /// Adds all methods in a class that have a <seealso cref="DomainRoute"/> attribute specified to the routes of this <seealso cref="WebServer"/> object.
    /// </summary>
    /// <param name="type">Type of the class</param>
    public void AddRoutingHandlerClass(Type type)
    {
      if (!type.IsClass)
        throw new InvalidOperationException("The specified type is not a class.");

      foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
      {
        ParameterInfo[] parameters = method.GetParameters();

        if (method.IsDefined(typeof(DomainRoute404)))
        {
          if (m_route404 != null)
            throw new InvalidOperationException($"Multiple 404 routes are not allowed.\nA 404 handler already exists: '{m_route404.Method.DeclaringType.FullName}.{m_route404.Method.Name}'");

          if (parameters.Length > 1 || (parameters.Length == 1 && parameters[0].ParameterType != typeof(HttpListenerRequest)))
            throw new InvalidOperationException($"The method '{method.DeclaringType.FullName}.{method.Name}' has invalid parameters. Accepted parameters for a DomainRoute404 are () and (HttpListenerRequest)");

          m_route404 = (DomainRoute404)method.GetCustomAttribute(typeof(DomainRoute404));
          m_route404.Method = method;
          continue;
        }

        if (!method.IsDefined(typeof(DomainRoute)))
          continue;

        if (method.ReturnType != typeof(Response))
          throw new InvalidOperationException($"The method '{method.DeclaringType.FullName}.{method.Name}' does not return a response. Therefore it cannot provide a response to the request.");

        if (parameters.Length > 2)
          throw new InvalidOperationException($"The method '{method.DeclaringType.FullName}.{method.Name}' has more than 2 parameters. Accepted parameters for a DomainRoute are (), (Parameters) and (Parameters, HttpListenerRequest)");

        if (parameters.Length == 2 && parameters[1].ParameterType != typeof(HttpListenerRequest) && parameters[1].ParameterType != typeof(Parameters))
          throw new InvalidOperationException($"The method '{method.DeclaringType.FullName}.{method.Name}' has an invalid parameter type. Type: {parameters[1].ParameterType}, Expected: HttpListenerRequest or Parameters");
        if (parameters.Length >= 1 && parameters[0].ParameterType != typeof(HttpListenerRequest) && parameters[0].ParameterType != typeof(Parameters))
          throw new InvalidOperationException($"The method '{method.DeclaringType.FullName}.{method.Name}' has an invalid parameter type. Type: {parameters[0].ParameterType}, Expected: HttpListenerRequest or Parameters");

        DomainRoute route = (DomainRoute)method.GetCustomAttribute(typeof(DomainRoute));
        route.Method = method;
        m_routes.Add(route);
      }
    }

    /// <summary>
    /// Start the <seealso cref="WebServer"/> instance.
    /// </summary>
    public void Run()
    {
      if (m_running)
        throw new InvalidOperationException("This WebServer instance is already running.");

      m_listener = new HttpListener();
      m_listener.Prefixes.Add($"http://localhost:{m_port}/");
      m_listener.Start();
      m_running = true;

      while (m_running)
      {
        ThreadPool.QueueUserWorkItem(Process, m_listener.GetContext());
      }
    }

    private void Process(object obj)
    {
      HttpListenerContext context = (HttpListenerContext)obj;
      DomainRoute route = m_routes.FirstOrDefault(x => x.Match(context.Request.RawUrl));
      MethodInfo method = null;
      List<object> parameters = new List<object>();

      if (route == null && m_route404 != null)
      {
        if (m_route404.Method.GetParameters().Length == 1 && m_route404.Method.GetParameters()[0].ParameterType == typeof(HttpListenerRequest))
          parameters.Add(context.Request);
        method = m_route404.Method;
      }
      else if (route == null)
        return;
      else
      {
        method = route.Method;
        for (int i = 0; i < route.Method.GetParameters().Length; i++)
        {
          Type type = route.Method.GetParameters()[i].ParameterType;
          if (type == typeof(HttpListenerRequest))
            parameters.Add(context.Request);
          else if (type == typeof(Parameters))
            parameters.Add(new Parameters(route, context.Request.RawUrl));
        }
      }

      Response response = (Response)method.Invoke(null, parameters.ToArray());

      context.Response.ContentEncoding = response.Encoding;
      context.Response.ContentLength64 = response.Content.Length;
      context.Response.ContentType = response.ContentType;
      context.Response.Cookies = response.Cookies;
      context.Response.Headers = response.Headers;
      context.Response.RedirectLocation = response.RedirectLocation;
      context.Response.StatusCode = response.StatusCode;
      context.Response.StatusDescription = response.StatusDescription;
      context.Response.OutputStream.Write(response.Content, 0, response.Content.Length);
      context.Response.Close();
    }

    /// <summary>
    /// Stop the <seealso cref="WebServer"/> instance.
    /// </summary>
    public void Stop()
    {
      if (!m_running)
        throw new InvalidOperationException("This WebServer instance is not running.");

      m_listener.Stop();
      m_running = false;
    }
  }
}