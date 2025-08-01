using System;
using UnityEngine;

public enum GameState
{
    factory_phase = 0,
    sell_phase = 1,
    upgrade_phase = 2,
    win_phase = 3,
    pause_phase = 4
}

public class GameStateManager : MonoBehaviour
{
    private GameState state = GameState.factory_phase;
    private GameState previous_state;
    private int current_day = 1;
    private int max_day = 5;
    private bool paused = false;

    public event Action signalWorkerPause;
    public event Action signalWorkerUnpause;

    public event Action signalSellMenuDisplay;

    public event Action signalTimerReset;
    public event Action<int> signalUIChange;

    public void onFactoryState()
    {
        state = GameState.factory_phase;
        signalWorkerUnpause?.Invoke();
        signalTimerReset?.Invoke();
        signalUIChange?.Invoke(0);
    }

    public void onSellState()
    {
        state = GameState.sell_phase;
        signalWorkerPause?.Invoke();
        signalUIChange?.Invoke(1);
    }

    public void onUpgradeState()
    {
        state = GameState.upgrade_phase;
        signalUIChange?.Invoke(2);
    }

    public void onWinState()
    {
        state = GameState.win_phase;
    }

    public void onPauseState()
    {
        previous_state = state;
        state = GameState.pause_phase;
    }

}
