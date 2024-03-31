using UnityEngine;
using UnityEngine.InputSystem;

public enum InputDevice
{
    KeyboardMouse,
    Gamepad
}

public class InputDeviceManager : MonoBehaviour
{
    public static InputDeviceManager Instance { get; private set; }

    public delegate void OnInputDeviceEvent(InputDevice inputDevice);
    public OnInputDeviceEvent OnDeviceChanged;

    public InputDevice CurrentDevice { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one Input Device Manager found in scene.");

        Instance = this;
    }

    private void Start()
    {
        OnControlsChanged(Player.Instance.PlayerInput);
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        CurrentDevice = (InputDevice)System.Enum.Parse(typeof(InputDevice), playerInput.currentControlScheme);
        OnDeviceChanged?.Invoke(CurrentDevice);
    }
}
