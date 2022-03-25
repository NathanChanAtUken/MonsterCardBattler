using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputLogic {
    #region Fields
        [Header("Injector Passed References")]
        [SerializeField]
        private InputManager inputManager;
        [SerializeField]
        private PlayerController playerController;
    #endregion

    #region Initialization Methods
    public InputLogic(InputManager inputManager, PlayerController playerController) {
        this.inputManager = inputManager;
        this.playerController = playerController;

        EventSubscribe();
    }

    private void EventSubscribe() {
        inputManager.clickEvent += OnClickEvent;
    }
    #endregion

    #region Event Methods
    public void OnClickEvent(List<GameObject> clickedObjects) {
        foreach (GameObject clickedObject in clickedObjects) {
            if (clickedObject.GetComponent<CardPhysics>() != null) {
                ProcessCardClickEvent(clickedObject);
            }
        }
    }

    private void ProcessCardClickEvent(GameObject card) {
        CardPhysics clickedCardPhysics = card.GetComponent<CardPhysics>();
        if (clickedCardPhysics.CardObject.GetComponent<Card>() != null) {
            CardLogic clickedCard = clickedCardPhysics.CardObject.GetComponent<Card>().CardLogic;
            if (playerController.IsInPlayerHand(clickedCard)) {
                playerController.SelectCard(clickedCard);
            }
        }
    }
    #endregion
}
