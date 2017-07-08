using UnityEngine;
public struct Swipe {
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 swipeDirection;
    private float swipeAngle; //0 deg == right

    public Vector2 Start { get { return startPosition; } }
    public Vector2 End { get { return endPosition; } }
    public Vector2 Direction { get { return swipeDirection; } }
    public float Angle { get { return swipeAngle; } }
    public EDirection GetEDirection {
        get {
            float r2o2 = Mathf.Sqrt(2) / 2; //45 deg, or pi/4
            if(swipeDirection.y >= r2o2)
                return EDirection.UP;
            if(swipeDirection.y <= -r2o2)
                return EDirection.DOWN;
            if(swipeDirection.x > r2o2)
                return EDirection.RIGHT;
            return EDirection.LEFT;
        }
    }

    public Swipe(Vector2 startPosition, Vector2 endPosition) {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.swipeDirection = (endPosition - startPosition).normalized;
        if(swipeDirection.y >= 0) {
            swipeAngle = Vector2.Angle(Vector2.right, swipeDirection);
        }
        else
            swipeAngle = 360 - Vector2.Angle(Vector2.right, swipeDirection);
    }

    public override string ToString() {
        return "Start: " + startPosition + " End: " + endPosition + " Direction: " + swipeDirection + " Angle: " + swipeAngle;
    }

}