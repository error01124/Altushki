using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Choice : MonoBehaviour
{
    [SerializeField] private TMP_Text _textArea;

    private ChoiceLabel _label;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(ChoiceLabel label)
    {
        _label = label;
    }

    public void Setup(string text)
    {
        _textArea.text = text;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSeleced);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSeleced);
    }

    private void OnSeleced()
    {
        _label.OnChoiceSelected(this);
    }
}
