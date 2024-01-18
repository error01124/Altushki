using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SaveChoice : MonoBehaviour
{
    public ISaveData SaveData => _saveData;

    private ISaveData _saveData;
    private SelectSaveWindow _window;
    private Button _button;

    public void Init(SelectSaveWindow window)
    {
        _window = window;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {

    }
}
