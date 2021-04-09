using UnityEngine;

public static class Services
{
    private static GameManager _gameManager ;
    public static GameManager gameManager
    {
        get
        {
            Debug.Assert(_gameManager != null);
            return _gameManager;
        }
        set => _gameManager = value;
    }

    private static EventManager _eventManager;
    public static EventManager EventManager
    {
        get
        {
            Debug.Assert(_eventManager != null);
            return _eventManager;
        }
        set => _eventManager = value;
    }
    
    private static KitchenSequence _kitchenSequence;
    public static KitchenSequence KitchenSequence
    {
        get
        {
            Debug.Assert(_kitchenSequence != null);
            return _kitchenSequence;
        }
        set => _kitchenSequence = value;
    }

}
