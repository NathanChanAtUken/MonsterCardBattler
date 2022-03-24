using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LinearMovement {
    public float movementSpeed, movementAngle, movementAcceleration;

    public LinearMovement(float moveSpd = 0f, float moveAng = 0f, float moveAcc = 0f) {
        movementSpeed = moveSpd;
        movementAngle = moveAng;
        movementAcceleration = moveAcc;
    }
}

public class MovementModule : MonoBehaviour {
    #region Fields
        [Header("Editor Injected References")]
        [SerializeField]
        private Rigidbody2D body;

        [Header("Instantiation Injected Parameters")]
        [SerializeField]
        private LinearMovement movementParameters;
        [SerializeField]
        private int collisionSteps;
        [SerializeField]
        private LayerMask defaultMask;

        [Header("Debugging Viewables")]
        [SerializeField]
        private Vector2 totalVelocity;

        public delegate void OnColDetected(RaycastHit2D colInfo, MovementModule colDetector);
        public event OnColDetected colDetected;
    #endregion

    #region Initialization Methods
    private void Awake() {
        
    }

    private void Start () {
		
	}

    public void InitializeValues(LinearMovement thisMovementParameters, LayerMask thisDefaultMask, int thisCollisionSteps = 10) {
        movementParameters = thisMovementParameters;
        defaultMask = thisDefaultMask;
        collisionSteps = thisCollisionSteps;
    }
    #endregion

    #region Cycle Methods
    private void Update () {
		
	}

    private void FixedUpdate() {
        
    }

    public void Move() {
        CalculateVelocity();
        ForecastCollisions();
        ApplyAcceleration();
    }

    private void CalculateVelocity() {
        float xVelocity = Mathf.Cos(movementParameters.movementAngle * Mathf.Deg2Rad) * movementParameters.movementSpeed * Time.deltaTime;
        float yVelocity = Mathf.Sin(movementParameters.movementAngle * Mathf.Deg2Rad) * movementParameters.movementSpeed * Time.deltaTime;
        totalVelocity = new Vector2(xVelocity, yVelocity);
    }

    private void ApplyAcceleration() {
        movementParameters.movementSpeed += movementParameters.movementAcceleration * Time.deltaTime;
    }

    private void ForecastCollisions() {
        float travelDistance = totalVelocity.magnitude;
        Vector2 stepVelocity = totalVelocity / collisionSteps;

        while (travelDistance > 0) {
            if (CheckCollision(stepVelocity, defaultMask)) {
                break;
            }
            else {
                body.position += stepVelocity;
                travelDistance -= stepVelocity.magnitude;
            }
        }
    }

    private bool CheckCollision(Vector2 stepVelocity, LayerMask layerMask) {
        Collider2D thisHitbox = GetComponent<Collider2D>();
        Collider2D collision = Physics2D.OverlapArea((Vector2)thisHitbox.bounds.min + stepVelocity, (Vector2)thisHitbox.bounds.max + stepVelocity, layerMask);

        if (collision != null && collision != thisHitbox) {
            RaycastHit2D collisionInfo = Physics2D.BoxCast(body.position, GetComponent<Collider2D>().bounds.size, 0, stepVelocity, stepVelocity.magnitude * 2, layerMask);

            if (collisionInfo && collisionInfo.collider != thisHitbox) {
                colDetected.Invoke(collisionInfo, this);
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    #endregion
}