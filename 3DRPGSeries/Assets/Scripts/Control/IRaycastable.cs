namespace RPG.Control
{
    public interface IRaycastable
    {
        bool HandleRaycast(PlayerController callingController);
        //passes the player ocntroller that is calling the method
    }
}