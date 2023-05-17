// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable

namespace Edanoue.ComponentSystem
{
    /// <summary>
    /// <para><see cref="EdaFeatureCollectorInternal" /> に所属することができるアクセサーに求められるインタフェース.</para>
    /// <para>Feature の登録のみが可能</para>
    /// </summary>
    public interface IWriteOnlyEdaFeatureAccessor : IEdaFeatureAccessor
    {
        void IEdaFeatureAccessor.GetFeatures(IEdaFeatureCollector collector)
        {
            // (南) ここで明示的実装をしておくことで擬似的に GetFeature の実装の必要がない
            // みたいにしていますが, やろうと思ったら別にかけるのでビミョーです
        }
    }
}