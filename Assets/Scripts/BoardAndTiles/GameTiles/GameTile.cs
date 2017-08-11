using UnityEngine;

public class GameTile : MonoBehaviour {
    [SerializeField]
    private bool isTraveseable = true;
    [SerializeField]
    private bool canChangeTraversability = false;
    [SerializeField]
    private ETile type = ETile.EMPTY;
    protected EColor colorChannel = EColor.DEFAULT;
    private IndexVector location = IndexVector.Zero;
    private Renderer rend = null;

    public IndexVector Location { get { return location; } set { location = value; } }
    public bool IsTraverseable { get { return isTraveseable; } protected set { isTraveseable = value; } }
    public bool CanChangeTraversability { get { return canChangeTraversability; } }
    public ETile Type { get { return type; } }
    protected Renderer Rend { get { if(rend == null) rend = GetComponent<Renderer>(); return rend; } }

    protected virtual void OnDestroy() {
        StopAllCoroutines();
    }

    public virtual void SetColor(EColor color) { 
        if(colorChannel == color)
            return;
        colorChannel = color;
        if(Rend == null) //if go doesn not have a renderer
            return;        
        Rend.material.color = ColorChartManager.GetColorMaterial(color).color;
    }
}
