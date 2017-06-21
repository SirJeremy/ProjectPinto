using UnityEngine;
using System.Collections;

public class TestActivatable : MonoBehaviour, IActivatable {
    private Renderer rend;
    public void ActivateAction(int actionID) {
        if(actionID == 0)
            ChangeColor();
    }
    private void ChangeColor() {
        if(rend == null)
            rend = GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
