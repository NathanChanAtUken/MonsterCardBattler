using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPhysics : MonoBehaviour, IClickable {
    #region Fields
        [Header("Injected References")]
        [SerializeField]
        private GameObject cardObject;
        public GameObject CardObject {
            get { return cardObject; }
            set { cardObject = value; }
        }
        
        [SerializeField]
        private MovementModule movementModule;
    #endregion

    #region Initialization Methods
    private void Awake() {
        ImplicitInit();
    }

    private void ImplicitInit() {
        LinearMovement movementParameters = new LinearMovement();
        LayerMask defaultCollisionMask = LayerMask.GetMask();
        movementModule.InitializeValues(movementParameters, defaultCollisionMask);
        movementModule.colDetected += OnColDetected;
    }

    private void Start() {
        
    }
    #endregion

    #region Cycle Methods
    private void Update() {
        
    }

    private void FixedUpdate() {
        movementModule.Move();
    }
    #endregion

    #region Event Methods
    public void OnColDetected(RaycastHit2D colInfo, MovementModule colDetector) {
        Debug.Log("I hit something");
    }
    #endregion

    #region Cleanup Methods
    private void OnDestroy() {
        movementModule.colDetected -= OnColDetected;
    }
    #endregion
}
