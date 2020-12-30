using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mine.Mono
{
    public class Singleton
    {
        public static Singleton Instance { get; private set; }

        public Params Params { get; private set; }
        public Module Modules { get; private set; }

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
            Modules = new Module();
        }
    }

    public class Module
    {
        public Func<List<Boid>> GetBoids;
        public Func<Simulator.AreaBound> GetAreaBound;
    }
}