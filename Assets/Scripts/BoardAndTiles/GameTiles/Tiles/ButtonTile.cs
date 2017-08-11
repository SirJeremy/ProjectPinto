using UnityEngine;

public class ButtonTile : GameTile {
    #region Varaibles
    [SerializeField]
    new private Renderer renderer = null;
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

    #region Methods
    public override void SetColor(EColor color) {
        if(colorChannel == color)
            return;
        colorChannel = color;
        Color newColor = ColorChartManager.GetColorMaterial(color);
        renderer.material.color = newColor;
    }

    #region EventCatchers
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
    #endregion

    #endregion
}
