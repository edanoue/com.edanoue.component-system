// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// <para>書き込み専用の EdaFeatureCollector</para>
    /// <para>Accessor 側から <see cref="IEdaFeature" /> の登録を行う</para>
    /// </summary>
    public interface IWriteOnlyEdaFeatureCollector
    {
        /// <summary>
        /// Collector に対して Feature の登録を行う.
        /// </summary>
        /// <param name="feature">Feature が実装されている参照.</param>
        /// <typeparam name="T">
        /// <para>登録する Feature の型</para>
        /// <para>省略した場合は item 引数自体の型に実装されているすべての <see cref="IEdaFeature" /> インタフェースでの登録が行われる.</para>
        /// </typeparam>
        public bool AddFeature<T>(IEdaFeature feature)
            where T : IEdaFeature;
    }
}