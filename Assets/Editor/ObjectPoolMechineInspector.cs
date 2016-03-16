using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectsPoolMechine))]
public class ObjectPoolMechineInspector : Editor
{
    public ObjectsPoolMechine current
    {
        get { return (ObjectsPoolMechine) target; }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("AddToList"))
        {
            if (!current.AddToList())
            {
                Debug.Log("Object already exist");
            }
        }

        if (GUILayout.Button("RemoveFromList"))
        {
            if (!current.RemoveFromList())
            {
                Debug.Log("Object dosen't exist");
            }
        }

        if (GUILayout.Button("ResetList"))
        {
            current.ResetList();
        }

   }
}
