// Copyright Edanoue, Inc. All Rights Reserved.

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edanoue.ComponentSystem.Wrapper
{
    /// <summary>
    /// </summary>
    public interface IEdaTransform : IEdaFeature
    {
        /// <summary>
        /// </summary>
        /// <param name="position"></param>
        public void SetWorldPosition(in Vector3 position);

        /// <summary>
        /// </summary>
        /// <param name="rotation"></param>
        public void SetWorldRotation(in Quaternion rotation);

        /// <summary>
        ///     <para>Set the world space position and rotation of the Transform component.</para>
        ///     <para>
        ///         When setting both the position and rotation of a transform,
        ///         calling this method is more efficient than assigning to position and rotation individually.
        ///     </para>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SetWorldPositionAndRotation(in Vector3 position, in Quaternion rotation);

        /// <summary>
        /// </summary>
        /// <param name="position"></param>
        public void SetLocalPosition(in Vector3 position);

        /// <summary>
        /// </summary>
        /// <param name="rotation"></param>
        public void SetLocalRotation(in Quaternion rotation);

        /// <summary>
        ///     <para>
        ///         Sets the position and rotation of the Transform component in local space (i.e. relative to its parent
        ///         transform).
        ///     </para>
        ///     <para>
        ///         When setting both the position and rotation of a transform,
        ///         calling this method is slightly more efficient than assigning to localPosition and localRotation individually.
        ///     </para>
        ///     <para>If the transform has no parent, then calling this is equivalent to calling SetPositionAndRotation.</para>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SetLocalPositionAndRotation(in Vector3 position, in Quaternion rotation);
    }

    /// <summary>
    /// </summary>
    public class EdaTransform : IEdaTransform
    {
        private readonly Transform _transform;

        public EdaTransform(Transform transform)
        {
            _transform = transform;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaTransform.SetWorldPosition(in Vector3 position)
        {
            _transform.position = position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaTransform.SetLocalPosition(in Vector3 position)
        {
            _transform.localPosition = position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaTransform.SetWorldRotation(in Quaternion rotation)
        {
            _transform.rotation = rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaTransform.SetLocalRotation(in Quaternion rotation)
        {
            _transform.localRotation = rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaTransform.SetLocalPositionAndRotation(in Vector3 position, in Quaternion rotation)
        {
            _transform.SetLocalPositionAndRotation(position, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IEdaTransform.SetWorldPositionAndRotation(in Vector3 position, in Quaternion rotation)
        {
            _transform.SetPositionAndRotation(position, rotation);
        }
    }
}