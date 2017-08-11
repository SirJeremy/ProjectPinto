using UnityEngine;
using System.Collections;

public class GateTile : GameTile {
    public enum EAnimState {
        IS_CLOSED,
        IS_OPEN,
        IS_OPENING,
        IS_CLOSING
    }

    [SerializeField]
    private Transform[] hinges = new Transform[4];
    [SerializeField]
    private AccelerationVelocityValue avv = new AccelerationVelocityValue();

    private bool isOpen = false;
    private bool isWatitingToChange = false;
    private bool isPlayingAnimation = false;
    private EAnimState animState = EAnimState.IS_CLOSED;
    private float coTime = 0;

    private void Start() {
        isOpen = IsTraverseable;
        if(IsTraverseable) {
            animState = EAnimState.IS_OPEN;
            avv.ResetToMax();
            SetHingeRotation(avv.Value);
        }
        else {
            animState = EAnimState.IS_CLOSED;
            avv.ResetToMin();
            SetHingeRotation(avv.Value);
        }
    }
    private void OnEnable() {
        EventManager.OnButtonDown += OnButtonDown;
        EventManager.OnPlayerLocationChange += OnPlayerLocationChange;
    }
    private void OnDisable() {
        EventManager.OnButtonDown -= OnButtonDown;
        EventManager.OnPlayerLocationChange -= OnPlayerLocationChange;
    }

    private void OnButtonDown(EColor color) {
        //Location is from GameTile
        if(colorChannel == color) {
            isOpen = !isOpen;
            if(!isWatitingToChange) {
                if(BoardManager.Instance.GameBoard.CurrentPlayerPosition == Location)
                    isWatitingToChange = true;
                else
                    ChangeTraversability();
            }
        }
    }
    private void OnPlayerLocationChange(IndexVector playerLocation) {
        if(isWatitingToChange && Location != playerLocation) {
            isWatitingToChange = false;
            ChangeTraversability();
        }
    }

    private void ChangeTraversability() {
        IsTraverseable = isOpen;
        EventManager.AnnounceOnTraversabilityChange(Location, IsTraverseable);
        if(IsTraverseable)
            StartOpenAnimation();
        else
            StartCloseAnimation();
    }

    private void StartCloseAnimation() /* Closed == hinges pointed up */ {
        animState = EAnimState.IS_CLOSING;
        if(!isPlayingAnimation)
            StartCoroutine(AnimCo());
    }
    private void StartOpenAnimation() /* Open == hinges pointed down */ {
        animState = EAnimState.IS_OPENING;
        if(!isPlayingAnimation)
            StartCoroutine(AnimCo());
    }
    private IEnumerator AnimCo() {
        isPlayingAnimation = true;
        coTime = Time.time - Time.deltaTime;
        while(true) {
            if(animState == EAnimState.IS_CLOSING) {
                SetHingeRotation(avv.Decrement(Time.time - coTime));
                coTime = Time.time;
                if(avv.IsValueAtMin) {
                    avv.ResetToMin();
                    animState = EAnimState.IS_CLOSED;
                    break;
                }
            }
            else if(animState == EAnimState.IS_OPENING) {
                SetHingeRotation(avv.Increment(Time.time - coTime));
                coTime = Time.time;
                if(avv.IsValueAtMax) {
                    avv.ResetToMax();
                    animState = EAnimState.IS_OPEN;
                    break;
                }
            }
            yield return null;
        }
        isPlayingAnimation = false;
        yield return null;
    }

    private void SetHingeRotation(float deg) {
        deg = Mathf.Repeat(deg, 360);
        foreach(Transform t in hinges) {
            t.localRotation = Quaternion.Euler(new Vector3(deg, t.localEulerAngles.y, t.localEulerAngles.z));
        }
    }
}
