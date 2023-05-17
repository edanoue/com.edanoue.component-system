// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// </summary>
    public sealed class EdaFeatureCollectionInternal : IEdaFeatureCollectionInternal
    {
        private readonly EdaComponentCollectorImplementation _impl = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? GetFeature<T>()
            where T : class, IEdaFeature
        {
            return _impl.GetFeature<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetFeatures<T>()
            where T : class, IEdaFeature
        {
            return _impl.GetFeatures<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetFeature<T>(out T? feature)
            where T : class, IEdaFeature
        {
            return _impl.TryGetFeature(out feature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IEdaFeatureCollection.TryGetFeatures<T>(out IEnumerable<T> features)
        {
            return _impl.TryGetFeatures(out features);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IEdaFeatureBuilder.Register<T>(IEdaFeature feature)
        {
            return _impl.AddFeature<T>(feature);
        }

        /// <summary>
        /// EdaFeatureCollector を生成する.
        /// </summary>
        /// <param name="accessor">collector に含める assessor のリスト</param>
        /// <returns></returns>
        public static IEdaFeatureCollection Create(IEnumerable<IEdaFeatureAccessor> accessor)
        {
            var collector = new EdaFeatureCollectionInternal();
            collector.RegisterComponents(accessor);
            // Collection に登録完了を通知する
            collector.OnEndRegister();
            return collector;
        }

        /// <summary>
        /// EdaFeatureCollector を生成する.
        /// </summary>
        /// <param name="accessor">collector に含める accessor のリスト</param>
        /// <returns></returns>
        public static IEdaFeatureCollection Create(params IEdaFeatureAccessor[] accessor)
        {
            var collector = new EdaFeatureCollectionInternal();
            collector.RegisterComponents(accessor);
            // Collection に登録完了を通知する
            collector.OnEndRegister();
            return collector;
        }

        private void OnEndRegister()
        {
            _impl.OnRegisteredComponents(this);
        }

        /// <summary>
        /// </summary>
        /// <param name="accessor"></param>
        private void RegisterComponents(IEnumerable<IEdaFeatureAccessor> accessor)
        {
            // 自身 に Component を追加する
            foreach (var component in accessor)
            {
                // Accessor の追加に失敗したらスキップ
                if (!_impl.AddAccessor(this, component))
                {
                    continue;
                }

                // 他の Accessor も登録するかの確認
                if (component.IsRegisterOtherAccessor(out var otherAccessor))
                {
                    RegisterComponents(otherAccessor);
                }
            }
        }
    }
}