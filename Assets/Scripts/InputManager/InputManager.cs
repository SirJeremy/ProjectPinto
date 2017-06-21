using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class InputManager {
    #region Varaibles
    private static HashSet<string> inputHash = null;
    private static EGameState currentGameState = EGameState.NONE; //changed by gameManager

    #region InputStrings
    private static string moveH = "MoveHorizontal";
    private static string moveV = "MoveVertical";
    private static string lookH = "LookHorizontal";
    private static string lookV = "LookVertical";
    private static string fire = "Fire";
    private static string jump = "Jump";
    private static string use = "Use";
    private static string confirm = "Confirm";
    private static string cancel = "Cancel";
    private static string pause = "Pause";
    #endregion

    #region Sensivity
    private static float mouseXSensitivity = 100;
    private static float mouseYSensitivity = 100;
    #endregion
    #endregion

    #region Properites
    public static float MouseXSensitivity { get { return mouseXSensitivity; } set { mouseXSensitivity = value; } }
    public static float MouseYSensitivity { get { return mouseYSensitivity; } set { mouseYSensitivity = value; } }

    public static string MoveH { get { return moveH; } }
    public static string MoveV { get { return moveV; } }
    public static string LookH { get { return lookH; } }
    public static string LookV { get { return lookV; } }
    public static string Fire { get { return fire; } }
    public static string Jump { get { return jump; } }
    public static string Use { get { return use; } }
    public static string Cancel { get { return cancel; } }
    public static string Pause { get { return pause; } }
    #endregion

    #region Constructor
    static InputManager() {
        InitalizeInputHash();
    }
    #endregion

    #region Methods
    #region Axies
    public static float GetAxis(string buttonName) {
        if(!IsExistingInputName(buttonName))
            return 0;
        return Input.GetAxis(buttonName);
    }
    public static float GetAxis(string buttonName, EGameState desiredGameStateInput) {
        if(!IsExistingInputName(buttonName))
            return 0;
        if(desiredGameStateInput == currentGameState)
            return Input.GetAxis(buttonName);
        return 0;
    }
    public static float GetRawAxis(string buttonName) {
        if(!IsExistingInputName(buttonName))
            return 0;
        return Input.GetAxisRaw(buttonName);
    }
    public static float GetRawAxis(string buttonName, EGameState desiredGameStateInput) {
        if(!IsExistingInputName(buttonName))
            return 0;
        if(desiredGameStateInput == currentGameState)
            return Input.GetAxisRaw(buttonName);
        return 0;
    }
    #endregion

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

    #region MoveMethods
    public static Vector3 GetMoveDirectionInput() {
        return new Vector3(GetRawAxis(MoveH), 0, GetRawAxis(MoveV));
    }
    public static Vector3 GetMoveDirectionInput(EGameState desiredGameStateInput) {
        if(desiredGameStateInput == currentGameState)
            return new Vector3(GetRawAxis(MoveH), 0, GetRawAxis(MoveV));
        return Vector3.zero;
    }
    public static Vector3 GetNormalizedMoveDirectionInput() {
        return new Vector3(GetRawAxis(MoveH), 0, GetRawAxis(MoveV)).normalized;
    }
    public static Vector3 GetNormalizedMoveDirectionInput(EGameState desiredGameStateInput) {
        if(desiredGameStateInput == currentGameState)
            return new Vector3(GetRawAxis(MoveH), 0, GetRawAxis(MoveV)).normalized;
        return Vector3.zero;
    }
    #endregion

    #region LookMethod
    public static float GetLookHRepeat(float currentMouseX) {
        currentMouseX += GetRawAxis(LookH) * MouseXSensitivity * Time.deltaTime;
        if(currentMouseX <= 0)
            currentMouseX += 360;
        else if(currentMouseX >= 360)
            currentMouseX -= 360;
        return currentMouseX;
    }
    public static float GetLookHRepeat(float currentMouseX, EGameState desiredGameStateInput) {
        if(desiredGameStateInput != currentGameState)
            return currentMouseX;
        currentMouseX += GetRawAxis(LookH) * MouseXSensitivity * Time.deltaTime;
        if(currentMouseX <= 0)
            currentMouseX += 360;
        else if(currentMouseX >= 360)
            currentMouseX -= 360;
        return currentMouseX;
    }
    public static float GetLookVRepeat(float currentMouseY) {
        return Mathf.Clamp(currentMouseY + GetRawAxis(LookV)  * MouseYSensitivity * Time.deltaTime, -90, 90);
    }
    public static float GetLookVRepeat(float currentMouseY, EGameState desiredGameStateInput) {
        if(desiredGameStateInput != currentGameState)
            return currentMouseY;
        return Mathf.Clamp(currentMouseY + GetRawAxis(LookV) * MouseYSensitivity * Time.deltaTime, -90, 90);
    }
    #endregion

    #region ErrorCheckMethods
    private static bool IsExistingInputName(string name) {
        if(!inputHash.Contains(name)) {
            Debug.LogError("Input source does not exist. Check Input for (" + name + ") to see if it does not exist, is spelt incorrectly, or if it has not been added to inputHash.");
            return false;
        }
        return true;
    }
    private static void InitalizeInputHash() {
        inputHash = new HashSet<string>() {
            moveH, moveV, lookH, lookV, fire, jump, use, confirm, cancel, pause
        };
    }
    #endregion
    #endregion
}
