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

        [SerializeField]
        private CardStackPhysics cardStackPhysics;

        [SerializeField]
        private CardStackView cardStackView;
    #endregion
    
    #region Initialization Methods
    public void InitializeValues(CardStackLogic cardStackLogic, CardStackView cardStackView) {
        this.cardStackLogic = cardStackLogic;
        this.cardStackView = cardStackView;
        this.cardStackView.Initialize(cardStackLogic, false);
    }
    #endregion

    public void RefreshView() {
        this.cardStackView.Initialize(this.cardStackLogic, true);
    }

    public void DisablePhysics() {
       this.cardStackPhysics.gameObject.SetActive(false);
    }
}
