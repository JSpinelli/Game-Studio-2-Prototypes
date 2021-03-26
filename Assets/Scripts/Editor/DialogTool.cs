
using System.Collections.Generic;
using System.Linq;
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
            Selection.objects = new []{interactable.gameObject};
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
                        true, true, true, false);
                    list.drawHeaderCallback = (Rect rect) => {
                        EditorGUI.LabelField(rect, "Dialogs attached");
                    };
                    list.drawElementCallback = 
                        (Rect rect, int index, bool isActive, bool isFocused) => {
                            var element = list.serializedProperty.GetArrayElementAtIndex(index);
                            rect.y += 2;
                            dt.dialogs[index]  = EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width - 20, EditorGUIUtility.singleLineHeight), dt.dialogs[index], typeof(Dialog)) as Dialog;
                            if (GUI.Button(new Rect(rect.x + rect.width - 20, rect.y, 20, EditorGUIUtility.singleLineHeight), "X"))
                            {
                                dt.dialogs = dt.dialogs.Skip(index).ToArray();
                                serializedObject.ApplyModifiedProperties();
                            }
                        };
                    serializedObject.Update();
                    list.DoLayoutList();
                    serializedObject.ApplyModifiedProperties();
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
                    st.InteractWillTurnOff = EditorGUILayout.Toggle("Interact Will Turn Off", st.InteractWillTurnOff);
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
                    mv.sound = EditorGUILayout.ObjectField("Sound",mv.sound,typeof(AudioSource)) as AudioSource;
                    mv.newPosition = EditorGUILayout.Vector3Field("Move to", mv.newPosition);
                    mv.newRotation = EditorGUILayout.Vector3Field("Rotate to", mv.newRotation);
                    EditorGUILayout.Separator();
                }
                
            }
        }
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Filters");
        showDialogTriggers = EditorGUILayout.Toggle("Show Dialog Triggers",showDialogTriggers);
        showSoundTriggers = EditorGUILayout.Toggle("Show Sound Triggers",showSoundTriggers);
        showMovable = EditorGUILayout.Toggle("Show Movables",showMovable);
        //showColliders = EditorGUILayout.Toggle("Show Collider Dialogs",showColliders);
        
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
