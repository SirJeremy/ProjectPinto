using UnityEngine;

public class ButtonTile : GameTile {
    private bool hasBeenPressed = false;

    private void OnEnable() {
        EventManager.OnPlayerLocationChange += OnPlayerLocationChange;
    }
    private void OnDisable() {
        EventManager.OnPlayerLocationChange -= OnPlayerLocationChange;
    }

    private void OnPlayerLocationChange(IndexVector location) {
        //Location is from GameTile
        if(Location == location)
            PressButton();
    }
    private void PressButton() {
        if(!hasBeenPressed) {
            EventManager.AnnounceOnButtonDown(colorChannel);
            hasBeenPressed = true;
        }
    }
}
