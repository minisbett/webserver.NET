using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET.Responses
{
  /// <summary>
  /// A response that takes a file and optionally a custom filename and tells the browser to perform a download on that data.
  /// </summary>
  public class FileDownloadResponse : DataDownloadResponse
  {
    /// <summary>
    /// Create a new <seealso cref="FileDownloadResponse"/> instance with the specified file.
    /// </summary>
    /// <param name="file">The path to the file</param>
    public FileDownloadResponse(string file) : base(File.ReadAllBytes(file), Path.GetFileName(file))
    {

    }

    /// <summary>
    /// Create a new <seealso cref="FileDownloadResponse"/> instance with the specified file and content type.
    /// </summary>
    /// <param name="file">The path to the file</param>
    /// <param name="type">The content type of the file</param>
    public FileDownloadResponse(string file, string type) : base(File.ReadAllBytes(file), type, Path.GetFileName(file))
    {

    }

    /// <summary>
    /// Create a new <seealso cref="FileDownloadResponse"/> instance with the specified file and content type.
    /// </summary>
    /// <param name="file">The path to the file</param>
    /// <param name="type">The content type of the file</param>
    public FileDownloadResponse(string file, string type, string filename) : base(File.ReadAllBytes(file), type, filename)
    {

    }
  }
}
