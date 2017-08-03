using UnityEngine;

[CreateAssetMenu(fileName = "NewWorldData", menuName = "Custom/World Data")]
public class WorldData : ScriptableObject {
    [SerializeField]
    private LevelData[] levels = new LevelData[0];
    public string worldName = "";

    public int NumberOfLevels { get { return levels.Length; } }

    public LevelData GetLevel(int levelIndex) {
        if(levelIndex < 0) {
            Debug.LogWarning("LevelIndex (" + levelIndex + ") is less than zero. Retreaving level 0 instead.");
            levelIndex = 0;
        }
        if(levelIndex < levels.Length) 
            return levels[levelIndex];
        if(levels.Length == 0) {
            Debug.LogError("There are no levels in " + name);
            return null;
        }
        Debug.LogWarning("LevelIndex (" + levelIndex + ") is greater than total levels. Retreaving last level (" + (levels.Length - 1) + ") instead.");
        return levels[levels.Length - 1];
    }
}
