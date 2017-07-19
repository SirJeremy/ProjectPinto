using UnityEngine;

public class CameraFocus : MonoSingleton<CameraFocus> {
    [SerializeField] [Range(.01f, .5f)] [Tooltip("Buffer created on bottom of screen to allow for ui buttons")]
    private float bottomScreenBuffer = .2f; //percentage 0-1
    [SerializeField] [Tooltip("Extra width on each side of the board")]
    private float borderWidthSpace = .5f; //is multiplyed by 2 in code to get this amount on each side
    [SerializeField] [Tooltip("Extra height on each side of the board")]
    private float borderHeightSpace = .5f; //is multiplyed by 2 in code to get this amount on each side

    private Camera cam;
    private Camera Cam { get { if(cam == null) cam = GetComponent<Camera>(); return cam; } }

    public void FocusCamera(int boardWidth, int boardHeight, bool bufferBottomScreen = false) {
        float bufferHeight = 0;
        Vector3 focus;
        float h;
        float w;
        if(bufferBottomScreen)
            bufferHeight = (boardHeight / (1 - bottomScreenBuffer)) * bottomScreenBuffer;
        focus = new Vector3(boardWidth / 2 - .5f, .5f, (boardHeight - bufferHeight) / 2 - .5f);
        //calculate distance from a frustrum cross section with height and width of the board as the height then see which is bigger and use that distance
        //basically fith the board in the view with both the width and height fitting into view
        h = (boardHeight + borderHeightSpace * 2 + bufferHeight) * .5f / Mathf.Tan(Cam.fieldOfView * .5f * Mathf.Deg2Rad);
        w = (((boardWidth + borderWidthSpace * 2) / Cam.aspect) + bufferHeight) * .5f / Mathf.Tan(Cam.fieldOfView * .5f * Mathf.Deg2Rad);
        if(h >= w)
            transform.position = focus + Vector3.up * h;
        else
            transform.position = focus + Vector3.up * w;
    }
}
