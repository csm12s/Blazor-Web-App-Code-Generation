using System;
using System.IO;

namespace Gardener.Common;

public static class FileHelper
{
    #region File IO
    public static void CreateFileReplace(string fileFullPath, string contentStr)
    {
        try
        {
            string directoryPath = GetDirectory(fileFullPath);
            CreateDirectory(directoryPath);

            File.WriteAllText(fileFullPath, contentStr, System.Text.Encoding.UTF8);
        }
        catch
        {
        }
    }

    public static string GetDirectory(string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        DirectoryInfo directory = file.Directory;
        return directory.FullName;
    }

    public static void CreateDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
    #endregion

    public static string GetGuid32(string formatStr = "N")
    {
        return Guid.NewGuid().ToString(formatStr);

        // Format string:

        // N	32 digits:
        // 00000000000000000000000000000000

        // D	32 digits separated by hyphens:
        // 00000000-0000-0000-0000-000000000000

        // B	32 digits separated by hyphens, enclosed in braces:
        // {00000000-0000-0000-0000-000000000000}

        // P	32 digits separated by hyphens, enclosed in parentheses:
        // (00000000-0000-0000-0000-000000000000)

        // X	Four hexadecimal values enclosed in braces,
        // where the fourth value is a subset of eight hexadecimal values that is
        // also enclosed in braces:
        // {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path">File or folder path</param>
    /// <param name="openParent"></param>
    public static void OpenFolder(string path, bool openParent = false)
    {
        try
        {
            // File
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", 
                    new DirectoryInfo(path).Parent.FullName);
            }
            // Folder
            else if (Directory.Exists(path))
            {
                var dir = new DirectoryInfo(path);
                var openDir = dir.FullName;
                if (openParent)
                {
                    openDir = dir.Parent.FullName;
                }

                System.Diagnostics.Process.Start("explorer.exe", openDir);
            }
        }
        catch
        {
        }
    }
}
