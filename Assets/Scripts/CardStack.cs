using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {
    #region Fields
        [Header("Debugging Viewables")]
        [SerializeField]
        private CardStackLogic cardStackLogic;
        public CardStackLogic CardStackLogic {
            get { return cardStackLogic; }
            set { cardStackLogic = value; }
        }
    #endregion
    
    #region Initialization Methods
    public void InitializeValues(CardStackLogic cardStackLogic) {
        this.cardStackLogic = cardStackLogic;
    }
    #endregion
}
