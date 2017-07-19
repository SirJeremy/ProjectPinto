using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour {
    [System.Serializable]
    private class ButtonSwitch {
        public GameObject gameObject = null;
        public Image image = null;
        public EColor currentColor = EColor.DEFAULT;

        public void SetActive(bool isActive) {
            if(gameObject != null)
                gameObject.SetActive(isActive);
        }
        public void SetColor(ColorPair color) {
            if(image != null)
                image.color = color.color;
            currentColor = color.eColor;
        }
        public void AnnounceOnButtonDown() {
            EventManager.AnnounceOnButtonDown(currentColor);
        }
    }
    [System.Serializable]
    private class ColorPair {
        public EColor eColor = EColor.DEFAULT;
        public Color color = Color.white;
    }
    [SerializeField]
    private ButtonSwitch[] buttons = new ButtonSwitch[3];
    [SerializeField]
    private ColorPair[] colorPairs = new ColorPair[3];

    public void OnButtonPress(int buttonID) {
        buttonID = Mathf.Clamp(buttonID, 0, buttons.Length - 1);
        buttons[buttonID].AnnounceOnButtonDown();
    }
    public void OnUIButtonControllerInitialize(int numberOfButtons, EColor[] colors) {
        int b = Mathf.Clamp(numberOfButtons, 0, buttons.Length);
        //if 0, turn off all buttons
        if(b == 0) {
            foreach(ButtonSwitch bs in buttons) {
                bs.SetActive(false);
            }
        }
        else {
            for(int i = 0; i < buttons.Length; i++) {
                //if i < number of buttons, trun on and initialized button i
                if(i < b) {
                    buttons[i].SetActive(true);
                    //find the color for button and set it
                    for(int j = 0; j < colorPairs.Length; j++) {
                        if(colorPairs[j].eColor == colors[i]) {
                            buttons[i].SetColor(colorPairs[j]);
                            break;
                        }
                    }
                }
                //else turn off the button
                else
                    buttons[i].SetActive(false);
            }
        }
    }
}
