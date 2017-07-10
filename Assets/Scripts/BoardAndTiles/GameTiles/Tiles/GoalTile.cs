using UnityEngine;

public class GoalTile : GameTile {
    private void OnEnable() {
        EventManager.OnPlayerStopLocation += OnPlayerStopLocation;
    }
    private void OnDisable() {
        EventManager.OnPlayerStopLocation -= OnPlayerStopLocation;
    }

    private void OnPlayerStopLocation(IndexVector location) {
        //Location is from GameTile
        if(Location == location)
            GoalReached();
    }
    private void GoalReached() {
        EventManager.AnnounceOnGoalReached();
    }
}
