using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    #region RuleSet

    [TaskCategory(Consts.taskCategory_base + "/Layout Manager Rule Set/Properties")]
    [TaskName("Layout Manager Rule Set: Get Enabled")]
    [TaskDescription("If enabled, the rule set will be evaluated. Otherwise, it will be ignored.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredLayoutManagerRuleSetGetEnabled : RewiredObjectGetBoolAction<ControllerMapLayoutManager.RuleSet> {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(TObject.enabled);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Layout Manager Rule Set/Properties")]
    [TaskName("Layout Manager Rule Set: Set Enabled")]
    [TaskDescription("If enabled, the rule set will be evaluated. Otherwise, it will be ignored.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredLayoutManagerRuleSetSetEnabled : RewiredObjectSetBoolAction<ControllerMapLayoutManager.RuleSet> {

        protected override TaskStatus DoUpdate() {
            TObject.enabled = value.Value;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Layout Manager Rule Set/Properties")]
    [TaskName("Layout Manager Rule Set: Get Tag")]
    [TaskDescription("The tag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredLayoutManagerRuleSetGetTag : RewiredObjectGetStringAction<ControllerMapLayoutManager.RuleSet> {

        protected override TaskStatus DoUpdate() {
            UpdateStoreValue(TObject.tag);
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/Layout Manager Rule Set/Properties")]
    [TaskName("Layout Manager Rule Set: Set Tag")]
    [TaskDescription("The tag.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredLayoutManagerRuleSetSetTag : RewiredObjectSetStringAction<ControllerMapLayoutManager.RuleSet> {

        protected override TaskStatus DoUpdate() {
            TObject.tag = value.Value;
            return TaskStatus.Success;
        }
    }

    #endregion
}