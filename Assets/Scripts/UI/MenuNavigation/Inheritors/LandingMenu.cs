using UnityEngine;

public class LandingMenu : MenuNavigation {
    [SerializeField]
    private MenuNavigation levelMenu;

    public override void NavigateTo() {
        base.NavigateTo();
        InputManager.BackButtonLeavesApp = true;
    }

    public void OnOtherButtonPress() {
        Debug.Log("Placeholder");
    }
    public void OnPlayButtonPress() {
        levelMenu.NavigateTo();
        NavigateAway();
    }
    public void OnOptionsButtonPress() {
        Debug.Log("Options button pressed");
    }
}
