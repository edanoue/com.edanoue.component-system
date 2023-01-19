// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable

using System.Linq;
using Edanoue.ComponentSystem;
using UnityEngine;

namespace Edanoue.ComponentSystemSamples
{
    public class SampleCollector : MonoBehaviour
    {
        // Awake で作成される Button の参照
        private IButton? _button;

        // GC に回収されないように Collector をキャッシュしておく
        private IReadOnlyEdaFeatureCollector? _collector;

        private void Awake()
        {
            // 自身の 兄弟にある IEdaFeatureAccessor 継承コンポーネントをすべて検索する
            var monoBehaviourAccessor = GetComponents<IEdaFeatureAccessor>();

            // C# Native の IEdaFeatureAccessor 継承クラスを初期化する
            var button = new Button();
            _button = button;
            var nativeAccessor = new IEdaFeatureAccessor[]
            {
                button
            };

            // Collector に渡す IEdaFeatureAccessor の集合を作成する
            var allAccessor = monoBehaviourAccessor.Concat(nativeAccessor);

            // Collector を作成する
            _collector = EdaFeatureCollector.Create(allAccessor);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 40), "Random Box Y"))
            {
                _button?.Push();
            }
        }
    }
}