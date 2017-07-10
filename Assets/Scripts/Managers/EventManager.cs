public static class EventManager {
    #region Delegates
    public delegate void Empty();
    public delegate void IndexVectorBool(IndexVector location, bool isTraversable);
    public delegate void DSwipe(Swipe swipe);
    public delegate void DIndexVector(IndexVector location);
    public delegate void DEColor(EColor color); 
    #endregion

    #region Events
    public static event IndexVectorBool OnTraversabilityChange; //when a game tile want to change its isTraversability
    public static event DSwipe OnSwipe; //when a swipe ovccurs
    public static event DIndexVector OnPlayerLocationChange; //when the player moves to a new space
    public static event DIndexVector OnPlayerStopLocation; //when the player stops on a space (no longer moving)
    public static event Empty OnGoalReached; //when the player reaches the goal
    public static event DEColor OnButtonDown; //when a button is pressed
    #endregion

    #region EventCalls
    public static void AnnounceOnTraversabilityChange(IndexVector location, bool isTraversable) { if(OnTraversabilityChange != null) OnTraversabilityChange(location, isTraversable); }
    public static void AnnounceOnSwipe(Swipe swipe) { if(OnSwipe != null) OnSwipe(swipe); }
    public static void AnnounceOnPlayerLocationChange(IndexVector location) { if(OnPlayerLocationChange != null) OnPlayerLocationChange(location); }
    public static void AnnounceOnPlayerStopLocation(IndexVector location) { if(OnPlayerStopLocation != null) OnPlayerStopLocation(location); }
    public static void AnnounceOnGoalReached() { if(OnGoalReached != null) OnGoalReached(); }
    public static void AnnounceOnButtonDown(EColor colorChannel) { if(OnButtonDown != null) OnButtonDown(colorChannel); }
    #endregion
}
