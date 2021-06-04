using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerNET
{
  /// <summary>
  /// Class that provides various utilities
  /// </summary>
  public static class Utils
  {
    /// <summary>
    /// Get the encoding of a file by it's file path
    /// </summary>
    public static Encoding GetFileEncoding(string file) // https://stackoverflow.com/a/30393739
    {
      using (StreamReader sr = new StreamReader(file, Encoding.UTF8, true))
      {
        sr.Peek();
        return sr.CurrentEncoding;
      }
    }
  }
}
