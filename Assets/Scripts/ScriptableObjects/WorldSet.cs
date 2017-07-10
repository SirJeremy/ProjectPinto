using UnityEngine;

[CreateAssetMenu(fileName = "NewWorldSet", menuName = "Custom/World Set")]
public class WorldSet : ScriptableObject {
    [SerializeField]
    private LevelSet[] worlds = new LevelSet[0];

    public BoardTileSet GetLevel(int worldIndex, int levelIndex) {
        return GetWorld(worldIndex).GetLevel(levelIndex);
    }
    public LevelSet GetWorld(int worldIndex) {
        if(worldIndex < 0) {
            Debug.LogWarning("WorldIndex (" + worldIndex + ") is less than zero. Retreaving world 0 instead.");
            worldIndex = 0;
        }
        if(worldIndex < worlds.Length)
            return worlds[worldIndex];
        if(worlds.Length == 0) {
            Debug.LogError("There are no worlds in " + name);
            return null;
        }
        Debug.LogWarning("WorldIndex (" + worldIndex + ") is greater than total levels. Retreaving last world (" + (worlds.Length - 1) + ") instead.");
        return worlds[worlds.Length - 1];
    }
}
