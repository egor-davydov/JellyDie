﻿using System;
using System.Collections.Generic;

namespace Code.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly GameStateFactory _gameStateFactory;
    private Dictionary<Type, IExitableState> _states;
    
    private IExitableState _currentState;

    public GameStateMachine(GameStateFactory gameStateFactory) => 
      _gameStateFactory = gameStateFactory;

    public void Setup()
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(LoadProgressState)] = _gameStateFactory.Get<LoadProgressState>(),
        [typeof(LoadLevelState)] = _gameStateFactory.Get<LoadLevelState>(),
      };
    }

    public void Enter<TState>() where TState : IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
    {
      IPayloadState<TPayload> state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : IExitableState
    {
      _currentState?.Exit();
      TState state = (TState)_states[typeof(TState)];
      _currentState = state;
      
      return state;
    }
  }
}