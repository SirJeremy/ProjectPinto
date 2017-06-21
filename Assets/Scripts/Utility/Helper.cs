using UnityEngine;
using System.Collections;

public static class Helper {
    public static Vector3 GetFlattenedVector(Vector3 vector) {
        vector.y = 0;
        return vector;
    }
    public static Vector3 GetFlattenedNormalizedVector(Vector3 vector) {
        vector.y = 0;
        return vector.normalized;
    }
}
