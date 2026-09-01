using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace UniSharper.Localization
{ 
    internal static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SetupMessagePackResolver()
        {
            // Create CompositeResolver
            StaticCompositeResolver.Instance.Register(MasterMemoryResolver.Instance, StandardResolver.Instance);

            // Create options with resolver
            var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);

            // Optional: as default.
            MessagePackSerializer.DefaultOptions = options;
        }
    }
}