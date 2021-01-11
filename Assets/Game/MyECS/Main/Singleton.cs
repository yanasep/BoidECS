using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mine.MyECS
{
    public class Singleton
    {
        public static Singleton Instance { get; private set; }

        public Params Params { get; private set; }
        public Module Module { get; private set; }
        public GameEvent GameEvent { get; private set; }

        const string paramsPath = "Param";

        public static void Init()
        {
            Instance = new Singleton
            {
                Params = Resources.Load<Params>(paramsPath)
            };
            Debug.Assert(Instance.Params != null);
            Instance.Reset();
        }

        public void Reset()
        {
            Module = new Module();
            GameEvent = new GameEvent();
        }
    }

    public class Module
    {
        public Func<GameObject> GetBoidPrefab;
        public Func<List<Boid>> GetBoids;
        public Func<AreaBounds> GetAreaBounds;
    }

    public class GameEvent
    {
        public Communicator<int> onSpawnBoids = new Communicator<int>();
    }
}