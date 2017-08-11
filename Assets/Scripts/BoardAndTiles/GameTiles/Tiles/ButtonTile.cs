using UnityEngine;

public class ButtonTile : GameTile {
    #region Varaibles
    [SerializeField]
    private bool canOnlyBePressedOnce = false;
    private bool hasBeenPressed = false;
    #endregion

    #region MonoBehaviours
    private void OnEnable() {
        EventManager.OnPlayerLocationChange += OnPlayerLocationChange;
    }
    private void OnDisable() {
        EventManager.OnPlayerLocationChange -= OnPlayerLocationChange;
    }
    #endregion

    private void OnPlayerLocationChange(IndexVector location) {
        //Location is from GameTile
        if(Location == location)
            PressButton();
    }
    private void PressButton() {
        if(canOnlyBePressedOnce) {
            if(!hasBeenPressed) {
                EventManager.AnnounceOnButtonDown(colorChannel);
                hasBeenPressed = true;
            }
        }
        else {
            EventManager.AnnounceOnButtonDown(colorChannel);
        }
        
    }
}
