using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreditsController))]
public class CreditsControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Get Credits"))
        {
            ((CreditsController)target).creditPanels = GetAllInstances<CreditPanel>();
            EditorUtility.SetDirty(target);
        }
        DrawDefaultInspector();
    }
    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }
}
