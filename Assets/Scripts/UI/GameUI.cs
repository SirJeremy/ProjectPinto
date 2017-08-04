using UnityEngine;

public class GameUI : MonoBehaviour {
    private void Awake() {
        EventManager.OnGameEnter += OnGameEnter;
        EventManager.OnGameExit += OnGameExit;
    }
    private void Start() {
        gameObject.SetActive(false);
    }

    private void OnGameEnter() {
        gameObject.SetActive(true);
    }
    private void OnGameExit() {
        gameObject.SetActive(false);
    }
}
