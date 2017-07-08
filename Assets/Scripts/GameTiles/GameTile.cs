using UnityEngine;

public class GameTile : MonoBehaviour {
    [SerializeField]
    private bool isTraveseable = true;
    [SerializeField]
    private bool canChangeTraversability = false;
    [SerializeField]
    private ETile type = ETile.EMPTY;
    private IndexVector location = IndexVector.Zero;

    public IndexVector Location { get { return location; } set { location = value; } }
    public bool IsTraverseable { get { return isTraveseable; } protected set { isTraveseable = value; } }
    public bool CanChangeTraversability { get { return canChangeTraversability; } }
    public ETile Type { get { return type; } }
}
