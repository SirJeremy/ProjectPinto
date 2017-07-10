using UnityEngine;

public class GoalTile : GameTile {
    private void OnEnable() {
        EventManager.OnPlayerLocationChange += OnPlayerLocationChange;
    }
    private void OnDisable() {
        EventManager.OnPlayerLocationChange -= OnPlayerLocationChange;
    }

    private void OnPlayerLocationChange(IndexVector location) {
        if(Location == location)
            GoalReached();
    }
    private void GoalReached() {
        EventManager.AnnounceOnGoalReached();
    }
}
