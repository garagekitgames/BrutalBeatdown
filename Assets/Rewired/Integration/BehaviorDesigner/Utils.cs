using System;
using System.Collections.Generic;

namespace Rewired.Integration.BehaviorDesigner {

    public static class Utils {
    
        private static Dictionary<int, Guid> __controllerTemplateTypes;
        private static Dictionary<int, Guid> _controllerTemplateTypes {
            get {
                return __controllerTemplateTypes ?? (__controllerTemplateTypes = new Dictionary<int, Guid>() {
                    { (int)Rewired.Integration.BehaviorDesigner.ControllerTemplateType.Gamepad, new Guid("83b427e4-086f-47f3-bb06-be266abd1ca5") },
                    { (int)Rewired.Integration.BehaviorDesigner.ControllerTemplateType.RacingWheel, new Guid("104e31d8-9115-4dd5-a398-2e54d35e6c83") },
                    { (int)Rewired.Integration.BehaviorDesigner.ControllerTemplateType.HOTAS, new Guid("061a00cf-d8c2-4f8d-8cb5-a15a010bc53e") },
                    { (int)Rewired.Integration.BehaviorDesigner.ControllerTemplateType.FlightYoke, new Guid("f311fa16-0ccc-41c0-ac4b-50f7100bb8ff") },
                    { (int)Rewired.Integration.BehaviorDesigner.ControllerTemplateType.FlightPedals, new Guid("f6fe76f8-be2a-4db2-b853-9e3652075913") },
                    { (int)Rewired.Integration.BehaviorDesigner.ControllerTemplateType.SixDofController, new Guid("2599beb3-522b-43dd-a4ef-93fd60e5eafa") }
                });
            }
        }

        public static Guid GetControllerTemplateTypeGuid(Rewired.Integration.BehaviorDesigner.ControllerTemplateType type) {
            return _controllerTemplateTypes[(int)type];
        }
    
        public static bool DoesTypeImplement(Type type, Type baseOrInterfaceType) {
#if UNITY_WP_8 || UNITY_WP_8_1 || (UNITY_WSA && NETFX_CORE) || (WINDOWS_UWP && NETFX_CORE)
            return baseOrInterfaceType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
#else
            return baseOrInterfaceType.IsAssignableFrom(type);
#endif
        }
    }
}