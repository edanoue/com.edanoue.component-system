// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using Edanoue.ComponentSystem;

namespace Edanoue.ComponentSystemSamples
{
    public interface IButton : IEdaFeature
    {
        void Push();
    }

    /// <summary>
    /// IEdaFeatureAccessor であり, IButton を実装している C# Native Class
    /// </summary>
    public class Button : IEdaFeatureAccessor, IButton
    {
        private readonly Random               _randomGenerator = new();
        private          IPositionController? _controller;

        public void Push()
        {
            // 押されるたびにランダムな Y 座標を設定
            var random = _randomGenerator.Next(0, 100);
            // IPositionController が取得できていたら座標を代入する
            _controller?.SetWorldPosition(0, random / 100f, 0);
        }

        void IEdaFeatureAccessor.AddFeatures(IWriteOnlyEdaFeatureCollector collector)
        {
            // 自身の持つ IButton 機能を collector に登録する
            // このサンプルでは他のクラスから参照されないため省略しても問題ない
            collector.AddFeature<IButton>(this);
        }

        void IEdaFeatureAccessor.GetFeatures(IReadOnlyEdaFeatureCollector collector)
        {
            // collector から IPositionController を参照する
            _controller = collector.GetFeature<IPositionController>();
        }
    }
}