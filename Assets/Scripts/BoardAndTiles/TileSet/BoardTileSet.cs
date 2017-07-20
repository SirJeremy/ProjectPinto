using UnityEngine;

[CreateAssetMenu(fileName = "NewBoardTileSet", menuName = "Custom/Board Tile Set")]
public class BoardTileSet : ScriptableObject {
    public IndexVector playerStartingPosition = IndexVector.Zero;
    public bool hasUIButtons = false;
    public EColor[] uiButtonColors = new EColor[3]; 
    public TileSetData tiles = new TileSetData(2, 2);
}
