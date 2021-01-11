using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Mine.Others
{
    public class RotateSystem : ComponentSystem
    {
        EntityQuery query;

        protected override void OnCreate()
        {   
            query = GetEntityQuery(ComponentType.ReadOnly<RotatingCubeComponent>());
        }

        protected override void OnUpdate()
        {
            Entities.With(query).ForEach((Entity e, RotatingCubeComponent cube) =>
            {
                cube.transform.Rotate(0, cube.roationSpeed * Time.DeltaTime, 0);
            });
        }
    }
}
