using UnityEngine;

public class BoardTile {
    #region Variables
    private bool isOccupiedByPlayer = false;
    private bool playerIsMovingIn = false;
    private bool isTraversable = true;
    private ETile tileType = ETile.EMPTY;
    //private EColor color = EColor.DEFAULT;
    private IndexVector location = IndexVector.Zero;
    private GameObject gameTile = null;

    private bool isSubscribed = false;
    #endregion

    #region Properties
    public bool IsOccupiedByPlayer { get { return isOccupiedByPlayer; } set { isOccupiedByPlayer = value; } }
    public bool IsPlayerIsMovingIn { get { return playerIsMovingIn; } set { playerIsMovingIn = value; } }
    public bool IsTraversable { get { return isTraversable; } set { isTraversable = value; } }
    public IndexVector Location { get { return location; } }
    public ETile Type { get { return tileType; } }
    public GameObject GameTile { get { return gameTile; } }
    #endregion

    #region Constructor
    public BoardTile(ETile tileType, EColor color, IndexVector location) {
        this.tileType = tileType;
        //this.color = color;
        this.location = location;

        //rather than creating another bool canChangeTraversability,
        //isSubscribed is used instead since they will have the same value and isSubsribed will be used outside of the constructor, unlike canChangeTraversability
        gameTile = GameTileSpawner.SpawnGameTile(tileType, color, location, out isTraversable, out isSubscribed);
        if(isSubscribed) 
            EventManager.OnTraversabilityChange += ChangeTraversability;
    }
    #endregion

    #region Methods
    public void DestroyTile() {
        Object.Destroy(gameTile);
        gameTile = null; //is neccessary?
        if(isSubscribed)
            EventManager.OnTraversabilityChange -= ChangeTraversability;
    }
    private void ChangeTraversability(IndexVector location, bool isTraversable) {
        if(this.location == location) {
            this.isTraversable = isTraversable;
        }
    }
    #endregion
}
