// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable

using System;
using System.Collections.Generic;

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// <para><see cref="EdaFeatureCollectorInternal" /> に所属することができるアクセサーに求められるインタフェース.</para>
    /// <para>Feature の登録と Feature の参照が行える様になる.</para>
    /// </summary>
    public interface IEdaFeatureAccessor
    {
        /// <summary>
        /// <para><see cref="EdaFeatureCollectorInternal" /> に対して自身に関連する <see cref="IEdaFeature" /> の登録を行う.</para>
        /// <para>登録する Feature がなければ空の実装でよい.</para>
        /// </summary>
        /// <param name="register">Feature の登録先となる <see cref="IEdaFeatureRegister" /></param>
        protected internal void AddFeatures(IEdaFeatureRegister register);

        /// <summary>
        /// <para><see cref="EdaFeatureCollectorInternal" /> から他の <see cref="IEdaFeature" /> を参照する.</para>
        /// <para>参照する Feature がなければ空の実装でよい.</para>
        /// </summary>
        /// <param name="collector">Feature の参照元となる <see cref="IEdaFeatureCollector" /></param>
        protected internal void GetFeatures(IEdaFeatureCollector collector);

        /// <summary>
        /// Collection 作成時に他の <see cref="IEdaFeatureAccessor" /> を宣言する
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        protected internal bool IsRegisterOtherAccessor(out IEnumerable<IEdaFeatureAccessor> accessor)
        {
            accessor = Array.Empty<IEdaFeatureAccessor>();
            return false;
        }
    }
}