using UnityEngine;
using System.Collections;

[System.Serializable]
public class CurveProgressValue {
    #region Variables
    [SerializeField] [Range(0, 1)]
    private float progress = 0;
    [SerializeField]
    private float progressSpeed = .2f;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float valueMultiplyer = 1;
    #endregion

    #region Properties
    public float Progress {
        get { return progress; }
        set {
            if(value < 0)
                progress = 0;
            else if(value > 1)
                progress = 1;
            else
                progress = value;
        }
    }
    public float ProgressSpeed {
        get { return progressSpeed; }
        set { progressSpeed = value; }
    }
    public float ValueMultiplyer {
        get { return valueMultiplyer; }
        set { valueMultiplyer = value; }
    }
    public AnimationCurve Curve {
        get { return curve; }
    }
    public float RawValue {
        get { return curve.Evaluate(progress); }
    }
    public float Value {
        get { return curve.Evaluate(progress) * valueMultiplyer; }
    }
    public bool IsMinProgress {
        get { return progress == 0; }
    }
    public bool IsMaxProgress {
        get { return progress == 1; }
    }
    #endregion

    #region Constructors
    public CurveProgressValue() { }
    public CurveProgressValue(float progressSpeed, float valueMultiplyer) {
        this.progressSpeed = progressSpeed;
        this.valueMultiplyer = valueMultiplyer;
    }
    #endregion

    #region Methods
    public void IncrementProgress() {
        Progress += Time.deltaTime * progressSpeed;
    }
    public void IncrementProgress(float deltaTime) {
        Progress += Time.deltaTime * progressSpeed;
    }
    public void DecrementProgress() {
        Progress -= Time.deltaTime * progressSpeed;
    }
    public void DecrementProgress(float deltaTime) {
        Progress -= Time.deltaTime * progressSpeed;
    }
    public float GetValueAndIncrementProgress() {
        IncrementProgress();
        return Value;
    }
    public float GetValueAndIncrementProgress(float deltaTime) {
        IncrementProgress(deltaTime);
        return Value;
    }
    public float GetValueAndDecrementProgress() {
        DecrementProgress();
        return Value;
    }
    public float GetValueAndDecrementProgress(float deltaTime) {
        DecrementProgress(deltaTime);
        return Value;
    }
    public void SetToMin() {
        progress = 0;
    }
    public void SetToMax() {
        progress = 1;
    }
    #endregion
}
