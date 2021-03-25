
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class DialogTool : EditorWindow
{
    private List<InteractableObject> _interactableObjectsOnScene = new List<InteractableObject>();
    private bool showDialogTriggers = true;
    private bool showSoundTriggers = true;
    private bool showMovable = true;
    private bool showColliders = true;

    private Vector2 scrollPos;
    [MenuItem("Window/Dialog Tool")]
    public static void ShowWindow()
    {
        GetWindow<DialogTool>("Dialog Tool");
    }

    private void ShowFocusButton(InteractableObject interactable)
    {
        if (GUILayout.Button("Focus",GUILayout.Width(100)))
        {
            Vector3 position = interactable.transform.position;
            position.z -= 1f;
            SceneView.lastActiveSceneView.pivot = position;
            SceneView.lastActiveSceneView.Repaint();
        }
    }

    private void ShowName(InteractableObject interactable)
    {
        
        interactable.name = EditorGUILayout.TextArea(interactable.name,GUILayout.Width(100));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh Interactables"))
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
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        if (_interactableObjectsOnScene.Count == 0)
        {
            GUILayout.Label("No Interactables loaded",EditorStyles.boldLabel);
        }
        else
        {
            foreach (var interactable in _interactableObjectsOnScene)
            {
                if (showDialogTriggers && interactable is DialogTrigger)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.BeginHorizontal();
                    ShowName(interactable);
                    ShowFocusButton(interactable);
                    EditorGUILayout.EndHorizontal();
                    
                    DialogTrigger dt = (DialogTrigger) interactable;

                    SerializedObject serializedObject = new UnityEditor.SerializedObject(dt);
                    //SerializedProperty serializedPropertyMyInt = serializedObject.FindProperty("dialogs");
                    ReorderableList  list = new ReorderableList(serializedObject, 
                        serializedObject.FindProperty("dialogs"), 
                        true, true, true, true);
                    serializedObject.Update();
                    list.DoLayoutList();
                    serializedObject.ApplyModifiedProperties();
                    list.drawElementCallback = 
                        (Rect rect, int index, bool isActive, bool isFocused) => {
                            var element = list.serializedProperty.GetArrayElementAtIndex(index);
                            rect.y += 2;
                            EditorGUILayout.ObjectField( ""+element, (Dialog) element, typeof( Dialog )) as Dialog;
                        };
                    //EditorGUI.PropertyField(new Rect(0, 0, 500, 30), serializedPropertyMyInt);
                    // for (int i =0; i< dt.dialogs.Length;i++)
                    // {
                    //     dt.dialogs[i] = EditorGUILayout.ObjectField( ""+i, dt.dialogs[i], typeof( Dialog )) as Dialog;
                    // }
                    EditorGUILayout.Separator();
                }
                if (showSoundTriggers && interactable is SoundTrigger)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.BeginHorizontal();
                    ShowName(interactable);
                    ShowFocusButton(interactable);
                    EditorGUILayout.EndHorizontal();
                    SoundTrigger st = (SoundTrigger) interactable;
                    st.source = EditorGUILayout.ObjectField("Audio Source",st.source,typeof(AudioSource)) as AudioSource;
                    EditorGUILayout.Separator();
                }

                if (showMovable && interactable is Movable)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.BeginHorizontal( );
                    ShowName(interactable);
                    ShowFocusButton(interactable);
                    EditorGUILayout.EndHorizontal();
                    Movable mv = (Movable) interactable;
                    EditorGUILayout.Separator();
                }
                
            }
        }

        showDialogTriggers = EditorGUILayout.Toggle("Show Dialog Triggers",showDialogTriggers);
        showSoundTriggers = EditorGUILayout.Toggle("Show Sound Triggers",showSoundTriggers);
        showMovable = EditorGUILayout.Toggle("Show Movables",showMovable);
        //showColliders = EditorGUILayout.Toggle("Show Collider Dialogs",showColliders);
        
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
