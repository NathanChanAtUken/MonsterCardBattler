using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStack : CardStack {
    #region Fields
        [SerializeField]
        private DrawStackView drawStackView;
        public DrawStackView DrawStackView {
          get { return this.drawStackView; }
          set { this.drawStackView = value; }
        }
    #endregion
    
    #region Initialization Methods
    public override void InitializeValues(CardStackLogic cardStackLogic) {
        this.cardStackLogic = cardStackLogic;
        this.drawStackView.Initialize(cardStackLogic);
    }
    #endregion

    public override void Refresh(CardStackLogic cardStackLogic) {
        this.drawStackView.Initialize(cardStackLogic);
    }
}
