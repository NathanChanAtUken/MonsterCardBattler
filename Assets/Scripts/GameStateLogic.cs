using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLogic {
    #region Fields
    public enum GameState {
        PlayerTurnIdle = 0,
        PlayerTurnHandCardSelected = 1
    }
    [SerializeField]
    private GameState state;
    public GameState State {
        get { return state; }
        set { state = value; }
    }
    #endregion

    #region Initialization Methods
    public GameStateLogic() {
        
    }
    #endregion
}
