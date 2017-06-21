using UnityEngine;
using System.Collections;

public class CameraFollowParent : MonoBehaviour {
    private Transform followTarget;

    private void Start() {
        if(transform.parent == null)
            Debug.LogError("No parent for " + gameObject.name);
        followTarget = transform.parent;
        transform.parent = null;
    }
    private void LateUpdate() {
        transform.position = followTarget.position;
        transform.rotation = followTarget.rotation;
    }
}
