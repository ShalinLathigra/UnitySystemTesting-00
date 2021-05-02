
public interface IAirEntity
{
    public abstract bool canJump { get; }
    public abstract bool airComplete { get; }
    public abstract bool shouldJump { get; }
    public AirSO airSO { get; }
}