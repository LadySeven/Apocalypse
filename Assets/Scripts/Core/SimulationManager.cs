// ====================== SimulationManager.cs =========================
/* Coordinates the currently implemented simulation systems.
 *
 * Currently responsible for:
 * - Creating the initial test survivors.
 * - Listening for time progression.
 * - Updating survivors when an hour passes.
 * - Asking the decision maker what each survivor should do.
 * - Executing the selected action.
 *
 * Future responsibilities may include coordinating:
 * - Resources.
 * - Base simulation.
 * - Exploration.
 * - World events.
 * - Outbreak progression.
 * - NPC groups.
 *
 * This class should primarily coordinate systems rather than
 * containing all of the game's rules itself.
 */

using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    private List<Survivor> survivors = new List<Survivor>();
    public TimeManager timeManager;
    private SurvivorDecisionMaker decisionMaker = new SurvivorDecisionMaker();
    private ResourceManager resourceManager = new ResourceManager();
    private OutbreakManager outbreakManager = new OutbreakManager();

    private WorldManager worldManager = new WorldManager();
    private ExplorationManager explorationManager = new ExplorationManager();

    void Awake()
    {
        timeManager.OnHourPassed += HandleHourPassed;
    }

    void Start()
    {
        CreateTestSurvivors();
        worldManager.CreateTestWorld();
        DisplayWorldLocations();
        TestExpedition();
        timeManager.AdvanceHours(5);
    }

    void CreateTestSurvivors()
    { 
        // Created temporary test survivors for testing the simulation systems. 
        // Later, survivors should be created through a dedicated SurvivorGenerator or Loaded from saved game data.
        Survivor maria = new Survivor();
        maria.Name = "Maria";
        maria.Occupation = "Nurse";
        maria.Health = 100;
        maria.Hunger = 90;
        maria.Thirst = 80;
        maria.Energy = 40;
        maria.Stress = 10;
        survivors.Add(maria);


        Survivor andrea = new Survivor();
        andrea.Name = "Andrea";
        andrea.Occupation = "Software Engineer";
        andrea.Health = 100;
        andrea.Hunger = 50;
        andrea.Thirst = 20;
        andrea.Energy = 100;
        andrea.Stress = 80;
        survivors.Add(andrea);


        Survivor joshua = new Survivor();
        joshua.Name = "Joshua";
        joshua.Occupation = "IT Specialist";
        joshua.Health = 100;
        joshua.Hunger = 30;
        joshua.Thirst = 30;
        joshua.Energy = 90;
        joshua.Stress = 60;
        survivors.Add(joshua);
    }

    void DisplayWorldLocations()
    {
        foreach (Location location in worldManager.Locations)
        {
            Debug.Log(
                "Location: " + location.Name +
                " | Type: " + location.Type +
                " | Food: " + location.Food +
                " | Water: " + location.Water +
                " | Medicine: " + location.Medicine +
                " | Materials: " + location.Materials
            );
        }
    }

    /*
    * Creates our temporary NPC expedition.
    *
    * Maria is assigned to Greenfield General Store.
    * this method does NOT manually advance or complete the expedition.
    *
    * The expedition will progress automatically as game hours pass.
    */
    void TestExpedition()
    {
        Survivor maria = survivors[0];
        Location generalStore = worldManager.Locations[0];

        Expedition expedition = new Expedition(maria, generalStore);
        explorationManager.StartExpedition(expedition);

        Debug.Log(
            "EXPEDITION STARTED" +
            " | Survivor: " + expedition.Survivor.Name +
            " | Target: " + expedition.TargetLocation.Name +
            " | State: " + expedition.State +
            " | Store Food: " + generalStore.Food +
            " | Settlement Food: " + resourceManager.Food
        );
    }

     /* Called every time TimeManager reports that one in-game hour has passed.
     * For every survivor:
     *   1. Update their needs.
     *   2. Ask the decision maker what they want to do.
     *   3. Store their selected action.
     *   4. Display the utility scores used for the decision.
     *   5. Execute the action.
     *   6. Display the resulting survivor and resource state.
     */
    void HandleHourPassed()
    {
        // This is placed not inside the loop because this is a world state,
        // not something belonging to any survivors.
        outbreakManager.UpdateOutbreak(timeManager.currentDay);
        Debug.Log(
            "World State" +
            " | Day: " + timeManager.currentDay +
            " | Hour: " + timeManager.currentHour +
            " | Outbreak: " + outbreakManager.CurrentLevel
        );

        // Progress all active NPC expeditions by one hour.
        explorationManager.UpdateExpeditions(resourceManager);

        // Displays the current state of every active expedition.
        foreach (Expedition expedition in explorationManager.ActiveExpeditions)
        {
            Debug.Log(
                "EXPEDITION UPDATE" +
                " | Survivor: " + expedition.Survivor.Name +
                " | Target: " + expedition.TargetLocation.Name +
                " | State: " + expedition.State +
                " | Food Collected: " + expedition.FoodCollected +
                " | Settlement Food: " + resourceManager.Food
            );
        }

        foreach (Survivor survivor in survivors)
        {
            survivor.PassHour();

            if (survivor.IsOnExpedition)
            {
                Debug.Log(
                    survivor.Name +
                    " is currently away on an expedition." +
                    " | Energy: " + survivor.Energy +
                    " | Hunger: " + survivor.Hunger +
                    " | Thirst: " + survivor.Thirst +
                    " | Current Action: " + survivor.CurrentAction
                );
                // Stops processing the current item and move to the next one
                continue;
            }

            SurvivorDecision decision = decisionMaker.DecideAction(survivor, resourceManager);
            survivor.CurrentAction = decision.SelectedAction;
            
            Debug.Log(
                survivor.Name +
                " Utility Scores: " +
                " | Eat: " + decision.EatScore +
                " | Drink: " + decision.DrinkScore +
                " | Rest: " + decision.RestScore +
                " | Selected: " + decision.SelectedAction
            );

            ExecuteAction(survivor);

            Debug.Log(
                survivor.Name +
                " | Energy: " + survivor.Energy +
                " | Hunger: " + survivor.Hunger +
                " | Thirst: " + survivor.Thirst +
                " | Current Action: " + survivor.CurrentAction +
                " | Food: " + resourceManager.Food +
                " | Water: " + resourceManager.Water +
                " | Medicine: " + resourceManager.Medicine +
                " | Materials: " + resourceManager.Materials
            );
        }
    }

    /* Executes the action selected by the decision maker.
     *
     * The decision maker determines WHAT the survivor wants
     * to do, while this method determines HOW that action
     * is currently performed.
     *
     * Future versions will delegate more complex actions
     * to dedicated systems such as:
     * - WorkSystem
     * - ExplorationSystem
     * - ResourceSystem
     * - CombatSystem
     */
    void ExecuteAction(Survivor survivor)
    {
        switch (survivor.CurrentAction)
        {
            case SurvivorAction.Eat:
                if(resourceManager.ConsumeFood(1))
                {
                    survivor.Eat();
                    Debug.Log(survivor.Name + " is eating.");
                }
                else
                {
                    Debug.Log(survivor.Name + " wanted to eat, but there is no food available.");
                }
                break;
            case SurvivorAction.Drink:
                if(resourceManager.ConsumeWater(1))
                {
                    survivor.Drink();
                    Debug.Log(survivor.Name + " is drinking.");
                }
                else
                {
                    Debug.Log(survivor.Name + " wanted to drink, but there is no water available.");
                }
                break;
            case SurvivorAction.Idle:
                Debug.Log(survivor.Name + " is idle.");
                break;
            case SurvivorAction.Rest:
                survivor.Rest();
                Debug.Log(survivor.Name + " is resting.");
                break;
            case SurvivorAction.Work:
                survivor.Work();
                Debug.Log(survivor.Name + " is working.");
                break;
            case SurvivorAction.Explore:
                Debug.Log(survivor.Name + " is exploring.");
                break;
        }
    }
}

