using UnityEngine;
using System.Collections;

public class GateTile : GameTile {
    #region Variables
    [SerializeField]
    private Renderer[] renderers = null;
    [SerializeField]
    private Transform[] hinges = new Transform[4];
    [SerializeField]
    private AccelerationVelocityValue avv = new AccelerationVelocityValue();

    private bool isOpen = false; //keeps track if tile should switch to is or !isTraversable when avaliable to do so
    private bool isWatitingToChange = false; //happens if something is over the gate
    private bool isPlayingAnimation = false;
    private float coTime = 0;
    #endregion

    #region MonoBehaviours
    private void Start() {
        isOpen = IsTraverseable;
        if(IsTraverseable) {
            avv.ResetToMax();
            SetHingeRotation(avv.Value);
        }
        else {
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
    #endregion

    #region Methods
    public override void SetColor(EColor color) {
        if(colorChannel == color)
            return;
        colorChannel = color;
        Color newColor = ColorChartManager.GetColorMaterial(color);
        foreach(Renderer ren in renderers) {
            ren.material.color = newColor;
        }
    }

    private void ChangeTraversability() {
        if(isOpen)
            Traversability = ETraversableDirection.FULLY_TRAVERSABLE;
        else
            Traversability = ETraversableDirection.NON_TRAVERSBLE;
        EventManager.AnnounceOnTraversabilityChange(Location, Traversability);
        if(IsTraverseable)
            StartOpenAnimation();
        else
            StartCloseAnimation();
    }
    private void SetHingeRotation(float deg) {
        deg = Mathf.Repeat(deg, 360);
        foreach(Transform t in hinges) {
            t.localRotation = Quaternion.Euler(new Vector3(deg, t.localEulerAngles.y, t.localEulerAngles.z));
        }
    }

    #region EventCatchers
    private void OnButtonDown(EColor color) {
        //Location is from GameTile
        if(colorChannel == color) {
            isOpen = !isOpen;
            if(!isWatitingToChange) {
                if(BoardManager.Instance.GameBoard.CurrentPlayerPosition == Location || BoardManager.Instance.GameBoard.PlayerMovingTo == Location)
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
    #endregion

    #region AnimationMethods
    private void StartCloseAnimation() /* Closed == hinges pointed up */ {
        if(!isPlayingAnimation)
            StartCoroutine(Animate());
    }
    private void StartOpenAnimation() /* Open == hinges pointed down */ {
        if(!isPlayingAnimation)
            StartCoroutine(Animate());
    }
    private IEnumerator Animate() {
        isPlayingAnimation = true;
        coTime = Time.time - Time.deltaTime;
        while(true) {
            if(IsTraverseable) /* Is opening */ {
                SetHingeRotation(avv.Increment(Time.time - coTime));
                coTime = Time.time;
                if(avv.IsValueAtMax) {
                    avv.ResetToMax();
                    break;
                }
            }
            else /* Is closing  */ {
                SetHingeRotation(avv.Decrement(Time.time - coTime));
                coTime = Time.time;
                if(avv.IsValueAtMin) {
                    avv.ResetToMin();
                    break;
                }
            }
            yield return null;
        }
        isPlayingAnimation = false;
        yield return null;
    }
    #endregion

    #endregion
}
