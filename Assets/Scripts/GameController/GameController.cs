using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartGame;
    [SerializeField] private UnityEvent gameOver;
    [SerializeField] private UnityEvent onPlayerWin;

    #region Unity Methods
    private void Start()
    {
        onStartGame?.Invoke();
    }

    private void OnEnable()
    {
        Clock.OnTimeEnd += GameOver;
        Player.OnPlayerDie += GameOver;
        DungeonController.OnClearAllRooms += WinGame;
    }

    private void OnDisable()
    {
        Clock.OnTimeEnd -= GameOver;
        Player.OnPlayerDie -= GameOver;
        DungeonController.OnClearAllRooms -= WinGame;
    }
    #endregion

    private void GameOver()
    {
        gameOver.Invoke();
    }

    private void WinGame()
    {
        onPlayerWin?.Invoke();
    }

    public void ResetScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
