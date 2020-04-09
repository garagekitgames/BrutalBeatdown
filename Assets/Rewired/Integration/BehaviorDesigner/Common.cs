using UnityEngine;
using System;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    public static class Consts {
        public const string taskIconPath = "Assets/Rewired/Integration/BehaviorDesigner/icon.png";
        public const string taskCategory_base = "Rewired";
        public const string taskCategory_reInput = taskCategory_base + "/ReInput";
        public const string taskCategory_player = taskCategory_base + "/Player";
        public const string taskCategory_player_events = taskCategory_player + "/Events";
        public const string taskCategory_player_maps = taskCategory_player + "/Maps";
        public const string taskCategory_player_layoutManager = taskCategory_player + "/Layout Manager";
        public const string taskCategory_player_mapEnabler = taskCategory_player + "/Map Enabler";
        public const string taskCategory_events = taskCategory_base + "/Events";
        public const string taskCategory_controller = taskCategory_base + "/Controller";
        public const string taskCategory_controllerProperties = taskCategory_controller + "/Properties";
        public const string taskCategory_controllerInput = taskCategory_controller + "/Input";
        public const string taskCategory_controllerMisc = taskCategory_controller + "/Misc";
        public const string taskCategory_config = taskCategory_base + "/Configuration";
    }

    #region Task Base Classes

    #region Action Base Classes

    public abstract class BaseAction : Action {

        public override TaskStatus OnUpdate() {
            if(!ValidateVars()) return TaskStatus.Failure;
            return DoUpdate();
        }

        protected virtual bool ValidateVars() {
            return true;
        }

        protected abstract TaskStatus DoUpdate();
    }

    public abstract class GetIntAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        public void UpdateStoreValue(int value) {
            storeValue.Value = value;
        }
    }

    public abstract class GetFloatAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public bool everyFrame = true;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
            everyFrame = true;
        }

        public void UpdateStoreValue(float value) {
            storeValue.Value = value;
        }
    }

    public abstract class GetBoolAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        public void UpdateStoreValue(bool value) {
            storeValue.Value = value;
        }
    }

    public abstract class GetStringAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in a string variable.")]
        public SharedString storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = string.Empty;
        }

        public void UpdateStoreValue(string value) {
            storeValue.Value = value;
        }
    }

    public abstract class GetVector2Action : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in a Vector2 variable.")]
        public SharedVector2 storeValue;

        public bool everyFrame = true;

        public override void OnReset() {
            base.OnReset();
            storeValue = Vector2.zero;
            everyFrame = true;
        }

        public void UpdateStoreValue(Vector2 value) {
            storeValue.Value = value;
        }
    }

    public abstract class GetRewiredObjectAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in a variable.")]
        public SharedRewiredObject storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObject();
        }

        public void UpdateStoreValue(RewiredObject value) {
            storeValue.Value = value;
        }
    }

    public abstract class GetRewiredObjectListAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in a list variable.")]
        public SharedRewiredObjectList storeValue;

        protected List<object> workingList;

        public override void OnAwake() {
            base.OnAwake();
            workingList = new List<object>();
        }

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObjectList();
            workingList = new List<object>();
        }

        public void UpdateStoreValue() {
            storeValue.Value.Clear();
            for(int i = 0; i < workingList.Count; i++) {
                storeValue.Value.Add(workingList[i]);
            }
            workingList.Clear();
        }
    }

    public abstract class GetEnumAction : BaseAction {

        [RequiredField]
        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        public void UpdateStoreValue(int value) {
            storeValue.Value = value;
        }
    }

    public abstract class SetBoolAction : BaseAction {

        [Tooltip("The value to set.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }
    }

    public abstract class SetIntAction : BaseAction {

        [Tooltip("The value to set.")]
        public SharedInt value;

        public override void OnReset() {
            base.OnReset();
            value = 0;
        }
    }

    public abstract class SetEnumAction : BaseAction {
    }

    public abstract class SetStringAction : BaseAction {

        [Tooltip("The value to set.")]
        public SharedString value;

        public override void OnReset() {
            base.OnReset();
            value = string.Empty;
        }
    }

    #endregion

    #region Conditional Base Classes

    public abstract class BaseConditional : Conditional {

        public override TaskStatus OnUpdate() {
            if(!ValidateVars()) return TaskStatus.Failure;
            return DoUpdate();
        }

        protected virtual bool ValidateVars() {
            return true;
        }

        protected abstract TaskStatus DoUpdate();
    }

    #endregion

    #region Player Base Classes

    public abstract class RewiredPlayerBDAction : BaseAction {

        [SharedRequired]
        [Tooltip("The Rewired Player object. If this is None, the Player Id will be used instead to look up the Player.")]
        public SharedRewiredObject player;

        [Tooltip("The Rewired Player Id. To use the System Player, enter any value < 0 or 9999999. This is not used if the Player variable is set.")]
        public SharedInt playerId;

        protected Player Player {
            get {
                if(!player.IsNone) {
                    return player.GetObject<Player>();
                } else {
                    if(playerId.Value == Rewired.Consts.systemPlayerId) return ReInput.players.GetSystemPlayer();
                    if(playerId.Value < 0) return null;
                    return ReInput.players.GetPlayer(playerId.Value);
                }
            }
        }

        public override void OnReset() {
            base.OnReset();
            player = new SharedRewiredObject();
            playerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(player.IsNone) {
                if(playerId.IsNone) {
                    Debug.LogError("Rewired Player Id must be assigned!");
                    return false;
                }
                if(playerId.Value != Rewired.Consts.systemPlayerId && (playerId.Value < 0 || playerId.Value >= ReInput.players.playerCount)) {
                    Debug.LogError("Rewired Player Id is out of range!");
                    return false;
                }
            } else {
                if(player.GetObject<Player>() == null) {
                    Debug.LogError("Rewired Player is null!");
                    return false;
                }
            }
            return true;
        }
    }

    public abstract class RewiredPlayerActionBDAction : RewiredPlayerBDAction {

        [Tooltip("The Action name string. Must match Action name exactly in the Rewired Input Manager.")]
        public SharedString actionName;

        public override void OnReset() {
            base.OnReset();
            actionName = string.Empty;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(actionName.IsNone || string.IsNullOrEmpty(actionName.Value) || ReInput.mapping.GetActionId(actionName.Value) < 0) {
                Debug.LogError("Invalid Rewired Action name\"" + actionName.Value + "\"!");
                return false;
            }
            return true;
        }
    }

    public abstract class RewiredPlayerActionGetFloatBDAction : RewiredPlayerActionBDAction {

        [RequiredField]
        [Tooltip("Store the result in a float variable.")]
        public SharedFloat storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0f;
        }

        protected TaskStatus UpdateStoreValue(float newValue) {
            if(newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerActionGetBoolBDAction : RewiredPlayerActionBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected TaskStatus UpdateStoreValue(bool newValue) {
            if(newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerActionGetRewiredObjectBDAction : RewiredPlayerActionBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedRewiredObject storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObject();
        }

        protected TaskStatus UpdateStoreValue(RewiredObject newValue) {
            if(newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerGetBoolBDAction : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected TaskStatus UpdateStoreValue(bool newValue) {
            if(newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerActionGetAxis2DBDAction : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a Vector2 variable.")]
        public SharedVector2 storeValue;

        [Tooltip("The Action name string for the X axis value. Must match Action name exactly in the Rewired Input Manager.")]
        public SharedString actionNameX;

        [Tooltip("The Action name string for the Y axis value. Must match Action name exactly in the Rewired Input Manager.")]
        public SharedString actionNameY;

        public override void OnReset() {
            base.OnReset();
            storeValue = Vector2.zero;
            actionNameX = string.Empty;
            actionNameY = string.Empty;
        }

        protected TaskStatus UpdateStoreValue(Vector2 newValue) {
            if(newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(actionNameX.IsNone || string.IsNullOrEmpty(actionNameX.Value) || ReInput.mapping.GetActionId(actionNameX.Value) < 0) {
                Debug.LogError("Invalid Rewired Action (X) name \"" + actionNameX.Value + "\"!");
                return false;
            }
            if(actionNameY.IsNone || string.IsNullOrEmpty(actionNameY.Value) || ReInput.mapping.GetActionId(actionNameY.Value) < 0) {
                Debug.LogError("Invalid Rewired Action (Y) name \"" + actionNameX.Value + "\"!");
                return false;
            }

            return true;
        }
    }

    public abstract class RewiredPlayerInputBehaviorBDAction : RewiredPlayerBDAction {

        [Tooltip("Input Behavior name string.")]
        public SharedString behaviorName;

        public InputBehavior Behavior {
            get {
                return Player.controllers.maps.GetInputBehavior(behaviorName.Value);
            }
        }

        public override void OnReset() {
            base.OnReset();
            behaviorName = string.Empty;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(behaviorName.IsNone || string.IsNullOrEmpty(behaviorName.Value) || ReInput.mapping.GetActionId(behaviorName.Value) < 0) {
                Debug.LogError("Invalid Input Behavior name \"" + behaviorName.Value + "\"!");
                return false;
            }

            return true;
        }
    }

    public abstract class RewiredPlayerGetRewiredObjectBDAction : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a variable.")]
        public SharedRewiredObject storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObject();
        }

        protected TaskStatus UpdateStoreValue(RewiredObject newValue) {
            if(newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerGetRewiredObjectListBDAction : RewiredPlayerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a list variable.")]
        public SharedRewiredObjectList storeValue;

        protected List<object> workingList;

        public override void OnAwake() {
            base.OnAwake();
            workingList = new List<object>();
        }

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObjectList();
            workingList = new List<object>();
        }

        protected void UpdateStoreValue() {
            storeValue.Value.Clear();
            for(int i = 0; i < workingList.Count; i++) {
                storeValue.Value.Add(workingList[i]);
            }
            workingList.Clear();
        }
    }

    public abstract class RewiredPlayerSetBoolBDAction : RewiredPlayerBDAction {

        [Tooltip("The value to set.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }
    }

    public abstract class RewiredPlayerControllerBDAction : RewiredPlayerBDAction {

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class RewiredPlayerLayoutManagerBDAction : RewiredPlayerBDAction {

        protected ControllerMapLayoutManager LayoutManager {
            get {
                if (Player == null) return null;
                return Player.controllers.maps.layoutManager;
            }
        }
    }

    public abstract class RewiredPlayerLayoutManagerGetBoolBDAction : RewiredPlayerLayoutManagerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected TaskStatus UpdateStoreValue(bool newValue) {
            if (newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerLayoutManagerSetBoolBDAction : RewiredPlayerLayoutManagerBDAction {

        [Tooltip("The value to set.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }
    }

    public abstract class RewiredPlayerLayoutManagerGetRewiredObjectListBDAction : RewiredPlayerLayoutManagerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a list variable.")]
        public SharedRewiredObjectList storeValue;

        protected List<object> workingList;

        public override void OnAwake() {
            base.OnAwake();
            workingList = new List<object>();
        }

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObjectList();
            workingList = new List<object>();
        }

        protected void UpdateStoreValue() {
            storeValue.Value.Clear();
            for (int i = 0; i < workingList.Count; i++) {
                storeValue.Value.Add(workingList[i]);
            }
            workingList.Clear();
        }
    }

    public abstract class RewiredPlayerMapEnablerBDAction : RewiredPlayerBDAction {

        protected ControllerMapEnabler MapEnabler {
            get {
                if (Player == null) return null;
                return Player.controllers.maps.mapEnabler;
            }
        }
    }

    public abstract class RewiredPlayerMapEnablerGetBoolBDAction : RewiredPlayerMapEnablerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a boolean variable.")]
        public SharedBool storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = false;
        }

        protected TaskStatus UpdateStoreValue(bool newValue) {
            if (newValue != storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = newValue;
            }

            return TaskStatus.Success;
        }
    }

    public abstract class RewiredPlayerMapEnablerSetBoolBDAction : RewiredPlayerMapEnablerBDAction {

        [Tooltip("The value to set.")]
        public SharedBool value;

        public override void OnReset() {
            base.OnReset();
            value = false;
        }
    }

    public abstract class RewiredPlayerMapEnablerGetRewiredObjectListBDAction : RewiredPlayerMapEnablerBDAction {

        [RequiredField]
        [Tooltip("Store the result in a list variable.")]
        public SharedRewiredObjectList storeValue;

        protected List<object> workingList;

        public override void OnAwake() {
            base.OnAwake();
            workingList = new List<object>();
        }

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObjectList();
            workingList = new List<object>();
        }

        protected void UpdateStoreValue() {
            storeValue.Value.Clear();
            for (int i = 0; i < workingList.Count; i++) {
                storeValue.Value.Add(workingList[i]);
            }
            workingList.Clear();
        }
    }

    #endregion

    #region Controller Base Classes

    public abstract class ControllerAction : BaseAction {

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerGetIntAction : GetIntAction {

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerGetFloatAction : GetFloatAction {

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerGetStringAction : GetStringAction {

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerGetBoolAction : GetBoolAction {

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerSetBoolAction : GetBoolAction {

        [Tooltip("The value to set.")]
        public SharedBool value;

        [SharedRequired]
        [Tooltip("The Controller object. If this is None, Controller Type and Controller Id will be used instead to look up the Controller.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected Controller Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<Controller>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            value = false;
            controller = new SharedRewiredObject();
            controllerType = ControllerType.Joystick;
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    #endregion

    #region Controller With Axes

    public abstract class ControllerWithAxesGetIntAction : GetIntAction {

        [SharedRequired]
        [Tooltip("The Controller object.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected ControllerWithAxes Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<ControllerWithAxes>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value) as ControllerWithAxes;
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerWithAxesGetFloatAction : GetFloatAction {

        [SharedRequired]
        [Tooltip("The Controller object.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected ControllerWithAxes Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<ControllerWithAxes>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value) as ControllerWithAxes;
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    public abstract class ControllerWithAxesGetVector2Action : GetVector2Action {

        [SharedRequired]
        [Tooltip("The Controller object.")]
        public SharedRewiredObject controller;

        [Tooltip("The controller type. This is not used if a Controller is set in the Controller variable.")]
        public ControllerType controllerType = ControllerType.Joystick;

        [Tooltip("The controller id. This is not used if a Controller is set in the Controller variable.")]
        public SharedInt controllerId = 0;

        protected ControllerWithAxes Controller {
            get {
                if(!controller.IsNone) {
                    return controller.GetObject<ControllerWithAxes>();
                } else {
                    return ReInput.controllers.GetController(controllerType, controllerId.Value) as ControllerWithAxes;
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            controller = new SharedRewiredObject();
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    #endregion

    #region Joystick Base Classes

    public abstract class JoystickAction : BaseAction {

        [SharedRequired]
        [Tooltip("The Joystick object.")]
        public SharedRewiredObject joystick;

        [Tooltip("The joystick id. This is not used if a joystick is specified in Joystick.")]
        public SharedInt joystickId;

        protected Joystick Joystick {
            get {
                if(!joystick.IsNone) {
                    return joystick.GetObject<Joystick>();
                } else {
                    return ReInput.controllers.GetJoystick(joystickId.Value);
                }
            }
        }

        protected bool HasJoystick { get { return Joystick != null; } }

        public override void OnReset() {
            base.OnReset();
            joystick = new SharedRewiredObject();
            joystickId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasJoystick) return false;

            return true;
        }
    }

    public abstract class JoystickGetIntAction : GetIntAction {

        [SharedRequired]
        [Tooltip("The Joystick object.")]
        public SharedRewiredObject joystick;

        [Tooltip("The joystick id. This is not used if a joystick is specified in Joystick.")]
        public SharedInt joystickId;

        protected Joystick Joystick {
            get {
                if(!joystick.IsNone) {
                    return joystick.GetObject<Joystick>();
                } else {
                    return ReInput.controllers.GetJoystick(joystickId.Value);
                }
            }
        }

        protected bool HasJoystick { get { return Joystick != null; } }

        public override void OnReset() {
            base.OnReset();
            joystick = new SharedRewiredObject();
            joystickId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasJoystick) return false;

            return true;
        }
    }

    public abstract class JoystickGetStringAction : GetStringAction {

        [SharedRequired]
        [Tooltip("The Joystick object.")]
        public SharedRewiredObject joystick;

        [Tooltip("The joystick id. This is not used if a joystick is specified in Joystick.")]
        public SharedInt joystickId;

        protected Joystick Joystick {
            get {
                if(!joystick.IsNone) {
                    return joystick.GetObject<Joystick>();
                } else {
                    return ReInput.controllers.GetJoystick(joystickId.Value);
                }
            }
        }

        protected bool HasJoystick { get { return Joystick != null; } }

        public override void OnReset() {
            base.OnReset();
            joystick = new SharedRewiredObject();
            joystickId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasJoystick) return false;

            return true;
        }
    }

    public abstract class JoystickGetBoolAction : GetBoolAction {

        [SharedRequired]
        [Tooltip("The Joystick object.")]
        public SharedRewiredObject joystick;

        [Tooltip("The joystick id. This is not used if a joystick is specified in Joystick.")]
        public SharedInt joystickId;

        protected Joystick Joystick {
            get {
                if(!joystick.IsNone) {
                    return joystick.GetObject<Joystick>();
                } else {
                    return ReInput.controllers.GetJoystick(joystickId.Value);
                }
            }
        }

        protected bool HasJoystick { get { return Joystick != null; } }

        public override void OnReset() {
            base.OnReset();
            joystick = new SharedRewiredObject();
            joystickId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasJoystick) return false;

            return true;
        }
    }

    #endregion

    #region Custom Controller Classes

    public abstract class CustomControllerAction : BaseAction {

        [SharedRequired]
        [Tooltip("The CustomController object.")]
        public SharedRewiredObject customController;

        [Tooltip("The controller id. This is not used if a CustomController is specified in CustomController.")]
        public SharedInt controllerId;

        protected CustomController Controller {
            get {
                if(!customController.IsNone) {
                    return customController.GetObject<CustomController>();
                } else {
                    return ReInput.controllers.GetCustomController(controllerId.Value);
                }
            }
        }

        protected bool HasController { get { return Controller != null; } }

        public override void OnReset() {
            base.OnReset();
            customController = new SharedRewiredObject();
            controllerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasController) return false;

            return true;
        }
    }

    #endregion

    #region ActionElementMap Base Classes

    public abstract class ActionElementMapAction : BaseAction {

        [SharedRequired]
        [Tooltip("The ActionElementMap object. If this is None, ActionElementMap Id will be used instead to look up the ActionElementMap.")]
        public SharedRewiredObject actionElementMap;

        [Tooltip("The actionElementMap id. This is not used if a ActionElementMap is set in the ActionElementMap variable.")]
        public SharedInt actionElementMapId = -1;

        protected ActionElementMap ActionElementMap {
            get {
                if(!actionElementMap.IsNone) {
                    return actionElementMap.GetObject<ActionElementMap>();
                } else {
                    return ActionElementMapAction.FindActionElementMap(actionElementMapId.Value);
                }
            }
        }

        protected bool HasActionElementMap { get { return ActionElementMap != null; } }

        public override void OnReset() {
            base.OnReset();
            actionElementMap = new SharedRewiredObject();
            actionElementMapId = -1;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasActionElementMap) return false;

            return true;
        }

        internal static ActionElementMap FindActionElementMap(int uid) {
            if(!ReInput.isReady) return null;

            int playerCount = ReInput.players.allPlayerCount;
            for(int i = 0; i < playerCount; i++) {
                Player player = ReInput.players.AllPlayers[i];
                foreach(var map in player.controllers.maps.GetAllMaps()) {
                    foreach(var aem in map.AllMaps) {
                        if(aem.id == uid) return aem;
                    }
                }
            }
            return null;
        }
    }

    public abstract class ActionElementMapGetIntAction : GetIntAction {

        [SharedRequired]
        [Tooltip("The ActionElementMap object. If this is None, ActionElementMap Type and ActionElementMap Id will be used instead to look up the ActionElementMap.")]
        public SharedRewiredObject actionElementMap;

        [Tooltip("The actionElementMap id. This is not used if a ActionElementMap is set in the ActionElementMap variable.")]
        public SharedInt actionElementMapId = -1;

        protected ActionElementMap ActionElementMap {
            get {
                if(!actionElementMap.IsNone) {
                    return actionElementMap.GetObject<ActionElementMap>();
                } else {
                    return ActionElementMapAction.FindActionElementMap(actionElementMapId.Value);
                }
            }
        }

        protected bool HasActionElementMap { get { return ActionElementMap != null; } }

        public override void OnReset() {
            base.OnReset();
            actionElementMap = new SharedRewiredObject();
            actionElementMapId = -1;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasActionElementMap) return false;

            return true;
        }
    }

    public abstract class ActionElementMapGetStringAction : GetStringAction {

        [SharedRequired]
        [Tooltip("The ActionElementMap object. If this is None, ActionElementMap Type and ActionElementMap Id will be used instead to look up the ActionElementMap.")]
        public SharedRewiredObject actionElementMap;

        [Tooltip("The actionElementMap id. This is not used if a ActionElementMap is set in the ActionElementMap variable.")]
        public SharedInt actionElementMapId = -1;

        protected ActionElementMap ActionElementMap {
            get {
                if(!actionElementMap.IsNone) {
                    return actionElementMap.GetObject<ActionElementMap>();
                } else {
                    return ActionElementMapAction.FindActionElementMap(actionElementMapId.Value);
                }
            }
        }

        protected bool HasActionElementMap { get { return ActionElementMap != null; } }

        public override void OnReset() {
            base.OnReset();
            actionElementMap = new SharedRewiredObject();
            actionElementMapId = -1;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasActionElementMap) return false;

            return true;
        }
    }

    public abstract class ActionElementMapGetBoolAction : GetBoolAction {

        [SharedRequired]
        [Tooltip("The ActionElementMap object. If this is None, ActionElementMap Type and ActionElementMap Id will be used instead to look up the ActionElementMap.")]
        public SharedRewiredObject actionElementMap;

        [Tooltip("The actionElementMap id. This is not used if a ActionElementMap is set in the ActionElementMap variable.")]
        public SharedInt actionElementMapId = -1;

        protected ActionElementMap ActionElementMap {
            get {
                if(!actionElementMap.IsNone) {
                    return actionElementMap.GetObject<ActionElementMap>();
                } else {
                    return ActionElementMapAction.FindActionElementMap(actionElementMapId.Value);
                }
            }
        }

        protected bool HasActionElementMap { get { return ActionElementMap != null; } }

        public override void OnReset() {
            base.OnReset();
            actionElementMap = new SharedRewiredObject();
            actionElementMapId = -1;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasActionElementMap) return false;

            return true;
        }
    }

    public abstract class ActionElementMapSetIntAction : SetIntAction {

        [SharedRequired]
        [Tooltip("The ActionElementMap object. If this is None, ActionElementMap Type and ActionElementMap Id will be used instead to look up the ActionElementMap.")]
        public SharedRewiredObject actionElementMap;

        [Tooltip("The actionElementMap id. This is not used if a ActionElementMap is set in the ActionElementMap variable.")]
        public SharedInt actionElementMapId = -1;

        protected ActionElementMap ActionElementMap {
            get {
                if(!actionElementMap.IsNone) {
                    return actionElementMap.GetObject<ActionElementMap>();
                } else {
                    return ActionElementMapAction.FindActionElementMap(actionElementMapId.Value);
                }
            }
        }

        protected bool HasActionElementMap { get { return ActionElementMap != null; } }

        public override void OnReset() {
            base.OnReset();
            actionElementMap = new SharedRewiredObject();
            actionElementMapId = -1;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasActionElementMap) return false;

            return true;
        }
    }

    public abstract class ActionElementMapSetBoolAction : SetBoolAction {

        [SharedRequired]
        [Tooltip("The ActionElementMap object. If this is None, ActionElementMap Type and ActionElementMap Id will be used instead to look up the ActionElementMap.")]
        public SharedRewiredObject actionElementMap;

        [Tooltip("The actionElementMap id. This is not used if a ActionElementMap is set in the ActionElementMap variable.")]
        public SharedInt actionElementMapId = -1;

        protected ActionElementMap ActionElementMap {
            get {
                if(!actionElementMap.IsNone) {
                    return actionElementMap.GetObject<ActionElementMap>();
                } else {
                    return ActionElementMapAction.FindActionElementMap(actionElementMapId.Value);
                }
            }
        }

        protected bool HasActionElementMap { get { return ActionElementMap != null; } }

        public override void OnReset() {
            base.OnReset();
            actionElementMap = new SharedRewiredObject();
            actionElementMapId = -1;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(!HasActionElementMap) return false;

            return true;
        }
    }

    #endregion

    #region Player Conditional Base Classes

    public abstract class RewiredPlayerBDConditional : BaseConditional {

        [SharedRequired]
        [Tooltip("The Rewired Player object. If this is None, the Player Id will be used instead to look up the Player.")]
        public SharedRewiredObject player;

        [Tooltip("The Rewired Player Id. To use the System Player, enter any value < 0 or 9999999. This is not used if the Player variable is set.")]
        public SharedInt playerId;

        protected Player Player {
            get {
                if(playerId.Value < 0 || playerId.Value == Rewired.Consts.systemPlayerId) return ReInput.players.GetSystemPlayer();
                return ReInput.players.GetPlayer(playerId.Value);
            }
        }

        public override void OnReset() {
            base.OnReset();
            player = new SharedRewiredObject();
            playerId = 0;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(player.IsNone) {
                if(playerId.IsNone) {
                    Debug.LogError("Rewired Player Id must be assigned!");
                    return false;
                }
                if(playerId.Value != Rewired.Consts.systemPlayerId && (playerId.Value < 0 || playerId.Value >= ReInput.players.playerCount)) {
                    Debug.LogError("Rewired Player Id is out of range!");
                    return false;
                }
            } else {
                if(player.GetObject<Player>() == null) {
                    Debug.LogError("Rewired Player is null!");
                    return false;
                }
            }
            return true;
        }
    }

    public abstract class RewiredPlayerActionBDConditional : RewiredPlayerBDConditional {

        [Tooltip("The Action name string. Must match Action name exactly in the Rewired Input Manager.")]
        public SharedString actionName;

        public override void OnReset() {
            base.OnReset();
            actionName = string.Empty;
        }

        protected override bool ValidateVars() {
            if(!base.ValidateVars()) return false;

            if(actionName.IsNone || string.IsNullOrEmpty(actionName.Value) || ReInput.mapping.GetActionId(actionName.Value) < 0) {
                Debug.LogError("Invalid Rewired Action name\"" + actionName.Value + "\"!");
                return false;
            }
            return true;
        }
    }

    public abstract class RewiredPlayerActionCompareFloatBDConditional : RewiredPlayerActionBDConditional {

        [Tooltip("The comparison operation to perform")]
        public CompareOperation operation;
        [Tooltip("The value to which to compare the returned value.")]
        public SharedFloat compareToValue;
        [Tooltip("Compare using the absolute values of the two operands.")]
        public SharedBool useAbsValues;

        public override void OnReset() {
            base.OnReset();
            operation = CompareOperation.None;
            compareToValue.Value = 0;
            useAbsValues = false;
        }

        protected TaskStatus Compare(float value) {
            if(operation == CompareOperation.None) return TaskStatus.Success;

            float val1, val2;
            if(useAbsValues.Value) {
                val1 = Mathf.Abs(value);
                val2 = Mathf.Abs(compareToValue.Value);
            } else {
                val1 = value;
                val2 = compareToValue.Value;
            }

            switch(operation) {
                case CompareOperation.LessThan:
                    return val1 < val2 ? TaskStatus.Success : TaskStatus.Failure;
                case CompareOperation.LessThanOrEqualTo:
                    return val1 <= val2 ? TaskStatus.Success : TaskStatus.Failure;
                case CompareOperation.EqualTo:
                    return val1 == val2 ? TaskStatus.Success : TaskStatus.Failure;
                case CompareOperation.NotEqualTo:
                    return val1 != val2 ? TaskStatus.Success : TaskStatus.Failure;
                case CompareOperation.GreaterThanOrEqualTo:
                    return val1 >= val2 ? TaskStatus.Success : TaskStatus.Failure;
                case CompareOperation.GreaterThan:
                    return val1 > val2 ? TaskStatus.Success : TaskStatus.Failure;
            }
            return TaskStatus.Failure;
        }
    }

    #endregion

    #region Rewired Object Base Classes

    public abstract class RewiredObjectAction<T> : BaseAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    public abstract class RewiredObjectGetIntAction<T> : GetIntAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    public abstract class RewiredObjectGetStringAction<T> : GetStringAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    public abstract class RewiredObjectGetBoolAction<T> : GetBoolAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    public abstract class RewiredObjectSetIntAction<T> : SetIntAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    public abstract class RewiredObjectSetBoolAction<T> : SetBoolAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    public abstract class RewiredObjectSetStringAction<T> : SetStringAction where T : class {

        [RequiredField]
        [Tooltip("The object.")]
        public SharedRewiredObject @object;

        protected T TObject {
            get {
                if (@object.IsNone) return null;
                return @object.GetObject<T>();
            }
        }

        protected bool HasObject { get { return TObject != null; } }

        public override void OnReset() {
            base.OnReset();
            @object = new SharedRewiredObject();
        }

        protected override bool ValidateVars() {
            if (!base.ValidateVars()) return false;
            if (!HasObject) return false;
            return true;
        }
    }

    #endregion

    #endregion

    #region Misc Classes

    // A wrapper for any System.Object.
    // Equals comparisons return true if inner contents match even if wrappers are different instances.
    public sealed class RewiredObject : IEquatable<RewiredObject> {

        [NonSerialized]
        public readonly object @object;

        public RewiredObject() {
        }
        public RewiredObject(object @object) {
            if(!object.Equals(@object, null) && Utils.DoesTypeImplement(@object.GetType(), typeof(RewiredObject))) {
                throw new Exception("Nested RewiredObjects are not allowed!");
            }
            this.@object = @object;
        }

        public T GetObject<T>() {
            if(object.Equals(@object, null)) return default(T);
            if(!Utils.DoesTypeImplement(@object.GetType(), typeof(T))) return default(T);
            return (T)@object;
        }

        #region Object.Equals Override

        public override bool Equals(object obj) {
            // Compare the inner objects, not the wrappers
            obj = RewiredObject.Unwrap(obj); // unwrap if necessary
            return object.Equals(@object, obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion

        #region IEquatable Implementation

        public bool Equals(RewiredObject other) {
            return Equals((object)other);
        }

        #endregion

        // Operators

        public static bool operator ==(RewiredObject a, RewiredObject b) {
            if(object.Equals(a, null) && object.Equals(b, null)) return true;
            if(object.Equals(a, null)) return b.Equals(a);
            if(object.Equals(b, null)) return a.Equals(b);
            return a.Equals(b);
        }

        public static bool operator !=(RewiredObject a, RewiredObject b) {
            if(object.Equals(a, null) && object.Equals(b, null)) return false;
            if(object.Equals(a, null)) return !b.Equals(a);
            if(object.Equals(b, null)) return !a.Equals(b);
            return !a.Equals(b);
        }

        // Static Methods

        public static RewiredObject Wrap(object @object) {
            if(object.Equals(@object, null)) return null;
            if(Utils.DoesTypeImplement(@object.GetType(), typeof(RewiredObject))) return (RewiredObject)@object; // already wrapped
            return new RewiredObject(@object);
        }

        public static object Unwrap(object @object) {
            if(object.Equals(@object, null)) return null;
            if(Utils.DoesTypeImplement(@object.GetType(), typeof(RewiredObject))) {
                @object = ((RewiredObject)@object).@object; // get the inner object for comparison
            }
            return @object;
        }
    }

    // Implemented as a List wrapper to avoid BD adding editor properties for the count and fields
    public sealed class RewiredObjectList {

        [NonSerialized]
        private List<RewiredObject> list;

        public RewiredObjectList() {
            list = new List<RewiredObject>();
        }
        public RewiredObjectList(List<RewiredObject> list) {
            this.list = list;
        }

        public RewiredObject this[int index] {
            get {
                return list[index];
            }
            set {
                list[index] = value;
            }
        }

        public int Count { get { return list != null ? list.Count : 0; } }

        public void Clear() {
            if(list == null) return;
            list.Clear();
        }

        public void Add(object item) {
            if(list == null) return;
            list.Add(RewiredObject.Wrap(item));
        }

        public void Remove(object item) {
            if(list == null) return;
            list.Remove(RewiredObject.Wrap(item));
        }

        public void RemoveAt(int index) {
            if(list == null) return;
            list.RemoveAt(index);
        }

        public void Insert(int index, object item) {
            if(list == null) return;
            list.Insert(index, RewiredObject.Wrap(item));
        }

        public int IndexOf(object item, int index) {
            if(list == null) return -1;
            return list.IndexOf(RewiredObject.Wrap(item), index);
        }

        public bool Contains(object item) {
            if(list == null) return false;
            return list.Contains(RewiredObject.Wrap(item));
        }
    }

    #endregion

    #region Enums

    public enum CompareOperation {
        None = 0,
        LessThan = 1,
        LessThanOrEqualTo = 2,
        EqualTo = 3,
        NotEqualTo = 4,
        GreaterThanOrEqualTo = 5,
        GreaterThan = 6
    }

    public enum ControllerTemplateType {
        Gamepad = 0,
        RacingWheel = 1,
        HOTAS = 2,
        FlightYoke = 3,
        FlightPedals = 4,
        SixDofController = 5
    }

    #endregion
}