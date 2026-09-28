// ====================== Expedition.cs =========================

/*
 * Expedition
 *
 * Represents a survivor or group of survivors sent into
 * the world with a specific destination.
 *
 * Currently:
 * - Stores one survivor.
 * - Stores the target location.
 * - Tracks collected food.
 * - Tracks whether the expedition has completed.
 *
 * Future versions may support:
 * - Multiple survivors.
 * - Expedition leaders.
 * - Player-issued orders.
 * - Travel time.
 * - Multiple resource types.
 * - Carrying capacity.
 * - Equipment and weapons.
 * - Injuries.
 * - Encounters.
 * - Noise.
 * - Retreating.
 * - Failed expeditions.
 * - Returning to the settlement.
 *
 * This class represents expedition DATA.
 * It does not need a Unity GameObject.
 */
public class Expedition
{
    public Survivor Survivor;
    public Location TargetLocation;

    public int FoodCollected;

    public ExpeditionState State;

    /*
     * Creates a new expedition.
     *
     * For our first test, an expedition contains only
     * one survivor and one destination.
     */
    public Expedition(
        Survivor survivor,
        Location targetLocation)
    {
        Survivor = survivor;
        TargetLocation = targetLocation;

        FoodCollected = 0;
        State = ExpeditionState.Preparing;
    }
}