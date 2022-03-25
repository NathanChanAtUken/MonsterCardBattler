using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    #region Fields
        public delegate void OnClick(List<GameObject> clickedObjects);
        public event OnClick clickEvent;
    #endregion

    #region Initialization Methods
    private void Start() {
        
    }
    #endregion

    #region Cycle Methods
    private void Update() {
        CheckMouseInputs();
    }
    #endregion

    #region Event Methods
    private void CheckMouseInputs() {
        if (Input.GetMouseButtonDown(0)) {
            List<GameObject> clickedObjects = CheckClicked();
            clickEvent?.Invoke(clickedObjects);
        }
    }

    private List<GameObject> CheckClicked() {
        List<GameObject> clickedObjects = new List<GameObject>();
        RaycastHit2D[] collisionInfo = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        foreach (RaycastHit2D collision in collisionInfo) {
            if (collision.collider.GetComponent<IClickable>() != null) {
                clickedObjects.Add(collision.collider.gameObject);
            }
        }

        return clickedObjects;
    }
    #endregion
}
