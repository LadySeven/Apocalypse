

using System.Collections.Generic;

public class WorldManager
{
    // Stores all currently simulated world locations.
    public List<Location> Locations = new List<Location>();

    public void CreateTestWorld()
    {
        Location generalStore = new Location(
            "Greenfield General Store",
            LocationType.GeneralStore,
            50,
            40,
            5,
            10
        );

        Locations.Add(generalStore);
    }
}