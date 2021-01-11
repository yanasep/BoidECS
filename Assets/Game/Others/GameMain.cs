using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Mine.Others
{
    public class GameMain : MonoBehaviour
    {
        private void Awake()
        {
            //var world = new World("MainWorld");
            //world.CreateSystem<RotateSystem>();
            //var systemTypes = new List<System.Type>()
            //{
            //    typeof(RotateSystem)
            //};

            //World.DefaultGameObjectInjectionWorld = world;
            //DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(world, systemTypes);
            //ScriptBehaviourUpdateOrder.UpdatePlayerLoop(world);
        }

        //private void Update()
        //{
        //    World.DefaultGameObjectInjectionWorld.Update();
        //}

        private void OnDestroy()
        {
            World.DisposeAllWorlds();
        }
    }
}
