using UnityEngine;
using System.Collections.Generic;

public class SwipeInputManager : MonoSingleton<SwipeInputManager> {
    private struct PositionTime {
        private Vector2 position;
        private float deltaTime;

        public Vector2 Position { get { return position; } }
        public float DeltaTime { get { return deltaTime; } }

        public PositionTime(Vector2 position, float deltaTime) {
            this.position = position;
            this.deltaTime = deltaTime;
        }

        public override string ToString() {
            return "(" + position + "," + deltaTime + ")";
        }
    }

    #region Variables
    [SerializeField]
    private bool autoDetectDpi = false;
    [SerializeField]
    private float manualDpi = 401;
    [SerializeField]
    private static float defaultDpi = 400;
    [SerializeField] [Tooltip("For swipes, it fudges the starting position and ending positions via averaging. This number caps the total seconds back it will store for starting position fudge.")]
    private float swipeFudge = 1;
    [SerializeField] [Tooltip("This controls how far back the old positions should be considered into decideding the final position of the swipe. Note that setting this value too big or bigger than swipe fudge may cause swipes to fail.")]
    private float endFudge = .05f;
    [SerializeField]
    private float swipeThreshhold = 1; //need to find

    private List<PositionTime> positions = new List<PositionTime>();
    private float positionsTime = 0; //bad name
    private Touch touch;
    #endregion

    #region Properties
    private float Dpi {
        get {
            if(autoDetectDpi) {
                if(Screen.dpi > 0)
                    return Screen.dpi;
                return defaultDpi;
            }
            return manualDpi;
        }
    }
    private Vector2 StartPosition {
        get {
            if(positions.Count > 0)
                return positions[0].Position;
            return Vector2.zero;
        }
    }
    #endregion

    #region MonoBehvaiours
    private void Update() {
        //wip
        if(Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                EndSwipe();
            else if(touch.phase == TouchPhase.Began)
                StartSwipe();
            else
                AddPosition(touch.position, touch.deltaTime);
        }
    }
    #endregion

    #region Methods
    private void StartSwipe() {
        AddPosition(touch.position, touch.deltaTime);
    }
    private void EndSwipe() {
        AddPosition(touch.position, touch.deltaTime);
        //Debug.Log(touch.phase + ",   " + touch.deltaPosition.magnitude + ",   " + Screen.dpi + "   =   "  + GetDistanceSwiped(touch.deltaPosition.magnitude, touch.deltaTime, Dpi));
        Vector2 endPosition;
        if(GetEndFudgedPosition(out endPosition)) {
            Debug.Log(GetDistanceSwiped(Vector2.Distance(StartPosition, endPosition), positionsTime, manualDpi));
            if(GetDistanceSwiped(Vector2.Distance(StartPosition, endPosition), positionsTime, manualDpi) >= swipeThreshhold) {
                Swipe swipe = new Swipe(StartPosition, endPosition);
                Debug.Log("Swipe successful  " + swipe.ToString());
            }
        }
        positions.Clear();
        positionsTime = 0;
    }
    private float GetDistanceSwiped(float deltaPixels, float deltaTouchTime, float dpi) /* Returns distance swiped scaled to one second in inches from delta pixles */ {
        return deltaPixels / (deltaTouchTime * dpi);
    }
    private void AddPosition(Vector2 position, float deltaTime) {
        positions.Add(new PositionTime(position, deltaTime));
        if(positionsTime > 0) {
            positionsTime += deltaTime;
            if((positionsTime > swipeFudge) && (positionsTime - positions[0].DeltaTime > swipeFudge)) {
                positionsTime -= positions[0].DeltaTime;
                positions.RemoveAt(0);
            }
        }
        else
            positionsTime = deltaTime;
    }
    private bool GetEndFudgedPosition(out Vector2 endPosition) {
        if(positions.Count > 2) {
            //the following is to find the wighted average of the position over the past endFudge
            int i = positions.Count - 1; //i == last position in positions
            Vector2 valueWeight = Vector2.zero; //valueWeight is (position.n * deltaTime.n) + (position.n-1 * deltaTime.n-1) + ....
            float totalWeight = 0; //totalWeight is the deltaTime over total positions considered in the fudge
            while(totalWeight < endFudge && i > 1) {
                totalWeight += positions[i].DeltaTime;
                valueWeight += positions[i].Position * positions[i].DeltaTime;
            }
            endPosition = valueWeight / totalWeight;
            return true;
        }
        else {
            endPosition = Vector2.zero;
            return false;
        }
    }
    #endregion

}
