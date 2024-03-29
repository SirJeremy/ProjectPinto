﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoSingleton<InputManager> {
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

    #region Static
    #region Variables
    private static HashSet<string> inputHash = null;
    private static EGameState currentGameState = EGameState.NONE; //changed by gameManager

    private static string cancel = "Cancel";
    private static string up = "Up";
    private static string down = "Down";
    private static string left = "Left";
    private static string right = "Right";

    private static bool backButtonLeavesApp = true;
    private static bool isShowingNotification = false;
    #endregion

    #region Properites
    public static string Cancel { get { return cancel; } }
    public static string Up { get { return up; } }
    public static string Down { get { return down; } }
    public static string Left { get { return left; } }
    public static string Right { get { return right; } }

    public static bool BackButtonLeavesApp {
        get { return backButtonLeavesApp; }
        set {
            backButtonLeavesApp = value;
            Input.backButtonLeavesApp = (backButtonLeavesApp && !NotificationWindow.IsShowingNotification);
        }
    }
    public static bool IsShowingNotification {
        get { return isShowingNotification; }
        set {
            isShowingNotification = value;
            Input.backButtonLeavesApp = (backButtonLeavesApp && !NotificationWindow.IsShowingNotification);
        }
    }

    #endregion

    #region Methods
    #region Buttons
    public static bool GetButton(string buttonName) {
        if(!IsExistingInputName(buttonName))
            return false;
        return Input.GetButton(buttonName);
    }
    public static bool GetButton(string buttonName, EGameState desiredGameStateInput) {
        if(!IsExistingInputName(buttonName))
            return false;
        if(desiredGameStateInput == currentGameState)
            return Input.GetButton(buttonName);
        return false;
    }
    public static bool GetButtonDown(string buttonName) {
        if(!IsExistingInputName(buttonName))
            return false;
        return Input.GetButtonDown(buttonName);
    }
    public static bool GetButtonDown(string buttonName, EGameState desiredGameStateInput) {
        if(!IsExistingInputName(buttonName))
            return false;
        if(desiredGameStateInput == currentGameState)
            return Input.GetButtonDown(buttonName);
        return false;
    }
    public static bool GetButtonUp(string buttonName) {
        if(!IsExistingInputName(buttonName))
            return false;
        return Input.GetButtonUp(buttonName);
    }
    public static bool GetButtonUp(string buttonName, EGameState desiredGameStateInput) {
        if(!IsExistingInputName(buttonName))
            return false;
        if(desiredGameStateInput == currentGameState)
            return Input.GetButtonUp(buttonName);
        return false;
    }
    #endregion

    #region MouseMethods
    public static void LockMouse() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public static void UnlockMouse() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public static void ConfineMouse() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    #endregion

    #region ErrorCheckMethods
    private static bool IsExistingInputName(string name) {
        if(inputHash == null)
            InitalizeInputHash();
        if(!inputHash.Contains(name)) {
            Debug.LogError("Input source does not exist. Check Input for (" + name + ") to see if it does not exist, is spelt incorrectly, or if it has not been added to inputHash.");
            return false;
        }
        return true;
    }
    private static void InitalizeInputHash() {
        inputHash = new HashSet<string>() {
            cancel,
            up,
            down,
            left,
            right
        };
    }
    #endregion
    #endregion
    #endregion

    #region NonStatic
    #region Variables
    [SerializeField]
    private bool autoDetectDpi = false;
    [SerializeField]
    private float manualDpi = 401;
    [SerializeField]
    private static float defaultDpi = 400;
    [SerializeField]
    [Tooltip("For swipes, it fudges the starting position and ending positions via averaging. This number caps the total seconds back it will store for starting position fudge.")]
    private float swipeFudge = 1;
    [SerializeField]
    [Tooltip("This controls how far back the old positions should be considered into decideding the final position of the swipe. Note that setting this value too big or bigger than swipe fudge may cause swipes to fail.")]
    private float endFudge = .05f;
    [SerializeField]
    private float swipeThreshhold = .5f; //need to find

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
        if(Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                EndSwipe();
            else if(touch.phase == TouchPhase.Began)
                StartSwipe();
            else
                AddPosition(touch.position, touch.deltaTime);
        }
        if(Input.GetButtonDown(Up))
            EventManager.AnnounceOnMoveInput(EDirection.UP);
        else if(Input.GetButtonDown(Down))
            EventManager.AnnounceOnMoveInput(EDirection.DOWN);
        else if(Input.GetButtonDown(Left))
            EventManager.AnnounceOnMoveInput(EDirection.LEFT);
        else if(Input.GetButtonDown(Right))
            EventManager.AnnounceOnMoveInput(EDirection.RIGHT);
        if(Input.GetButtonDown(Cancel))
            EventManager.AnnounceOnCancelInput();
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
            //Debug.Log(GetDistanceSwiped(Vector2.Distance(StartPosition, endPosition), positionsTime, manualDpi));
            if(GetDistanceSwiped(Vector2.Distance(StartPosition, endPosition), positionsTime, manualDpi) >= swipeThreshhold) {
                Swipe swipe = new Swipe(StartPosition, endPosition);
                //Debug.Log("Swipe successful  " + swipe.ToString());
                EventManager.AnnounceOnMoveInput(swipe.GetEDirection);
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
        if(positions.Count == 2) {
            endPosition = positions[1].Position;
            return true;
        }
        else if(positions.Count > 2) {
            //the following is to find the wighted average of the position over the past endFudge
            int i = positions.Count - 1; //i == last position in positions
            Vector2 valueWeight = Vector2.zero; //valueWeight is (position.n * deltaTime.n) + (position.n-1 * deltaTime.n-1) + ....
            float totalWeight = 0; //totalWeight is the deltaTime over total positions considered in the fudge
            while(totalWeight < endFudge && i > 0) {
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
    #endregion    
}
