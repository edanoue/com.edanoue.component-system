// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable

using Edanoue.ComponentSystem;
using UnityEngine;

namespace Edanoue.ComponentSystemSamples
{
    /// <summary>
    /// IEdaFeature (マーカーインターフェース) を継承したインタフェースを定義する
    /// このインタフェース経由で機能が利用される
    /// </summary>
    public interface IPositionController : IEdaFeature
    {
        /// <summary>
        /// ワールド座標を設定する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetWorldPosition(float x, float y, float z);
    }

    /// <summary>
    /// IPositionController を実装している EdaFeatureAccessor を定義する
    /// </summary>
    public class PositionController : MonoBehaviour, IEdaFeatureAccessor, IPositionController
    {
        /// <summary>
        /// Collector に対して機能を登録する関数
        /// </summary>
        /// <param name="collector"></param>
        void IEdaFeatureAccessor.AddFeatures(IWriteOnlyEdaFeatureCollector collector)
        {
            // collector に自身のもつ IPositionController 機能を登録する
            collector.AddFeature<IPositionController>(this);
        }

        /// <summary>
        /// Collector から他の機能を取得する関数
        /// </summary>
        /// <param name="collector"></param>
        void IEdaFeatureAccessor.GetFeatures(IReadOnlyEdaFeatureCollector collector)
        {
            // このコンポーネントでは特に利用しない
        }

        void IPositionController.SetWorldPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }
    }
}