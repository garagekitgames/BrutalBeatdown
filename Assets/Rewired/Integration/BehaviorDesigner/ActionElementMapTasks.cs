using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    #region Get

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Enabled")]
    [TaskDescription("Gets whether the the Action Element Map is enabled. Disabled maps will never return input.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetEnabled : ActionElementMapGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.enabled);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Action Id")]
    [TaskDescription("Gets the id of the Action to which the element is bound.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetActionId : ActionElementMapGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.actionId);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Element Type")]
    [TaskDescription("Gets the element type of the controller element bound to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetElementType : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.elementType);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(ControllerElementType newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Element Identifier Id")]
    [TaskDescription("Gets the controller element identifier id bound to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetElementIdentifierId : ActionElementMapGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.elementIdentifierId);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Axis Range")]
    [TaskDescription("Gets the range of the axis.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetAxisRange : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.axisRange);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(AxisRange newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Invert")]
    [TaskDescription("Gets whether the axis inverted.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetInvert : ActionElementMapGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.invert);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Axis Contribution")]
    [TaskDescription("Gets the axis contribution of the axis.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetAxisContribution : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.axisContribution);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(Pole newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Keyboard Key Code")]
    [TaskDescription("Gets the keyboard key code. Only used for keyboard bindings. Returns Rewired.KeyboardKeyCode value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetKeyboardKeyCode : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.keyboardKeyCode);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(KeyboardKeyCode newValue) {
            if(newValue != (KeyboardKeyCode)storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Modifier Key 1")]
    [TaskDescription("Gets the first keyboard modifier key.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetModifierKey1 : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.modifierKey3);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(ModifierKey newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Modifier Key 2")]
    [TaskDescription("Gets the second keyboard modifier key.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetModifierKey2 : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.modifierKey2);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(ModifierKey newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Modifier Key 3")]
    [TaskDescription("Gets the third keyboard modifier key.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetModifierKey3 : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.modifierKey3);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(ModifierKey newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Axis Type")]
    [TaskDescription("Gets the axis type.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetAxisType : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.axisType);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(AxisType newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Modifier Key Flags")]
    [TaskDescription("Gets glags representing all the assigned keyboard modifier keys.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetModifierKeyFlags : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.modifierKeyFlags);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(ModifierKeyFlags newValue) {
            if(!newValue.Equals(storeValue.Value)) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Key Code")]
    [TaskDescription("Gets the keyboard key code. Only used for keyboard bindings. Returns UnityEngine.KeyCode value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetKeyCode : ActionElementMapAction {

        [Tooltip("Store the result in a variable.")]
        public SharedInt storeValue;

        public override void OnReset() {
            base.OnReset();
            storeValue = 0;
        }

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.keyCode);
            return TaskStatus.Success;
        }

        protected void UpdateStoreValue(KeyCode newValue) {
            if(newValue != (KeyCode)storeValue.Value) { // value changed
                // Store new value
                storeValue.Value = (int)newValue;
            }
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Has Modifiers")]
    [TaskDescription("Gets whether this use any keyboard modfiier keys.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetHasModifiers : ActionElementMapGetBoolAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.hasModifiers);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Element Identifier Name")]
    [TaskDescription("Gets the name of the element identifier bound to the Action. For split axes, this will return the Positive or Negative name or the Descriptive Name with a +/- suffix.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetElementIdentifierName : ActionElementMapGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.elementIdentifierName);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Element Index")]
    [TaskDescription("Gets the controller element index pointed to by this mapping.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetElementIndex : ActionElementMapGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.elementIndex);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Id")]
    [TaskDescription("Gets the unique runtime id of this ActionElementMap. This value is not consistent between game sessions, so do not store it.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetId : ActionElementMapGetIntAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.id);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Get Action Descriptive Name")]
    [TaskDescription("Gets the descriptive name of the Action. For split axes, this will return the Positive or Negative Descriptive Name or the Descriptive Name with a +/- suffix.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapGetActionDescriptiveName : ActionElementMapGetStringAction {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(ActionElementMap.actionDescriptiveName);
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Set

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Enabled")]
    [TaskDescription("Sets whether the the Action Element Map is enabled. Disabled maps will never return input.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetEnabled : ActionElementMapSetBoolAction {

        protected override TaskStatus DoUpdate() {
            ActionElementMap.enabled = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Action Id")]
    [TaskDescription("Sets the id of the Action to which the element is bound.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetActionId : ActionElementMapSetIntAction {

        protected override TaskStatus DoUpdate() {
            ActionElementMap.actionId = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Element Identifier Id")]
    [TaskDescription("Sets the controller element identifier id bound to the Action.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetElementIdentifierId : ActionElementMapSetIntAction {

        protected override TaskStatus DoUpdate() {
            ActionElementMap.elementIdentifierId = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Axis Range")]
    [TaskDescription("Sets the range of the axis.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetAxisRange : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public AxisRange value;

        public override void OnReset() {
            base.OnReset();
            value = AxisRange.Full;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.axisRange = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Invert")]
    [TaskDescription("Sets whether the axis inverted.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetInvert : ActionElementMapSetBoolAction {

        protected override TaskStatus DoUpdate() {
            ActionElementMap.invert = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Axis Contribution")]
    [TaskDescription("Sets the axis contribution of the axis.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetAxisContribution : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public Pole value;

        public override void OnReset() {
            base.OnReset();
            value = Pole.Positive;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.axisContribution = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Keyboard Key Code")]
    [TaskDescription("Sets the keyboard key code. Only used for keyboard bindings. Returns Rewired.KeyboardKeyCode value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetKeyboardKeyCode : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public KeyboardKeyCode value;

        public override void OnReset() {
            base.OnReset();
            value = KeyboardKeyCode.None;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.keyboardKeyCode = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Modifier Key 1")]
    [TaskDescription("Sets the first keyboard modifier key.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetModifierKey1 : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public ModifierKey value;

        public override void OnReset() {
            base.OnReset();
            value = ModifierKey.None;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.modifierKey3 = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Modifier Key 2")]
    [TaskDescription("Sets the second keyboard modifier key.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetModifierKey2 : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public ModifierKey value;

        public override void OnReset() {
            base.OnReset();
            value = ModifierKey.None;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.modifierKey2 = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Modifier Key 3")]
    [TaskDescription("Sets the third keyboard modifier key.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetModifierKey3 : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public ModifierKey value;

        public override void OnReset() {
            base.OnReset();
            value = ModifierKey.None;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.modifierKey3 = value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Action Element Map/Properties")]
    [TaskName("Action Element Map: Set Key Code")]
    [TaskDescription("Sets the keyboard key code. Only used for keyboard bindings. Returns UnityEngine.KeyCode value.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredActionElementMapSetKeyCode : ActionElementMapAction {

        [Tooltip("The value to set.")]
        public KeyCode value;

        public override void OnReset() {
            base.OnReset();
            value = KeyCode.None;
        }

        protected override TaskStatus DoUpdate() {
            ActionElementMap.keyCode = value;
            return TaskStatus.Success;
        }
    }

    #endregion
}