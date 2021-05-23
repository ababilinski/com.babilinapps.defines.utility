  #if !TEXTMESHPROEXAMPLE
    using System.Collections; 
    using BabilinApps.Defines.Utility.Editor; 
    using UnityEditor; 
    using UnityEngine;
    namespace BabilinApps.Defines.Utility.Sample.Editor{
    [UnityEditor.InitializeOnLoad] 
    public static class TextMeshProExampleDefineSymbol
    {  
	    static TextMeshProExampleDefineSymbol()
        {   
            
	        if (DefineSymbolsUtility.DirectoryPathExistsWildCard("Library/PackageCache/","com.unity.textmeshpro*"))
            {
                DefineSymbolsUtility.AddDefineSymbol("TEXTMESHPROEXAMPLE");
                Debug.Log("<color=green> Added TEXTMESHPROEXAMPLE Define Symbol.</color>");
            }
        }
    } 
}
#endif