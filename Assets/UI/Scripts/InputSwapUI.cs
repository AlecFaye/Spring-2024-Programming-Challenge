using System.Collections.Generic;
using UnityEngine;

public class InputSwapUI : MonoBehaviour
{
    [SerializeField] private GameObject keyboardMouseObject;
    [SerializeField] private GameObject gamepadObject;

    private readonly Dictionary<InputDevice, GameObject> inputDeviceObjects = new();

    private void Awake()
    {
        inputDeviceObjects.Add(InputDevice.KeyboardMouse, keyboardMouseObject);
        inputDeviceObjects.Add(InputDevice.Gamepad, gamepadObject);
    }

    private void Start()
    {
        InputDeviceManager.Instance.OnDeviceChanged += InputDevice_OnDeviceChanged;
    }

    private void OnEnable()
    {
        if (InputDeviceManager.Instance != null)
            ChangeDeviceIcon(InputDeviceManager.Instance.CurrentDevice);
    }

    private void InputDevice_OnDeviceChanged(InputDevice inputDevice)
    {
        ChangeDeviceIcon(inputDevice);
    }

    private void ChangeDeviceIcon(InputDevice inputDevice)
    {
        foreach (KeyValuePair<InputDevice, GameObject> kvp in inputDeviceObjects)
            kvp.Value.SetActive(kvp.Key == inputDevice);
    }
}
