public static class EventManager {
    #region Delegates
    public delegate void Empty();
    public delegate void EnumEGameState(EGameState activeGameState);
    #endregion

    #region Events
    public static event EnumEGameState GameStateAnnouncement;
    #endregion

    #region EventCalls
    public static void AnnounceGameState(EGameState activeGameState) { if(GameStateAnnouncement != null) GameStateAnnouncement(activeGameState); }
    #endregion
}
