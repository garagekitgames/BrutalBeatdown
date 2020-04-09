using System;
using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    #region Controller

    #region Properties

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Enabled")]
    [TaskDescription("Gets whether the controller is enabled. Disabled controllers return no input.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetEnabled : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.enabled);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Set Controller Enabled")]
    [TaskDescription("Sets whether the controller is enabled. Disabled controllers return no input.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerSetEnabled : ControllerSetBoolAction {

        protected override TaskStatus DoUpdate() {
            Controller.enabled = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Id")]
    [TaskDescription("Gets the Rewired unique id of this controller. This is not an index. The id is unique among controllers of a specific controller type.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetId : ControllerGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.id);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Name")]
    [TaskDescription("Gets the name of the Controller. For Joysticks, this is drawn from the controller definition for recognized Joysticks. For unrecognized Joysticks, the name returned by the hardware is used instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetName : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.name);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Tag")]
    [TaskDescription("Gets the tag assigned to the controller. Can be used for find a controller by tag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetTag : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.tag);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Set Controller Tag")]
    [TaskDescription("Sets the tag assigned to the controller. Can be used for find a controller by tag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerSetTag : ControllerAction {

        [Tooltip("The tag to set.")]
        public SharedString tag;

        public override void OnReset() {
            base.OnReset();
            tag = string.Empty;
        }

        protected override TaskStatus DoUpdate() {
            Controller.tag = tag.Value;
            return TaskStatus.Success;
        }
    }
    
    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Hardware Name")]
    [TaskDescription("Gets the name the controller hardware returns.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetHardwareName : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.hardwareName);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Type")]
    [TaskDescription("Gets the type of this controller.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetType : ControllerGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)Controller.type);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Is Controller Connected")]
    [TaskDescription("Is the controller connected?")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetIsConnected : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.isConnected);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Button Count")]
    [TaskDescription("Gets the button count in the controller.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonCount : ControllerGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.buttonCount);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Hardware Identifier")]
    [TaskDescription("Gets the string of information from the controller used for identifying unknown controller maps for saving/loading.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetHardwareIdentifier : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.hardwareIdentifier);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Map Type String")]
    [TaskDescription("Gets the string representation of the controller map type. Can be used for saving/loading.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetMapTypeString : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.mapTypeString);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Hardware Type Guid String")]
    [TaskDescription("Gets the Rewired GUID associated with this device.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetHardwareTypeGuidString : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.hardwareTypeGuid.ToString());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Device Instance Guid String")]
    [TaskDescription(
        "The unique persistent instance GUID of this device. " +
        "This is an id generated for the device that may stay constant between application sessions and system restarts. " +
        "This can be used for device assignment persistence between runs. The specific platform and input sources in use " +
        "affects the reliability of this value for device assignment persistence. " +
        "A value of Guid.Empty means the device or input source has no reliable unique identifier so persistant assignment " +
        "isn't possible using this value. Even if a Guid is provided, reliability when multiple identical controllers are " +
        "attached depends greatly on the platform and input source(s) currently in use."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetDeviceInstanceGuidString : ControllerGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.deviceInstanceGuid.ToString());
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Methods

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Last Time Active")]
    [TaskDescription("Gets the last timestamp any axis or button was active. NOTE: If comparing time against current time, always compare to ReInput.time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetLastTimeActive : ControllerGetFloatAction {

        [Tooltip("Use raw axis values.")]
        public SharedBool useRawValues;

        public override void OnReset() {
            base.OnReset();
            useRawValues = false;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetLastTimeActive(useRawValues.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Last Time Any Button Pressed")]
    [TaskDescription("Gets the last timestamp any button was active. NOTE: If comparing time against current time, always compare to ReInput.time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetLastTimeAnyButtonPressed : ControllerGetFloatAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetLastTimeAnyButtonPressed());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Last Time Any Button Changed")]
    [TaskDescription("Gets the last timestamp any button's state changed. NOTE: If comparing time against current time, always compare to ReInput.time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetLastTimeAnyButtonChanged : ControllerGetFloatAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetLastTimeAnyButtonChanged());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Last Time Any Element Changed")]
    [TaskDescription("Gets the last timestamp any element changed state. NOTE: If comparing time against current time, always compare to ReInput.time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetLastTimeAnyElementChanged : ControllerGetFloatAction {

        [Tooltip("Use raw axis values.")]
        public SharedBool useRawValues;

        public override void OnReset() {
            base.OnReset();
            useRawValues = false;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetLastTimeAnyElementChanged(useRawValues.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Any Button")]
    [TaskDescription("Gets the button held state of any hardware button. This will return TRUE as long as any button is held. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAnyButton : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAnyButton());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Any Button Down")]
    [TaskDescription("Gets the button just pressed state of any hardware button. This will only return TRUE only on the first frame the button is pressed This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAnyButtonDown : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAnyButtonDown());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Any Button Up")]
    [TaskDescription("Gets the button just released state of any hardware button. This will only return TRUE only on the first frame the button is released This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAnyButtonUp : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAnyButtonUp());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Any Button Prev")]
    [TaskDescription("Gets the previous button held state of any hardware button. This will return TRUE if any button was held in the previous frame. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAnyButtonPrev : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAnyButtonPrev());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Any Button Changed")]
    [TaskDescription("Returns true if any button has changed state from the previous frame to the current. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAnyButtonChanged : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAnyButtonChanged());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button")]
    [TaskDescription("Gets the button held state of the hardware button at the specified index. This will return TRUE as long as the button is held. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButton : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButton(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Down")]
    [TaskDescription("Gets the button just pressed state of the hardware button at the specified index. This will only return TRUE only on the first frame the button is pressed This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonDown : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonDown(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Up")]
    [TaskDescription("Gets the button just released state of the hardware button at the specified index. This will only return TRUE only on the first frame the button is released This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonUp : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonUp(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Prev")]
    [TaskDescription("Gets the previous button held state of the hardware button at the specified index. This will return TRUE if the button was held in the previous frame. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonPrev : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonPrev(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Changed")]
    [TaskDescription("Returns true if the button has changed state from the previous frame to the current. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonChanged : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonChanged(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button By Id")]
    [TaskDescription("Gets the button held state of the hardware button at the specified element identifier id. This will return TRUE as long as the button is held. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonById : ControllerGetBoolAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Down By Id")]
    [TaskDescription("Gets the button just pressed state of the hardware button at the specified element identifier id. This will only return TRUE only on the first frame the button is pressed This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonDownById : ControllerGetBoolAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonDownById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Up By Id")]
    [TaskDescription("Gets the button just released state of the hardware button at the specified element identifier id. This will only return TRUE only on the first frame the button is released This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonUpById : ControllerGetBoolAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonUpById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Prev By Id")]
    [TaskDescription("Gets the previous button held state of the hardware button at the specified element identifier id. This will return TRUE if the button was held in the previous frame. This does not take into acount any controller mapping or Actions -- this is the unmapped physical button value only. Use the Player class to get button values mapped to Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonPrevById : ControllerGetBoolAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonPrevById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Double Press Hold")]
    [TaskDescription("Gets the double press and hold state of the button at the specified index. This will return TRUE after the double press is detected and for as long as the button is held thereafter.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonDoublePressHold : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        [Tooltip("The speed at which the button must be pressed twice in seconds in order to be considered a double press. If 0 or less, the default speed will be used.")]
        public SharedFloat speed;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
            speed = 0f;
        }

        protected override TaskStatus DoUpdate() {
            if(speed.Value > 0f) UpdateStoreValue(Controller.GetButtonDoublePressHold(buttonIndex.Value, speed.Value));
            else UpdateStoreValue(Controller.GetButtonDoublePressHold(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Double Press Down")]
    [TaskDescription("Gets the double press down state of the button at the specified index. This will return TRUE only on the first frame the double press is detected.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonDoublePressDown : ControllerGetBoolAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        [Tooltip("The speed at which the button must be pressed twice in seconds in order to be considered a double press. If 0 or less, the default speed will be used.")]
        public SharedFloat speed;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
            speed = 0f;
        }

        protected override TaskStatus DoUpdate() {
            if(speed.Value > 0f) UpdateStoreValue(Controller.GetButtonDoublePressDown(buttonIndex.Value, speed.Value));
            else UpdateStoreValue(Controller.GetButtonDoublePressDown(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Double Press Hold By Id")]
    [TaskDescription("Gets the double press and hold state of the button at the specified element identifier id. This will return TRUE after the double press is detected and for as long as the button is held thereafter.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonDoublePressHoldById : ControllerGetBoolAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        [Tooltip("The speed at which the button must be pressed twice in seconds in order to be considered a double press. If 0 or less, the default speed will be used.")]
        public SharedFloat speed;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
            speed = 0f;
        }

        protected override TaskStatus DoUpdate() {
            if(speed.Value > 0f) UpdateStoreValue(Controller.GetButtonDoublePressHoldById(elementIdentifierId.Value, speed.Value));
            else UpdateStoreValue(Controller.GetButtonDoublePressHoldById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Double Press Down By Id")]
    [TaskDescription("Gets the double press down state of the button at the specified element identifier id. This will return TRUE only on the first frame the double press is detected.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonDoublePressDownById : ControllerGetBoolAction {
        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        [Tooltip("The speed at which the button must be pressed twice in seconds in order to be considered a double press. If 0 or less, the default speed will be used.")]
        public SharedFloat speed;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
            speed = 0f;
        }

        protected override TaskStatus DoUpdate() {
            if(speed.Value > 0f) UpdateStoreValue(Controller.GetButtonDoublePressDownById(elementIdentifierId.Value, speed.Value));
            else UpdateStoreValue(Controller.GetButtonDoublePressDownById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Time Pressed")]
    [TaskDescription("Gets the length of time the button at index has been active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonTimePressed : ControllerGetFloatAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonTimePressed(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Time Unpressed")]
    [TaskDescription("Gets the length of time the button at index has been inactive.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonTimeUnpressed : ControllerGetFloatAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonTimeUnpressed(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Last Time Pressed")]
    [TaskDescription("Gets the last timestamp the button at index was active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonLastTimePressed : ControllerGetFloatAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonLastTimePressed(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Last Time Unpressed")]
    [TaskDescription("Gets the last timestamp the button at index was inactive.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonLastTimeUnpressed : ControllerGetFloatAction {

        [Tooltip("Button index.")]
        public SharedInt buttonIndex;

        public override void OnReset() {
            base.OnReset();
            buttonIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonLastTimeUnpressed(buttonIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Time Pressed By Id")]
    [TaskDescription("Gets the length of time the button with the element identifier id has been active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonTimePressedById : ControllerGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonTimePressedById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Time Unpressed By Id")]
    [TaskDescription("Gets the length of time the button with the element identifier id has been inactive.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonTimeUnpressedById : ControllerGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonTimeUnpressedById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Last Time Pressed By Id")]
    [TaskDescription("Gets the last timestamp the button with the element identifier id was active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonLastTimePressedById : ControllerGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonLastTimePressedById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Button Last Time Unpressed By Id")]
    [TaskDescription("Gets the last timestamp the button with the element identifier id was inactive.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonLastTimeUnpressedById : ControllerGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonLastTimeUnpressedById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Misc

    [TaskCategory(Consts.taskCategory_controllerMisc)]
    [TaskName("Controller Get Button Index By Id")]
    [TaskDescription("Gets the index of the Button with the specified element idenfitier id.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetButtonIndexById : ControllerGetIntAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetButtonIndexById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }


    [TaskCategory(Consts.taskCategory_controllerMisc)]
    [TaskName("Controller Implements Template")]
    [TaskDescription("Determines if the Controller implements a Controller Template.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerImpelementsTemplate : ControllerGetBoolAction {

        [Tooltip("Controller template type.")]
        public Rewired.Integration.BehaviorDesigner.ControllerTemplateType templateType;

        [Tooltip("Finds the template by Guid. If enabled, the Guid is used to find the Controller Template and the Template Type field is ignored. Use this if using a custom Controller Template.")]
        public SharedBool findTemplateByGuid;

        [Tooltip("The Controller Template Guid. This is used to find the Controller Template if Find Template by Guid is enabled.")]
        public SharedString templateGuid;

        public override void OnReset() {
            base.OnReset();
            templateType = Rewired.Integration.BehaviorDesigner.ControllerTemplateType.Gamepad;
            findTemplateByGuid = false;
            templateGuid = string.Empty;
        }

        protected override TaskStatus DoUpdate() {
            if(findTemplateByGuid.Value) {
                try {
                    UpdateStoreValue(Controller.ImplementsTemplate(new Guid(templateGuid.Value)));
                } catch {
                    Debug.LogError("Invalid Guid string format.");
                }
            } else {
                UpdateStoreValue(Controller.ImplementsTemplate(Rewired.Integration.BehaviorDesigner.Utils.GetControllerTemplateTypeGuid(templateType)));
            }
            return TaskStatus.Success;
        }
    }

    #endregion

    #endregion

    #region ControllerWithAxes

    #region Properties

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Axis Count")]
    [TaskDescription("Gets the axis count in the controller.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisCount : ControllerGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((Controller as ControllerWithAxes).axis2DCount);
            return TaskStatus.Success;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;
            if(Controller as ControllerWithAxes == null) {
                Debug.LogError("Controller is an incompatible type.");
                return false;
            }
            return true;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Controller Axis2D Count")]
    [TaskDescription("Gets the Axis2D count in the controller.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxis2DCount : ControllerGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((Controller as ControllerWithAxes).axis2DCount);
            return TaskStatus.Success;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;
            if(Controller as ControllerWithAxes == null) {
                Debug.LogError("Controller is an incompatible type.");
                return false;
            }
            return true;
        }
    }

    #endregion

    #region Input
    
    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Last Time Any Axis Active")]
    [TaskDescription("Gets the last timestamp any axis was active. NOTE: If comparing time against current time, always compare to ReInput.time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetLastTimeAnyAxisActive : ControllerWithAxesGetFloatAction {

        [Tooltip("Use raw axis values.")]
        public SharedBool useRawValues;

        public override void OnReset() {
            base.OnReset();
            useRawValues = false;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetLastTimeAnyAxisActive(useRawValues.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Last Time Any Axis Changed")]
    [TaskDescription("Gets the last timestamp any axis changed state. NOTE: If comparing time against current time, always compare to ReInput.time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetLastTimeAnyAxisChanged : ControllerWithAxesGetFloatAction {

        [Tooltip("Use raw axis values.")]
        public SharedBool useRawValues;

        public override void OnReset() {
            base.OnReset();
            useRawValues = false;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetLastTimeAnyAxisChanged(useRawValues.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis")]
    [TaskDescription("Gets the current axis value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxis : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxis(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Prev")]
    [TaskDescription("Gets the axis value from the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisPrev : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisPrev(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw")]
    [TaskDescription("Gets the current raw axis value. Excludes calibration.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRaw : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRaw(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Prev")]
    [TaskDescription("Gets the raw axis value from the previous frame. Excludes calibration.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawPrev : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawPrev(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis By Id")]
    [TaskDescription("Gets the current axis value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Prev By Id")]
    [TaskDescription("Gets the axis value from the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisPrevById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisPrevById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw By Id")]
    [TaskDescription("Gets the current raw axis value. Excludes calibration.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Prev By Id")]
    [TaskDescription("Gets the raw axis value from the previous frame. Excludes calibration.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawPrevById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawPrevById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis 2D")]
    [TaskDescription("Gets the current 2D axis value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxis2D : ControllerWithAxesGetVector2Action {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxis2D(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis 2D Prev")]
    [TaskDescription("Gets the 2D axis value from the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxis2DPrev : ControllerWithAxesGetVector2Action {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxis2DPrev(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis 2D Raw")]
    [TaskDescription("Gets the current raw 2D axis value. Excludes calibration.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxis2DRaw : ControllerWithAxesGetVector2Action {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxis2DRaw(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis 2D Raw Prev")]
    [TaskDescription("Gets the raw 2D axis value from the previous frame. Excludes calibration.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxis2DRawPrev : ControllerWithAxesGetVector2Action {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxis2DRawPrev(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Last Time Active")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisLastTimeActive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisLastTimeActive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Last Time Inactive")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisLastTimeInactive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisLastTimeInactive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Last Time Active")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawLastTimeActive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawLastTimeActive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Last Time Inactive")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawLastTimeInactive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawLastTimeInactive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Time Active")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisTimeActive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisTimeActive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Time Inactive")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisTimeInactive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisTimeInactive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Time Active")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawTimeActive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawTimeActive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Time Inactive")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawTimeInactive : ControllerWithAxesGetFloatAction {

        [Tooltip("Axis index.")]
        public SharedInt axisIndex;

        public override void OnReset() {
            base.OnReset();
            axisIndex = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawTimeInactive(axisIndex.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Last Time Active By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisLastTimeActiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisLastTimeActiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Last Time Inactive By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisLastTimeInactiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisLastTimeInactiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Last Time Active By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawLastTimeActiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawLastTimeActiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Last Time Inactive By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawLastTimeInactiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawLastTimeInactiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Time Active By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisTimeActiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisTimeActiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Time Inactive By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisTimeInactiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisTimeInactiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Time Active By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawTimeActiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawTimeActiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerInput)]
    [TaskName("Controller Get Axis Raw Time Inactive By Id")]
    [TaskDescription("")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisRawTimeInactiveById : ControllerWithAxesGetFloatAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisRawTimeInactiveById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Misc

    [TaskCategory(Consts.taskCategory_controllerMisc)]
    [TaskName("Controller Get Axis Index By Id")]
    [TaskDescription("Gets the index of the Axis with the specified element idenfitier id.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerGetAxisIndexById : ControllerWithAxesGetIntAction {

        [Tooltip("Element identifier id.")]
        public SharedInt elementIdentifierId;

        public override void OnReset() {
            base.OnReset();
            elementIdentifierId = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Controller.GetAxisIndexById(elementIdentifierId.Value));
            return TaskStatus.Success;
        }
    }

    #endregion

    #endregion

    // Joystick

    // systemId is not supported because ?long is not a supported type in PlayMaker

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Joystick Unity Id")]
    [TaskDescription("Gets the unity joystick id of this joystick. This value is only used on platforms that use Unity input as the underlying input source. This value is a 1-based index corresponding to the joystick number in the Unity input manager. Generally, you should never need to use this, but it is exposed for advanced uses. Returns 0 if the platform does not use Unity input or if the joystick is not associated with a Unity joystick.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredJoystickGetUnityId : JoystickGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Joystick.unityId);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Joystick Hardware Type Guid String")]
    [TaskDescription("Gets the Rewired GUID associated with this device. A GUID of all zeros is an Unknown Controller.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredJoystickGetHardwareTypeGuidString : JoystickGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Joystick.hardwareTypeGuid.ToString());
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Joystick Supports Vibration")]
    [TaskDescription("Does this controller support vibration?")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredJoystickGetSupportsVibration : JoystickGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Joystick.supportsVibration);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_controllerProperties)]
    [TaskName("Get Joystick Vibration Motor Count")]
    [TaskDescription("Gets the number of vibration motors this device supports.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredJoystickGetVibrationMotorCount : JoystickGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Joystick.vibrationMotorCount);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Set Joystick Vibration")]
    [TaskDescription("Sets vibration level for a motor at a specified index in a Joystick.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredJoystickSetVibration : JoystickAction {

        [Tooltip("Sets the vibration motor level. [0 - 1]")]
        public SharedFloat motorLevel;

        [Tooltip("The index of the motor to vibrate.")]
        public SharedInt motorIndex;

        [Tooltip("Length of time in seconds to activate the motor before it stops. [0 = Infinite]")]
        public SharedFloat duration;

        [Tooltip("Stop all other motors except this one.")]
        public SharedBool stopOtherMotors;

        public override void OnReset() {
            base.OnReset();
            motorLevel = 0.0f;
            motorIndex = 0;
            duration = 0.0f;
            stopOtherMotors = false;
        }

        protected override TaskStatus DoUpdate() {
            if (motorIndex.Value < 0) return TaskStatus.Failure;
            motorLevel.Value = Mathf.Clamp01(motorLevel.Value);

            if (!Joystick.supportsVibration) return TaskStatus.Success;
            if (motorIndex.Value >= Joystick.vibrationMotorCount) return TaskStatus.Success;
            Joystick.SetVibration(motorIndex.Value, motorLevel.Value, duration.Value, stopOtherMotors.Value);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Stop Joystick Vibration")]
    [TaskDescription("Stops all vibration motors in the Joystick.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredJoystickStopVibration : JoystickAction {

        protected override TaskStatus DoUpdate() {
            if (!Joystick.supportsVibration) return TaskStatus.Success;
            Joystick.StopVibration();
            return TaskStatus.Success;
        }
    }

}