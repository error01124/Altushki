using UnityEngine;
using UnityEngine.UI;

public class StartMenuWindow : Window
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _selectSaveButton;

    private WindowManager _manager;
    private GameController _gameController;

    public void Init(WindowManager manager)
    {
        _manager = manager;
        _gameController = ServiceLocator.Instance.Get<GameController>();
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
        // TODO
        //_manager.Show();
    }

    // TODO 
    private void StartGame()
    {
        _gameController.StartGame(null);
        _manager.Hide(this);
    }
}
