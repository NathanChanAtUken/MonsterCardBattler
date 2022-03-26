using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardStack : MonoBehaviour {
    #region Fields
        [Header("Debugging Viewables")]
        [SerializeField]
        protected CardStackLogic cardStackLogic;
    #endregion
    
    #region Initialization Methods
    public abstract void InitializeValues(CardStackLogic cardStackLogic);
    #endregion

    public abstract void Refresh(CardStackLogic cardStackLogic);
}
