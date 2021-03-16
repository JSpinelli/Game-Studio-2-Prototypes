using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager {
	
    //Hashmap of all the Events
	private Dictionary<Type, GameEvent.Handler> _registeredHandlers = new Dictionary<Type, GameEvent.Handler>();

	public void Register<T>(GameEvent.Handler handler) where T : GameEvent 
	{
		var type = typeof(T);
		if (_registeredHandlers.ContainsKey(type)) 
		{
			if (!IsEventHandlerRegistered(type, handler))
				_registeredHandlers[type] += handler;         
		} 
		else 
		{
			_registeredHandlers.Add(type, handler);         
		}     
	} 

	public void Unregister<T>(GameEvent.Handler handler) where T : GameEvent 
	{         
		var type = typeof(T);
		if (!_registeredHandlers.TryGetValue(type, out var handlers)) return;
		
		handlers -= handler;  
		
		if (handlers == null) 
		{                 
			_registeredHandlers.Remove(type);             
		} 
		else
		{
			_registeredHandlers[type] = handlers;             
		}
	}      
		
	public void Fire(GameEvent e) 
	{       
		var type = e.GetType();

        //Find all the handlers of the triggered event
		if (_registeredHandlers.TryGetValue(type, out var handlers)) 
		{   
            //Pass the vent to the handlers nad let them figure it out         
			handlers(e);
		}     
	} 

    //Check if a handler is already registered
	public bool IsEventHandlerRegistered (Type typeIn, Delegate prospectiveHandler)
	{
		return _registeredHandlers[typeIn].GetInvocationList().Any(existingHandler => existingHandler == prospectiveHandler);
	}
}

public abstract class GameEvent 
{
	public delegate void Handler (GameEvent e);
}

public class BabyMoved : GameEvent
{
	public Transform position;
	
	public BabyMoved(Transform pos)
	{
		position = pos;
	}
}

public class SpookyMeterChange: GameEvent
{
	public float CurrentSpookyValue;
	public SpookyMeterChange( float currentSpooky)
	{
		CurrentSpookyValue = currentSpooky;
	}
}
public class DialogTriggered : GameEvent
{
	public string[] dialog;
	public float[] timers;
	public AudioClip soundClip;
	public DialogTriggered( string[] txt, float[] timers, AudioClip sound)
	{
		dialog = txt;
		this.timers = timers;
		soundClip = sound;
	}
}

public class InteractionTriggered : GameEvent
{
	public string name;
	public InteractionTriggered(string interactionName)
	{
		name = interactionName;
	}
}

