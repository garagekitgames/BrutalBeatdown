using UnityEngine;

namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;
    using global::BehaviorDesigner.Runtime.Tasks;

    #region Actions

    [TaskCategory(Consts.taskCategory_base + "/List")]
    [TaskName("Get Count")]
    [TaskDescription("Gets the count of elements from the list.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredObjectListGetCount : GetIntAction {

        [RequiredField]
        [Tooltip("The list.")]
        public SharedRewiredObjectList list;

        public override void OnReset() {
            base.OnReset();
            list = new SharedRewiredObjectList();
        }

        protected override TaskStatus DoUpdate() {
            int count = !list.IsNone && list.Value != null ? list.Value.Count : 0;
            storeValue.Value = count;
            return TaskStatus.Success;
        }
    }

    [TaskCategory(Consts.taskCategory_base + "/List")]
    [TaskName("Get Item At Index")]
    [TaskDescription("Gets the item at the specified index from the list.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredObjectListGetItemAtIndex : GetRewiredObjectAction {

        [RequiredField]
        [Tooltip("The list.")]
        public SharedRewiredObjectList list;

        [Tooltip("The index of the element to retrieve.")]
        public SharedInt index;

        public override void OnReset() {
            base.OnReset();
            list = new SharedRewiredObjectList();
            index = 0;
        }

        protected override TaskStatus DoUpdate() {
            int count = !list.IsNone && list.Value != null ? list.Value.Count : 0;
            if(index.Value < 0 || index.Value >= count) return TaskStatus.Failure;
            UpdateStoreValue(list.Value[index.Value]);
            return TaskStatus.Success;
        }
    }

    #endregion

    #region Decorators

    [TaskCategory(Consts.taskCategory_base + "/List")]
    [TaskName("For Each")]
    [TaskDescription("The For Each decorator executes the child tasks once for each item in the List starting at the Current Index. The current item retrieved from the List is stored in the Store Value variable.")]
    [TaskIcon(Consts.taskIconPath)]
    public class RewiredObjectListForEach : Decorator {

        [RequiredField]
        [Tooltip("The list to iterate.")]
        public SharedRewiredObjectList list;

        [RequiredField]
        [Tooltip("Store the value in a variable.")]
        public SharedRewiredObject storeValue;

        [Tooltip("The current element index. You can set this to modify the starting index of the iteration.")]
        public SharedInt currentIndex;

        [Tooltip("Reset the current index counter to zero when the loop finishes? Enable this if you want to iterate the list again.")]
        public bool resetIndexWhenFinished = true;

        private bool isRunning = false;

        public override void OnReset() {
            base.OnReset();
            storeValue = new SharedRewiredObject();
            currentIndex = 0;
            isRunning = false;
        }

        public override bool CanExecute() {
            // Keep running until there are no more items in the list.
            return HasMoreItems();
        }

        public override void OnChildStarted() {
            GetNextItem();
        }

        public override void OnEnd() {
            ResetIteration();
        }

        private bool HasMoreItems() {
            int count = !list.IsNone && list.Value != null ? list.Value.Count : 0;
            int index = currentIndex.Value;
            if(index < 0 || index >= count) return false;
            return true;
        }

        private void GetNextItem() {
            if(!HasMoreItems()) return;
            if(!isRunning) isRunning = true;
            UpdateStoreValue(list.Value[currentIndex.Value]);
            currentIndex.Value += 1; // increment
        }

        private void UpdateStoreValue(RewiredObject value) {
            storeValue.Value = value;
        }

        private void ResetIteration() {
            if(resetIndexWhenFinished) currentIndex.Value = 0;
            isRunning = false;
        }
    }

    #endregion
}