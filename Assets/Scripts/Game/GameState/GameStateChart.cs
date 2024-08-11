using Core;
using Core.Actions;
using Game.GameState.MapState;
using Module.States;
using System;
using UnityEngine;
using Zenject;

namespace Game.GameState
{
	public interface IGameStateChart
    {
        void ExecuteStateTrigger(MapStateTrigger trigger);
    }
    
    public class GameStateChart : IGameStateChart, IInitializable, IDisposable
    {
        [Inject]
        private StateFactory stateFactory;
        
        private readonly FiniteStateMachine<MapStateType, MapStateTrigger> _mapFsm = new FiniteStateMachine<MapStateType, MapStateTrigger>();
        
        public void Initialize()
        {
            Debug.Log("Initialize GameStateChart");

            ActionQueue queue = new ActionQueue();
            queue.AddAction(stateFactory.Create<BootStateAction>());
            queue.AddAction(stateFactory.Create<MainStateAction>());
            queue.Start();
            
            _mapFsm.AddState(MapStateType.Idle)
                .AddTransition(MapStateType.MapSelectLoad, MapStateTrigger.Open);

            _mapFsm.AddState(MapStateType.MapSelectLoad)
                .OnEntryAction(MapSelectLoad)
                .AddTransition(MapStateType.MapSelectUnload, MapStateTrigger.Unload);
            
            _mapFsm.AddState(MapStateType.MapSelectUnload)
                .OnEntryAction(MapSelectUnload)
                .AddTransition(MapStateType.Idle, MapStateTrigger.Close);
            
            _mapFsm.Finalize(MapStateType.Idle);
        }

        private void MapSelectLoad()
        {
            ActionQueue queue = new ActionQueue();
            queue.AddAction(stateFactory.Create<UnloadMainState>());
			queue.AddAction(stateFactory.Create<LoadMapAction>());
			//queue.AddAction(stateFactory.Create<LoadAbbysMapAction>());
            queue.Start();
        }
        
        private void MapSelectUnload()
        {
            ActionQueue queue = new ActionQueue();
            //queue.AddAction(stateFactory.Create<UnloadAbbysMapAction>());
            queue.AddAction(stateFactory.Create<MainStateAction>());
            queue.Start();

            ExecuteStateTrigger(MapStateTrigger.Close);
        }
        
        public void ExecuteStateTrigger(MapStateTrigger trigger)
        {
            if (_mapFsm.IsFinalized)
            {
                _mapFsm.ExecuteTrigger(trigger);
            }
        }
        
        public void Dispose()
        {

        }
    }
}