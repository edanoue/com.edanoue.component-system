// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System.Collections.Generic;

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// <para>読み取り専用の EdaFeatureCollector</para>
    /// <para>登録されている <see cref="IEdaFeature" /> の取得を行う</para>
    /// </summary>
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
        /// <para>Collector から指定した Feature を取得する</para>
        /// <para>同じ Feature が複数個登録されている場合は最初に登録されていたものを返す</para>
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
}