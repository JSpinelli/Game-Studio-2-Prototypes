
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
                EditorGUILayout.Separator();
                interactable.name = EditorGUILayout.TextArea(interactable.name);
                if (GUILayout.Button("Focus"))
                {
                    Vector3 position = interactable.transform.position;
                    position.z -= 1f;
                    SceneView.lastActiveSceneView.pivot = position;
                    SceneView.lastActiveSceneView.Repaint();
                }
                if (interactable is DialogTrigger)
                {
                    DialogTrigger dt = (DialogTrigger) interactable;
                    for (int i =0; i< dt.dialogs.Length;i++)
                    {
                        dt.dialogs[i] = EditorGUILayout.ObjectField( ""+i, dt.dialogs[i], typeof( Dialog )) as Dialog;
                    }
                    // EditorGUILayout.
                    // EditorGUILayout.ObjectField("Audio Source",dt.au,AudioSource)
                }
                if (interactable is SoundTrigger)
                {
                    SoundTrigger st = (SoundTrigger) interactable;
                    st.source = EditorGUILayout.ObjectField("Audio Source",st.source,typeof(AudioSource)) as AudioSource;
                }
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
