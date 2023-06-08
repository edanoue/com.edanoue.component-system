// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using UnityEngine;

namespace Edanoue.ComponentSystem
{
    internal sealed class EdaFeatureAccessorCollector : MonoBehaviour
    {
        [SerializeField]
        private Target m_collectTarget;

        private void Awake()
        {
            switch (m_collectTarget)
            {
                case Target.SiblingOnly:
                {
                    var mbAccessor = GetComponents<IEdaFeatureAccessor>();
                    EdaFeatureCollector.Create(mbAccessor);
                    break;
                }
                case Target.AllDescendants:
                {
                    var mbAccessor = GetComponentsInChildren<IEdaFeatureAccessor>();
                    EdaFeatureCollector.Create(mbAccessor);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum Target
        {
            SiblingOnly,
            AllDescendants
        }
    }
}