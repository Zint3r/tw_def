using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.GameDesign
{
    public class GameDefinitionModel : IGameDefinitionDataProvider
    {
        private readonly Dictionary<Type, Dictionary<string, GameDefinition>> definitions = new Dictionary<Type, Dictionary<string, GameDefinition>>();
        
        public TDefinition GetOrCreate<TDefinition>(string definitionId) where TDefinition : GameDefinition
        {
            return GetOrCreate(typeof(TDefinition), definitionId) as TDefinition;
        }
        
        public GameDefinition GetOrCreate(Type definitionType, string definitionId)
        {
            if (string.IsNullOrEmpty(definitionId))
            {
                Debug.LogError("Empty definition id encountered! Use GetOrCreateNullable instead.");
                return null;
            }

            if (!definitions.TryGetValue(definitionType, out Dictionary<string, GameDefinition> typeDict))
            {
                typeDict = new Dictionary<string, GameDefinition>();
                definitions[definitionType] = typeDict;
            }

            if (!typeDict.TryGetValue(definitionId, out GameDefinition definitionInstance))
            {
                definitionInstance = (GameDefinition) Activator.CreateInstance(definitionType);
                definitionInstance.DefinitionId = string.Intern(definitionId);
                typeDict[definitionId] = definitionInstance;
            }

            return definitionInstance;
        }
        
        public TDefinition Get<TDefinition>(string definitionId) where TDefinition : GameDefinition
        {
            if (!TryGet(definitionId, out TDefinition gameDefinition))
            {
                Debug.LogWarning($"The {definitionId} was not found!");
            }

            return gameDefinition;
        }
        
        public bool TryGet<TDefinition>(string definitionId, out TDefinition gameDefinition) where TDefinition : GameDefinition
        {
            Type defType = typeof(TDefinition);

            if (!definitions.TryGetValue(defType, out Dictionary<string, GameDefinition> gameDefinitions))
            {
                gameDefinition = null;
                return false;
            }

            bool contains = gameDefinitions.TryGetValue(definitionId, out GameDefinition result);
            gameDefinition = result as TDefinition;

            return contains;
        }
    }
}