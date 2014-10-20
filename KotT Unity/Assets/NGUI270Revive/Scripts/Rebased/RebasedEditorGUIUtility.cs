using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RebasedEditorGUIUtility
{
	public static void LookLikeControls (float labelWidth)
	{
        #if UNITY_EDITOR
		EditorGUIUtility.labelWidth = labelWidth;
		EditorGUIUtility.fieldWidth = 0f;
        #endif
	}

}


