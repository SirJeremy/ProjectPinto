using UnityEngine;

public class ButtonTile : GameTile {
    [SerializeField]
    private EColor colorChannel = EColor.BLUE;
    [SerializeField] [Tooltip("Controls if the button can only be pressed once")]
    private bool onePress = true;

    private bool hasBeenPressed = false;
    private Renderer rend = null;

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
        if(onePress && !hasBeenPressed) {
            EventManager.AnnounceOnButtonDown(colorChannel);
            hasBeenPressed = true;
            SetRenderer(false);
        }
        else if(!onePress) {
            EventManager.AnnounceOnButtonDown(colorChannel);
        }
    }
    private void SetRenderer(bool isOn) {
        if(rend == null)
            rend = GetComponent<Renderer>();
        rend.enabled = isOn;
    }
}
