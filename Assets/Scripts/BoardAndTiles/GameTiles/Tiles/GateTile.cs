using UnityEngine;
public class GateTile : GameTile {
    [SerializeField]
    private EColor colorChannel = EColor.BLUE;

    private Renderer rend = null;

    private void Start() {
        SetRenderer();
    }
    private void OnEnable() {
        EventManager.OnButtonDown += OnButtonDown;
    }
    private void OnDisable() {
        EventManager.OnButtonDown -= OnButtonDown;
    }

    private void OnButtonDown(EColor color) {
        //Location is from GameTile
        if(colorChannel == color)
            ChangeTraversability();
    }
    private void ChangeTraversability() {
        Debug.Log(IsTraverseable + "  ");
        IsTraverseable = !IsTraverseable;
        SetRenderer();
        EventManager.AnnounceOnTraversabilityChange(Location, IsTraverseable);
    }
    private void SetRenderer() {
        if(rend == null)
            rend = GetComponent<Renderer>();
        rend.enabled = !IsTraverseable;
    }
}
