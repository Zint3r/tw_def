namespace Game.GameState.MapState
{
    public enum MapStateType
    {
        Idle,
        MapSelectLoad,
		MapSelectUnload
	}
    
    public enum MapStateTrigger
    {
        Open,
        OpeningComplete,
        Close,
        ClosingComplete,
        Unload
    }
}