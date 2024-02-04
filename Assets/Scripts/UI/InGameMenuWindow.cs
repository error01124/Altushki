using UnityEngine;
using UnityEngine.UI;

public class InGameMenuWindow : Window
{
    [SerializeField] private Button _saveAndQuitButton;

    private WindowManager _manager;
    private GameController _gameController;

    public void Init(WindowManager manager)
    {
        _manager = manager;
        _gameController = ServiceLocator.Instance.Get<GameController>();
    }

    private void OnEnable()
    {
        _saveAndQuitButton?.onClick.AddListener(OnSaveAndQuitButtonClicked);
    }

    private void OnDisable()
    {
        _saveAndQuitButton?.onClick.RemoveListener(OnSaveAndQuitButtonClicked);
    }

    public override void Open()
    {
        _gameController.Pause();
    }

    public override void Close()
    {
        _gameController.Resume();
    }

    private void OnSaveAndQuitButtonClicked()
    {
        _gameController.CreateSave();
        _gameController.StopGame();
    }
}
