using UnityEngine;

public class BackButton : MonoBehaviour {
    public void OnBackButtonClick() {
        EventManager.AnnounceOnCancelInput();
    }
}
