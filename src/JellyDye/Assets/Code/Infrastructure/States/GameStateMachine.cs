﻿using System;
using System.Collections.Generic;
using Code.Services.Factories;

namespace Code.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly GameStateFactory _gameStateFactory;
    private Dictionary<Type, IExitableState> _states;
    
    private IExitableState _currentState;

    public GameStateMachine(GameStateFactory gameStateFactory) => 
      _gameStateFactory = gameStateFactory;

    public void SetupStates()
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(InitializationState)] = _gameStateFactory.Create<InitializationState>(),
        [typeof(LoadProgressState)] = _gameStateFactory.Create<LoadProgressState>(),
        [typeof(WarmUpState)] = _gameStateFactory.Create<WarmUpState>(),
        [typeof(LoadLevelState)] = _gameStateFactory.Create<LoadLevelState>(),
        [typeof(GameLoopState)] = _gameStateFactory.Create<GameLoopState>(),
      };
    }

    public void Enter<TState>() where TState : IState
    {
      IState state = ChangeState<TState>();
      state.Enter().Forget();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
    {
      IPayloadState<TPayload> state = ChangeState<TState>();
      state.Enter(payload).Forget();
    }

    private TState ChangeState<TState>() where TState : IExitableState
    {
      _currentState?.Exit().Forget();
      TState state = (TState)_states[typeof(TState)];
      _currentState = state;
      
      return state;
    }
  }
}