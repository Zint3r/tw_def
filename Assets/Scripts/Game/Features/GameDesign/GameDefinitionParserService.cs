using System;
using System.Collections.Generic;
using System.Reflection;
using Core;
using UnityEngine;
using Zenject;

namespace Game.Features.GameDesign
{
    public class GameDefinitionParserService
    {
        [Inject]
        private GameDefinitionModel gameDefinitionDataProvider;
        
        private readonly Dictionary<Type, Parser> parsers = new Dictionary<Type, Parser>();
        
        public void Init(List<IDefinitionParser> handlers)
        {
            handlers.ForEach(handler =>
            {
				MethodInfo[] methods = handler.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);

                foreach (MethodInfo method in methods)
                {
					DefinitionParserAttribute attr = System.Attribute.GetCustomAttribute(method, typeof(DefinitionParserAttribute)) as DefinitionParserAttribute;
                    if (attr != null)
                    {
						Type voType = method.ReturnType;
						Type dtoType = method.GetParameters()[0].ParameterType;

                        if (method.GetParameters().Length != 1
                            || !typeof(IGameDefinition).IsAssignableFrom(voType)
                            || !typeof(IMessage).IsAssignableFrom(dtoType))
                        {
							Debug.LogError("Invalid definition parser: " + method);
                            continue;
                        }

						Parser parser = new Parser
						{
                            handler = handler,
                            method = method,
                            dtoType = dtoType,
                            voType = voType
                        };
						parsers[dtoType] = parser;
                    }
                }
            });
        }
        
        public IGameDefinition Parse(IMessage message)
        {
            Type dtoType = message.GetType();
            if (!parsers.TryGetValue(dtoType, out Parser parser))
            {
                Debug.LogWarning($"Could not parse GD reference with type {dtoType}");
                return null;
            }
            
            return (IGameDefinition) parser.method.Invoke(parser.handler, new object[] {message});
        }
        
        private class Parser
        {
            public Type dtoType;
            public object handler;
            public MethodInfo method;
            public Delegate parserDelegate;
            public Type voType;
        }
    }
}