// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using System.Collections.Generic;

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// </summary>
    internal sealed class EdaComponentCollectorImplementation
    {
        // 登録された Component のリスト
        private readonly HashSet<IEdaFeatureAccessor> _components = new();

        // 登録された Feature の辞書
        private readonly Dictionary<Type, List<IEdaFeature>> _features = new();

        // コンポーネントを追加できるかどうかのフラグ
        private bool _canAddComponent = true;

        public bool AddComponent(IEdaFeatureCollector collector, IEdaFeatureAccessor featureAssessor)
        {
            // コンポーネントを追加できる状態で無いならばスキップする
            if (!_canAddComponent)
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

            // コンポーネント側に通知を送る
            featureAssessor.AddFeatures(collector);

            return true;
        }

        /// <summary>
        /// </summary>
        public void OnRegisteredComponents(IEdaFeatureCollector collector)
        {
            if (!_canAddComponent)
            {
                return;
            }

            // 登録済みのコンポーネント全てに通知を送る
            foreach (var component in _components)
            {
                component.GetFeatures(collector);
            }

            _canAddComponent = false;
        }

        private void AddFeatureInternal(Type type, IEdaFeature feature)
        {
            try
            {
                _features[type].Add(feature);
            }
            // まだ辞書に登録されていなかった場合は作成して追加する
            catch (KeyNotFoundException)
            {
                var list = new List<IEdaFeature> { feature };
                _features.Add(type, list);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="feature"></param>
        /// <typeparam name="T"></typeparam>
        public void AddFeature<T>(T feature)
            where T : IEdaFeature
        {
            var typeIEdaFeature = typeof(IEdaFeature);
            var inType = typeof(T);

            // IEdaFeature 自体が渡されたらスキップする
            if (inType == typeIEdaFeature)
            {
                return;
            }

            // IEdaFeature 継承クラスが T に指定されたら単体の登録を行う
            if (inType.IsInterface && typeIEdaFeature.IsAssignableFrom(inType))
            {
                AddFeatureInternal(inType, feature);
                return;
            }

            // インターフェース以外が渡された場合は, 実装インタフェースのうち IEdaFeature を継承しているものをすべて登録する
            foreach (var interfaceType in typeof(T).GetInterfaces())
            {
                // IEdaFeature そのものであればスキップ
                if (interfaceType == typeof(IEdaFeature))
                {
                    continue;
                }

                // IEdaFeature を継承したインタフェースでなければスキップ
                if (!typeof(IEdaFeature).IsAssignableFrom(interfaceType))
                {
                    continue;
                }

                AddFeatureInternal(interfaceType, feature);
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetFeature<T>()
            where T : class, IEdaFeature
        {
            try
            {
                return _features[typeof(T)][0] as T;
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
            var type = typeof(T);
            if (_features.ContainsKey(type))
            {
                return (IEnumerable<T>)_features[type];
            }

            return Array.Empty<T>();
        }

        /// <summary>
        /// </summary>
        /// <param name="feature"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool TryGetFeature<T>(out T? feature)
            where T : class, IEdaFeature
        {
            try
            {
                feature = _features[typeof(T)][0] as T;
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
            var type = typeof(T);
            if (_features.ContainsKey(type))
            {
                features = (IEnumerable<T>)_features[type];
                return true;
            }

            features = Array.Empty<T>();
            return false;
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            _features.Clear();
            _components.Clear();
        }
    }
}