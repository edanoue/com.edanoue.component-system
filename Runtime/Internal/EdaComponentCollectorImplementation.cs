// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// (内部用)
    /// </summary>
    internal interface IEdaFeatureCollector :
        IReadOnlyEdaFeatureCollector,
        IWriteOnlyEdaFeatureCollector
    {
    }

    /// <summary>
    /// </summary>
    internal sealed class EdaComponentCollectorImplementation
    {
        // 登録された Accessor のリスト
        private readonly HashSet<IEdaFeatureAccessor> _components = new();

        // 登録された IEdaFeature の辞書
        private readonly Dictionary<Type, IFeaturePool> _features = new();

        // Accessor を追加できるかどうかのフラグ
        private bool _canAddAccessor = true;

        public bool AddAccessor(IEdaFeatureCollector collector, IEdaFeatureAccessor featureAssessor)
        {
            // Accessor を追加できる状態で無いならばスキップする
            if (!_canAddAccessor)
            {
                return false;
            }

            // すでに登録済みの Component であればスキップする
            if (_components.Contains(featureAssessor))
            {
                return false;
            }

            // 内部でキャッシュしておく
            _components.Add(featureAssessor);

            // Accessor からの Feature 登録を行う
            featureAssessor.AddFeatures(collector);

            return true;
        }

        /// <summary>
        /// </summary>
        public void OnRegisteredComponents(IEdaFeatureCollector collector)
        {
            // Accessor を追加できる状態で無いならばスキップする
            if (!_canAddAccessor)
            {
                return;
            }

            // 登録済みのコンポーネント全てに通知を送る
            foreach (var component in _components)
            {
                component.GetFeatures(collector);
            }

            // これ以上の Accessor の追加を無効化する
            _canAddAccessor = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddFeatureInternal<T>(T feature)
            where T : IEdaFeature
        {
            try
            {
                _features[typeof(T)].Add(feature);
            }
            // まだ辞書に登録されていなかった場合は作成して追加する
            catch (KeyNotFoundException)
            {
                _features.Add(typeof(T), new FeaturePool<T>(feature));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="feature"></param>
        /// <typeparam name="T"></typeparam>
        public bool AddFeature<T>(IEdaFeature feature)
            where T : IEdaFeature
        {
            var inType = typeof(T);
            var typeIEdaFeature = typeof(IEdaFeature);

            // IEdaFeature 自体が渡されたらスキップする
            if (inType == typeIEdaFeature)
            {
                return false;
            }

            // IEdaFeature 継承クラスが T に指定されたら単体の登録を行う
            if (feature is not T t)
            {
                return false;
            }

            AddFeatureInternal(t);
            return true;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetFeature<T>()
            where T : IEdaFeature
        {
            try
            {
                return ((FeaturePool<T>)_features[typeof(T)]).GetFirstFeature();
            }
            // 辞書に登録されていなかったとき
            catch (KeyNotFoundException)
            {
                return default;
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetFeatures<T>()
            where T : IEdaFeature
        {
            try
            {
                return ((FeaturePool<T>)_features[typeof(T)]).GetAllFeatures();
            }
            // 辞書に登録されていなかったとき
            catch (KeyNotFoundException)
            {
                return Array.Empty<T>();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="feature"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool TryGetFeature<T>(out T? feature)
            where T : IEdaFeature
        {
            try
            {
                feature = ((FeaturePool<T>)_features[typeof(T)]).GetFirstFeature();
                return true;
            }
            // 辞書に登録されていなかったとき
            catch (KeyNotFoundException)
            {
                feature = default;
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="features"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool TryGetFeatures<T>(out IEnumerable<T> features)
            where T : IEdaFeature
        {
            try
            {
                features = ((FeaturePool<T>)_features[typeof(T)]).GetAllFeatures();
                return true;
            }
            // 辞書に登録されていなかったとき
            catch (KeyNotFoundException)
            {
                features = Array.Empty<T>();
                return false;
            }
        }
    }
}