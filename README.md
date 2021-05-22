# Define Symbols Utility
The script allows you to easily add custom **#Define Symbols**. Which helps avoid compiling code that is missing a dependency.

# Table of Contents (up to date)

 - #### [WHAT IS IT?](#Define-Symbols-Utility)
 - #### [WHY YOU USE IT?](#Why-use-it?)
 - #### [INSTALLING](#Installing-the-package)
 - #### [HOW IT WORKS](#How-does-it-work?)
---

# Why use it?
Have you ever wanted to create your own Define Symbol to avoid errors if a specific plugin was not installed? My teams have. While working in a project that has many submodules and custom plugins, it's important to avoid requiring everyone to download a specific asset.

# Installing the package

- Use the Unity Package Manager and past the GitHub URL []
- Download the script/package from the Release Section


# How does it work?

The script uses `PlayerSettings.GetScriptingDefineSymbolsForGroup()` and then splits up the string to a list. After the list is created it adds or removes your defined symbol.

### Checking for a required file is your responsibility.

![Cheech Marin Responsibility gif](Media/Cheech-Marin-Responsibility.gif)
That said, we added some class to check for specific files: `EditorFileUtility.FileExistsInAbsolutePath()` checks for a path. the start path is the project root folder (outside of the Assets). You can also check for a valid folder in your assets by calling `EditorFileUtility.ValidFolder()`

You can use the code mentioned above as a condition to add or remove a definition. I would recommend creating a script in the Editor folder called **[YOUR-PLUGIN-NAME]DefineSymbol.cs** and add the following:

    #if ![YOUR-PLUGIN-DEFINE-SYMBOL]  
    using System.Collections; 
    using Common.EditorUtilities; 
    using UnityEditor; 
    using UnityEngine;
    
    [UnityEditor.InitializeOnLoad] 
    public static class [your-plugin-here]DefineSymbol 
    {  
	    static [your-plugin-here]DefineSymbol()
        { 
	        if (AssetDatabaseUtility.HasAsset("YOUR ASSET") 
               || EditorFileUtility.ValidFolder("YOUR FOLDER"))
            {
                DefineSymbolsUtility.AddDefineSymbol("[your-plugin-DefineSymbol]");
                Debug.Log("Added your-plugin Define Symbol.");
            }
        }
    } 
    #endif

  

And that's it! You will now have your custom definition added if the condition is met.
