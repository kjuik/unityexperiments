using System;

/// <summary>
/// Parent class of your game-specific GameState.
/// Inherit from this class to define data members needed to save and load your game state.
/// For saving complex object states, use subclasses of ObjectState.
/// </summary>
[Serializable]
public abstract class GameState { }
