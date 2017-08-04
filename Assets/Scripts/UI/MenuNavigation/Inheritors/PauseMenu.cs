using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuNavigation {
    public void OnBackgroundClick() {
        NavigateAway();
    }
    public void OnResumeButtonClick() {
        NavigateAway();
    }
    public void OnRestartButtonClick() {
        ShowRestartConfirmation();
    }
    public void OnLevelSelectButtonClick() {
        ShowLevelSelectConfirmation();
    }
    public void OnOptionButtonClick() {
        Debug.Log("Options");
    }

    private void ShowRestartConfirmation() {
        string notification = "Are you sure you want to restart the level?";
        string[] option = new string[] {
            "Yes",
            "No"
        };
        int defaultReturn = 1;
        NotificationWindow.Instance.ShowNotification(notification, option, defaultReturn, OnRestartConfirmCallback);
    }
    private void ShowLevelSelectConfirmation() {
        string notification = "Are you sure you want to exit to level select?";
        string[] option = new string[] {
            "Yes",
            "No"
        };
        int defaultReturn = 1;
        NotificationWindow.Instance.ShowNotification(notification, option, defaultReturn, OnLevelSelectConfirmCallback);
    }
    private void OnRestartConfirmCallback(int value) {
        switch(value) {
            case 0:
                BoardManager.Instance.RestartLevel();
                NavigateAway();
                break;
            case 1:
                break;
            default:
                Debug.LogError("Invalid case");
                break;
        }
    }
    private void OnLevelSelectConfirmCallback(int value) {
        switch(value) {
            case 0:
                BoardManager.Instance.DestroyLevel();
                EventManager.AnnounceOnGameExit();
                NavigateAway();
                break;
            case 1:
                break;
            default:
                Debug.LogError("Invalid case");
                break;
        }
    }
}
