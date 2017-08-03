using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Custom/Level Data")]
public class LevelData : ScriptableObject {
    public IndexVector playerStartingPosition = IndexVector.Zero;
    public bool hasUIButtons = false;
    public EColor[] uiButtonColors = new EColor[3];
    public string levelName = "";
    public TileSetData tiles = new TileSetData(2, 2);
}
