// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using System.Collections.Generic;

namespace Edanoue.ComponentSystem
{
    public interface IWriteOnlyEdaFeatureCollector
    {
        /// <summary>
        /// Collector に対して Feature の登録を行う.
        /// </summary>
        /// <param name="item">Feature が実装されている参照.</param>
        /// <typeparam name="T">
        /// <para>登録する Feature の型</para>
        /// <para>省略した場合は item 引数自体の型に実装されているすべての <see cref="IEdaFeature" /> インタフェースでの登録が行われる.</para>
        /// </typeparam>
        public void AddFeature<T>(T item)
            where T : IEdaFeature;
    }

    public interface IReadOnlyEdaFeatureCollector
    {
        /// <summary>
        /// Collector から指定した Feature を取得する. 同じ Feature が複数個登録されている場合は最初に登録されていたものを返す.
        /// </summary>
        /// <typeparam name="T">Feature の型を指定</typeparam>
        /// <returns>見つかった Feature の参照, 取得に失敗した場合は null.</returns>
        public T? GetFeature<T>()
            where T : class, IEdaFeature;

        /// <summary>
        /// Collector から指定した Feature をすべて取得する. 
        /// </summary>
        /// <typeparam name="T">Feature の型を指定</typeparam>
        /// <returns>見つかったすべての Feature, 見つからなかった場合は空</returns>
        public IEnumerable<T> GetFeatures<T>()
            where T : class, IEdaFeature;

        /// <summary>
        /// Collector から指定した Feature を取得する. 同じ Feature が複数個登録されている場合は最初に登録されていたものを返す.
        /// </summary>
        /// <param name="feature">見つかった Feature の参照, 取得に失敗した場合は null.</param>
        /// <typeparam name="T">Feature の型を指定</typeparam>
        /// <returns>見つかった場合は true, 見つからなかった場合は false</returns>
        public bool TryGetFeature<T>(out T? feature)
            where T : class, IEdaFeature;

        /// <summary>
        /// Collector から指定した Feature をすべて取得する. 
        /// </summary>
        /// <param name="features">見つかったすべての Feature</param>
        /// <typeparam name="T">Feature の型を指定</typeparam>
        /// <returns>一つでも見つかった場合は true, 見つからなかった場合は false</returns>
        public bool TryGetFeatures<T>(out IEnumerable<T> features)
            where T : class, IEdaFeature;
    }

    /// <summary>
    /// IEdaComponent を管理する
    /// </summary>
    internal interface IEdaFeatureCollector :
        IReadOnlyEdaFeatureCollector,
        IWriteOnlyEdaFeatureCollector,
        IDisposable
    {
        /// <summary>
        /// (内部用)
        /// </summary>
        /// <param name="component"></param>
        internal void AddComponent(IEdaFeatureAccessor component);

        /// <summary>
        /// (内部用)
        /// </summary>
        internal void OnRegisteredComponents();
    }
}