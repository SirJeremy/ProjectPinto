using UnityEngine;

public class PauseButton : MonoBehaviour {
    [SerializeField]
    private MenuNavigation pauseMenu;

    private void OnEnable() {
        EventManager.OnCancelInput += OnCancelInput;
    }
    private void OnDisable() {
        EventManager.OnCancelInput -= OnCancelInput;
    }

    private void ShowPauseMenu() {
        pauseMenu.NavigateTo();
    }
    private void OnCancelInput() {
        ShowPauseMenu();
    }
    public void OnPauseButtonClick() {
        ShowPauseMenu();
    }
}
