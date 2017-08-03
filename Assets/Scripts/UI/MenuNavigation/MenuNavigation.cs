using UnityEngine;
public class MenuNavigation : MonoBehaviour {
    [SerializeField]
    private bool isActiveMenu = false;
    public bool IsActiveMenu { get { return isActiveMenu; } protected set { isActiveMenu = value; } }

    protected virtual void Awake() {
        gameObject.SetActive(IsActiveMenu);
    }
    protected virtual void OnEnable() {
        EventManager.OnCancelInput += OnCancelInput;
    }
    protected virtual void OnDisable() {
        EventManager.OnCancelInput -= OnCancelInput;
    }

    public virtual void NavigateTo() {
        isActiveMenu = true;
        gameObject.SetActive(true);
    }
    protected virtual void NavigateAway() {
        isActiveMenu = false;
        gameObject.SetActive(false);
    }
    protected virtual void OnCancelInput() {
        if(!NotificationWindow.IsShowingNotification)
            NavigateAway();
    }
}
