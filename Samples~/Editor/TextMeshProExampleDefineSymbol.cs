using BabilinApps.Defines.Utility.Editor;
using UnityEditor;


namespace BabilinApps.Defines.Utility.Sample.Editor
{
  public class CustomDefinesExampleWindow : EditorWindow
  {

    private const string CHECK_SYMBOL_MENU_PATH = "Example/Custom Defines/TextMeshPro/Check";
    private const string REMOVE_SYMBOL_MENU_PATH = "Example/Custom Defines/TextMeshPro/Remove Symbol";
    
    [MenuItem(CHECK_SYMBOL_MENU_PATH)]
    public static void Check()
    {
      if (DefineSymbolsUtility.AssemblyExists("textmeshpro"))
      {
        if (EditorUtility.DisplayDialog("Example Custom Defines",
                                        "TextMeshPro HAS been found in the project. Do you want to ADD the [TEXTMESHPRO] Symbol?", "Add Symbol", "Cancel"))
        {
          DefineSymbolsUtility.AddDefineSymbol("TEXTMESHPRO");
          EditorUtility.DisplayDialog("Example Custom Defines",
                                      "Symbol [TEXTMESHPRO] ADDED!", "Ok");
        }
       
      }
      else
      {
        if (DefineSymbolsUtility.ContainsDefineSymbol("TEXTMESHPRO"))
        {
          if (EditorUtility.DisplayDialog("Example Custom Defines",
                                          "TextMeshPro HAS NOT been found in the project, but they symbol is in your #define directives. Do you want to REMOVE the [TEXTMESHPRO] Symbol?", "Remove Symbol", "Cancel"))
          {
            DefineSymbolsUtility.RemoveDefineSymbol("TEXTMESHPRO");
            EditorUtility.DisplayDialog("Example Custom Defines",
                                        "Symbol [TEXTMESHPRO] REMOVED!", "Ok");
          }
        }
        else
        {
          EditorUtility.DisplayDialog("Example Custom Defines",
                                      "TextMeshPro has not been found in your project.", "Ok");
        }
      }
      
    }


    [MenuItem(REMOVE_SYMBOL_MENU_PATH)]
    public static void Remove()
    {
      if (DefineSymbolsUtility.ContainsDefineSymbol("TEXTMESHPRO"))
      {
        if (EditorUtility.DisplayDialog("Example Custom Defines",
                                        "The [TEXTMESHPRO] Symbol will be removed from your #define directives. Do you want to continue?", "Remove Symbol", "Cancel"))
        {
          DefineSymbolsUtility.RemoveDefineSymbol("TEXTMESHPRO");
          EditorUtility.DisplayDialog("Example Custom Defines",
                                      "Symbol [TEXTMESHPRO] REMOVED!", "Ok");
        }
      }
      else
      {
        EditorUtility.DisplayDialog("Example Custom Defines",
                                    "Cannot remove symbol [TEXTMESHPRO] because it does not exist in your #define directives.", "Ok");
      }
    }

    }
  }

