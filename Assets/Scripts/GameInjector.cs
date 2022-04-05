using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInjector : MonoBehaviour {
    #region Fields
        [Header("Inspector Injected References")]
        [SerializeField]
        private InputManager inputManager;
        [SerializeField]
        private GameView gameView;
        [SerializeField]
        private MonsterView monsterView;
        [SerializeField]
        private ComboView comboView;

        [Header("Inspector Injected Parameters")]
        [SerializeField]
        private int numCardStacks;
        [SerializeField]
        private int numCardsPerStack;
        [SerializeField]
        private int initialHandSize;
        [SerializeField]
        private int initialDrawStackSize;

        [Header("Prefab References")]
        [SerializeField]
        private GameObject CardPrefab;
        [SerializeField]
        private GameObject CardStackPrefab;
        [SerializeField]
        private GameObject PlayCardStackViewPrefab;
        [SerializeField]
        private GameObject PlayerHandViewPrefab;
        [SerializeField]
        private GameObject DrawStackViewPrefab;

  [Header("Debugging Viewables")]
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private MonsterController monsterController;
        [SerializeField]
        private ComboController comboController;
        [SerializeField]
        private InputLogic inputLogic;
        [SerializeField]
        private GameLogic gameLogic;
    #endregion

    #region Initialization Methods
    private void Awake() {
        CardStackLogic playerHand = InitializePlayerHand(initialHandSize);
        CardStackLogic drawStack = InitializeDrawStack(initialDrawStackSize);
        List<CardStackLogic> playStacks = InitializePlayStacks();
        playerController = InitializePlayerController(playerHand, drawStack, playStacks);
        monsterController = InitializeMonsterController(Monster.GenerateDefaultMonster(), playerController.PlayerCombatEntity, this.monsterView);
        comboController = InitializeComboController(3, 5, playerController.PlayerCombatEntity, monsterController.MonsterCombatEntity, this.comboView);
        inputLogic = InitializeInputLogic(inputManager, playerController);
        gameLogic = InitializeGameLogic(playerController, monsterController, comboController);

        gameView.Initialize(playStacks, playerHand, drawStack);
    }

    private CardStackLogic InitializePlayerHand(int initialHandSize) {
        return InitializeStack(PlayerHandViewPrefab, initialHandSize, "PlayerHand", initialHandSize);
    }

    private CardStackLogic InitializeDrawStack(int initialDrawStackSize) {
        return InitializeStack(DrawStackViewPrefab, initialDrawStackSize, "DrawStack");
    }

    private List<CardStackLogic> InitializePlayStacks() {
        List<CardStackLogic> playStacks = new List<CardStackLogic>();

        for (int i = 0; i < numCardStacks; i++) {
            CardStackLogic newPlayStack = InitializeStack(PlayCardStackViewPrefab, numCardsPerStack, "Stack " + i);
            playStacks.Add(newPlayStack);
        }

        return playStacks;
    }

    private CardStackLogic InitializeStack(GameObject cardStackViewPrefab, int numCards = 0, string stackName = "Stack", int stackLimit = int.MaxValue) {
        CardStack cardStack = Instantiate(CardStackPrefab).GetComponent<CardStack>();
        CardStackView cardStackView = Instantiate(cardStackViewPrefab, cardStack.transform).GetComponent<CardStackView>();
        List<CardLogic> cards = new List<CardLogic>();

        for (int i = 0; i < numCards; i++) {
            GameObject newCardObject = Instantiate(CardPrefab, cardStack.transform);
            CardLogic newCard = new CardLogic(newCardObject);
            cards.Add(newCard);
            newCardObject.GetComponent<Card>().InitializeValues(newCard);
        }

        CardStackLogic newPlayStack = new CardStackLogic(cardStack.gameObject, cards, stackLimit);
        cardStack.name = stackName;
        cardStack.InitializeValues(newPlayStack, cardStackView);

        return newPlayStack;
    }

    private PlayerController InitializePlayerController(CardStackLogic playerHand, CardStackLogic drawStack, List<CardStackLogic> playStacks) {
        return new PlayerController(playerHand, drawStack, playStacks);
    }

    private MonsterController InitializeMonsterController(Monster monster, CombatEntity opponent, MonsterView monsterView) {
        return new MonsterController(monster, opponent, monsterView);
    }

    private ComboController InitializeComboController(int comboCount, int maxActionValue, CombatEntity self, CombatEntity opponent, ComboView comboView) {
        return new ComboController(ComboController.GenerateRandomCombos(comboCount, maxActionValue, self, opponent), comboView);
    }

    private InputLogic InitializeInputLogic(InputManager inputManager, PlayerController playerController) {
        return new InputLogic(inputManager, playerController);
    }

    private GameLogic InitializeGameLogic(PlayerController playerController, MonsterController monsterController, ComboController comboController) {
        return new GameLogic(playerController, monsterController, comboController);
    }
    #endregion
}
