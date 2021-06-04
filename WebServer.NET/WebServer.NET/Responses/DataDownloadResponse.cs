using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET.Responses
{
  /// <summary>
  /// A response that takes data and a filename and tells the browser to perform a download on that data.
  /// </summary>
  public class DataDownloadResponse : Response
  {
    /// <summary>
    /// Create a new <seealso cref="DataDownloadResponse"/> instance with the specified data and filename.
    /// </summary>
    /// <param name="data">The data of the download content</param>
    /// <param name="filename">The filename that is shown to the user when downloading the file</param>
    public DataDownloadResponse(byte[] data, string filename)
    {
      Content = data;
      Headers.Add("Content-Transfer-Encoding", "binary");
      Headers.Add("Content-Disposition", $"attachment; filename=\"{filename}\"");
    }

    /// <summary>
    /// Create a new <seealso cref="DataDownloadResponse"/> instance with the specified data, filename and content type.
    /// </summary>
    /// <param name="data">The data of the download content</param>
    /// <param name="type">The type of the content</param>
    /// <param name="filename">The filename that is shown to the user when downloading the file</param>
    public DataDownloadResponse(byte[] data, string type, string filename)
    {
      Content = data;
      ContentType = type;
      Headers.Add("Content-Transfer-Encoding", "binary");
      Headers.Add("Content-Disposition", $"attachment; filename=\"{filename}\"");
    }
  }
}
