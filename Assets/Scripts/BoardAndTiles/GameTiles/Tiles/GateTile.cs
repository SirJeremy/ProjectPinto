using UnityEngine;
public class GateTile : GameTile {
    private void Start() {
        SetRenderer(!IsTraverseable);
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
        SetRenderer(!IsTraverseable);
        EventManager.AnnounceOnTraversabilityChange(Location, IsTraverseable);
    }
    
}
