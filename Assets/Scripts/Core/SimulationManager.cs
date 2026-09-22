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


    void Awake()
    {
        timeManager.OnHourPassed += HandleHourPassed;
    }

    void Start()
    {
        CreateTestSurvivors();
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
        maria.Hunger = 80;
        maria.Thirst = 80;
        maria.Energy = 80;
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

    void HandleHourPassed()
    {
        foreach (var survivor in survivors)
        {
            survivor.PassHour();
            survivor.CurrentAction = decisionMaker.DecideAction(survivor, resourceManager);
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

