using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Choice : SceneObject<Choice>
{
    [SerializeField] private TMP_Text _textArea;

    private ChoiceWindow _window;
    private Button _button;
    private string _text;

    public void Init(ChoiceWindow window)
    {
        base.Init();
        _button = GetComponent<Button>();
        _window = window;
        Clear();
    }

    public IEnumerator Display(string id, string text)
    {
        Clear();
        _name = id;
        _textArea.text = _text;
        yield return null;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSelected);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSelected);
    }

    private void OnSelected()
    {
        _window.OnChoiceSelected(this);
    }
}
