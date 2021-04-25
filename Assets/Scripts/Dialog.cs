using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public AudioClip clip;
    public float[] screenTime;
    public string[] line;
    public string source;
}
