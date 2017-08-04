using UnityEngine;
using UnityEngine.UI;

public class NotificationWindow : MonoSingleton<NotificationWindow> {
    #region Variables
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private Text[] buttonTexts;
    [SerializeField]
    private Text notificationText;
    private bool notificationMustBeAnswered = false; //if false, user may use escape button or red X to close notifcation, in addtion to the buttons
    private int notificationDefaultReturn = 0;

    public delegate void NotificationReturn(int value);
    private NotificationReturn returnMethod; //have no idea what to name this

    private static bool isShowingNotification = false;
    #endregion

    #region Properties
    public static bool IsShowingNotification { get { return isShowingNotification; } private set { isShowingNotification = value; InputManager.IsShowingNotification = value; } }
    #endregion

    #region MonoBehviours
    protected override void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance == this)
            Destroy(gameObject);
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        EventManager.OnCancelInput += OnAttemptClose;
    }
    private void OnDisable() {
        EventManager.OnCancelInput -= OnAttemptClose;
    }
    #endregion

    #region Methods
    public void ShowNotification(string notification, string[] buttonLabels, int defaultReturn, NotificationReturn callbackMethod, bool mustBeAnswered = false) {
        //if already showing notification, return default (even if must be answered)
        if(IsShowingNotification) 
            SendCallback(notificationDefaultReturn);
        //check if null
        if(buttonLabels == null) {
            Debug.LogError("buttonLabels is null");
            gameObject.SetActive(false);
            return;
        }
        //
        gameObject.SetActive(true); //enable the notification UI
        notificationMustBeAnswered = mustBeAnswered; 
        notificationDefaultReturn = defaultReturn;
        IsShowingNotification = true;
        returnMethod = callbackMethod; //set the callback method
        //Error check
        if(buttonLabels.Length > 5)
            Debug.LogWarning("Notification contains " + buttonLabels.Length + " options when the max is 5. Clamping to first 5 options.");
        if(defaultReturn < 0 || defaultReturn > 4 || defaultReturn >= buttonLabels.Length) {
            //defaultReturn is > 0, or < number of possible options, clamp it 
            Debug.LogError("DefaultReturn (" + defaultReturn + ") does not fit in [0, options).");
            defaultReturn = Mathf.Clamp(defaultReturn, 0, Mathf.Min(buttonLabels.Length - 1, 4));
        }
        //Set notification
        notificationText.text = notification;
        //Set buttons
        if(buttonLabels.Length >= 5) /* If all buttons are used */ {
            for(int i = 0; i < 5; i++) {
                buttons[i].SetActive(true);
                buttonTexts[i].text = buttonLabels[i];  
            }
        }
        else {
            int j = Mathf.Min(buttonLabels.Length, 5);
            for(int i = 0; i < 5; i++) {
                if(i < j) /* if i is less then the number of buttons to show */ {
                    buttons[i].SetActive(true);
                    buttonTexts[i].text = buttonLabels[i];
                }
                else {
                    buttons[i].SetActive(false);
                    buttonTexts[i].text = "I hate bugs! (Taxidermy)"; //default text
                }
            }
        }
    }
    public void OnButtonPress(int value) {
        SendCallback(value);
    }
    public void OnAttemptClose() {
        if(!notificationMustBeAnswered)
            SendCallback(notificationDefaultReturn);
    }
    private void SendCallback(int value) {
        if(returnMethod != null) {
            returnMethod(value);
            returnMethod = null;
            IsShowingNotification = false;
            gameObject.SetActive(false);
        }
    }
    #endregion
}
