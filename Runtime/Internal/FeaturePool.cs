// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edanoue.ComponentSystem
{
    internal interface IFeaturePool
    {
        public void Add(IEdaFeature feature);
    }

    internal class FeaturePool<T> : IFeaturePool
        where T : IEdaFeature
    {
        private readonly List<T> _list;

        public FeaturePool(T feature)
        {
            _list = new List<T> { feature };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IFeaturePool.Add(IEdaFeature feature)
        {
            if (feature is T t)
            {
                Add(t);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(T feature)
        {
            _list.Add(feature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetFirstFeature()
        {
            // コンストラクタで強制しているため必ず取得できる
            return _list[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetAllFeatures()
        {
            return _list;
        }
    }
}