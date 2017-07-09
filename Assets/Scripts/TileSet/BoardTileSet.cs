using UnityEngine;

[CreateAssetMenu(fileName = "NewBoardTileSet", menuName = "Custom/Board Tile Set")]
public class BoardTileSet : ScriptableObject {
    [SerializeField]
    private IndexVector playerStartingPosition = IndexVector.Zero;
    [SerializeField]
    private TileData tiles = new TileData(2, 2);

    public IndexVector PlayerStartingPosition { get { return playerStartingPosition; } }
    public TileData Tiles { get { return tiles; } }
}
