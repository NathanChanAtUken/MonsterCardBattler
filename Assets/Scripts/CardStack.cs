using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {
    #region Fields
        [Header("Debugging Viewables")]
        [SerializeField]
        private CardStackLogic cardStackLogic;
        [SerializeField]
        private CardStackView cardStackView;
    #endregion
    
    #region Initialization Methods
    public void InitializeValues(CardStackLogic cardStackLogic) {
        this.cardStackLogic = cardStackLogic;
        this.cardStackView.Initialize(cardStackLogic);
    }
    #endregion
}
