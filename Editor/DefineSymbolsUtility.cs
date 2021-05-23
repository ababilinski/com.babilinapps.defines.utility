/* Copyright (C) 2019 Adrian Babilinski
* You may use, distribute and modify this code under the
* terms of the MIT License
*
*Permission is hereby granted, free of charge, to any person obtaining a copy
*of this software and associated documentation files (the "Software"), to deal
*in the Software without restriction, including without limitation the rights
*to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
*copies of the Software, and to permit persons to whom the Software is
*furnished to do so, subject to the following conditions:
*
*The above copyright notice and this permission notice shall be included in all
*copies or substantial portions of the Software.
*
*THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
*IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
*AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
*OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
*SOFTWARE.
*
*/




namespace BabilinApps.Defines.Utility.Editor
{
  using System.IO;
  using UnityEditor;
  using System.Collections.Generic;
  using System.Linq;
  using UnityEngine;




  public class DefineSymbolsUtility
  {
    /// <summary>
    /// Check if the current define symbols contain a definition
    /// </summary>
    public static bool ContainsDefineSymbol(string symbol)
    {
      string definesString =
        PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
      List<string> allDefines = definesString.Split(';').ToList();
      return allDefines.Contains(symbol);
    }

    /// <summary>
    /// Add define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbols(string[] symbols)
    {
      string definesString =
          PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
      List<string> allDefines = definesString.Split(';').ToList();
      allDefines.AddRange(symbols.Except(allDefines));
      PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                                                       string.Join(";", allDefines.ToArray()));
    }

    /// <summary>
    /// Remove define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbols(string[] symbols)
    {
      string definesString =
          PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
      List<string> allDefines = definesString.Split(';').ToList();

      for (int i = 0; i < symbols.Length; i++)
      {
        if (!allDefines.Contains(symbols[i]))
        {
          Debug.LogWarning($"Remove Defines Ignored. Symbol [{symbols[i]}] does not exists.");

        }
        else
        {
          allDefines.Remove(symbols[i]);
        }

      }
      PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                                                       string.Join(";", allDefines.ToArray()));
    }

    /// <summary>
    /// Add define symbol as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbol(string symbol)
    {
      string definesString =
          PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
      List<string> allDefines = definesString.Split(';').ToList();
      if (allDefines.Contains(symbol))
      {
        Debug.LogWarning($"Add Defines Ignored. Symbol [{symbol}] already exists.");
        return;
      }

      allDefines.Add(symbol);
      PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                                                       string.Join(";", allDefines.ToArray()));
    }

    /// <summary>
    /// Remove define symbol as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbol(string symbol)
    {
      string definesString =
          PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
      List<string> allDefines = definesString.Split(';').ToList();
      if (!allDefines.Contains(symbol))
      {
        Debug.LogWarning($"Remove Defines Ignored. Symbol [{symbol}] does not exists.");

      }
      else
      {
        allDefines.Remove(symbol);

        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                                                         string.Join(";", allDefines.ToArray()));
      }

    }

        /// <summary>
    /// path to project folder;
    /// </summary>
    public static string AbsolutePath
    {
      get { return "../" + Application.dataPath; }
    }

    /// <summary>
    /// Determines whether the specified file exists relative to the root project
    /// </summary>
    /// <returns></returns>
    public static bool FilePathExists(string path)
    {
      return File.Exists(AbsolutePath + path);
    }
    /// <summary>
    /// Find Wild Card File Path
    /// </summary>
    public static bool FilePathExistsWildCard(string path, string searchPattern)
    {
      return Directory.EnumerateFiles(path, searchPattern).Any();
    }
    /// <summary>
    /// Find Wild Card Directory Path
    /// </summary>
    public static bool DirectoryPathExistsWildCard(string path, string searchPattern)
    {
      return Directory.EnumerateDirectories(path, searchPattern).Any();
    }
    public static bool ValidFolder(string path)
    {
      return AssetDatabase.IsValidFolder(path);
    }

  
  }
}