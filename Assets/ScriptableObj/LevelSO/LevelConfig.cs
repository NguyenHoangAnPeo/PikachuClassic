using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/NewLevel")]
public class LevelConfig : ScriptableObject
{
    public int levelCount;
    public int width;
    public int height;
    public float timeLimit;
}
