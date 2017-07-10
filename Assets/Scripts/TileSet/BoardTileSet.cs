using UnityEngine;

[CreateAssetMenu(fileName = "NewBoardTileSet", menuName = "Custom/Board Tile Set")]
public class BoardTileSet : ScriptableObject {
    public IndexVector playerStartingPosition = IndexVector.Zero;
    public TileData tiles = new TileData(2, 2);
}
