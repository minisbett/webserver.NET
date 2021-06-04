using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET.Responses
{
  /// <summary>
  /// A response that takes a file and sends the content back to the client.
  /// </summary>
  public class FileResponse : Response
  {
    /// <summary>
    /// Create a new <seealso cref="FileResponse"/> instance with the specified file.
    /// </summary>
    /// <param name="file">The file path</param>
    public FileResponse(string file)
    {
      Encoding = Utils.GetFileEncoding(file);
      Content = File.ReadAllBytes(file);
    }

    /// <summary>
    /// Create a new <seealso cref="FileResponse"/> instance with the specified file and content type.
    /// </summary>
    /// <param name="file">The file path</param>
    /// <param name="type">The content type</param>
    public FileResponse(string file, string type)
    {
      Encoding = Utils.GetFileEncoding(file);
      Content = File.ReadAllBytes(file);
      ContentType = type;
    }
  }
}
