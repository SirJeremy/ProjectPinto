using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSet", menuName = "Custom/Level Set")]
public class LevelSet : ScriptableObject {
    [SerializeField]
    private BoardTileSet[] levels = new BoardTileSet[0];

    public BoardTileSet GetLevel(int levelIndex) {
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
