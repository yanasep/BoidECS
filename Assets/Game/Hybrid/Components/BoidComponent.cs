using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Mine.ECS
{
    public class BoidComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Boid());
        }
    }
}
