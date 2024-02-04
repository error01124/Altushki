using UnityEngine;

public class Keybinds : MonoBehaviour, IService
{
    public bool Interact() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);

    public bool Quit() => Input.GetKeyDown(KeyCode.Q);
}
