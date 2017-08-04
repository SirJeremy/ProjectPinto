using UnityEngine;
public static class EventManager {
    #region Delegates
    public delegate void Empty();
    public delegate void IndexVectorBool(IndexVector location, bool isTraversable);
    public delegate void DEDirection(EDirection direction);
    public delegate void DIndexVector(IndexVector location);
    public delegate void DEColor(EColor color);
    public delegate void EColorArr(EColor[] colors);
    #endregion

    #region Events
    public static event IndexVectorBool OnTraversabilityChange; //when a game tile want to change its isTraversability
    public static event DEDirection OnMoveInput; //when move input occurs
    public static event DIndexVector OnPlayerLocationChange; //when the player moves to a new space
    public static event DIndexVector OnPlayerStopLocation; //when the player stops on a space (no longer moving)
    public static event Empty OnGoalReached; //when the player reaches the goal
    public static event Empty OnCancelInput; //when the back button is pressed
    public static event Empty OnGameEnter; //when the player goes from menu to game
    public static event Empty OnGameExit;  //when the player goes from game to menu
    public static event DEColor OnButtonDown; //when a button is pressed
    public static event EColorArr OnUIButtonControllerInitialize; //used to tell UI button controller to initialize 
    #endregion

    #region EventCalls
    public static void AnnounceOnTraversabilityChange(IndexVector location, bool isTraversable) { if(OnTraversabilityChange != null) OnTraversabilityChange(location, isTraversable); }
    public static void AnnounceOnMoveInput(EDirection direction) { if(OnMoveInput != null) OnMoveInput(direction); }
    public static void AnnounceOnPlayerLocationChange(IndexVector location) { if(OnPlayerLocationChange != null) OnPlayerLocationChange(location); }
    public static void AnnounceOnPlayerStopLocation(IndexVector location) { if(OnPlayerStopLocation != null) OnPlayerStopLocation(location); }
    public static void AnnounceOnGoalReached() { if(OnGoalReached != null) OnGoalReached(); }
    public static void AnnounceOnButtonDown(EColor colorChannel) { if(OnButtonDown != null) OnButtonDown(colorChannel); }
    public static void AnnounceOnUIButtonControllerInitialize(EColor[] colors) { if(OnUIButtonControllerInitialize != null) OnUIButtonControllerInitialize(colors); }
    public static void AnnounceOnCancelInput() { if(OnCancelInput != null && Input.backButtonLeavesApp == false) { OnCancelInput(); } }
    public static void AnnounceOnGameEnter() { if(OnGameEnter != null) { OnGameEnter(); } }
    public static void AnnounceOnGameExit() { if(OnGameExit != null) { OnGameExit(); } }
    #endregion
}
