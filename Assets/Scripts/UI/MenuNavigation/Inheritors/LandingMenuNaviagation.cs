using UnityEngine;

public class LandingMenuNaviagation : MenuNavigation {
    [SerializeField]
    private MenuNavigation levelMenu;

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
