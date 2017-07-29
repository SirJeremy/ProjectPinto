using UnityEngine;
using UnityEngine.UI;

public class NotificationWindow : MonoSingleton<NotificationWindow> {
    #region Variables
    #endregion

    #region MonoBehviours
    protected override void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        gameObject.SetActive(false);
    }
    #endregion

    #region Methods
    #endregion
}
