using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class InputManager {
    #region Varaibles
    private static HashSet<string> inputHash = null;
    private static EGameState currentGameState = EGameState.NONE; //changed by gameManager

    #region InputStrings
    private static string back = "Back";
    #endregion
    #endregion

    #region Properites
    public static string MoveH { get { return back; } }
    #endregion

    #region Constructor
    static InputManager() {
        InitalizeInputHash();
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
        if(!inputHash.Contains(name)) {
            Debug.LogError("Input source does not exist. Check Input for (" + name + ") to see if it does not exist, is spelt incorrectly, or if it has not been added to inputHash.");
            return false;
        }
        return true;
    }
    private static void InitalizeInputHash() {
        inputHash = new HashSet<string>() {
            back
        };
    }
    #endregion
    #endregion
}
