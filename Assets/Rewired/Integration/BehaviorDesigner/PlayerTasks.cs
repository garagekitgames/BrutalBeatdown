using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {
    using System;
    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    // Actions

    #region Player Properties

    [TaskCategory(Consts.taskCategory_player + "/Properties")]
    [TaskName("Player Get Name")]
    [TaskDescription("The descriptive name of the Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetName : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a string variable.")]
        public SharedString storeValue;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.name;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Properties")]
    [TaskName("Player Get Descriptive Name")]
    [TaskDescription("The scripting name of the Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetDescriptiveName : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a string variable.")]
        public SharedString storeValue;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.descriptiveName;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Properties")]
    [TaskName("Player Get Is Playing")]
    [TaskDescription("Is this Player currently playing?")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetIsPlaying : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.isPlaying;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Properties")]
    [TaskName("Player Set Is Playing")]
    [TaskDescription("Sets whether this Player currently playing.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerSetIsPlaying : RewiredPlayerBDAction {

        [Tooltip("Sets the boolean value.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            Player.isPlaying = value.Value;
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Get Axis

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis")]
    [TaskDescription("Gets the axis value of an Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxis : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxis(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Prev")]
    [TaskDescription("Gets the axis value of an Action during the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisPrev : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisPrev(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Delta")]
    [TaskDescription("Gets the change in axis value of an Action since the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisDelta : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisDelta(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Raw")]
    [TaskDescription("Gets the raw axis value of an Action. The raw value excludes any digital axis simulation modification by the Input Behavior assigned to this Action. This raw value is modified by deadzone and axis calibration settings in the controller. To get truly raw values, you must get the raw value directly from the Controller element.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisRaw : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisRaw(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Raw Prev")]
    [TaskDescription("Gets the raw axis value of an Action during the previous frame. The raw value excludes any digital axis simulation modification by the Input Behavior assigned to this Action. This raw value is modified by deadzone and axis calibration settings in the controller. To get truly raw values, you must get the raw value directly from the Controller element.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisRawPrev : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisRawPrev(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Raw Delta")]
    [TaskDescription("Gets the change in raw axis value of an Action since the previous frame. The raw value excludes any digital axis simulation modification by the Input Behavior assigned to this Action. This raw value is modified by dead zone and axis calibration settings in the controller. To get truly raw values, you must get the raw value directly from the Controller element.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisRawDelta : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisRawDelta(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Time Active")]
    [TaskDescription("Gets the length of time in seconds that an axis has been continuously active as calculated from the raw value. Returns 0 if the axis is not currently active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisTimeActive : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisTimeActive(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Time Inactive")]
    [TaskDescription("Gets the length of time in seconds that an axis has been inactive as calculated from the raw value. Returns 0 if the axis is currently active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisTimeInactive : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisTimeInactive(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Raw Time Active")]
    [TaskDescription("Gets the length of time in seconds that an axis has been continuously active as calculated from the raw value. Returns 0 if the axis is not currently active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisRawTimeActive : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisRawTimeActive(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis Raw Time Inctive")]
    [TaskDescription("Gets the length of time in seconds that an axis has been inactive as calculated from the raw value. Returns 0 if the axis is currently active.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxisRawTimeInactive : RewiredPlayerActionGetFloatBDAction {
        
        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxisRawTimeInactive(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis 2D")]
    [TaskDescription("Gets the axis value of two Actions.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxis2d : RewiredPlayerActionGetAxis2DBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxis2D(actionNameX.Value, actionNameY.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis 2D Prev")]
    [TaskDescription("Gets the axis value of two Actions during the previous frame. ")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxis2dPrev : RewiredPlayerActionGetAxis2DBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxis2DPrev(actionNameX.Value, actionNameY.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis 2D Raw")]
    [TaskDescription("Gets the raw axis value of two Actions. The raw value excludes any digital axis simulation modification by the Input Behavior assigned to this Action. This raw value is modified by deadzone and axis calibration settings in the controller. To get truly raw values, you must get the raw value directly from the Controller element.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxis2dRaw : RewiredPlayerActionGetAxis2DBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxis2DRaw(actionNameX.Value, actionNameY.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Axis 2D Raw Prev")]
    [TaskDescription("Gets the raw axis value of two Actions during the previous frame. The raw value excludes any digital axis simulation modification by the Input Behavior assigned to this Action. This raw value is modified by deadzone and axis calibration settings in the controller. To get truly raw values, you must get the raw value directly from the Controller element.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAxis2dRawPrev : RewiredPlayerActionGetAxis2DBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAxis2DRawPrev(actionNameX.Value, actionNameY.Value));
        }
    }

    #endregion

    #region Get Button

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button")]
    [TaskDescription("Gets the button held state of an Action. This will return TRUE as long as the button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButton : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButton(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Down")]
    [TaskDescription("Gets the button just pressed state of an Action. This will only return TRUE only on the first frame the button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to this Action. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonDown(actionName.Value));
        }

    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Up")]
    [TaskDescription("Get the button just released state for an Action. This will only return TRUE for the first frame the button is released. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Prev")]
    [TaskDescription("Gets the button held state of an Action during the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonPrev : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonPrev(actionName.Value));
        }
    }
    
    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Single Press Hold")]
    [TaskDescription(
        "Gets the button single pressed and held state of an Action. " +
        "This will return TRUE after a button is held and the double press timeout has expired. " +
        "This will never return TRUE if a double press occurs. " +
        "This method is delayed because it only returns TRUE after the double press timeout has expired. " +
        "Only use this method if you need to check for both a single press and a double press on the same Action. " +
        "Otherwise, use GetButton instead for instantaneous button press detection. " +
        "The double press speed is set in the Input Behavior assigned to the Action."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonSinglePressHold : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonSinglePressHold(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Single Press Down")]
    [TaskDescription(
        "Gets the button just single pressed and held state of an Action. " +
        "This will return TRUE for only the first frame after a button press and after the double press timeout has expired.. " +
        "This will never return TRUE if a double press occurs. " +
        "This method is delayed because it only returns TRUE after the double press timeout has expired. " +
        "Only use this method if you need to check for both a single press and a double press on the same Action. " +
        "Otherwise, use GetButtonDown instead for instantaneous button press detection. " +
        "The double press speed is set in the Input Behavior assigned to the Action."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonSinglePressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonSinglePressDown(actionName.Value));
        }
    }
    
    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Single Press Up")]
    [TaskDescription(
        "Gets the button single pressed and just released state of an Action. " +
        "This will return TRUE for only the first frame after the release of a single press. " +
        "This will never return TRUE if a double press occurs. " +
        "This method is delayed because it only returns TRUE after the double press timeout has expired. " +
        "Only use this method if you need to check for both a single press and a double press on the same Action. " +
        "Otherwise, use GetButtonUp instead for instantaneous button press detection. " +
        "The double press speed is set in the Input Behavior assigned to the Action."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonSinglePressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonSinglePressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Double Press Hold")]
    [TaskDescription("Gets the button double pressed and held state of an Action. This will return TRUE after a double press and the button is then held. The double press speed is set in the Input Behavior assigned to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonDoublePressHold : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonDoublePressHold(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Double Press Down")]
    [TaskDescription("Gets the button double pressed state of an Action. This will return TRUE only on the first frame of a double press. The double press speed is set in the Input Behavior assigned to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonDoublePressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonDoublePressDown(actionName.Value));
        }
    }
    
    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Double Press Up")]
    [TaskDescription("Gets the button double pressed and just released state of an Action. This will return TRUE only on the first frame after double press is released. The double press speed is set in the Input Behavior assigned to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonDoublePressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonDoublePressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Timed Press")]
    [TaskDescription("Gets the button held state of an Action after being held for a period of time. This will return TRUE only after the button has been held for the specified time and will continue to return TRUE until the button is released. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonTimedPress : RewiredPlayerActionGetBoolBDAction {
        
        [RequiredField]
        [Tooltip("Minimum time the button must be held before returning true.")]
        public SharedFloat time;

        [Tooltip("Time in seconds after activation that the press will expire. Once expired, it will no longer return true even if held. [0 = Never expire]")]
        public SharedFloat expireIn;

        public override void OnReset() {
            base.OnReset();
            time = 0.0f;
            expireIn = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonTimedPress(actionName.Value, time.Value, expireIn.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Timed Press Down")]
    [TaskDescription("Gets the button state of an Action after being held for a period of time. This will return TRUE only on the frame in which the button had been held for the specified time. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonTimedPressDown : RewiredPlayerActionGetBoolBDAction {

        [RequiredField]
        [Tooltip("Minimum time the button must be held before returning true.")]
        public SharedFloat time;

        public override void OnReset() {
            base.OnReset();
            time = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonTimedPressDown(actionName.Value, time.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Timed Press Up")]
    [TaskDescription("Gets the button state of an Action after being held for a period of time and then released. This will return TRUE only on the frame in which the button had been held for at least the specified time and then released. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonTimedPressUp : RewiredPlayerActionGetBoolBDAction {

        [RequiredField]
        [Tooltip("Minimum time the button must be held before returning true.")]
        public SharedFloat time;

        [Tooltip("Time in seconds after activation that the press will expire. Once expired, it will no longer return true even if held. [0 = Never expire]")]
        public SharedFloat expireIn;

        public override void OnReset() {
            base.OnReset();
            time = 0.0f;
            expireIn = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonTimedPressUp(actionName.Value, time.Value, expireIn.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Short Press")]
    [TaskDescription("Gets the button held state of an Action after being held for a period of time. This will return TRUE only after the button has been held for the specified time and will continue to return TRUE until the button is released. This also applies to axes being used as buttons. The button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetButtonTimedPress instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonShortPress : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonShortPress(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Short Press Down")]
    [TaskDescription("Gets the button state of an Action after being held for a period of time. This will return TRUE only on the frame in which the button had been held for the specified time. This also applies to axes being used as buttons. The button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetButtonTimedPressDown instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonShortPressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonShortPressDown(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Short Press Up")]
    [TaskDescription("Gets the button state of an Action after being held for a period of time and then released. This will return TRUE only on the frame in which the button had been held for at least the specified time and then released. This also applies to axes being used as buttons. The button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetButtonTimedPressUp instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonShortPressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonShortPressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Long Press")]
    [TaskDescription("Gets the button held state of an Action after being held for a period of time. This will return TRUE only after the button has been held for the specified time and will continue to return TRUE until the button is released. This also applies to axes being used as buttons. The button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetButtonTimedPress instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonLongPress : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonLongPress(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Long Press Down")]
    [TaskDescription("Gets the button state of an Action after being held for a period of time. This will return TRUE only on the frame in which the button had been held for the specified time. This also applies to axes being used as buttons. The button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetButtonTimedPressDown instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonLongPressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonLongPressDown(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Long Press Up")]
    [TaskDescription("Gets the button state of an Action after being held for a period of time and then released. This will return TRUE only on the frame in which the button had been held for at least the specified time and then released. This also applies to axes being used as buttons. The button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetButtonTimedPressUp instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonLongPressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonLongPressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Repeating")]
    [TaskDescription("Gets the repeating button state of an Action. " +
        "This will return TRUE when immediately pressed, then FALSE until the Input Behaviour button repeat delay has elapsed, " +
        "then TRUE for a 1-frame duration repeating at the interval specified in the Input Behavior assigned to the Action. " +
        "This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonRepeating : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonRepeating(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Time Pressed")]
    [TaskDescription("Gets the length of time in seconds that a button has been continuously held down. Returns 0 if the button is not currently pressed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonTimePressed : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonTimePressed(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Button Time Unpressed")]
    [TaskDescription("Gets the length of time in seconds that a button has not been pressed. Returns 0 if the button is currently pressed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetButtonTimeUnpressed : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetButtonTimeUnpressed(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Button")]
    [TaskDescription("Gets the button held state of all Actions. This will return TRUE as long as any button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyButton : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyButton());
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Button Down")]
    [TaskDescription("Gets the button just pressed state of all Actions. This will only return TRUE only on the first frame any button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to the Action. This will return TRUE each time any button is pressed even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyButtonDown : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyButtonDown());
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Button Up")]
    [TaskDescription("Get the button just released state for all Actions. This will only return TRUE for the first frame the button is released. This will return TRUE each time any button is released even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyButtonUp : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyButtonUp());
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Button Prev")]
    [TaskDescription("Gets the button held state of an any Action during the previous frame. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyButtonPrev : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyButtonPrev());
        }
    }

    #endregion

    #region Get Negative Button

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button")]
    [TaskDescription("Gets the negative button held state of an Action. This will return TRUE as long as the negative button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButton : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButton(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Down")]
    [TaskDescription("Gets the negative button just pressed state of an Action. This will only return TRUE only on the first frame the negative button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to this Action. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonDown(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Up")]
    [TaskDescription("Get the negative button just released state for an Action. This will only return TRUE for the first frame the negative button is released. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Prev")]
    [TaskDescription("Gets the negative button held state of an Action during the previous frame.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonPrev : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonPrev(actionName.Value));
        }
    }
    
    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Single Press Hold")]
    [TaskDescription(
        "Gets the negative button single pressed and held state of an Action. " +
        "This will return TRUE after a negative button is held and the double press timeout has expired. " +
        "This will never return TRUE if a double press occurs. " +
        "This method is delayed because it only returns TRUE after the double press timeout has expired. " +
        "Only use this method if you need to check for both a single press and a double press on the same Action. " +
        "Otherwise, use GetNegativeButton instead for instantaneous negative button press detection. " +
        "The double press speed is set in the Input Behavior assigned to the Action."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonSinglePressHold : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonSinglePressHold(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Single Press Down")]
    [TaskDescription(
        "Gets the negative button just single pressed and held state of an Action. " +
        "This will return TRUE for only the first frame after a negative button press and after the double press timeout has expired.. " +
        "This will never return TRUE if a double press occurs. " +
        "This method is delayed because it only returns TRUE after the double press timeout has expired. " +
        "Only use this method if you need to check for both a single press and a double press on the same Action. " +
        "Otherwise, use GetNegativeButtonDown instead for instantaneous negative button press detection. " +
        "The double press speed is set in the Input Behavior assigned to the Action."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonSinglePressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonSinglePressDown(actionName.Value));
        }
    }
    
    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Single Press Up")]
    [TaskDescription(
        "Gets the negative button single pressed and just released state of an Action. " +
        "This will return TRUE for only the first frame after the release of a single press. " +
        "This will never return TRUE if a double press occurs. " +
        "This method is delayed because it only returns TRUE after the double press timeout has expired. " +
        "Only use this method if you need to check for both a single press and a double press on the same Action. " +
        "Otherwise, use GetNegativeButtonUp instead for instantaneous negative button press detection. " +
        "The double press speed is set in the Input Behavior assigned to the Action."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonSinglePressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonSinglePressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Double Press Down")]
    [TaskDescription("Gets the negative button double pressed state of an Action. This will return TRUE only on the first frame of a double press. The double press speed is set in the Input Behavior assigned to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonDoublePressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonDoublePressDown(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Double Press Up")]
    [TaskDescription("Gets the negative button double pressed and just released state of an Action. This will return TRUE only on the first frame after double press is released. The double press speed is set in the Input Behavior assigned to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonDoublePressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonDoublePressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Double Press Hold")]
    [TaskDescription("Gets the negative button double pressed and held state of an Action. This will return TRUE after a double press and the negative button is then held. The double press speed is set in the Input Behavior assigned to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonDoublePressHold : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonDoublePressHold(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Timed Press")]
    [TaskDescription("Gets the negative button held state of an Action after being held for a period of time. This will return TRUE only after the negative button has been held for the specified time and will continue to return TRUE until the negative button is released. This also applies to axes being used as negative buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonTimedPress : RewiredPlayerActionGetBoolBDAction {

        [RequiredField]
        [Tooltip("Minimum time the negative button must be held before returning true.")]
        public SharedFloat time;

        [Tooltip("Time in seconds after activation that the press will expire. Once expired, it will no longer return true even if held. [0 = Never expire]")]
        public SharedFloat expireIn;

        public override void OnReset() {
            base.OnReset();
            time = 0.0f;
            expireIn = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonTimedPress(actionName.Value, time.Value, expireIn.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Timed Press Down")]
    [TaskDescription("Gets the negative button state of an Action after being held for a period of time. This will return TRUE only on the frame in which the negative button had been held for the specified time. This also applies to axes being used as negative buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonTimedPressDown : RewiredPlayerActionGetBoolBDAction {

        [RequiredField]
        [Tooltip("Minimum time the negative button must be held before returning true.")]
        public SharedFloat time;

        public override void OnReset() {
            base.OnReset();
            time = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonTimedPressDown(actionName.Value, time.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Timed Press Up")]
    [TaskDescription("Gets the negative button state of an Action after being held for a period of time and then released. This will return TRUE only on the frame in which the negative button had been held for at least the specified time and then released. This also applies to axes being used as negative buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonTimedPressUp : RewiredPlayerActionGetBoolBDAction {

        [RequiredField]
        [Tooltip("Minimum time the negative button must be held before returning true.")]
        public SharedFloat time;

        [Tooltip("Time in seconds after activation that the press will expire. Once expired, it will no longer return true even if held. [0 = Never expire]")]
        public SharedFloat expireIn;

        public override void OnReset() {
            base.OnReset();
            time = 0.0f;
            expireIn = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonTimedPressUp(actionName.Value, time.Value, expireIn.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Short Press")]
    [TaskDescription("Gets the negative button held state of an Action after being held for a period of time. This will return TRUE only after the negative button has been held for the specified time and will continue to return TRUE until the negative button is released. This also applies to axes being used as negative buttons. The negative button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetNegativeButtonTimedPress instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonShortPress : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonShortPress(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Short Press Down")]
    [TaskDescription("Gets the negative button state of an Action after being held for a period of time. This will return TRUE only on the frame in which the negative button had been held for the specified time. This also applies to axes being used as negative buttons. The negative button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetNegativeButtonTimedPressDown instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonShortPressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonShortPressDown(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Short Press Up")]
    [TaskDescription("Gets the negative button state of an Action after being held for a period of time and then released. This will return TRUE only on the frame in which the negative button had been held for at least the specified time and then released. This also applies to axes being used as negative buttons. The negative button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetNegativeButtonTimedPressUp instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonShortPressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonShortPressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Long Press")]
    [TaskDescription("Gets the negative button held state of an Action after being held for a period of time. This will return TRUE only after the negative button has been held for the specified time and will continue to return TRUE until the negative button is released. This also applies to axes being used as negative buttons. The negative button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetNegativeButtonTimedPress instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonLongPress : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonLongPress(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Long Press Down")]
    [TaskDescription("Gets the negative button state of an Action after being held for a period of time. This will return TRUE only on the frame in which the negative button had been held for the specified time. This also applies to axes being used as negative buttons. The negative button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetNegativeButtonTimedPressDown instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonLongPressDown : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonLongPressDown(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Long Press Up")]
    [TaskDescription("Gets the negative button state of an Action after being held for a period of time and then released. This will return TRUE only on the frame in which the negative button had been held for at least the specified time and then released. This also applies to axes being used as negative buttons. The negative button short press time is set in the Input Behavior assigned to the Action. For a custom duration, use GetNegativeButtonTimedPressUp instead.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonLongPressUp : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonLongPressUp(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Repeating")]
    [TaskDescription("Gets the repeating negative button state of an Action. " +
        "This will return TRUE when immediately pressed, then FALSE until the Input Behaviour button repeat delay has elapsed, " +
        "then TRUE for a 1-frame duration repeating at the interval specified in the Input Behavior assigned to the Action. " +
        "This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonRepeating : RewiredPlayerActionGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonRepeating(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Time Pressed")]
    [TaskDescription("Gets the length of time in seconds that a negative button has been continuously held down. Returns 0 if the negative button is not currently pressed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonTimePressed : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonTimePressed(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Negative Button Time Unpressed")]
    [TaskDescription("Gets the length of time in seconds that a negative button has not been pressed. Returns 0 if the negative button is currently pressed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetNegativeButtonTimeUnpressed : RewiredPlayerActionGetFloatBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetNegativeButtonTimeUnpressed(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Negative Button")]
    [TaskDescription("Gets the negative button held state of all Actions. This will return TRUE as long as any negative button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyNegativeButton : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyNegativeButton());
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Negative Button Down")]
    [TaskDescription("Gets the negative button just pressed state of all Actions. This will only return TRUE only on the first frame any negative button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to the Action. This will return TRUE each time any negative button is pressed even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyNegativeButtonDown : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyNegativeButtonDown());
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Negative Button Up")]
    [TaskDescription("Get the negative button just released state for all Actions. This will only return TRUE for the first frame the negative button is released. This will return TRUE each time any negative button is released even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyNegativeButtonUp : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyNegativeButtonUp());
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Get Any Negative Button Prev")]
    [TaskDescription("Gets the negative button held state of an any Action during the previous frame. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetAnyNegativeButtonPrev : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(Player.GetAnyNegativeButtonPrev());
        }
    }

    #endregion

    #region Vibration

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Set All Controller Vibration")]
    [TaskDescription("Sets vibration level for a motor at a specified index on controllers assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerSetAllControllerVibration : RewiredPlayerBDAction {

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
            if(motorIndex.Value < 0) return TaskStatus.Failure;
            motorLevel.Value = Mathf.Clamp01(motorLevel.Value);

            int joystickCount = Player.controllers.joystickCount;
            IList<Joystick> joysticks = Player.controllers.Joysticks;
            for(int i = 0; i < joystickCount; i++) {
                Joystick joystick = joysticks[i];
                if(!joystick.supportsVibration) continue;
                if(motorIndex.Value >= joystick.vibrationMotorCount) continue;
                joystick.SetVibration(motorIndex.Value, motorLevel.Value, duration.Value, stopOtherMotors.Value);
            }
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Stop All Controller Vibration")]
    [TaskDescription("Stops vibration on all controllers assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerStopAllControllerVibration : RewiredPlayerBDAction {

        protected override TaskStatus DoUpdate() {
            int joystickCount = Player.controllers.joystickCount;
            IList<Joystick> joysticks = Player.controllers.Joysticks;
            for(int i = 0; i < joystickCount; i++) {
                Joystick joystick = joysticks[i];
                if(!joystick.supportsVibration) continue;
                joystick.StopVibration();
            }
            return TaskStatus.Success;
        }
    }

    #endregion

    #region ControllerHelper

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Has Mouse")]
    [TaskDescription("Is the mouse assigned to this Player?")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetHasMouse : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.controllers.hasMouse;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Set Has Mouse")]
    [TaskDescription("Sets whether the mouse is assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerSetHasMouse : RewiredPlayerSetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            Player.controllers.hasMouse = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Joystick Count")]
    [TaskDescription("Gets the number of joysticks assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetJoystickCount : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.controllers.joystickCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Joysticks")]
    [TaskDescription("Gets a list of Joysticks assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetJoysticks : RewiredPlayerGetRewiredObjectListBDAction {

        protected override TaskStatus DoUpdate() {
            IList<Joystick> joysticks = Player.controllers.Joysticks;
            int count = joysticks != null ? joysticks.Count : 0;

            for(int i = 0; i < count; i++) {
                workingList.Add(joysticks[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Custom Controller Count")]
    [TaskDescription("The number of Custom Controllers assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetCustomControllerCount : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.controllers.customControllerCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Custom Controllers")]
    [TaskDescription("Gets a list of Custom Controllers assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetCustomControllers : RewiredPlayerGetRewiredObjectListBDAction {

        protected override TaskStatus DoUpdate() {
            IList<CustomController> customControllers = Player.controllers.CustomControllers;
            int count = customControllers != null ? customControllers.Count : 0;

            for(int i = 0; i < count; i++) {
                workingList.Add(customControllers[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Exclude From Controller Auto Assignment")]
    [TaskDescription("Gets whether controllers can be auto-assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetExcludeFromControllerAutoAssignment : RewiredPlayerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(Player.controllers.excludeFromControllerAutoAssignment);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Set Exclude From Controller Auto Assignment")]
    [TaskDescription("Sets whether controllers can be auto-assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerSetExcludeFromControllerAutoAssignment : RewiredPlayerSetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            Player.controllers.excludeFromControllerAutoAssignment = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Add Controller")]
    [TaskDescription("Assign a controller to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAddController : RewiredPlayerControllerBDAction {

        [Tooltip("Unassign this controller from other Players?")]
        public SharedBool removeFromOtherPlayers = true;

        public override void OnReset() {
            base.OnReset();
            removeFromOtherPlayers = true;
        }

        protected override TaskStatus DoUpdate() {
            Player.controllers.AddController(Controller, removeFromOtherPlayers.Value);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Remove Controller")]
    [TaskDescription("Unassign a controller from this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerRemoveController : RewiredPlayerControllerBDAction {

        protected override TaskStatus DoUpdate() {
            Player.controllers.RemoveController(Controller);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Remove Controllers")]
    [TaskDescription("Unassign controllers from this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerRemoveControllers : RewiredPlayerBDAction {

        [Tooltip("Remove only controllers of a certain type. If false, all assignable controllers will be removed.")]
        public SharedBool byControllerType;

        [Tooltip("Controller type to remove from Player. Not used if Clear By Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Joystick;

        public override void OnReset() {
            base.OnReset();
            byControllerType = false;
            controllerType = ControllerType.Joystick;
        }

        protected override TaskStatus DoUpdate() {
            if(byControllerType.Value) {
                Player.controllers.ClearControllersOfType(controllerType);
            } else {
                Player.controllers.ClearAllControllers();
            }
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Contains Controller")]
    [TaskDescription("Checks if a controller is assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerContainsController : RewiredPlayerControllerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected override TaskStatus DoUpdate() {
            storeValue.Value = Player.controllers.ContainsController(controllerType, controllerId.Value);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Controllers")]
    [TaskName("Player Get Last Active Controller Type")]
    [TaskDescription("Get the last controller type that contributed input through the Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetLastActiveControllerType : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            Controller controller = Player.controllers.GetLastActiveController();
            if(controller == null) {
                storeValue.Value = 0;
                return TaskStatus.Failure;
            }
            storeValue.Value = (int)controller.type;
            
            return TaskStatus.Success;
        }
    }

    #endregion

    #region MapHelper

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Clear Controller Maps")]
    [TaskDescription("Removes all controller maps or maps of a specific type.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerClearMaps : RewiredPlayerBDAction {

        [Tooltip("If this is true, all maps for the controller type set in Controller Type will be cleared. If this is false, all maps will be cleared.")]
        public SharedBool byControllerType;

        [Tooltip("Clear maps of a particular controller type. Not used if Set By Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        [Tooltip("If true, only maps that are flagged user-assignable will be cleared, otherwise all maps will be cleared.")]
        public SharedBool userAssignableOnly;

        public override void OnReset() {
            base.OnReset();
            byControllerType = false;
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            if(byControllerType.Value) {
                Player.controllers.maps.ClearMaps(controllerType, userAssignableOnly.Value);
            } else {
                Player.controllers.maps.ClearAllMaps(userAssignableOnly.Value);
            }
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Load Default Controller Maps")]
    [TaskDescription("Loads the maps defined in the Rewired Editor and assigned to this player for the specified controller type. All existing maps will be cleared and replaced with the default maps. The Enabled state of each map will attempt to be preserved, but if you have added or removed maps through scripting, the result may not be as expected and you should set the Enabled states manually.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLoadDefaultControllerMaps : RewiredPlayerBDAction {

        [Tooltip("Default maps will be loaded for all controllers of this controller type.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        public override void OnReset() {
            base.OnReset();
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            Player.controllers.maps.LoadDefaultMaps(controllerType);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Load Controller Map")]
    [TaskDescription("Loads a controller map from the maps defined in the Rewired Editor. Replaces if a map already exists with the same category and layout.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLoadControllerMap : RewiredPlayerBDAction {

        [Tooltip("Controller type.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        [Tooltip("Controller id - Get this from the Controller.id property. For Keyboard and Mouse, just use 0.")]
        public SharedInt controllerId = 0;

        [Tooltip("Category name")]
        public SharedString categoryName;

        [Tooltip("Layout name")]
        public SharedString layoutName;

        [Tooltip("Start this map enabled?")]
        public SharedBool startEnabled = true;

        public override void OnReset() {
            base.OnReset();
            controllerType = ControllerType.Keyboard;
            controllerId = 0;
            categoryName = string.Empty;
            layoutName = string.Empty;
            startEnabled = true;
        }

        protected override TaskStatus DoUpdate() {
            Player.controllers.maps.LoadMap(controllerType, controllerId.Value, categoryName.Value, layoutName.Value, startEnabled.Value);
            return TaskStatus.Success;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(ReInput.controllers.GetController(controllerType, controllerId.Value) == null) {
                return false;
            }

            if(ReInput.mapping.GetActionCategory(categoryName.Name) == null) {
                Debug.Log(categoryName.Value + " is not a valid Map Category.");
                return false;
            }

            if(ReInput.mapping.GetLayout(controllerType, layoutName.Name) == null) {
                Debug.Log(layoutName.Value + " is not a valid Layout for controller type " + controllerType.ToString());
                return false;
            }

            return true;
        }
    }

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Load Controller Map")]
    [TaskDescription("Removes a controller map for a specific controller.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerRemoveControllerMap : RewiredPlayerBDAction {

        [Tooltip("Controller type.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        [Tooltip("Controller id - Get this from the Controller.id property. For Keyboard and Mouse, just use 0.")]
        public SharedInt controllerId = 0;

        [Tooltip("Category name")]
        public SharedString categoryName;

        [Tooltip("Layout name")]
        public SharedString layoutName;

        public override void OnReset() {
            base.OnReset();
            controllerType = ControllerType.Keyboard;
            controllerId = 0;
            categoryName = string.Empty;
            layoutName = string.Empty;
        }

        protected override TaskStatus DoUpdate() {
            Player.controllers.maps.RemoveMap(controllerType, controllerId.Value, categoryName.Value, layoutName.Value);
            return TaskStatus.Success;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(ReInput.controllers.GetController(controllerType, controllerId.Value) == null) {
                return false;
            }

            if(ReInput.mapping.GetActionCategory(categoryName.Name) == null) {
                Debug.Log(categoryName.Value + " is not a valid Map Category.");
                return false;
            }

            if(ReInput.mapping.GetLayout(controllerType, layoutName.Name) == null) {
                Debug.Log(layoutName.Value + " is not a valid Layout for controller type " + controllerType.ToString());
                return false;
            }

            return true;
        }
    }

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Set Controller Maps Enabled")]
    [TaskDescription("Set the enabled state in all maps in a particular category and/or layout.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerSetControllerMapsEnabled : RewiredPlayerBDAction {

        [Tooltip("Set the enabled state.")]
        public SharedBool enabledState;

        [Tooltip("The Controller Map category name.")]
        public SharedString categoryName;

        [Tooltip("The Controller Map layout name. [Optional]")]
        public SharedString layoutName;

        [Tooltip("Set the enabled state of maps for a particular controller type.")]
        public SharedBool byControllerType;

        [Tooltip("Set the enabled state of maps for a particular controller type. Not used if Set By Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Joystick;

        public override void OnReset() {
            base.OnReset();
            categoryName = string.Empty;
            layoutName = string.Empty;
            byControllerType = false;
            controllerType = ControllerType.Joystick;
        }

        protected override TaskStatus DoUpdate() {
            SetMapsEnabled();
            return TaskStatus.Success;
        }

        private void SetMapsEnabled() {
            if(byControllerType.Value) {
                SetMapsEnabled(enabledState.Value, controllerType, categoryName.Value, layoutName.Value);
            } else {
                SetMapsEnabled(enabledState.Value, categoryName.Value, layoutName.Value);
            }
        }

        private void SetMapsEnabled(bool state, ControllerType controllerType, string categoryName, string layoutName) {
            if(string.IsNullOrEmpty(layoutName)) Player.controllers.maps.SetMapsEnabled(state, controllerType, categoryName);
            else Player.controllers.maps.SetMapsEnabled(state, controllerType, categoryName, layoutName);
        }

        private void SetMapsEnabled(bool state, string categoryName, string layoutName) {
            if(string.IsNullOrEmpty(layoutName)) Player.controllers.maps.SetMapsEnabled(state, categoryName);
            else Player.controllers.maps.SetMapsEnabled(state, categoryName, layoutName);
        }
    }

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Set All Controller Maps Enabled")]
    [TaskDescription("Set the enabled state in all controller maps.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerSetAllControllerMapsEnabled : RewiredPlayerBDAction {

        [Tooltip("Set the enabled state.")]
        public SharedBool enabledState;

        [Tooltip("Set the enabled state of maps for a particular controller type.")]
        public SharedBool byControllerType;

        [Tooltip("Set the enabled state of maps for a particular controller type. Not used if Set By Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Joystick;

        public override void OnReset() {
            base.OnReset();
            byControllerType = false;
            controllerType = ControllerType.Joystick;
        }

        protected override TaskStatus DoUpdate() {
            SetMapsEnabled();
            return TaskStatus.Success;
        }

        private void SetMapsEnabled() {
            if(byControllerType.Value) {
                Player.controllers.maps.SetAllMapsEnabled(enabledState.Value, controllerType);
            } else {
                Player.controllers.maps.SetAllMapsEnabled(enabledState.Value);
            }
        }
    }

    [TaskCategory(Consts.taskCategory_player_maps)]
    [TaskName("Player Get First Element Map With Action")]
    [TaskDescription("Get the first ActionElementMap id for that contains a specific Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerGetFirstElementMapWithAction : RewiredPlayerActionGetRewiredObjectBDAction {

        [Tooltip("Get element map for only controllers of a certain type. If false, all controller types will be used.")]
        public SharedBool byControllerType;

        [Tooltip("Controller type. Ignored if By Controller Type is False.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        [Tooltip("Get the element map for a controller with a particular controller id. If false, only the Controller Type will be used. Ignored if By Controller Type is False.")]
        public SharedBool byControllerId;

        [Tooltip("Controller id - Get this from the Controller.id property. For Keyboard and Mouse, just use 0. Ignored if By Controller Type or By Controller Id is False.")]
        public SharedInt controllerId;

        [Tooltip("Get element map for only controller elements of a certain type. If false, all controller element types will be used.")]
        public SharedBool byElementType;

        [Tooltip("Get element map for a specific controller element type.")]
        public ControllerElementType elementType = ControllerElementType.Button;

        [Tooltip("Skip disabled Controller Maps?")]
        public SharedBool skipDisabledMaps;

        public override void OnReset() {
            base.OnReset();
            byElementType = false;
            elementType = ControllerElementType.Button;
            byControllerType = false;
            controllerType = ControllerType.Keyboard;
            byControllerId = false;
            controllerId = 0;
            skipDisabledMaps = true;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(byControllerType.Value && byControllerId.Value) {
                if(ReInput.controllers.GetController((ControllerType)controllerType, controllerId.Value) == null) {
                    return false;
                }
            }

            return true;
        }

        protected override TaskStatus DoUpdate() {
            return Go();
        }

        private TaskStatus Go() {
            ActionElementMap aem = null;

            if(byControllerType.Value) {
                if(byControllerId.Value) {
                    aem = GetFirstElementMapWithAction(controllerType, actionName.Value, skipDisabledMaps.Value);
                } else {
                    aem = GetFirstElementMapWithAction(controllerType, controllerId.Value, actionName.Value, skipDisabledMaps.Value);
                }
            } else {
                aem = GetFirstElementMapWithAction(actionName.Value, skipDisabledMaps.Value);
            }

            storeValue.Value = new RewiredObject(aem);
            return aem != null ? TaskStatus.Success : TaskStatus.Failure;
        }

        private ActionElementMap GetFirstElementMapWithAction(ControllerType controllerType, string actionName, bool skipDisabledMaps) {
            if(byElementType.Value) {
                switch(elementType) {
                    case ControllerElementType.Axis:
                        return Player.controllers.maps.GetFirstAxisMapWithAction(controllerType, actionName, skipDisabledMaps);
                    case ControllerElementType.Button:
                        return Player.controllers.maps.GetFirstButtonMapWithAction(controllerType, actionName, skipDisabledMaps);
                    case ControllerElementType.CompoundElement:
                    default:
                        Debug.LogWarning(elementType.ToString() + " is not a supported Controller Element Type.");
                        return null;
                }
            } else {
                return Player.controllers.maps.GetFirstElementMapWithAction(controllerType, actionName, skipDisabledMaps);
            }
        }

        private ActionElementMap GetFirstElementMapWithAction(ControllerType controllerType, int controllerId, string actionName, bool skipDisabledMaps) {
            var controller = ReInput.controllers.GetController(controllerType, controllerId);
            if(controller == null) return null;
            if(byElementType.Value) {
                switch(elementType) {
                    case ControllerElementType.Axis:
                        return Player.controllers.maps.GetFirstAxisMapWithAction(controller, actionName, skipDisabledMaps);
                    case ControllerElementType.Button:
                        return Player.controllers.maps.GetFirstButtonMapWithAction(controller, actionName, skipDisabledMaps);
                    case ControllerElementType.CompoundElement:
                    default:
                        Debug.LogWarning(elementType.ToString() + " is not a supported Controller Element Type.");
                        return null;
                }
            } else {
                return Player.controllers.maps.GetFirstElementMapWithAction(controller, actionName, skipDisabledMaps);
            }
        }

        private ActionElementMap GetFirstElementMapWithAction(string actionName, bool skipDisabledMaps) {
            if(byElementType.Value) {
                switch(elementType) {
                    case ControllerElementType.Axis:
                        return Player.controllers.maps.GetFirstAxisMapWithAction(actionName, skipDisabledMaps);
                    case ControllerElementType.Button:
                        return Player.controllers.maps.GetFirstButtonMapWithAction(actionName, skipDisabledMaps);
                    case ControllerElementType.CompoundElement:
                    default:
                        Debug.LogWarning(elementType.ToString() + " is not a supported Controller Element Type.");
                        return null;
                }
            } else {
                return Player.controllers.maps.GetFirstElementMapWithAction(actionName, skipDisabledMaps);
            }
        }
    }

    #endregion

    #region Input Behaviors

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Digital Axis Simulation")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetDigitalAxisSimulation : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.digitalAxisSimulation;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Digital Axis Gravity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetDigitalAxisGravity : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.digitalAxisGravity;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Digital Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetDigitalAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.digitalAxisSensitivity;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Digital Axis Snap")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetDigitalAxisSnap : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.digitalAxisSnap;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Digital Axis Instant Reverse")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetDigitalAxisInstantReverse : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.digitalAxisInstantReverse;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Joystick Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetJoystickAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.joystickAxisSensitivity;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Custom Controller Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetCustomControllerAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.customControllerAxisSensitivity;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Mouse XY Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetMouseXYAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.mouseXYAxisSensitivity;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Mouse Other Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetMouseOtherAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.mouseOtherAxisSensitivity;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Mouse Other Axis Mode")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetMouseOtherAxisMode : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = (int)Behavior.mouseOtherAxisMode;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Mouse XY Axis Delta Calc")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetMouseXYAxisDeltaCalc : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = (int)Behavior.mouseXYAxisDeltaCalc;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Mouse XY Axis Mode")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetMouseXYAxisMode : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = (int)Behavior.mouseXYAxisMode;
            return TaskStatus.Success;
        }
    }


    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Dead Zone")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonDeadZone : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonDeadZone;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Double Press Speed")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonDoublePressSpeed : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonDoublePressSpeed;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Short Press Time")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonShortPressTime : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonShortPressTime;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Short Press Expires In")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonShortPressExpiresIn : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonShortPressExpiresIn;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Long Press Time")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonLongPressTime : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonLongPressTime;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Long Press Expires In")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonLongPressExpiresIn : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonLongPressExpiresIn;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Get Button Down Buffer")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorGetButtonDownBuffer : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            storeValue.Value = Behavior.buttonDownBuffer;
            return TaskStatus.Success;
        }
    }

    // Set

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Digital Axis Simulation")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetDigitalAxisSimulation : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.digitalAxisSimulation = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Digital Axis Gravity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetDigitalAxisGravity : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.digitalAxisGravity = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Digital Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetDigitalAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.digitalAxisSensitivity = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Digital Axis Snap")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetDigitalAxisSnap : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.digitalAxisSnap = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Digital Axis Instant Reverse")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetDigitalAxisInstantReverse : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.digitalAxisInstantReverse = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Joystick Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetJoystickAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.joystickAxisSensitivity = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Custom Controller Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetCustomControllerAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.customControllerAxisSensitivity = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Mouse XY Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetMouseXYAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.mouseXYAxisSensitivity = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Mouse Other Axis Sensitivity")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetMouseOtherAxisSensitivity : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.mouseOtherAxisSensitivity = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Mouse Other Axis Mode")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetMouseOtherAxisMode : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt value;

        public override void OnReset() {
            base.OnReset();
            value = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.mouseOtherAxisMode = (MouseOtherAxisMode)value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Mouse XY Axis Delta Calc")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetMouseXYAxisDeltaCalc : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt value;

        public override void OnReset() {
            base.OnReset();
            value = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.mouseXYAxisDeltaCalc = (MouseXYAxisDeltaCalc)value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Mouse XY Axis Mode")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetMouseXYAxisMode : RewiredPlayerInputBehaviorBDAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt value;

        public override void OnReset() {
            base.OnReset();
            value = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.mouseXYAxisMode = (MouseXYAxisMode)value.Value;
            return TaskStatus.Success;
        }
    }


    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Dead Zone")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonDeadZone : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonDeadZone = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Double Press Speed")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonDoublePressSpeed : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonDoublePressSpeed = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Short Press Time")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonShortPressTime : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonShortPressTime = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Short Press Expires In")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonShortPressExpiresIn : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonShortPressExpiresIn = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Long Press Time")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonLongPressTime : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonLongPressTime = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Long Press Expires In")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonLongPressExpiresIn : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonLongPressExpiresIn = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/InputBehaviors")]
    [TaskName("Player Input Behavior Set Button Down Buffer")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerInputBehaviorSetButtonDownBuffer : RewiredPlayerInputBehaviorBDAction {

        [Tooltip("Set the value.")]
        public SharedFloat value;

        public override void OnReset() {
            base.OnReset();
            value = 0.0f;
        }

        protected override TaskStatus DoUpdate() {
            if(Behavior == null) return TaskStatus.Failure;
            Behavior.buttonDownBuffer = value.Value;
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Layout Manager

    #region Layout Manager

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Get Enabled")]
    [TaskDescription(
        "If enabled, loaded Controller Maps will be evaluated when Controllers are assigned, after saved data is loaded, etc. " +
        "Changes to Controller Maps will be applied immediately in the Player when enabled."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerGetEnabled : RewiredPlayerLayoutManagerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(LayoutManager.enabled);
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Set Enabled")]
    [TaskDescription(
        "If enabled, loaded Controller Maps will be evaluated when Controllers are assigned, after saved data is loaded, etc. " +
        "Changes to Controller Maps will be applied immediately in the Player when enabled."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerSetEnabled : RewiredPlayerLayoutManagerSetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            value.Value = LayoutManager.enabled;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Get Load From User Data Store")]
    [TaskDescription(
        "If enabled, Controller Maps will be loaded from UserDataStore (if available) instead of from the Rewired Input Manager " +
        "defaults. If no matching Controller Map is found in UserDataStore, the Rewired Input Manager default will be loaded. " +
        "Note: The UserDataStore implementation must implement IControllerMapStore to be used."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerGetLoadFromUserDataStore : RewiredPlayerLayoutManagerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(LayoutManager.loadFromUserDataStore);
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Set Load From User Data Store")]
    [TaskDescription(
        "If enabled, Controller Maps will be loaded from UserDataStore (if available) instead of from the Rewired Input Manager " +
        "defaults. If no matching Controller Map is found in UserDataStore, the Rewired Input Manager default will be loaded. " +
        "Note: The UserDataStore implementation must implement IControllerMapStore to be used."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerSetLoadFromUserDataStore : RewiredPlayerLayoutManagerSetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            value.Value = LayoutManager.loadFromUserDataStore;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Apply")]
    [TaskDescription(
        "Applies settings to Controller Maps in the Player. " +
        "This must be called if you make changes to anything in Rule Sets in " +
        "order for those changes to be applied to the Player's Controller Maps."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerApply : RewiredPlayerLayoutManagerBDAction {

        protected override TaskStatus DoUpdate() {
            LayoutManager.Apply();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Load Defaults")]
    [TaskDescription("Loads the default settings from the Rewired Input Manager.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerLoadDefaults : RewiredPlayerLayoutManagerBDAction {

        protected override TaskStatus DoUpdate() {
            LayoutManager.LoadDefaults();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Get Rule Sets")]
    [TaskDescription(
        "The list of rule sets. " +
        "When Apply is called (whether manually or on various events which trigger it), each rule set in the list " +
        "will be evaluated and Controller Maps for the Controller(s) specified in the rule properties will be loaded or removed. " +
        "After modifying or replacing the list, you must call Apply for the changes to take effect in the Player. "
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerGetRuleSets : RewiredPlayerLayoutManagerGetRewiredObjectListBDAction {

        protected override TaskStatus DoUpdate() {
            var ruleSets = Player.controllers.maps.layoutManager.ruleSets;
            int count = ruleSets != null ? ruleSets.Count : 0;

            for (int i = 0; i < count; i++) {
                workingList.Add(ruleSets[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_layoutManager)]
    [TaskName("Player Layout Manager Get Rule Set")]
    [TaskDescription(
        "Gets the specified Layout Manager Rule Set."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerLayoutManagerGetRuleSet : RewiredPlayerLayoutManagerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a variable.")]
        public SharedRewiredObject storeValue;

        [Tooltip("The Tag of the Rule Set.")]
        public SharedString tag;

        protected ControllerMapLayoutManager.RuleSet RuleSet {
            get {
                if (Player == null) return null;
                return Player.controllers.maps.layoutManager.ruleSets.Find(x => string.Equals(x.tag, tag.Value, StringComparison.Ordinal));
            }
        }

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObject();
            tag = string.Empty;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(new RewiredObject(RuleSet));
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            return RuleSet != null;
        }

        protected TaskStatus UpdateStoreValue(RewiredObject newValue) {
            if (newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }
            return TaskStatus.Success;
        }
    }

    #endregion

    #region LayoutManager.RuleSet

    #endregion

    #region LayoutManager.Rule

    #endregion

    #endregion

    #region Map Enabler

    #region Map Enabler

    [TaskCategory(Consts.taskCategory_player_mapEnabler)]
    [TaskName("Player Map Enabler Get Enabled")]
    [TaskDescription(
        "If enabled, loaded Controller Maps will be evaluated when Controllers are assigned, after saved data is loaded, etc. " +
        "Changes to Controller Maps will be applied immediately in the Player when enabled."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerMapEnablerGetEnabled : RewiredPlayerMapEnablerGetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(MapEnabler.enabled);
        }
    }

    [TaskCategory(Consts.taskCategory_player_mapEnabler)]
    [TaskName("Player Map Enabler Set Enabled")]
    [TaskDescription(
        "If enabled, loaded Controller Maps will be evaluated when Controllers are assigned, after saved data is loaded, etc. " +
        "Changes to Controller Maps will be applied immediately in the Player when enabled."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerMapEnablerSetEnabled : RewiredPlayerMapEnablerSetBoolBDAction {

        protected override TaskStatus DoUpdate() {
            value.Value = MapEnabler.enabled;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_mapEnabler)]
    [TaskName("Player Map Enabler Apply")]
    [TaskDescription(
        "Applies settings to Controller Maps in the Player. " +
        "This must be called if you make changes to anything in Rule Sets in " +
        "order for those changes to be applied to the Player's Controller Maps."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerMapEnablerApply : RewiredPlayerMapEnablerBDAction {

        protected override TaskStatus DoUpdate() {
            MapEnabler.Apply();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_mapEnabler)]
    [TaskName("Player Map Enabler Load Defaults")]
    [TaskDescription("Loads the default settings from the Rewired Input Manager.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerMapEnablerLoadDefaults : RewiredPlayerMapEnablerBDAction {

        protected override TaskStatus DoUpdate() {
            MapEnabler.LoadDefaults();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_mapEnabler)]
    [TaskName("Player Map Enabler Get Rule Sets")]
    [TaskDescription(
        "The list of rule sets. " +
        "When Apply is called (whether manually or on various events which trigger it), each rule set in the list " +
        "will be evaluated and Controller Maps for the Controller(s) specified in the rule properties will be loaded or removed. " +
        "After modifying or replacing the list, you must call Apply for the changes to take effect in the Player. "
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerMapEnablerGetRuleSets : RewiredPlayerMapEnablerGetRewiredObjectListBDAction {

        protected override TaskStatus DoUpdate() {
            var ruleSets = Player.controllers.maps.mapEnabler.ruleSets;
            int count = ruleSets != null ? ruleSets.Count : 0;

            for (int i = 0; i < count; i++) {
                workingList.Add(ruleSets[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_player_mapEnabler)]
    [TaskName("Player Map Enabler Get Rule Set")]
    [TaskDescription(
        "Gets the specified Map Enabler Rule Set."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerMapEnablerGetRuleSet : RewiredPlayerMapEnablerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a variable.")]
        public SharedRewiredObject storeValue;

        [Tooltip("The Tag of the Rule Set.")]
        public SharedString tag;

        protected ControllerMapEnabler.RuleSet RuleSet {
            get {
                if (Player == null) return null;
                return Player.controllers.maps.mapEnabler.ruleSets.Find(x => string.Equals(x.tag, tag.Value, StringComparison.Ordinal));
            }
        }

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObject();
            tag = string.Empty;
        }

        protected override TaskStatus DoUpdate() {
            return UpdateStoreValue(new RewiredObject(RuleSet));
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            return RuleSet != null;
        }

        protected TaskStatus UpdateStoreValue(RewiredObject newValue) {
            if (newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }
            return TaskStatus.Success;
        }
    }

    #endregion

    #endregion

    // Conditionals

    #region Button States

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Button Just Pressed")]
    [TaskDescription("Evaluates the button just pressed state of an Action. This will only return TRUE only on the first frame the button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to this Action. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerButtonJustPressed : RewiredPlayerActionBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetButtonDown(actionName.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Button Just Released")]
    [TaskDescription("Evaluates the button just released state for an Action. This will only return TRUE for the first frame the button is released. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerButtonJustReleased : RewiredPlayerActionBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetButtonUp(actionName.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Button Held")]
    [TaskDescription("Evaluates the button held state of an Action. This will return TRUE as long as the button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerButtonHeld : RewiredPlayerActionBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetButton(actionName.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Negative Button Just Pressed")]
    [TaskDescription("Evaluates the negative button just pressed state of an Action. This will only return TRUE only on the first frame the negative button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to this Action. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerNegativeButtonJustPressed : RewiredPlayerActionBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetNegativeButtonDown(actionName.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Negative Button Just Released")]
    [TaskDescription("Evaluates the negative button just released state for an Action. This will only return TRUE for the first frame the negative button is released. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerNegativeButtonJustReleased : RewiredPlayerActionBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetNegativeButtonUp(actionName.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Negative Button Held")]
    [TaskDescription("Evaluates the negative button held state of an Action. This will return TRUE as long as the negative button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerNegativeButtonHeld : RewiredPlayerActionBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetNegativeButton(actionName.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Button Just Pressed")]
    [TaskDescription("Evaluates the button just pressed state of all Actions. This will only return TRUE only on the first frame any button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to the Action. This will return TRUE each time any button is pressed even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyButtonJustPressed : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyButtonDown() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Button Just Released")]
    [TaskDescription("Evaluates the button just released state for all Actions. This will only return TRUE for the first frame the button is released. This will return TRUE each time any button is released even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyButtonJustReleased : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyButtonUp() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Button Held")]
    [TaskDescription("Evaluates the button held state of all Actions. This will return TRUE as long as any button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyButtonHeld : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyButton() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Button Held Prev")]
    [TaskDescription("Evaluates the button held state of an any Action during the previous frame. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyButtonHeldPrev : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyButtonPrev() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Negative Button Just Pressed")]
    [TaskDescription("Evaluates the button just pressed state of all Actions. This will only return TRUE only on the first frame any button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to the Action. This will return TRUE each time any button is pressed even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyNegativeButtonJustPressed : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyNegativeButtonDown() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Negative Button Just Released")]
    [TaskDescription("Evaluates the button just released state for all Actions. This will only return TRUE for the first frame the button is released. This will return TRUE each time any button is released even if others are being held down. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyNegativeButtonJustReleased : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyNegativeButtonUp() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Negative Button Held")]
    [TaskDescription("Evaluates the negative button held state of all Actions. This will return TRUE as long as any negative button is held. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyNegativeButtonHeld : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyNegativeButton() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Any Negative Button Held Prev")]
    [TaskDescription("Evaluates the negative button held state of an any Action during the previous frame. This also applies to axes being used as buttons.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerAnyNegativeButtonHeldPrev : RewiredPlayerBDConditional {

        protected override TaskStatus DoUpdate() {
            return Player.GetAnyNegativeButtonPrev() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

    #endregion

    #region Axis States

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Compare Axis")]
    [TaskDescription("Evaluates the the axis value of an Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerCompareAxis : RewiredPlayerActionCompareFloatBDConditional {

        protected override TaskStatus DoUpdate() {
            return Compare(Player.GetAxis(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Compare Axis Prev")]
    [TaskDescription("Evaluates the the previous axis value of an Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerCompareAxisPrev : RewiredPlayerActionCompareFloatBDConditional {

        protected override TaskStatus DoUpdate() {
            return Compare(Player.GetAxisPrev(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Compare Axis Raw")]
    [TaskDescription("Evaluates the the raw axis value of an Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerCompareAxisRaw : RewiredPlayerActionCompareFloatBDConditional {

        protected override TaskStatus DoUpdate() {
            return Compare(Player.GetAxisRaw(actionName.Value));
        }
    }

    [TaskCategory(Consts.taskCategory_player + "/Input")]
    [TaskName("Player Compare Axis Raw Prev")]
    [TaskDescription("Evaluates the the previous raw axis value of an Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerCompareAxisRawPrev : RewiredPlayerActionCompareFloatBDConditional {

        protected override TaskStatus DoUpdate() {
            return Compare(Player.GetAxisRawPrev(actionName.Value));
        }
    }

    #endregion

    #region Event

    [TaskCategory(Consts.taskCategory_player_events)]
    [TaskName("Player Controller Added Event")]
    [TaskDescription("Event triggered when a controller is assigned to this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerControllerAddedEvent : RewiredPlayerBDConditional {

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerId = -1;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerType = 0;

        private bool hasEvent = false;

        public override void OnAwake() {
            base.OnAwake();
            if (Player != null) {
                Player.controllers.ControllerAddedEvent += OnControllerAdded;
            }
        }

        public override void OnReset() {
            base.OnReset();
            storeControllerId = -1;
            storeControllerType = 0;
            hasEvent = false;
        }

        public override void OnBehaviorComplete() {
            if (Player != null) {
                Player.controllers.ControllerAddedEvent -= OnControllerAdded;
            }
        }

        protected override TaskStatus DoUpdate() {
            if (!hasEvent) return TaskStatus.Failure;
            hasEvent = false;
            return TaskStatus.Success;
        }

        private void OnControllerAdded(ControllerAssignmentChangedEventArgs args) {
            hasEvent = true;
            storeControllerId.Value = args.controller.id;
            storeControllerType.Value = (int)args.controller.type;
        }
    }

    [TaskCategory(Consts.taskCategory_player_events)]
    [TaskName("Player Controller Removed Event")]
    [TaskDescription("Event triggered when a controller is removed from this Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredPlayerControllerRemovedEvent : RewiredPlayerBDConditional {

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerId = -1;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerType = 0;

        private bool hasEvent = false;

        public override void OnAwake() {
            base.OnAwake();
            if (Player != null) {
                Player.controllers.ControllerAddedEvent += OnControllerAdded;
            }
        }

        public override void OnReset() {
            base.OnReset();
            storeControllerId = -1;
            storeControllerType = 0;
            hasEvent = false;
        }

        public override void OnBehaviorComplete() {
            if (Player != null) {
                Player.controllers.ControllerAddedEvent -= OnControllerAdded;
            }
        }

        protected override TaskStatus DoUpdate() {
            if (!hasEvent) return TaskStatus.Failure;
            hasEvent = false;
            return TaskStatus.Success;
        }

        private void OnControllerAdded(ControllerAssignmentChangedEventArgs args) {
            hasEvent = true;
            storeControllerId.Value = args.controller.id;
            storeControllerType.Value = (int)args.controller.type;
        }
    }

    #endregion
}