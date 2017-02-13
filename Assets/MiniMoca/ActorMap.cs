// <copyright file="ActorMap.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 9:23</date>

using System;
using System.Collections.Generic;

namespace MiniMoca
{
    /// <summary>
    /// ActorMap - map for registration actors, models and managers
    /// </summary>
    public class ActorMap
    {
        //================================       Public Setup       =================================
        //================================    Systems properties    =================================
        private static readonly string ActorInterface = typeof(IMocaActor).ToString();
        /// <summary>
        /// Struct for saving previous access request to temporary cache
        /// </summary>
        private struct AccessRequest
        {
            /// <summary>
            /// Index of instance in holder list
            /// </summary>
            public int Index;

            /// <summary>
            /// ActorType of instance
            /// </summary>
            public Type ActorType;
        }

        private AccessRequest _requestsCache; //TODO: Add several slots for cache
        private readonly List<IMocaActor> _actors;
        private readonly Dictionary<Type, int> _actorMap;
        //================================      Public methods      =================================

        public void MapInstance(IMocaActor actor, Type asType)
        {
            if (asType == null)
                asType = actor.GetType();

            if (_actorMap.ContainsKey(asType))
                throw new ArgumentException("Actor " + asType + " already was mapped!", "asType");
            //Actor can be added to context
            int index = _actors.Count;
            _actorMap.Add(asType, index);
            _actors.Add(actor);
        }

        public void MapInstance(IMocaActor actor)
        {
             MapInstance(actor, actor.GetType());
        }

        public void MapInstance(Type actorType)
        {
            if(actorType.GetInterface(ActorInterface) == null)
                throw new ArgumentException("Actor type not implement IMocaActor interface", "actorType");
            MapInstance(Instantiate(actorType), actorType);
        }

        public void MapInstance<T>() where T : IMocaActor
        {
            MapInstance(typeof(T));
        }


        public IMocaActor Instantiate(Type actorType)
        {
            return MocaReflector.InstantiateActor(actorType);
        }

        public IMocaActor GetActor(Type actorType)
        {
            int index;
            if (_requestsCache.Index < 0 || _requestsCache.ActorType != actorType)
            {
                if (!_actorMap.TryGetValue(actorType, out index))
                    throw new ArgumentException("Actor with type" + actorType + " wasn't mapped!");

                //Save request to temporary cache
                _requestsCache.Index = index;
                _requestsCache.ActorType = actorType;
            }
            else
                index = _requestsCache.Index;

            //return actor
            return _actors[index];
        }

        public T GetActor<T>() where T : IMocaActor
        {
            return (T) GetActor(typeof(T));
        }

        public bool HasActor(Type type)
        {
            return _actorMap.ContainsKey(type);
        }

        public void UnMapInstance(Type type)
        {
            if (_requestsCache.ActorType == type)
            {
                _requestsCache.Index = -1;
                _requestsCache.ActorType = null;
            }
            IMocaActor actor = GetActor(type);
            _actorMap.Remove(type);
            //Removal actor from list but do not change list fragmentation
            int i = _actors.IndexOf(actor);
            _actors[i] = null;
//            actor.Dispose();
        }

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        public ActorMap()
        {
            _requestsCache = new AccessRequest{Index = -1};
            _actorMap = new Dictionary<Type, int>();
            _actors = new List<IMocaActor>();
        }

    }
}