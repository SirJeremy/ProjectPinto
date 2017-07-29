using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    #region Variables
    private float moveSpeed = 2f;
    private IndexVector currentLocation = IndexVector.Zero;
    private bool isMoving = false;
    private bool canMove = true;
    #endregion

    #region MonoBehaviours
    private void OnEnable() {
        EventManager.OnMoveInput += OnMoveInput;
        EventManager.OnGoalReached += OnGoalReached;
    }
    private void OnDisable() {
        EventManager.OnMoveInput -= OnMoveInput;
        EventManager.OnGoalReached -= OnGoalReached;
    }
    private void OnDestroy() {
        StopAllCoroutines();
    }
    #endregion

    #region Methods
    public static GameObject SpawnPlayer(IndexVector startingLocation) {
        GameObject player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), startingLocation.ToVector3, Quaternion.identity);
        player.GetComponent<Player>().Initialize(startingLocation);
        return player;
    }
    public void Initialize(IndexVector startingLocation) {
        currentLocation = startingLocation;
    }
    #endregion

    #region EventCatchers
    private void OnGoalReached() {
        canMove = false;
    }
    private void OnMoveInput(EDirection direction) {
        if(isMoving || !canMove)
            return;
        if(BoardManager.Instance.GameBoard.CanMoveInDirection(direction))
            StartCoroutine(Move(direction));
    }
    #endregion

    #region Coroutines
    private IEnumerator Move(EDirection dir) {
        //Declartions
        Vector3 startPoint = currentLocation.ToVector3;
        Vector3 endPoint = currentLocation.ToVector3 + IndexVector.GetDirection(dir).ToVector3;
        float startTime = Time.time;

        //Start
        isMoving = true;

        //move until obsticle is met
        while(true) { 
            //Start of move cycle
            BoardManager.Instance.GameBoard.StartMovePlayer(dir);
            startPoint = currentLocation.ToVector3;
            endPoint = currentLocation.ToVector3 + IndexVector.GetDirection(dir).ToVector3;
            startTime = Time.time;

            //Update of move cycle
            while((Time.time - startTime) * moveSpeed < 1) {
                transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) * moveSpeed); //dont have to worry about clamping because t cant go over 1 because while condition
                yield return null;
            }

            //Exit of loop cycle
            transform.position = endPoint;
            currentLocation += IndexVector.GetDirection(dir);
            BoardManager.Instance.GameBoard.FinishMovePlayer(dir);
            EventManager.AnnounceOnPlayerLocationChange(currentLocation);

            yield return null;
            //next frame check to see if can move again
            if(!BoardManager.Instance.GameBoard.CanMoveInDirection(dir))
                break;
        }
        
        //Exit
        isMoving = false;
        EventManager.AnnounceOnPlayerStopLocation(currentLocation);        
        
        yield return null;
    }
    #endregion
}
