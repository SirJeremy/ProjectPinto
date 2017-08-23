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

    #region Flags
    public static bool HasFlag(ETraversableDirection flagsToCheck, ETraversableDirection flagsCheckingFor) {
        if(flagsCheckingFor == 0) {
            return flagsToCheck == 0;
        }
        return ((flagsToCheck & flagsCheckingFor) != 0);
    }
    public static ETraversableDirection SetFlag(ETraversableDirection flagsToSet, ETraversableDirection flagTuningOn) {
        return flagsToSet | flagTuningOn;
    }
    public static ETraversableDirection UnsetFlag(ETraversableDirection flagsToUnset, ETraversableDirection flagTuningOff) {
        return flagsToUnset & ~flagTuningOff;
    }
    #endregion
}
