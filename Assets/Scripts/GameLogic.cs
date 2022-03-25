using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLogic {
    #region Fields
        [Header("Injected References")]
        [SerializeField]
        private PlayerController playerController;
    #endregion

    #region Initialization Methods
    public GameLogic(PlayerController playerController) {
        this.playerController = playerController;

        GameStart();
    }

    public void GameStart() {
        DrawCards(true);
    }
    #endregion

    #region Event Methods
    private void DrawCards(bool fillHand = false, int numCards = 1) {
        numCards = fillHand ? playerController.PlayerHand.EmptySlots() : numCards;

        for (int i = 0; i < numCards; i++) {
            playerController.DrawCard();
        }
    }
    #endregion
}
