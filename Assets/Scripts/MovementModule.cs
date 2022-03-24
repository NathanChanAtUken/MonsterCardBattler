using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LinearMovement {
    public float movementSpeed, movementAngle, movementAcceleration;

    public LinearMovement(float moveSpd = 0f, float moveAng = 0f, float moveAcc = 0f) {
        movementSpeed = moveSpd;
        movementAngle = moveAng;
        movementAcceleration = moveAcc;
    }
}

public struct ArcMovement {
    // Field Declarations
    public float arcRadius, angularSpeed, arcAngle, angularAcceleration, radialSpeed, radialAcceleration;
    public int arcDirection, radialDirection;

    public const float ArcLinAngDiff = 90f;

    // Constructors
    public ArcMovement(float arcRad, float angSpd, int arcDir, float arcAng = 0f, float angAcc = 0f, float radSpd = 0f, float radAcc = 0f, int radDir = 0) {
        arcRadius = arcRad;
        angularSpeed = angSpd;
        arcDirection = arcDir;
        arcAngle = arcAng;
        angularAcceleration = angAcc;
        radialSpeed = radSpd;
        radialAcceleration = radAcc;
        radialDirection = radDir;
    }

    // Accessors
    public float arcVelocity() {
        return angularSpeed * arcDirection;
    }

    public float radialVelocity() {
        return radialSpeed * radialDirection;
    }
}

public class MovementModule : MonoBehaviour {
    // Field Declarations

    // References
    [SerializeField]
    public Rigidbody2D body;

    // Input Fields
    public LinearMovement linearMovement;
    public ArcMovement arcMovement;

    // Exposed for Debugging
    [SerializeField]
    public Vector2 linearVelocity, arcVelocity, totalVelocity;
    public LayerMask defaultMask;
    [SerializeField]
    public int stutterSteps = 10;

    // Event Fields
    public delegate void OnColDetected(RaycastHit2D colInfo, MovementModule colDetector);
    public event OnColDetected colDetected;

    // Set Up Methods

    private void Awake() {
        
    }

    private void Start () {
		
	}
	
    public void InitializeValues(LinearMovement linMove, LayerMask mask) {
        linearMovement = linMove;
        arcMovement = new ArcMovement();
        defaultMask = mask;
    }

    public void InitializeValues(ArcMovement arcMove, LayerMask mask) {
        linearMovement = new LinearMovement();
        arcMovement = arcMove;
        defaultMask = mask;
    }

    public void InitializeValues(LinearMovement linMove, ArcMovement arcMove, LayerMask mask) {
        linearMovement = linMove;
        arcMovement = arcMove;
        defaultMask = mask;
    }

    // Update Methods

    private void Update () {
		
	}

    private void FixedUpdate() {
        
    }

    public void Move() {
        // Calcuate Linear Movement
        float linXVel = Mathf.Cos(linearMovement.movementAngle * Mathf.Deg2Rad) * linearMovement.movementSpeed * Time.deltaTime;
        float linYVel = Mathf.Sin(linearMovement.movementAngle * Mathf.Deg2Rad) * linearMovement.movementSpeed * Time.deltaTime;
        linearVelocity = new Vector2(linXVel, linYVel);

        // Calculate Arc Velocity
        arcMovement.arcAngle += arcMovement.arcVelocity() * Time.deltaTime;
        arcMovement.arcRadius += arcMovement.radialVelocity() * Time.deltaTime;

        float arcXVel = Mathf.Cos((arcMovement.arcAngle + ArcMovement.ArcLinAngDiff * arcMovement.arcDirection) * Mathf.Deg2Rad) * arcMovement.angularSpeed * Mathf.Deg2Rad * arcMovement.arcRadius * Time.deltaTime;
        float arcYVel = Mathf.Sin((arcMovement.arcAngle + ArcMovement.ArcLinAngDiff * arcMovement.arcDirection) * Mathf.Deg2Rad) * arcMovement.angularSpeed * Mathf.Deg2Rad * arcMovement.arcRadius * Time.deltaTime;
        arcVelocity = new Vector2(arcXVel, arcYVel);

        // Forecast Collisions
        totalVelocity = linearVelocity + arcVelocity;

        ForecastCollisions(totalVelocity, defaultMask, stutterSteps);

        // Acceleration
        Acceleration();
    }

    private void Acceleration() {
        linearMovement.movementSpeed += linearMovement.movementAcceleration * Time.deltaTime;
        arcMovement.angularSpeed += arcMovement.angularAcceleration * Time.deltaTime;
        arcMovement.radialSpeed += arcMovement.radialAcceleration * Time.deltaTime;
    }

    private void ForecastCollisions(Vector2 velocity, LayerMask mask, float numSteps) {
        float travelDistance = velocity.magnitude;
        Vector2 stepVelocity = velocity / numSteps;

        while (travelDistance > 0) {
            if (CheckCollision(stepVelocity, mask)) {
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

    public void PlaneReflection(RaycastHit2D collision) {
        // Obtain surface normal
        Vector2 surfaceNorm = collision.normal;

        // Apply reflection off of plane surface
        Vector2 linReflection = Vector2.Reflect(linearVelocity, surfaceNorm);
        linearMovement.movementAngle = Mathf.Atan2(linReflection.y, linReflection.x) * Mathf.Rad2Deg;

        arcMovement.arcDirection = -arcMovement.arcDirection;
        Vector2 arcReflection = Vector2.Reflect(arcVelocity, surfaceNorm);
        arcMovement.arcAngle = Mathf.Atan2(arcReflection.y, arcReflection.x) * Mathf.Rad2Deg - ArcMovement.ArcLinAngDiff * arcMovement.arcDirection;
    }
}
