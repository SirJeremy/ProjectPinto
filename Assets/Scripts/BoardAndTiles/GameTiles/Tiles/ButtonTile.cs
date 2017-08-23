using UnityEngine;
using System.Collections;

public class ButtonTile : GameTile {
    #region Varaibles
    [SerializeField]
    new private Renderer renderer = null;
    [SerializeField]
    private bool canOnlyBePressedOnce = false;
    [SerializeField]
    private Transform buttonPivot = null;
    [SerializeField]
    private CurveProgressValue pressAnim = new CurveProgressValue();

    private bool hasBeenPressed = false;
    private bool needToRelease = false;
    private bool isAnimationInProgress = false;
    private bool isPlayingPressAnimation = false;
    private float coTime = 0;
    #endregion

    #region MonoBehaviours
    private void OnEnable() {
        EventManager.OnPlayerLocationChange += OnPlayerLocationChange;
        EventManager.OnPlayerStartLocationChange += OnPlayerStartLocationChange;
    }
    private void OnDisable() {
        EventManager.OnPlayerLocationChange -= OnPlayerLocationChange;
        EventManager.OnPlayerStartLocationChange -= OnPlayerStartLocationChange;
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
        if(Location == location) {
            PressButton();
        }
    }
    private void OnPlayerStartLocationChange(IndexVector location) {
        if(Location == location) {
            if(!canOnlyBePressedOnce) {
                needToRelease = true;
                EnterPressAnimation();
            }
            else if(!hasBeenPressed) {
                EnterPressAnimation();
            } 
        }
        else if(needToRelease) {
            EnterReleaseAnimation();
            needToRelease = false;
        }
    }
    #endregion

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

    private void EnterPressAnimation() {
        isPlayingPressAnimation = true;
        pressAnim.SetToMin();
        if(!isAnimationInProgress)
            StartCoroutine(PressAnimation());
    }
    private void EnterReleaseAnimation() {
        isPlayingPressAnimation = false;
        pressAnim.SetToMax();
        if(!isAnimationInProgress)
            StartCoroutine(PressAnimation());
    }
    private IEnumerator PressAnimation() {
        isAnimationInProgress = true;
        coTime = Time.time - Time.deltaTime;
        while(true) {
            if(isPlayingPressAnimation) /* Press */ {
                buttonPivot.localPosition = new Vector3(0, pressAnim.GetValueAndIncrementProgress(Time.time - coTime), 0);
                coTime = Time.time;
                if(pressAnim.IsMaxProgress) {
                    break;
                }
            }
            else /* Depress */ {
                buttonPivot.localPosition = new Vector3(0, pressAnim.GetValueAndDecrementProgress(Time.time - coTime), 0);
                coTime = Time.time;
                if(pressAnim.IsMinProgress) {
                    break;
                }
            }
            yield return null;
        }
        isAnimationInProgress = false;
        yield return null;
    }
    #endregion
}
