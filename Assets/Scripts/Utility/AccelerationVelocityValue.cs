using UnityEngine;

[System.Serializable]
public class AccelerationVelocityValue {
    #region Vairables
    [SerializeField]
    private float acceleration = 1; //dont set this to negative, instead use decrement
    [SerializeField]
    private float velocity = 0;
    [SerializeField]
    private float minVelocity = 0;
    [SerializeField]
    private float maxVelocity = 2;
    [SerializeField]
    private float value = 0;
    [SerializeField]
    private float minValue = 0;
    [SerializeField]
    private float maxValue = 10;
    #endregion

    #region Properties
    public float Acceleration { get { return acceleration; } set { acceleration = value; } }
    public float Velocity { get { return velocity; } }
    public float MinVelocity { get { return minVelocity; } set { minVelocity = value; } }
    public float MaxVelocity { get { return maxVelocity; } set { maxVelocity = value; } }
    public float Value { get { return value; } }
    public float MinValue { get { return minValue; } set { minValue = value; } }
    public float MaxValue { get { return maxValue; } set { maxValue = value; } }
    public bool IsVelocityAtMin { get { return velocity <= minVelocity; } }
    public bool IsVelocityAtMax { get { return velocity >= maxVelocity; } }
    public bool IsValueAtMin { get { return value <= minValue; } }
    public bool IsValueAtMax { get { return value >= maxValue; } }
    #endregion

    #region Methods
    public float Increment() {
        return Increment(Time.deltaTime);
    }
    public float Increment(float deltaTime) {
        velocity = Mathf.Clamp(velocity + acceleration / deltaTime, minVelocity, maxVelocity);
        value = Mathf.Clamp(value + velocity, minValue, maxValue);
        return value;
    }
    public float Decrement() {
        return Decrement(Time.deltaTime);
    }
    public float Decrement(float deltaTime) {
        velocity = Mathf.Clamp(velocity - acceleration / deltaTime, minVelocity, maxVelocity);
        value = Mathf.Clamp(value + velocity, minValue, maxValue);
        return value;
    }
    public void ResetVelocityToMin() {
        velocity = minVelocity;
    }
    public void ResetVelocityToMax() {
        velocity = maxVelocity;
    }
    public void ResetValueToMin() {
        value = minValue;
    }
    public void ResetValueToMax() {
        value = maxValue;
    }
    public void ResetAllToMin() {
        ResetVelocityToMin();
        ResetValueToMin();
    }
    public void ResetAllToMax() {
        ResetVelocityToMax();
        ResetValueToMax();
    }
    #endregion
}
