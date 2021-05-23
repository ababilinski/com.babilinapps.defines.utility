using UnityEngine;
namespace BabilinApps.Defines.Utility.Sample{
public class CustomDefinesSample : MonoBehaviour
{
	void Start()
	{
		#if !TEXTMESHPROEXAMPLE
		 Debug.Log("<color=orange>CANNOT FIND [TEXTMESHPROEXAMPLE] Define Symbol.</color>");
		#else
		  Debug.Log("<color=green>FOUND [TEXTMESHPROEXAMPLE] Define Symbol.</color>");
		#endif
	}
}
}