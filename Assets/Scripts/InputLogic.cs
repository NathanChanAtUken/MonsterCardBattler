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
                OnCardClickEvent(clickedObject);
            }
            else if (clickedObject.GetComponent<CardStackPhysics>() != null) {
                OnCardStackClickEvent(clickedObject);
            }
        }
    }

    private void OnCardClickEvent(GameObject card) {
        CardPhysics clickedCardPhysics = card.GetComponent<CardPhysics>();
        if (clickedCardPhysics.CardObject.GetComponent<Card>() != null) {
            CardLogic clickedCard = clickedCardPhysics.CardObject.GetComponent<Card>().CardLogic;
            CheckCardClickContext(clickedCard);
        }
    }

    private void CheckCardClickContext(CardLogic card) {
        if (playerController.IsInPlayerHand(card)) {
            if (playerController.SelectedCards.Contains(card)) {
                playerController.DeselectCard(card);
            }
            else {
                playerController.SelectCard(card);
            }
        }
    }

    private void OnCardStackClickEvent(GameObject cardStack) {
        CardStackPhysics clickedCardStackPhysics = cardStack.GetComponent<CardStackPhysics>();
        if (clickedCardStackPhysics.CardStackObject.GetComponent<CardStack>() != null) {
            CardStackLogic clickedCardStack = clickedCardStackPhysics.CardStackObject.GetComponent<CardStack>().CardStackLogic;
            CheckCardStackClickContext(clickedCardStack);
        }
    }

    private void CheckCardStackClickContext(CardStackLogic cardStack) {
        if (playerController.SelectedCards.Count == 1) {
            playerController.PlayFromHandToStack(playerController.SelectedCards[0], cardStack);
        }
    }
    #endregion
}
