public static class EventManager {
    #region Delegates
    public delegate void Empty();
    public delegate void IndexVectorBool(IndexVector location, bool isTraversable);
    public delegate void DSwipe(Swipe swipe);
    public delegate void DIndexVector(IndexVector location);
    #endregion

    #region Events
    public static event IndexVectorBool OnTraversabilityChange;
    public static event DSwipe OnSwipe;
    public static event DIndexVector OnPlayerLocationChange;
    #endregion

    #region EventCalls
    public static void AnnounceOnTraversabilityChange(IndexVector location, bool isTraversable) { if(OnTraversabilityChange != null) OnTraversabilityChange(location, isTraversable); }
    public static void AnnounceOnSwipe(Swipe swipe) { if(OnSwipe != null) OnSwipe(swipe); }
    public static void AnnounceOnPlayerLocationChange(IndexVector location) { if(OnPlayerLocationChange != null) OnPlayerLocationChange(location); }
    #endregion
}
