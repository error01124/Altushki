using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour, IService
{
    [SerializeField] private Image _backgroundImage;

    public void Init()
    {
        
    }

    public void Show(string path)
    {
        _backgroundImage.gameObject.SetActive(true);
        _backgroundImage.sprite = Resources.Load<Sprite>(path);
    }

    public void Hide()
    {
        _backgroundImage.gameObject.SetActive(false);
    }
}
