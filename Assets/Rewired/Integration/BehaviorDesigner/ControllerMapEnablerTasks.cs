using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    #region Rule Set

    [TaskCategory(Consts.taskCategory_base + "/Map Enabler Rule Set/Properties")]
    [TaskName("Map Enabler Rule Set: Get Enabled")]
    [TaskDescription("If enabled, the rule set will be evaluated. Otherwise, it will be ignored.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredMapEnablerRuleSetGetEnabled : RewiredObjectGetBoolAction<ControllerMapEnabler.RuleSet> {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(TObject.enabled);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Map Enabler Rule Set/Properties")]
    [TaskName("Map Enabler Rule Set: Set Enabled")]
    [TaskDescription("If enabled, the rule set will be evaluated. Otherwise, it will be ignored.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredMapEnablerRuleSetSetEnabled : RewiredObjectSetBoolAction<ControllerMapEnabler.RuleSet> {

        protected override TaskStatus DoUpdate() {
            TObject.enabled = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Map Enabler Rule Set/Properties")]
    [TaskName("Map Enabler Rule Set: Get Tag")]
    [TaskDescription("The tag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredMapEnablerRuleSetGetTag : RewiredObjectGetStringAction<ControllerMapEnabler.RuleSet> {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(TObject.tag);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Map Enabler Rule Set/Properties")]
    [TaskName("Map Enabler Rule Set: Set Tag")]
    [TaskDescription("The tag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredMapEnablerRuleSetSetTag : RewiredObjectSetStringAction<ControllerMapEnabler.RuleSet> {

        protected override TaskStatus DoUpdate() {
            TObject.tag = value.Value;
            return TaskStatus.Success;
        }
    }

    #endregion
}
