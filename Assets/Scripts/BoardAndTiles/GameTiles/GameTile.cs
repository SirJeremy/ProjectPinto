using UnityEngine;

public class GameTile : MonoBehaviour {
    [SerializeField]
    private ETraversableDirection traversability = ETraversableDirection.FULLY_TRAVERSABLE;
    [SerializeField]
    private bool canChangeTraversability = false;
    [SerializeField]
    private ETile type = ETile.EMPTY;
    protected EColor colorChannel = EColor.DEFAULT;
    private IndexVector location = IndexVector.Zero;

    public IndexVector Location { get { return location; } set { location = value; } }
    public bool IsTraverseable { get { return Helper.HasFlag(traversability, ETraversableDirection.FULLY_TRAVERSABLE); } }
    public ETraversableDirection Traversability { get { return traversability; } protected set { traversability = value; } }
    public bool CanChangeTraversability { get { return canChangeTraversability; } }
    public ETile Type { get { return type; } }

    protected virtual void OnDestroy() {
        StopAllCoroutines();
    }

    public virtual void SetColor(EColor color) {
        //Do nothing unless overriden
    }
}
