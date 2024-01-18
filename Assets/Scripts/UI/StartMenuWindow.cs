using UnityEngine;
using UnityEngine.UI;

public class StartMenuWindow : Window
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _selectSaveButton;

    private WindowManager _manager;
    private GameController _gameController;

    private void Awake()
    {
        _gameController = ServiceLocator.Instance.Get<GameController>();
        _manager = ServiceLocator.Instance.Get<WindowManager>();
    }

    private void OnEnable()
    {
        _selectSaveButton.onClick.AddListener(OnSelectSaveButtonClicked);
        _startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _selectSaveButton.onClick.RemoveListener(OnSelectSaveButtonClicked);
        _startButton.onClick.RemoveListener(StartGame);
    }

    private void OnSelectSaveButtonClicked()
    {
        _manager.Show<SelectSaveWindow>();
    }

    private void StartGame()
    {
        _gameController.StartGame(null);
    }
}
