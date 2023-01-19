// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// <para><see cref="EdaFeatureCollector" /> に所属することができるアクセサーに求められるインタフェース.</para>
    /// <para>Feature の登録と Feature の参照が行える様になる.</para>
    /// </summary>
    public interface IEdaFeatureAccessor
    {
        /// <summary>
        /// <para><see cref="EdaFeatureCollector" /> に対して自身に関連する <see cref="IEdaFeature" /> の登録を行う.</para>
        /// <para>登録する Feature がなければ空の実装でよい.</para>
        /// </summary>
        /// <param name="collector">Feature の登録先となる <see cref="IWriteOnlyEdaFeatureCollector" /></param>
        public void AddFeatures(IWriteOnlyEdaFeatureCollector collector);

        /// <summary>
        /// <para><see cref="EdaFeatureCollector" /> から他の <see cref="IEdaFeature" /> を参照する.</para>
        /// <para>参照する Feature がなければ空の実装でよい.</para>
        /// </summary>
        /// <param name="collector">Feature の参照元となる <see cref="IReadOnlyEdaFeatureCollector" /></param>
        public void GetFeatures(IReadOnlyEdaFeatureCollector collector);
    }
}