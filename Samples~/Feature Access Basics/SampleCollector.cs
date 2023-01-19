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
        private Button? _button;

        private void Awake()
        {
            // 自身の 兄弟にある IEdaFeatureAccessor 継承コンポーネントをすべて検索する
            var monoBehaviourAccessor = GetComponents<IEdaFeatureAccessor>();

            // C# Native の IEdaFeatureAccessor 継承クラスを初期化する
            _button = new Button();
            var nativeAccessor = new IEdaFeatureAccessor[]
            {
                _button
            };

            // Collector に渡す IEdaFeatureAccessor の集合を作成する
            var allAccessor = monoBehaviourAccessor.Concat(nativeAccessor);

            // Collector を作成する
            // このタイミングで 各 Accessor に実装されている関数が呼ばれる 
            EdaFeatureCollector.Create(allAccessor);
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