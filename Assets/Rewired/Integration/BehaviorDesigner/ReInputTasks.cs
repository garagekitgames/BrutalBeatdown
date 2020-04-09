using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;
    using Rewired.Config;
    using Rewired.Platforms;

    #region Players

    [TaskCategory(Consts.taskCategory_reInput + "/Players")]
    [TaskName("Get Player Count")]
    [TaskDescription("Get the number of Players. Does not include the System Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetPlayerCount : GetIntAction {

        protected override TaskStatus DoUpdate() {
            storeValue.Value = ReInput.players.playerCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Players")]
    [TaskName("Get All Players Count")]
    [TaskDescription("Gets the number of Players including the System Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAllPlayersCount : GetIntAction {

        protected override TaskStatus DoUpdate() {
            storeValue.Value = ReInput.players.allPlayerCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Players")]
    [TaskName("Get Players")]
    [TaskDescription("Gets a collection of Players. Does not include the System Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetPlayers : GetRewiredObjectListAction {

        protected override TaskStatus DoUpdate() {
            IList<Player> players = ReInput.players.Players;
            int count = players != null ? players.Count : 0;

            for(int i = 0; i < count; i++) {
                workingList.Add(players[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Players")]
    [TaskName("Get All Players")]
    [TaskDescription("Gets a collection of Players including the System Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAllPlayers : GetRewiredObjectListAction {

        protected override TaskStatus DoUpdate() {
            IList<Player> players = ReInput.players.AllPlayers;
            int count = players != null ? players.Count : 0;

            for(int i = 0; i < count; i++) {
                workingList.Add(players[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Controllers

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Controller Count")]
    [TaskDescription("The number of controllers of all types currently connected.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetControllerCount : GetIntAction {

        protected override TaskStatus DoUpdate() {
            storeValue.Value = ReInput.controllers.controllerCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Joystick Count")]
    [TaskDescription("The number of joysticks currently connected.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetJoystickCount : GetIntAction {

        protected override TaskStatus DoUpdate() {
            storeValue.Value = ReInput.controllers.joystickCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Joysticks")]
    [TaskDescription("Gets a collection of connected Joysticks.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetJoysticks : GetRewiredObjectListAction {

        protected override TaskStatus DoUpdate() {
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            int count = joysticks != null ? joysticks.Count : 0;

            for(int i = 0; i < count; i++) {
                workingList.Add(joysticks[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Custom Controller Count")]
    [TaskDescription("The number of custom controllers.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetCustomControllerCount : GetIntAction {
        
        protected override TaskStatus DoUpdate() {
            storeValue.Value = ReInput.controllers.customControllerCount;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Custom Controllers")]
    [TaskDescription("Gets a collection of connected Custom Controllers.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetCustomControllers : GetRewiredObjectListAction {

        protected override TaskStatus DoUpdate() {
            IList<CustomController> customControllers = ReInput.controllers.CustomControllers;
            int count = customControllers != null ? customControllers.Count : 0;

            for(int i = 0; i < count; i++) {
                workingList.Add(customControllers[i]);
            }

            UpdateStoreValue();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Last Active Controller Type")]
    [TaskDescription("Get the last controller type that produced input.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetLastActiveControllerType : GetIntAction {

        protected override TaskStatus DoUpdate() {
            storeValue.Value = (int)ReInput.controllers.GetLastActiveControllerType();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Is Controller Assigned")]
    [TaskDescription("Is the specified controller assigned to any players?")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredIsControllerAssigned : ControllerGetBoolAction {

        protected override TaskStatus DoUpdate() {
            if(!HasController) return TaskStatus.Failure;
            UpdateStoreValue(ReInput.controllers.IsControllerAssigned(Controller.type, Controller));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Is Controller Assigned To Player")]
    [TaskDescription("Is the specified controller assigned to the specified player?")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredIsControllerAssignedToPlayer : ControllerGetBoolAction {

        [RequiredField]
        [Tooltip("The Rewired Player Id. To use the System Player, enter any value < 0 or 9999999.")]
        public SharedInt playerId;

        public override void OnReset() {
            base.OnReset();
            playerId = 0;
        }

        protected override TaskStatus DoUpdate() {
            if(!HasController) return TaskStatus.Failure;
            UpdateStoreValue(ReInput.controllers.IsControllerAssignedToPlayer(Controller.type, Controller.id, playerId.Value));
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Remove Controller From All Players")]
    [TaskDescription("De-assigns the specified controller from all players.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredRemoveControllerFromAllPlayers : ControllerAction {

        [Tooltip("Do we de-assign from the System player also?")]
        public SharedBool includeSystemPlayer = true;

        public override void OnReset() {
            base.OnReset();
            includeSystemPlayer = true;
        }

        protected override TaskStatus DoUpdate() {
            if(!HasController) return TaskStatus.Failure;
            ReInput.controllers.RemoveControllerFromAllPlayers(Controller.type, Controller.id, includeSystemPlayer.Value);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Auto Assign Joystick")]
    [TaskDescription("Auto-assigns a Joystick to a Player based on the joystick auto-assignment settings in the Rewired Input Manager. If the Joystick is already assigned to a Player, the Joystick will not be re-assigned.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredAutoAssignJoystick : JoystickAction {

        protected override TaskStatus DoUpdate() {
            ReInput.controllers.AutoAssignJoystick(Joystick);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Auto Assign Joysticks")]
    [TaskDescription("Auto-assigns all unassigned Joysticks to Players based on the joystick auto-assignment settings in the Rewired Input Manager.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredAutoAssignJoysticks : JoystickAction {

        protected override TaskStatus DoUpdate() {
            ReInput.controllers.AutoAssignJoysticks();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Create Custom Controller")]
    [TaskDescription("Create a new CustomController object from a source definition in the Rewired Input Manager.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredCreateCustomController : GetRewiredObjectAction {

        [Tooltip("Source id of the CustomController definition.")]
        public SharedInt sourceId;

        [Tooltip("Tag to assign.")]
        public SharedString tag;

        protected override TaskStatus DoUpdate() {
            CustomController cc;
            if(!string.IsNullOrEmpty(tag.Value)) {
                cc = ReInput.controllers.CreateCustomController(sourceId.Value, tag.Value);
            } else {
                cc = ReInput.controllers.CreateCustomController(sourceId.Value);
            }

            if(cc != null) UpdateStoreValue(new RewiredObject(cc));
            else UpdateStoreValue(null);

            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Destroy Custom Controller")]
    [TaskDescription("Destroys a CustomController.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredDestroyCustomController : CustomControllerAction {

        protected override TaskStatus DoUpdate() {
            if(!HasController) return TaskStatus.Failure;
            ReInput.controllers.DestroyCustomController(Controller);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Any Button")]
    [TaskDescription("Get the button held state of all buttons on all controllers. Returns TRUE if any button is held. This retrieves the value from the actual hardware buttons, not Actions as mapped by Controller Maps in Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAnyButton : GetBoolAction {

        [Tooltip("If enabled, the button state will be obtained only for controllers matching the chosen Controller Type.")]
        public bool useControllerType;

        [Tooltip("The type of controller. This is ignored if Use Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        public override void OnReset() {
            base.OnReset();
            useControllerType = false;
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            if(useControllerType) ReInput.controllers.GetAnyButton(controllerType);
            else ReInput.controllers.GetAnyButton();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Any Button Down")]
    [TaskDescription("Get the button just pressed state of all buttons on all controllers. This will only return TRUE only on the first frame a button is pressed. This retrieves the value from the actual hardware buttons, not Actions as mapped by Controller Maps in Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAnyButtonDown : GetBoolAction {

        [Tooltip("If enabled, the button state will be obtained only for controllers matching the chosen Controller Type.")]
        public bool useControllerType;

        [Tooltip("The type of controller. This is ignored if Use Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        public override void OnReset() {
            base.OnReset();
            useControllerType = false;
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            if(useControllerType) ReInput.controllers.GetAnyButtonDown(controllerType);
            else ReInput.controllers.GetAnyButtonDown();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Any Button Up")]
    [TaskDescription("Get the button just released state of all buttons on all controllers of a specified type. This will only return TRUE only on the first frame a button is released. This retrieves the value from the actual hardware buttons, not Actions as mapped by Controller Maps in Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAnyButtonUp : GetBoolAction {

        [Tooltip("If enabled, the button state will be obtained only for controllers matching the chosen Controller Type.")]
        public bool useControllerType;

        [Tooltip("The type of controller. This is ignored if Use Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        public override void OnReset() {
            base.OnReset();
            useControllerType = false;
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            if(useControllerType) ReInput.controllers.GetAnyButtonUp(controllerType);
            else ReInput.controllers.GetAnyButtonUp();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Any Button Prev")]
    [TaskDescription("Get the previous button held state of all buttons on all controllers. Returns TRUE if any button was held in the previous frame. This retrieves the value from the actual hardware buttons, not Actions as mapped by Controller Maps in Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAnyButtonPrev : GetBoolAction {

        [Tooltip("If enabled, the button state will be obtained only for controllers matching the chosen Controller Type.")]
        public bool useControllerType;

        [Tooltip("The type of controller. This is ignored if Use Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        public override void OnReset() {
            base.OnReset();
            useControllerType = false;
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            if(useControllerType) ReInput.controllers.GetAnyButtonPrev(controllerType);
            else ReInput.controllers.GetAnyButtonPrev();
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_reInput + "/Controllers")]
    [TaskName("Get Any Button Changed")]
    [TaskDescription("Returns true if any button has changed state from the previous frame to the current. This retrieves the value from the actual hardware buttons, not Actions as mapped by Controller Maps in Player.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetAnyButtonChanged : GetBoolAction {

        [Tooltip("If enabled, the button state will be obtained only for controllers matching the chosen Controller Type.")]
        public bool useControllerType;

        [Tooltip("The type of controller. This is ignored if Use Controller Type is false.")]
        public ControllerType controllerType = ControllerType.Keyboard;

        public override void OnReset() {
            base.OnReset();
            useControllerType = false;
            controllerType = ControllerType.Keyboard;
        }

        protected override TaskStatus DoUpdate() {
            if(useControllerType) ReInput.controllers.GetAnyButtonChanged(controllerType);
            else ReInput.controllers.GetAnyButtonChanged();
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Time

    [TaskCategory(Consts.taskCategory_reInput + "/Time")]
    [TaskName("Get Unscaled Time")]
    [TaskDescription("Current unscaled time since start of the game. Always use this when doing current time comparisons for button and axis active/inactive times instead of Time.time or Time.unscaledTime.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredGetUnscaledTime : GetFloatAction {

        protected override TaskStatus DoUpdate() {
            storeValue.Value = ReInput.time.unscaledTime;
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Events

    [TaskCategory(Consts.taskCategory_events)]
    [TaskName("Controller Connected Event")]
    [TaskDescription("Event triggered when a controller is conected.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerConnectedEvent : Conditional {

        [Tooltip("Store the result in a string variable.")]
        public SharedString storeControllerName;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerId = -1;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerType = 0;

        private bool hasEvent = false;

        public override void OnAwake() {
            base.OnAwake();
            ReInput.ControllerConnectedEvent += OnControllerConnected;
        }

        public override void OnReset() {
            base.OnReset();
            storeControllerName = string.Empty;
            storeControllerId = -1;
            storeControllerType = 0;
            hasEvent = false;
        }

        public override void OnBehaviorComplete() {
            ReInput.ControllerDisconnectedEvent -= OnControllerConnected;
        }

        public override TaskStatus OnUpdate() {
            
            return DoUpdate();
        }

        public TaskStatus DoUpdate() {
            if(!hasEvent) return TaskStatus.Failure;
            hasEvent = false;
            return TaskStatus.Success;
        }

        private void OnControllerConnected(ControllerStatusChangedEventArgs args) {
            hasEvent = true;
            storeControllerName.Value = args.name;
            storeControllerId.Value = args.controllerId;
            storeControllerType.Value = (int)args.controllerType;
        }
    }

    [TaskCategory(Consts.taskCategory_events)]
    [TaskName("Controller PreDisconnect Event")]
    [TaskDescription("Event triggered just before a controller is disconnected. You can use this event to save controller maps before the controller is removed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerPreDisconnectEvent : Conditional {

        [Tooltip("Store the result in a string variable.")]
        public SharedString storeControllerName;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerId = -1;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerType = 0;

        private bool hasEvent = false;

        public override void OnAwake() {
            base.OnAwake();
            ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
        }

        public override void OnReset() {
            base.OnReset();
            storeControllerName = string.Empty;
            storeControllerId = -1;
            storeControllerType = 0;
            hasEvent = false;
        }

        public override void OnBehaviorComplete() {
            ReInput.ControllerPreDisconnectEvent -= OnControllerPreDisconnect;
        }

        public override TaskStatus OnUpdate() {
            return DoUpdate();
        }

        public TaskStatus DoUpdate() {
            if(!hasEvent) return TaskStatus.Failure;
            hasEvent = false;
            return TaskStatus.Success;
        }

        private void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args) {
            hasEvent = true;
            storeControllerName.Value = args.name;
            storeControllerId.Value = args.controllerId;
            storeControllerType.Value = (int)args.controllerType;
        }
    }

    [TaskCategory(Consts.taskCategory_events)]
    [TaskName("Controller Disconnected Event")]
    [TaskDescription("Event triggered after a controller is disconnected.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredControllerDisconnectedEvent : Conditional {

        [Tooltip("Store the result in a string variable.")]
        public SharedString storeControllerName;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerId = -1;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerType = 0;

        private bool hasEvent = false;

        public override void OnAwake() {
            base.OnAwake();
            ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        }

        public override void OnReset() {
            base.OnReset();
            storeControllerName = string.Empty;
            storeControllerId = -1;
            storeControllerType = 0;
            hasEvent = false;
        }

        public override void OnBehaviorComplete() {
            ReInput.ControllerDisconnectedEvent -= OnControllerDisconnected;
        }

        public override TaskStatus OnUpdate() {
            return DoUpdate();
        }

        public TaskStatus DoUpdate() {
            if(!hasEvent) return TaskStatus.Failure;
            hasEvent = false;
            return TaskStatus.Success;
        }

        private void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
            hasEvent = true;
            storeControllerName.Value = args.name;
            storeControllerId.Value = args.controllerId;
            storeControllerType.Value = (int)args.controllerType;
        }
    }

    [TaskCategory(Consts.taskCategory_events)]
    [TaskName("Last Active Controller Changed Event")]
    [TaskDescription("Event triggered every time the last active controller changes.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredLastActiveControllerChangedEvent : Conditional {

        [Tooltip("Store the result in a string variable.")]
        public SharedString storeControllerName;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerId = -1;

        [Tooltip("Store the result in an int variable.")]
        public SharedInt storeControllerType = 0;

        private bool hasEvent = false;

        public override void OnAwake() {
            base.OnAwake();
            ReInput.controllers.AddLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
        }

        public override void OnReset() {
            base.OnReset();
            storeControllerName = string.Empty;
            storeControllerId = -1;
            storeControllerType = 0;
            hasEvent = false;
        }

        public override void OnBehaviorComplete() {
            ReInput.controllers.RemoveLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
        }

        public override TaskStatus OnUpdate() {
            return DoUpdate();
        }

        public TaskStatus DoUpdate() {
            if(!hasEvent) return TaskStatus.Failure;
            hasEvent = false;
            return TaskStatus.Success;
        }

        private void OnLastActiveControllerChanged(Controller controller) {
            hasEvent = true;
            storeControllerName = controller.name;
            storeControllerType.Value = (int)controller.type;
            storeControllerId = controller.id;
        }
    }

    #endregion

    #region Config Helper

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Use XInput")]
    [TaskDescription("Toggles the use of XInput in Windows Standalone and Windows UWP during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetUseXInput : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.useXInput);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Use XInput")]
    [TaskDescription("Toggles the use of XInput in Windows Standalone and Windows UWP during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetUseXInput : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.useXInput = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Update Loop")]
    [TaskDescription("Changes the Update Loop setting during runtime. Rewired will be completely reset if this value is changed. This can be set to multiple values simultaneously. Note: Update is required. Update will be enabled even if you unset the Update flag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetUpdateLoop : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.updateLoop);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Update Loop")]
    [TaskDescription("Changes the Update Loop setting during runtime. Rewired will be completely reset if this value is changed. This can be set to multiple values simultaneously. Note: Update is required. Update will be enabled even if you unset the Update flag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetUpdateLoop : BaseAction {

        [Tooltip("Update Rewired in the FixedUpdate loop.")]
        public SharedBool fixedUpdate;

        [Tooltip("Update Rewired in the OnGUI loop.")]
        public SharedBool onGUI;

        public override void OnReset() {
            base.OnReset();
            fixedUpdate = false;
            onGUI = false;
        }

        protected override TaskStatus DoUpdate() {
            UpdateLoopSetting value = UpdateLoopSetting.Update; // always enable Update
            if(fixedUpdate.Value) value |= UpdateLoopSetting.FixedUpdate;
            if(onGUI.Value) value |= UpdateLoopSetting.OnGUI;
            ReInput.configuration.updateLoop = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Windows Standalone Primary Input Source")]
    [TaskDescription("Changes the primary input source in Windows Standalone during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetWindowsStandalonePrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.windowsStandalonePrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Windows Standalone Primary Input Source")]
    [TaskDescription("Changes the primary input source in Windows Standalone during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetWindowsStandalonePrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public WindowsStandalonePrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.windowsStandalonePrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get OSX Standalone Primary Input Source")]
    [TaskDescription("Changes the primary input source in OSX Standalone during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetOSXStandalonePrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.osxStandalonePrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set OSX Standalone Primary Input Source")]
    [TaskDescription("Changes the primary input source in OSX Standalone during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetOSXStandalonePrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public OSXStandalonePrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.osxStandalonePrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Linux Standalone Primary Input Source")]
    [TaskDescription("Changes the primary input source in Linux Standalone during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetLinuxStandalonePrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.linuxStandalonePrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Linux Standalone Primary Input Source")]
    [TaskDescription("Changes the primary input source in Linux Standalone during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetLinuxStandalonePrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public LinuxStandalonePrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.linuxStandalonePrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Windows UWP Primary Input Source")]
    [TaskDescription("Changes the primary input source in Windows 10 Universal during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetWindowsUWPPrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.windowsUWPPrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Windows UWP Primary Input Source")]
    [TaskDescription("Changes the primary input source in Windows 10 Universal during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetWindowsUWPPrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public WindowsUWPPrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.windowsUWPPrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Windows UWP Support HID Devices")]
    [TaskDescription("Toggles support for HID devices in Windows UWP. This includes older gamepads, gamepads made for Android, flight controllers, racing wheels, " +
        "etc. In order to use this feature, you must add support for HID gamepads and joysticks to the app manifest file. " +
        "Please see the Special Platform Support -> Windows 10 Universal documentation for details. " +
        "Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetWindowsUWPSupportHIDDevices : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.windowsUWPSupportHIDDevices);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Windows UWP Support HID Devices")]
    [TaskDescription("Toggles support for HID devices in Windows UWP. This includes older gamepads, gamepads made for Android, flight controllers, racing wheels, " +
        "etc. In order to use this feature, you must add support for HID gamepads and joysticks to the app manifest file. " +
        "Please see the Special Platform Support -> Windows 10 Universal documentation for details. " +
        "Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetWindowsUWPSupportHIDDevices : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.windowsUWPSupportHIDDevices = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Xbox One Primary Input Source")]
    [TaskDescription("Changes the primary input source in Xbox One during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetXboxOnePrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.xboxOnePrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Xbox One Primary Input Source")]
    [TaskDescription("Changes the primary input source in Xbox One during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetXboxOnePrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public XboxOnePrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.xboxOnePrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get PS4 Primary Input Source")]
    [TaskDescription("Changes the primary input source in PS4 during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetPS4PrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.ps4PrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set PS4 Primary Input Source")]
    [TaskDescription("Changes the primary input source in PS4 during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetPS4PrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public PS4PrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.ps4PrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get WebGL Primary Input Source")]
    [TaskDescription("Changes the primary input source in WebGL during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetWebGLPrimaryInputSource : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.webGLPrimaryInputSource);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set WebGL Primary Input Source")]
    [TaskDescription("Changes the primary input source in WebGL during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetWebGLPrimaryInputSource : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public WebGLPrimaryInputSource value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.webGLPrimaryInputSource = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Always Use Unity Input")]
    [TaskDescription("Toggles the use of Unity input during runtime. Rewired will be completely reset if this value is changed. This is an alias for disableNativeInput.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetAlwaysUseUnityInput : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.alwaysUseUnityInput);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Always Use Unity Input")]
    [TaskDescription("Toggles the use of Unity input during runtime. Rewired will be completely reset if this value is changed. This is an alias for disableNativeInput.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetAlwaysUseUnityInput : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.alwaysUseUnityInput = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Disable Native Input")]
    [TaskDescription("Toggles the use of Unity input during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetDisableNativeInput : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.disableNativeInput);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Disable Native Input")]
    [TaskDescription("Toggles the use of Unity input during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetDisableNativeInput : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.disableNativeInput = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Native Mouse Support")]
    [TaskDescription("Toggles the use of native mouse handling during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetNativeMouseSupport : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.nativeMouseSupport);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Native Mouse Support")]
    [TaskDescription("Toggles the use of native mouse handling during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetNativeMouseSupport : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.nativeMouseSupport = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Native Keyboard Support")]
    [TaskDescription("Toggles the use of native keyboard handling during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetNativeKeyboardSupport : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.nativeKeyboardSupport);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Native Keyboard Support")]
    [TaskDescription("Toggles the use of native keyboard handling during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetNativeKeyboardSupport : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.nativeKeyboardSupport = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Enhanced Device Support")]
    [TaskDescription("Toggles the use of enhanced device support during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetEnhancedDeviceSupport : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.enhancedDeviceSupport);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Enhanced Device Support")]
    [TaskDescription("Toggles the use of enhanced device support during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetEnhancedDeviceSupport : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.enhancedDeviceSupport = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Joystick Refresh Rate")]
    [TaskDescription("The joystick refresh rate in frames per second. [0 - 2000] [0 = Default] Set this to a higher value if you need higher precision input timing at high frame rates such as for a music beat game. Higher values result in higher CPU usage. Note that setting this to a very high value when the game is running at a low frame rate will not result in higher precision input. This settings only applies to input sources that use a separate thread to poll for joystick input values (currently XInput and Direct Input). This setting does not apply to event-based input sources such as Raw Input. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetJoystickRefreshRate : GetIntAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.joystickRefreshRate);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Joystick Refresh Rate")]
    [TaskDescription("The joystick refresh rate in frames per second. [0 - 2000] [0 = Default] Set this to a higher value if you need higher precision input timing at high frame rates such as for a music beat game. Higher values result in higher CPU usage. Note that setting this to a very high value when the game is running at a low frame rate will not result in higher precision input. This settings only applies to input sources that use a separate thread to poll for joystick input values (currently XInput and Direct Input). This setting does not apply to event-based input sources such as Raw Input. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetJoystickRefreshRate : SetIntAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.joystickRefreshRate = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Ignore Input When App Not In Focus")]
    [TaskDescription("Ignores input if the application is not in focus This setting has no effect on some platforms. NOTE: Disabling this does not guarantee that input will be processed when the application is out of focus. Whether input is received by the application or not is dependent on A) the input device type B) the current platform C) the input source(s) being used. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetIgnoreInputWhenAppNotInFocus : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.ignoreInputWhenAppNotInFocus);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Ignore Input When App Not In Focus")]
    [TaskDescription("Ignores input if the application is not in focus This setting has no effect on some platforms. NOTE: Disabling this does not guarantee that input will be processed when the application is out of focus. Whether input is received by the application or not is dependent on A) the input device type B) the current platform C) the input source(s) being used. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetIgnoreInputWhenAppNotInFocus : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.ignoreInputWhenAppNotInFocus = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Android Support Unknown Gamepads")]
    [TaskDescription("Toggles the support of unknown gamepads on the Android platform during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetAndroidSupportUnknownGamepads : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.android_supportUnknownGamepads);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Android Support Unknown Gamepads")]
    [TaskDescription("Toggles the support of unknown gamepads on the Android platform during runtime. Rewired will be completely reset if this value is changed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetAndroidSupportUnknownGamepads : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.android_supportUnknownGamepads = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Default Axis Sensitivity Type")]
    [TaskDescription(
        "Changes the default axis sensitivity type for axes. This setting can be changed without resetting Rewired. " +
        "Changing this setting will not change the AxisSensitivityType on Controllers already connected during the game session. " +
        "It will also not change the AxisSensitivityType in saved user data that is loaded."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetDefaultAxisSensitivityType : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.defaultAxisSensitivityType);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Default Axis Sensitivity Type")]
    [TaskDescription(
        "Changes the default axis sensitivity type for axes. This setting can be changed without resetting Rewired. " +
        "Changing this setting will not change the AxisSensitivityType on Controllers already connected during the game session. " +
        "It will also not change the AxisSensitivityType in saved user data that is loaded."
    )]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetDefaultAxisSensitivityType : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public AxisSensitivityType value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.defaultAxisSensitivityType = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Default Joystick Axis2D Dead Zone Type")]
    [TaskDescription("Changes the default dead zone type for 2D joystick axes for recognized controllers. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetDefaultJoystickAxis2DDeadZoneType : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.defaultJoystickAxis2DDeadZoneType);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Default Joystick Axis2D Dead Zone Type")]
    [TaskDescription("Changes the default dead zone type for 2D joystick axes for recognized controllers. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetDefaultJoystickAxis2DDeadZoneType : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public DeadZone2DType value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.defaultJoystickAxis2DDeadZoneType = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Default Joystick Axis2D Sensitivity Type")]
    [TaskDescription("Changes the default sensitivity type for 2D joystick axes for recognized controllers. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetDefaultJoystickAxis2DSensitivityType : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.defaultJoystickAxis2DSensitivityType);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Default Joystick Axis2D Sensitivity Type")]
    [TaskDescription("Changes the default sensitivity type for 2D joystick axes for recognized controllers. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetDefaultJoystickAxis2DSensitivityType : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public AxisSensitivity2DType value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.defaultJoystickAxis2DSensitivityType = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Force 4-Way Hats")]
    [TaskDescription("Force all 8-way hats on recognized joysticks to be treated as 4-way hats. If enabled, the corner directions on all hats will activate the adjacent 2 cardinal direction buttons instead of the corner button. This is useful if you need joystick hats to behave like D-Pads instead of 8-way hats. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetForce4WayHats : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.force4WayHats);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Force 4-Way Hats")]
    [TaskDescription("Force all 8-way hats on recognized joysticks to be treated as 4-way hats. If enabled, the corner directions on all hats will activate the adjacent 2 cardinal direction buttons instead of the corner button. This is useful if you need joystick hats to behave like D-Pads instead of 8-way hats. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetForce4WayHats : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.force4WayHats = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Activate Action Buttons on Negative Value")]
    [TaskDescription("Determines how button values are calculated by Player Actions. If enabled, Actions with either a negative or positive Axis value will return True when queried with player.GetButton. If disabled, Actions with a negative Axis value will always return False when queried with player.GetButton, and must be queried with player.GetNegativeButton. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetActivateActionButtonsOnNegativeValue : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.activateActionButtonsOnNegativeValue);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Activate Action Buttons on Negative Value")]
    [TaskDescription("Determines how button values are calculated by Player Actions. If enabled, Actions with either a negative or positive Axis value will return True when queried with player.GetButton. If disabled, Actions with a negative Axis value will always return False when queried with player.GetButton, and must be queried with player.GetNegativeButton. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetActivateActionButtonsOnNegativeValue : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.activateActionButtonsOnNegativeValue = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Throttle Calibration Mode")]
    [TaskDescription("Determines how throttles on recognized controllers are calibrated. By default, throttles are calibrated for a range of 0 to +1. This is suitable for most flight and racing games. Some games may require a range of -1 to +1 such as space flight games where a negative value denotes a reverse thrust. Changing this setting will revert all throttle calibrations to the default values for the chosen calibration mode.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetThrottleCalibrationMode : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.throttleCalibrationMode);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Throttle Calibration Mode")]
    [TaskDescription("Determines how throttles on recognized controllers are calibrated. By default, throttles are calibrated for a range of 0 to +1. This is suitable for most flight and racing games. Some games may require a range of -1 to +1 such as space flight games where a negative value denotes a reverse thrust. Changing this setting will revert all throttle calibrations to the default values for the chosen calibration mode.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetThrottleCalibrationMode : SetEnumAction {

        [RequiredField]
        [Tooltip("The value to set.")]
        public ThrottleCalibrationMode value;

        protected override TaskStatus DoUpdate() {
            ReInput.configuration.throttleCalibrationMode = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Defer Controller Connected Events on Start")]
    [TaskDescription("Defer controller connected events for controllers already connected when Rewired initializes until the Start event instead of during initialization. Normally, it's impossible to receive controller connection events at the start of runtime because Rewired initializes before any other script is able to subscribe to the controller connected event. Enabling this will defer the controller connected events until the Start event, allowing your scripts to subscribe to the controller connected event in Awake and still receive the event callback. If disabled, controller connection events for controllers already connected before runtime starts will be missed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetDeferControllerConnectedEventsOnStart : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.deferControllerConnectedEventsOnStart);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Defer Controller Connected Events on Start")]
    [TaskDescription("Defer controller connected events for controllers already connected when Rewired initializes until the Start event instead of during initialization. Normally, it's impossible to receive controller connection events at the start of runtime because Rewired initializes before any other script is able to subscribe to the controller connected event. Enabling this will defer the controller connected events until the Start event, allowing your scripts to subscribe to the controller connected event in Awake and still receive the event callback. If disabled, controller connection events for controllers already connected before runtime starts will be missed.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetDeferControllerConnectedEventsOnStart : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.deferControllerConnectedEventsOnStart = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Auto Assign Joysticks")]
    [TaskDescription("Toggles joystick auto-assignment during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetAutoAssignJoysticks : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.autoAssignJoysticks);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Auto Assign Joysticks")]
    [TaskDescription("Toggles joystick auto-assignment during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetAutoAssignJoysticks : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.autoAssignJoysticks = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Max Joysticks Per Player")]
    [TaskDescription("Set the max number of joysticks assigned to each Player by joystick auto-assignment during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetMaxJoysticksPerPlayer : GetIntAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.maxJoysticksPerPlayer);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Max Joysticks Per Player")]
    [TaskDescription("Set the max number of joysticks assigned to each Player by joystick auto-assignment during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetMaxJoysticksPerPlayer : SetIntAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.maxJoysticksPerPlayer = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Distribute Joysticks Evenly")]
    [TaskDescription("Toggles even joystick auto-assignment distribution among Players during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetDistributeJoysticksEvenly : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.distributeJoysticksEvenly);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Distribute Joysticks Evenly")]
    [TaskDescription("Toggles even joystick auto-assignment distribution among Players during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetDistributeJoysticksEvenly : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.distributeJoysticksEvenly = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Assign Joysticks to Playing Players Only")]
    [TaskDescription("Toggles even joystick auto-assignment to Players with isPlayer = True only during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetAssignJoysticksToPlayingPlayersOnly : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.assignJoysticksToPlayingPlayersOnly);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Assign Joysticks to Playing Players Only")]
    [TaskDescription("Toggles even joystick auto-assignment to Players with isPlayer = True only during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetAssignJoysticksToPlayingPlayersOnly : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.assignJoysticksToPlayingPlayersOnly = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Reassign Joystick to Previous Owner on Reconnect")]
    [TaskDescription("Toggles joystick auto-reassignment when re-connected to the last owning Player during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetReassignJoystickToPreviousOwnerOnReconnect : GetBoolAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ReInput.configuration.reassignJoystickToPreviousOwnerOnReconnect);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Reassign Joystick to Previous Owner on Reconnect")]
    [TaskDescription("Toggles joystick auto-reassignment when re-connected to the last owning Player during runtime. This setting can be changed without resetting Rewired.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetReassignJoystickToPreviousOwnerOnReconnect : SetBoolAction {
        protected override TaskStatus DoUpdate() {
            ReInput.configuration.reassignJoystickToPreviousOwnerOnReconnect = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Get Log Level")]
    [TaskDescription("Determines the level of internal logging.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigGetLogLevel : GetEnumAction {
        protected override TaskStatus DoUpdate() {
            UpdateStoreValue((int)ReInput.configuration.logLevel);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_config)]
    [TaskName("Set Log Level")]
    [TaskDescription("Determines the level of internal logging.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredConfigSetLogLevel : BaseAction {

        [Tooltip("Log Info messages.")]
        public SharedBool info;

        [Tooltip("Log Warning messages.")]
        public SharedBool warning;

        [Tooltip("Log Error messages.")]
        public SharedBool error;

        [Tooltip("Log Debug messages.")]
        public SharedBool debug;

        public override void OnReset() {
            base.OnReset();
            info = true;
            warning = true;
            error = true;
            debug = false;
        }

        protected override TaskStatus DoUpdate() {
            Rewired.Config.LogLevelFlags value = Rewired.Config.LogLevelFlags.Off;
            if(info.Value) value |= LogLevelFlags.Info;
            if(warning.Value) value |= LogLevelFlags.Warning;
            if(error.Value) value |= LogLevelFlags.Error;
            if(debug.Value) value |= LogLevelFlags.Debug;
            ReInput.configuration.logLevel = value;
            return TaskStatus.Success;
        }
    }

    #endregion
}