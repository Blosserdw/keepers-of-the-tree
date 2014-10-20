using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RebasedUndo
{	
	public static void RegisterUndo (UnityEngine.Object objectToUndo, string name)
	{
        #if UNITY_EDITOR
		Undo.RecordObject (objectToUndo, name);
        #endif
	}
}