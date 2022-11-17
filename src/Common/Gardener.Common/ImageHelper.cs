using System;
using System.IO;

namespace Gardener.Common;

public class ImageHelper
{
    #region Get image base64 data from image path
    /// <summary>
    /// Get image base64 data from image path
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ImageToBase64(string filePath)
    {
        try
        {
            string extension = Path.GetExtension(filePath);
            string mimeType = GetImageMimeTypeFromExtension(extension);

            byte[] inArray = System.IO.File.ReadAllBytes(filePath);
            var base64Data = Convert.ToBase64String(inArray);

            return "data:" + mimeType + ";base64," + base64Data;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    private static string GetImageMimeTypeFromExtension(string extension)
    {
        string mimetype;

        switch (extension)
        {
            case ".png":
                mimetype = "image/png";
                break;
            case ".gif":
                mimetype = "image/gif";
                break;
            case ".jpg":
            case ".jpeg":
                mimetype = "image/jpeg";
                break;
            case ".bmp":
                mimetype = "image/bmp";
                break;
            case ".tiff":
                mimetype = "image/tiff";
                break;
            case ".wmf":
                mimetype = "image/wmf";
                break;
            case ".jp2":
                mimetype = "image/jp2";
                break;
            case ".svg":
                mimetype = "image/svg+xml";
                break;
            default:
                mimetype = "application/octet-stream";
                break;
        }
        return mimetype;
    }
    #endregion

    // End
}
