// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// </summary>
    public class EdaFeatureCollector : IEdaFeatureCollector
    {
        private readonly EdaComponentCollectorImplementation _impl = new();

        void IDisposable.Dispose()
        {
            _impl.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaFeatureCollector.AddComponent(IEdaFeatureAccessor featureAssessor)
        {
            _impl.AddComponent(this, featureAssessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaFeatureCollector.OnRegisteredComponents()
        {
            _impl.OnRegisteredComponents(this);
        }

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
        bool IReadOnlyEdaFeatureCollector.TryGetFeatures<T>(out IEnumerable<T> features)
        {
            return _impl.TryGetFeatures(out features);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IWriteOnlyEdaFeatureCollector.AddFeature<T>(T item)
        {
            _impl.AddFeature(item);
        }

        /// <summary>
        /// EdaFeatureCollector を生成する.
        /// </summary>
        /// <param name="accessor">collector に含める assessor のリスト</param>
        /// <returns></returns>
        public static IReadOnlyEdaFeatureCollector Create(IEnumerable<IEdaFeatureAccessor> accessor)
        {
            var collector = new EdaFeatureCollector();
            collector.RegisterComponents(accessor);
            return collector;
        }

        /// <summary>
        /// EdaFeatureCollector を生成する.
        /// </summary>
        /// <param name="accessor">collector に含める accessor のリスト</param>
        /// <returns></returns>
        public static IReadOnlyEdaFeatureCollector Create(params IEdaFeatureAccessor[] accessor)
        {
            var collector = new EdaFeatureCollector();
            collector.RegisterComponents(accessor);
            return collector;
        }

        /// <summary>
        /// </summary>
        /// <param name="accessor"></param>
        private void RegisterComponents(IEnumerable<IEdaFeatureAccessor> accessor)
        {
            // 自身 に Component を追加する
            foreach (var component in accessor)
            {
                _impl.AddComponent(this, component);
            }

            // Collection に登録完了を通知する
            _impl.OnRegisteredComponents(this);
        }
    }
}