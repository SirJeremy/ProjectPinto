using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : MonoBehaviour
{
    #region Variables
    [Header("Base Prjectile Variables")]
    #region GeneralVariables
    //speed, damage, destroyTimer, and myTeam should be initialized by whatever fired it
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float damage = 0;
    [SerializeField]
    private float destroyTime = 0;
    [SerializeField]
    private bool actionOnDestroy = false;
    private bool performingOnDestroyAction = false;
    #endregion GeneralVaribles

    #region RaycastVariables
    [Header("Raycast Varaibles")]
    [SerializeField]
    private bool sphereCast = false;
    [SerializeField]
    private float sphereCastRadius = 1;
    private RaycastHit[] hits;
    private Quaternion targetNormal; //for particles once we get there
    private Rigidbody rb;
    private Vector3 lastPos = Vector3.zero;
    private Vector3 directionMoved = Vector3.zero;
    private float deltaMove = 0.0f;
    #endregion RaycastVariables

    #region DebugVariables
    [Header("Debug Variables")]
    [SerializeField]
    private bool drawGizmos = false;
    [SerializeField]
    private Color gizmoColor = Color.white;
    #endregion
    #endregion Variables

    #region MonoBehaviours
    private void Start() {
        InitializeVariables();
    }
    private void Update() {
        Opereate();
    }
    void OnDrawGizmos()
    {
        if(!drawGizmos)
            return;
        Gizmos.color = gizmoColor;
        if(sphereCast)
        {
            Gizmos.DrawSphere(lastPos, sphereCastRadius);
            Gizmos.DrawSphere(lastPos + directionMoved * -deltaMove, sphereCastRadius);
        }
        Gizmos.DrawRay(lastPos, -directionMoved * deltaMove);

    }
    #endregion

    #region Methods
    public virtual void InitializeVariables() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        lastPos = transform.position;
        destroyTime += Time.time;
    }
    protected virtual void Opereate() {
        if(!IsTimeToDestroy() && !performingOnDestroyAction) {
            RaycastPos();
            CollisionCheck();
            StoreLastPosition();
        }
        else
            OnDestroyAction();
    }
    protected bool IsTimeToDestroy() {
        if(Time.time >= destroyTime)
            return true;
        else
            return false;
    }
    protected void RaycastPos() { //Raycasting from previous position to current position, excludes player layer
        directionMoved = transform.position - lastPos;
        deltaMove = directionMoved.magnitude;
        directionMoved = directionMoved.normalized;
        //Raycast and then order hits by closesest 
        if(sphereCast)
            hits = Physics.SphereCastAll(lastPos, sphereCastRadius, directionMoved, deltaMove).OrderBy(h => h.distance).ToArray();
        else
            hits = Physics.RaycastAll(lastPos, directionMoved, deltaMove).OrderBy(h => h.distance).ToArray();
    }
    protected virtual void CollisionCheck() {
        if(hits == null)
            return;

        IDamageable tmpIDam;
        foreach(RaycastHit hit in hits)
        {
            tmpIDam = hit.collider.gameObject.GetComponent<IDamageable>();
            if(tmpIDam != null) {
                tmpIDam.TakeDamage(damage);
            }
            Collision();
        }
    }
    protected void StoreLastPosition() {
        lastPos = transform.position;
    }
    protected virtual void Collision() {
        if(actionOnDestroy) {
            performingOnDestroyAction = true;
            OnDestroyAction();
        }
        else
            Destroy(gameObject);
    }
    protected virtual void OnDestroyAction() {
        Destroy(gameObject);
    }
    #endregion 
}
