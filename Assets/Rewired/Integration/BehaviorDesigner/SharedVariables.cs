
namespace Rewired.Integration.BehaviorDesigner {

    using global::BehaviorDesigner.Runtime;

    [System.Serializable]
    public class SharedRewiredObject : SharedVariable<RewiredObject> {

        public T GetObject<T>() {
            if(this.Value == null) return default(T);
            return this.Value.GetObject<T>();
        }

        public static implicit operator SharedRewiredObject(RewiredObject value) { return new SharedRewiredObject { Value = value }; }
    }

    [System.Serializable]
    public class SharedRewiredObjectList : SharedVariable<RewiredObjectList> {
        public SharedRewiredObjectList()
            : base() {
            this.Value = new RewiredObjectList();
        }
        public SharedRewiredObjectList(RewiredObjectList list)
            : base() {
            this.Value = list;
        }

        public static implicit operator SharedRewiredObjectList(RewiredObjectList value) { return new SharedRewiredObjectList { Value = value }; }
    }
}
