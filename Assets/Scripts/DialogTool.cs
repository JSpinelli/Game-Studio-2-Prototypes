
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogTool : EditorWindow
{
    private List<InteractableObject> _interactableObjectsOnScene = new List<InteractableObject>();
    [MenuItem("Window/Dialog Tool")]
    public static void ShowWindow()
    {
        GetWindow<DialogTool>("Dialog Tool");
    }

    private void OnGUI()
    {
        
        if (_interactableObjectsOnScene.Count == 0)
        {
            GUILayout.Label("No Interactables loaded",EditorStyles.boldLabel);
        }
        else
        {
            foreach (var interactable in _interactableObjectsOnScene)
            {
                GUILayout.Label(interactable.name,EditorStyles.boldLabel);
            }
        }

        if (GUILayout.Button("Find Interactables"))
        {
           var objects =  GameObject.FindGameObjectsWithTag("Interactable");
           foreach (var obj in objects)
           {
               InteractableObject interactable = obj.GetComponent<InteractableObject>();
               if (interactable)
               {
                   _interactableObjectsOnScene.Add(interactable);
               }
           }
        }

    }
}
