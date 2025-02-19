﻿using System.IO;

namespace Gardener.Common;

public class FileHelper
{
    public static void CreateFileReplace(string fileFullPath, string contentStr)
    {
        string? directoryPath = GetDirectory(fileFullPath);
        if (directoryPath == null) { return; }
        CreateDirectory(directoryPath);
        File.WriteAllText(fileFullPath, contentStr, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Get full directory of a file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string? GetDirectory(string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        DirectoryInfo? directory = file.Directory;
        return directory?.FullName;
    }

    public static void CreateDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
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
                var pPath= new DirectoryInfo(path).Parent?.FullName;
                if(pPath!=null)
                {
                    System.Diagnostics.Process.Start("explorer.exe", pPath);
                }
                
            }
            // Folder
            else if (Directory.Exists(path))
            {
                var dir = new DirectoryInfo(path);
                var openDir = dir.FullName;
                if (openParent)
                {
                    openDir = dir.Parent?.FullName;
                }
                if (openDir != null)
                {
                    System.Diagnostics.Process.Start("explorer.exe", openDir);
                }
            }
        }
        catch
        {
        }
    }

    /// <summary>
    /// Get parent directory
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string? GetParentDirectory(string path)
    {
        string? parent =null;

        // File
        if (File.Exists(path))
        {
            parent = new DirectoryInfo(path).Parent?.Parent?.FullName;
        }
        // Folder
        else if (Directory.Exists(path))
        {
            parent = new DirectoryInfo(path).Parent?.FullName;
        }

        return parent;
    }
}
