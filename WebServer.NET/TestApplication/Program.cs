using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServerNET;
using WebServerNET.Responses;

namespace TestApplication
{
  public class Program
  {
    private static void Main(string[] args)
    {
      WebServer server = new WebServer(8080);
      server.AddRoutingHandlerClass(typeof(Program));
      server.Run();
    }

    [DomainRoute("/redirect/$url")]
    public static Response MainPage(Parameters parameters)
    {
      string url = parameters["url"];

      return new RedirectResponse($"https://{url}");
    }

    [DomainRoute404]
    public static Response Error404(HttpListenerRequest request)
    {
      return new Response(Response.DefaultEncoding.GetBytes($"404 for '{request.RawUrl}' :("));
    }
  }
}
