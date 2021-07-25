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


using System;
using System.Dynamic;
using System.Reflection;

namespace BabilinApps.Defines.Utility.Editor
{
  using System.IO;
  using UnityEditor;
  using System.Collections.Generic;
  using System.Linq;
  using UnityEngine;




  public class DefineSymbolsUtility
  {
    private static BuildTargetGroup[] _cachedBuildTargets;
    
    private static BuildTargetGroup[] _buildTargets
    {
      get
      {
        if (_cachedBuildTargets == null)
        {
          
          var t = (BuildTargetGroup[]) Enum.GetValues(typeof(BuildTargetGroup));
          var buildTargetGroupType = typeof(BuildTargetGroup);
          var fields = buildTargetGroupType.GetFields();
          var obsoleteValueByBuildTargetGroup = new Dictionary<string, bool>();
          for (int i = 0; i < fields.Length; i++)
          {
            obsoleteValueByBuildTargetGroup.Add(fields[i].Name,fields[i].GetCustomAttribute(typeof(ObsoleteAttribute)) != null);
          }
          _cachedBuildTargets = Array.FindAll(t,(e)=> !obsoleteValueByBuildTargetGroup[e.ToString()]);
        }

        return _cachedBuildTargets;
      }
    }


    /// <summary>
    /// Check if the all define symbols for a definition
    /// </summary>
    public static bool ContainsDefineSymbolInAnyBuildTargetGroup(string symbol)
    {
      
      for (int i = 0; i < _buildTargets.Length; i++)
      {
        if (ContainsDefineSymbol(symbol, _buildTargets[i]))
        {
          return true;
        }
      }

      return false;

    }
    /// <summary>
    /// Check if the current define symbols contain a definition
    /// </summary>
    public static bool ContainsDefineSymbol(string symbol)
    {
       return  ContainsDefineSymbol(symbol, EditorUserBuildSettings.selectedBuildTargetGroup);
      
    }

    /// <summary>
    /// Check if the current define symbols contain a definition
    /// </summary>
    public static bool ContainsDefineSymbol(string symbol, BuildTargetGroup buildTarget)
    {
      string definesString =PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
      
      return definesString.Contains(symbol);
    }

    /// <summary>
    /// Add define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbolsToAllBuildTargetGroups(string[] symbols)
    {
      for (int i = 0; i < _buildTargets.Length; i++)
      {
        AddDefineSymbols(symbols, _buildTargets[i]);
      }
    }

    /// <summary>
    /// Add define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbols(string[] symbols)
    {
      AddDefineSymbols(symbols, EditorUserBuildSettings.selectedBuildTargetGroup);
    }

    /// <summary>
    /// Add define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbols(string[] symbols, BuildTargetGroup buildTarget)
    {
      string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
      List<string> allDefines = definesString.Split(';').ToList();
      allDefines.AddRange(symbols.Except(allDefines));
      PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                                                       string.Join(";", allDefines.ToArray()));
    }

    /// <summary>
    /// Remove define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbolsFromAllBuildTargetGroups(string[] symbols)
    {
      for (int i = 0; i < _buildTargets.Length; i++)
      {
        RemoveDefineSymbols(symbols, _buildTargets[i]);
      }
    }
    /// <summary>
    /// Remove define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbols(string[] symbols)
    {
      RemoveDefineSymbols(symbols, EditorUserBuildSettings.selectedBuildTargetGroup);
     
    }

    /// <summary>
    /// Remove define symbols as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbols(string[] symbols, BuildTargetGroup buildTarget)
    {
      string definesString =
        PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
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
    public static void AddDefineSymbolToAllBuildTargetGroups(string symbol)
    {
      for (int i = 0; i < _buildTargets.Length; i++)
      {
        AddDefineSymbol(symbol, _buildTargets[i]);
      }
    }

    /// <summary>
    /// Add define symbol as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbol(string symbol)
    {
      AddDefineSymbol(symbol, EditorUserBuildSettings.selectedBuildTargetGroup);
    }

    /// <summary>
    /// Add define symbol as soon as Unity gets done compiling.
    /// </summary>
    public static void AddDefineSymbol(string symbol, BuildTargetGroup buildTarget)
    {
      string definesString =
        PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
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
    public static void RemoveDefineSymbol(string symbol, BuildTargetGroup buildTarget)
    {
      string definesString =
        PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
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
    /// Remove define symbol as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbolFromAllBuildTargetGroups(string symbol)
    {
      for (int i = 0; i < _buildTargets.Length; i++)
      {
        RemoveDefineSymbol(symbol, _buildTargets[i]);
      }

    }

    /// <summary>
    /// Remove define symbol as soon as Unity gets done compiling.
    /// </summary>
    public static void RemoveDefineSymbol(string symbol)
    {
      RemoveDefineSymbol(symbol, EditorUserBuildSettings.selectedBuildTargetGroup);

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



    /// <summary>
    /// Checks if a Type exists in the project by name.
    /// </summary>
    /// <param name="contains"> full or partial name</param>
    /// <param name="doesNotContain">filter</param>
    /// <returns></returns>
    public static bool TypeExists(string contains, string doesNotContain = null)
    {
      System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

      foreach (var assembly in assemblies)
      {
        System.Type[] types = assembly.GetTypes();

        foreach (var scriptType in types)
        {
          if (scriptType.FullName != null)
          {
            if (!string.IsNullOrEmpty(doesNotContain) && scriptType.FullName.Contains(doesNotContain))
            {
              continue;
            }

            if (scriptType.FullName.Contains(contains))
            {
              return true;


            }

            //if (scriptType.FullName != null && (scriptType.FullName.Contains(contains)))
            //{
            //    Debug.Log($"script name: {scriptType.FullName}");
            //}
          }

        }
      }

      return false;




    }

    /// <summary>
    /// Checks if an assembly exists in the project by name.
    /// </summary>
    /// <param name="contains"> full or partial name</param>
    /// <param name="doesNotContain">filter</param>
    /// <returns></returns>
    public static bool AssemblyExists(string contains, string doesNotContain = null)
    {
      System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

      foreach (var assembly in assemblies)
      {
        if (!string.IsNullOrEmpty(doesNotContain) && assembly.FullName.Contains(doesNotContain))
        {
          continue;
        }

        if (assembly.FullName.Contains(contains))
        {
          return true;


        }
      }

      return false;




    }

    public static bool ValidFolder(string path)
    {
      return AssetDatabase.IsValidFolder(path);
    }

  
  }
}