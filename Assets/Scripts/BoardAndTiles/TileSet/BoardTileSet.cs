using UnityEngine;

[CreateAssetMenu(fileName = "NewBoardTileSet", menuName = "Custom/Board Tile Set")]
public class BoardTileSet : ScriptableObject {
    public IndexVector playerStartingPosition = IndexVector.Zero;
    public TileSetData tiles = new TileSetData(2, 2);
}
