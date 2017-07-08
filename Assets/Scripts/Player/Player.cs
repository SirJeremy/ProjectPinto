using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    #region Variables
    private float moveSpeed = 2f;
    private IndexVector currentLocation = IndexVector.Zero;
    private bool isMoving = false;
    #endregion

    #region MonoBehaviours
    private void OnEnable() {
        EventManager.OnSwipe += OnSwipe;
    }
    private void OnDisable() {
        EventManager.OnSwipe -= OnSwipe;
    }
    private void OnDestroy() {
        StopAllCoroutines();
    }
    #endregion

    #region Methods
    public static GameObject SpawnPlayer(IndexVector startingLocation) {
        GameObject player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), startingLocation.ToVector3, Quaternion.identity);
        return player;
    }
    public void Initialize(IndexVector startingLocation) {
        currentLocation = startingLocation;
    }
    private void OnSwipe(Swipe swipe) {
        if(isMoving)
            return;
        EDirection dir = swipe.GetEDirection;
        if(BoardManager.Instance.GameBoard.CanMoveInDirection(dir))             
            StartCoroutine(Move(currentLocation + IndexVector.GetDirection(dir), dir));
    }
    #endregion

    #region Coroutines
    private IEnumerator Move(IndexVector location, EDirection dir) {
        //Declartions
        Vector3 startPoint = currentLocation.ToVector3;
        Vector3 endPoint = location.ToVector3;
        float startTime = Time.time;
        //Start
        isMoving = true;
        BoardManager.Instance.GameBoard.StartMovePlayer(dir);
        //Update
        while((Time.time - startTime) * moveSpeed < 1) {
            transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) * moveSpeed); //dont have to worry about clamping because t cant go over 1 because while condition
            yield return null;
        }
        //Exit
        transform.position = endPoint;
        isMoving = false;
        currentLocation = location;
        BoardManager.Instance.GameBoard.FinishMovePlayer(dir);
        EventManager.AnnounceOnPlayerLocationChange(currentLocation);
        yield return null;
    }
    #endregion
}
