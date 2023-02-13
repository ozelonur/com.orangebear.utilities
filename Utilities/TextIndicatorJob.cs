using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace OrangeBear.Utilities
{
    public struct TextIndicatorJob : IJobParallelForTransform
    {
        [ReadOnly] public float UpSpeed;
        [ReadOnly] public float DeltaTime;
        
        public void Execute(int index, TransformAccess transform)
        {
            transform.localPosition += Vector3.up * DeltaTime * UpSpeed;
        }
    }
}