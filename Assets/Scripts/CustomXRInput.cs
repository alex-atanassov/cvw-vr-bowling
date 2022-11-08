using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class CustomXRInput : MonoBehaviour
{

    InputDevice LeftController;
    InputDevice RightController;


    // Editor functions to make life easier when testing in Unity editor instead of with controllers
    // usage: click checkbox in Editor to set to true (i.e. trigger is down), reclick to set to false (i.e. trigger is up)

    [Header("Debug actions (set while playing in Editor)")]
    public bool Debug_LeftTriggerDown;
    public bool Debug_RightTriggerDown;
    public bool Debug_LeftGripDown;
    public bool Debug_RightGripDown;
    public bool Debug_AButtonPressed;
    public bool Debug_BButtonPressed;
    public bool Debug_XButtonPressed;
    public bool Debug_YButtonPressed;


    // PUBLIC INPUT VALUES AND UNITY EVENTS

    [Header("VR values and events")]

    // How far you have to press the trigger before triggering the event
    public float TriggerSensitivity = 0.6f;

    // primary triggers
    // usage: float input = CustomXRInputReference.LeftTriggerValue;
    public float LeftTriggerValue; // 0...1
    public float RightTriggerValue; // 0...1

    public bool HorLeftAxisDown;
    public bool VerLeftAxisDown;
    public bool HorRightAxisDown;
    public bool VerRightAxisDown;

    public bool LeftTriggerDown; // true, false
    public bool RightTriggerDown; // true, false

    // grip triggers
    // usage: float input = CustomXRInputReference.LeftGripValue;
    public float LeftGripValue; // 0...1
    public float RightGripValue; // 0...1
    public bool LeftGripDown; // true, false
    public bool RightGripDown; // true, false

    // buttons
    // usage: bool input = CustomXRInputReference.AButtonDown;
    public bool AButtonDown; // true, false
    public bool BButtonDown; // true, false
    public bool XButtonDown; // true, false
    public bool YButtonDown; // true, false

    // unity events
    // usage: assign UnityEvent functions from other scripts in Editor
    public UnityEvent HorLeftAxisDownEvent;
    public UnityEvent HorLeftAxisReleasedEvent;
    public UnityEvent HorRightAxisDownEvent;
    public UnityEvent HorRightAxisReleasedEvent;
    public UnityEvent VerLeftAxisDownEvent;
    public UnityEvent VerLeftAxisReleasedEvent;
    public UnityEvent VerRightAxisDownEvent;
    public UnityEvent VerRightAxisReleasedEvent;

    public UnityEvent LeftTriggerDownEvent;
    public UnityEvent LeftTriggerReleasedEvent;
    public UnityEvent RightTriggerDownEvent;
    public UnityEvent RightTriggerReleasedEvent;
    public UnityEvent LeftGripDownEvent;
    public UnityEvent LeftGripReleasedEvent;
    public UnityEvent RightGripDownEvent;
    public UnityEvent RightGripReleasedEvent;
    public UnityEvent AButtonPressedEvent;
    public UnityEvent AButtonReleasedEvent;
    public UnityEvent BButtonPressedEvent;
    public UnityEvent BButtonReleasedEvent;
    public UnityEvent XButtonPressedEvent;
    public UnityEvent XButtonReleasedEvent;
    public UnityEvent YButtonPressedEvent;
    public UnityEvent YButtonReleasedEvent;



    void Start()
    {
        // Set up XR controllers
        GetDevices();
    }

    void Update()
    {

        // Check for input
        if (!Application.isEditor) GetXRInput(); // VR input
        else GetEditorInput(); // Editor input for debugging and dev
    }

    /* /** / Refers to parts that should be moved out to a dedicated script */ 
    /**/ private void SnapTurn()
    {
        HorLeftAxisDownEvent.Invoke();
    }


    //************** INTERNAL FUNCTIONS **************//


    // PUBLIC UNITY EVENTS

    // left horizontal axis pressed event
    void HorizontalLeftAxisDown()
    {
        /**/ InvokeRepeating("SnapTurn", 0f, 0.5f);
        HorLeftAxisDownEvent.Invoke();
    }
    // left horizontal axis released event
    void HorizontalLeftAxisReleased()
    {
        /**/ CancelInvoke();
        HorLeftAxisReleasedEvent.Invoke();
    }
    // right horizontal axis pressed event
    void HorizontalRightAxisDown()
    {
        HorRightAxisDownEvent.Invoke();
    }
    // right horizontal axis released event
    void HorizontalRightAxisReleased()
    {
        HorRightAxisReleasedEvent.Invoke();
    }
    // left vertical axis pressed event
    void VerticalLeftAxisDown()
    {
        /**/ InvokeRepeating("SnapTurn", 0f, 0.5f);
        VerLeftAxisDownEvent.Invoke();
    }
    // left vertical axis released event
    void VerticalLeftAxisReleased()
    {
        /**/ CancelInvoke();
        VerLeftAxisReleasedEvent.Invoke();
    }
    // right vertical axis pressed event
    void VerticalRightAxisDown()
    {
        VerRightAxisDownEvent.Invoke();
    }
    // right vertical axis released event
    void VerticalRightAxisReleased()
    {
        VerRightAxisReleasedEvent.Invoke();
    }



    // left trigger pressed event
    void InternalLeftTriggerDown()
    {
        LeftTriggerDownEvent.Invoke();
    }

    // right trigger pressed event
    void InternalRightTriggerDown()
    {
        RightTriggerDownEvent.Invoke();
    }

    // left trigger released event
    void InternalLeftTriggerReleased()
    {
        LeftTriggerReleasedEvent.Invoke();
    }

    // right trigger released event
    void InternalRightTriggerReleased()
    {
        RightTriggerReleasedEvent.Invoke();
    }

    // left grip pressed event
    void InternalLeftGripDown()
    {
        LeftGripDownEvent.Invoke();
    }

    // right grip pressed event
    void InternalRightGripDown()
    {
        RightGripDownEvent.Invoke();
    }

    // left grip released event
    void InternalLeftGripReleased()
    {
        LeftGripReleasedEvent.Invoke();
    }

    // right grip released event
    void InternalRightGripReleased()
    {
        RightGripReleasedEvent.Invoke();
    }

    // A button pressed
    void InternalAButtonPressed()
    {
        AButtonPressedEvent.Invoke();
    }

    // A button released
    void InternalAButtonReleased()
    {
        AButtonReleasedEvent.Invoke();
    }

    // B button pressed
    void InternalBButtonPressed()
    {
        BButtonPressedEvent.Invoke();
    }

    // B button released
    void InternalBButtonReleased()
    {
        BButtonReleasedEvent.Invoke();
    }

    // X button pressed
    void InternalXButtonPressed()
    {
        XButtonPressedEvent.Invoke();
    }

    // X button released
    void InternalXButtonReleased()
    {
        XButtonReleasedEvent.Invoke();
    }

    // Y button pressed
    void InternalYButtonPressed()
    {
        YButtonPressedEvent.Invoke();
    }

    // Y button released
    void InternalYButtonReleased()
    {
        YButtonReleasedEvent.Invoke();
    }



    // GETTING INPUT

    // Controller input in VR
    void GetXRInput()
    {

        // primary trigger events

        LeftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        RightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);

        LeftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftAxisValue);
        RightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightAxisValue);

        LeftTriggerValue = leftTriggerValue;
        RightTriggerValue = rightTriggerValue;

        // joystick horizontal and vertical, moved and released
        if (!HorLeftAxisDown && (leftAxisValue.x < -TriggerSensitivity || leftAxisValue.x > TriggerSensitivity))
        {
            HorizontalLeftAxisDown();
            HorLeftAxisDown = true;
        }
        if (!VerLeftAxisDown && leftAxisValue.y < -TriggerSensitivity)
        {
            VerticalLeftAxisDown();
            VerLeftAxisDown = true;
        }

        if (HorLeftAxisDown && leftAxisValue.x >= -TriggerSensitivity && leftAxisValue.x <= TriggerSensitivity)
        {
            HorizontalLeftAxisReleased();
            HorLeftAxisDown = false;
        }
        if (VerLeftAxisDown && leftAxisValue.y >= -TriggerSensitivity)
        {
            VerticalLeftAxisReleased();
            VerLeftAxisDown = false;
        }

        if (!VerRightAxisDown && (rightAxisValue.y < -TriggerSensitivity || rightAxisValue.y > TriggerSensitivity))
        {
            VerticalRightAxisDown();
            VerRightAxisDown = true;
        }
        if (VerRightAxisDown && rightAxisValue.y >= -TriggerSensitivity && rightAxisValue.y <= TriggerSensitivity)
        {
            VerticalRightAxisReleased();
            VerRightAxisDown = false;
        }

        // left press
        if (leftTriggerValue > TriggerSensitivity)
        {
            if (!LeftTriggerDown)
            {
                InternalLeftTriggerDown();
                LeftTriggerDown = true;
            }
        }

        // left release
        if (LeftTriggerDown && leftTriggerValue == 0)
        {
            InternalLeftTriggerReleased();
            LeftTriggerDown = false;
        }

        // right press
        if (rightTriggerValue > TriggerSensitivity)
        {
            if (!RightTriggerDown)
            {
                InternalRightTriggerDown();
                RightTriggerDown = true;
            }
        }

        // right release
        if (RightTriggerDown && rightTriggerValue == 0)
        {
            InternalRightTriggerReleased();
            RightTriggerDown = false;
        }


        // grip trigger events

        LeftController.TryGetFeatureValue(CommonUsages.grip, out float leftGripValue);
        RightController.TryGetFeatureValue(CommonUsages.grip, out float rightGripValue);

        LeftGripValue = leftGripValue;
        RightGripValue = rightGripValue;

        // left press
        if (leftGripValue > TriggerSensitivity)
        {
            if (!LeftGripDown)
            {
                InternalLeftGripDown();
                LeftGripDown = true;
            }
        }

        // left release
        if (LeftGripDown && leftGripValue == 0)
        {
            InternalLeftGripReleased();
            LeftGripDown = false;
        }

        // right press
        if (rightGripValue > TriggerSensitivity)
        {
            if (!RightGripDown)
            {
                InternalRightGripDown();
                RightGripDown = true;
            }
        }

        // right release
        if (RightGripDown && rightGripValue == 0)
        {
            InternalRightGripReleased();
            RightGripDown = false;
        }


        // button events

        // a button
        RightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue);
        if (rightPrimaryButtonValue)
        {
            if (!AButtonDown)
            {
                InternalAButtonPressed();
                AButtonDown = true;
            }
        }
        else
        {
            if (AButtonDown)
            {
                InternalAButtonReleased();
            }
            AButtonDown = false;
        }

        // b button
        RightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightSecondaryButtonValue);
        if (rightSecondaryButtonValue)
        {
            if (!BButtonDown)
            {
                InternalBButtonPressed();
                BButtonDown = true;
            }
        }
        else
        {
            if (BButtonDown)
            {
                InternalBButtonReleased();
            }
            BButtonDown = false;
        }

        // x button
        LeftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue);
        if (leftPrimaryButtonValue)
        {
            if (!XButtonDown)
            {
                InternalXButtonPressed();
                XButtonDown = true;
            }
        }
        else
        {
            if (XButtonDown)
            {
                InternalXButtonReleased();
            }
            XButtonDown = false;
        }

        // y button
        LeftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftSecondaryButtonValue);
        if (leftSecondaryButtonValue)
        {
            if (!YButtonDown)
            {
                InternalYButtonPressed();
                YButtonDown = true;
            }
        }
        else
        {
            if (YButtonDown)
            {
                InternalYButtonReleased();
            }
            YButtonDown = false;
        }



    }

    // Inspector input in Editor to make life easier for non-Link users
    void GetEditorInput()
    {

        if (Debug_LeftTriggerDown)
        {
            if (!LeftTriggerDown)
            {
                InternalLeftTriggerDown();
                LeftTriggerDown = true;
                Debug.Log("CustomXRInput: LeftTriggerDown");
            }
        }

        if (!Debug_LeftTriggerDown && LeftTriggerDown)
        {
            InternalLeftTriggerReleased();
            LeftTriggerDown = false;
            Debug.Log("CustomXRInput: LeftTriggerReleased");
        }

        if (Debug_RightTriggerDown)
        {
            if (!RightTriggerDown)
            {
                InternalRightTriggerDown();
                RightTriggerDown = true;
                Debug.Log("CustomXRInput: RightTriggerDown");
            }
        }

        if (!Debug_RightTriggerDown && RightTriggerDown)
        {
            InternalRightTriggerReleased();
            RightTriggerDown = false;
            Debug.Log("CustomXRInput: RightTriggerReleased");
        }


        if (Debug_LeftGripDown)
        {
            if (!LeftGripDown)
            {
                InternalLeftGripDown();
                LeftGripDown = true;
                Debug.Log("CustomXRInput: LeftGripDown");
            }
        }

        if (!Debug_LeftGripDown && LeftGripDown)
        {
            InternalLeftGripReleased();
            LeftGripDown = false;
            Debug.Log("CustomXRInput: LeftGripReleased");
        }

        if (Debug_RightGripDown)
        {
            if (!RightGripDown)
            {
                InternalRightGripDown();
                RightGripDown = true;
                Debug.Log("CustomXRInput: RightGripDown");
            }
        }

        if (!Debug_RightGripDown && RightGripDown)
        {
            InternalRightGripReleased();
            RightGripDown = false;
            Debug.Log("CustomXRInput: RightGripReleased");
        }


        if (Debug_AButtonPressed)
        {
            if (!AButtonDown)
            {
                InternalAButtonPressed();
                AButtonDown = true;
                Debug.Log("CustomXRInput: AButtonDown");
            }
        }

        if (!Debug_AButtonPressed && AButtonDown)
        {
            InternalAButtonReleased();
            AButtonDown = false;
            Debug.Log("CustomXRInput: AButtonReleased");
        }

        if (Debug_BButtonPressed)
        {
            if (!BButtonDown)
            {
                InternalBButtonPressed();
                BButtonDown = true;
                Debug.Log("CustomXRInput: BButtonDown");
            }
        }

        if (!Debug_BButtonPressed && BButtonDown)
        {
            InternalBButtonReleased();
            BButtonDown = false;
            Debug.Log("CustomXRInput: BButtonReleased");
        }

        if (Debug_XButtonPressed)
        {
            if (!XButtonDown)
            {
                InternalXButtonPressed();
                XButtonDown = true;
                Debug.Log("CustomXRInput: XButtonDown");
            }
        }

        if (!Debug_XButtonPressed && XButtonDown)
        {
            InternalXButtonReleased();
            XButtonDown = false;
            Debug.Log("CustomXRInput: XButtonReleased");
        }

        if (Debug_YButtonPressed)
        {
            if (!YButtonDown)
            {
                InternalYButtonPressed();
                YButtonDown = true;
                Debug.Log("CustomXRInput: YButtonDown");
            }
        }

        if (!Debug_YButtonPressed && YButtonDown)
        {
            InternalYButtonReleased();
            YButtonDown = false;
            Debug.Log("CustomXRInput: YButtonReleased");
        }

    }




    // SETUP

    // Get references Left and Right controllers
    void GetDevices()
    {
        List<InputDevice> LeftControllerDevices = new List<InputDevice>();
        InputDeviceCharacteristics LeftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(LeftControllerCharacteristics, LeftControllerDevices);
        if (LeftControllerDevices.Count > 0) LeftController = LeftControllerDevices[0];

        List<InputDevice> RightControllerDevices = new List<InputDevice>();
        InputDeviceCharacteristics RightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(RightControllerCharacteristics, RightControllerDevices);
        if (RightControllerDevices.Count > 0) RightController = RightControllerDevices[0];
    }

}
