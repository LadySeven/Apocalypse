// ====================== ExplorationManager.cs =========================

/*
 * Handles exploration and scavenging operations.
 *
 * Currently:
 * - Allows an expedition to scavenge food.
 * - Records successfully collected food.
 * - Completes an expedition.
 * - Transfers collected food into settlement storage.
 *
 * Future responsibilities may include:
 * - Travel.
 * - Searching locations.
 * - Multiple survivors.
 * - Player-issued expedition orders.
 * - Zombie encounters.
 * - Human encounters.
 * - Injuries.
 * - Equipment.
 * - Carrying capacity.
 * - Noise.
 * - Retreating.
 * - Returning to the settlement.
 *
 * This is an ordinary C# class.
 * It does not need a Unity GameObject.
 */
 
using System.Collections.Generic;
using UnityEngine;

public class ExplorationManager
{
    /*
     * Attempts to collect food from an expedition's
     * target location.
     *
     * Location.TakeFood() performs the validation for us.
     *
     * Food is only recorded by the expedition if the
     * location successfully provides the requested amount.
     */

    public List<Expedition> ActiveExpeditions = new List<Expedition>();
    public List<Expedition> CompletedExpeditions = new List<Expedition>();
    
    public void StartExpedition(Expedition expedition)
    {
        if (expedition == null)
        {
            return;
        }

        if (ActiveExpeditions.Contains(expedition))
        {
            return;
        }

        if (expedition.Survivor.IsOnExpedition)
        {
            return;
        }

        expedition.Survivor.IsOnExpedition = true;
        expedition.Survivor.CurrentAction = SurvivorAction.Explore;

        ActiveExpeditions.Add(expedition);
    }

    public void UpdateExpeditions(ResourceManager resourceManager)
    {
        foreach (Expedition expedition in ActiveExpeditions)
        {
            switch (expedition.State)
            {
                case ExpeditionState.Preparing:
                    expedition.State = ExpeditionState.TravellingToLocation;
                    break;
                
                case ExpeditionState.TravellingToLocation:
                    expedition.State = ExpeditionState.Searching;
                    break;
                
                case ExpeditionState.Searching:
                    ScavengeFood(expedition, 10);
                    expedition.State = ExpeditionState.Returning;
                    break;

                case ExpeditionState.Returning:
                    CompleteExpedition(expedition, resourceManager);
                    break;
            }
        }

        for (int i = ActiveExpeditions.Count - 1; i >= 0; i--)
        {
            Expedition expedition = ActiveExpeditions[i];

            if (expedition.State == ExpeditionState.Completed)
            {
                CompletedExpeditions.Add(expedition);
                Debug.Log(
                    "EXPEDITION MOVED TO HISTORY" +
                    " | Survivor: " + expedition.Survivor.Name +
                    " | Target: " + expedition.TargetLocation.Name +
                    " | Food Collected: " + expedition.FoodCollected
                );

                ActiveExpeditions.RemoveAt(i);
            }
        }
    }

    public void ScavengeFood(
        Expedition expedition,
        int amount)
    {
        if (expedition.TargetLocation.TakeFood(amount))
        {
            expedition.FoodCollected += amount;
        }
    }

    /*
     * Completes an expedition and transfers its collected
     * food into settlement storage.
     *
     * Resources can only be deposited when the expedition
     * is Returning.
     *
     * Once deposited, the expedition becomes Completed.
     */
    public void CompleteExpedition(Expedition expedition, ResourceManager resourceManager)
    {
        if (expedition.State != ExpeditionState.Returning)
        {
            return;
        }

        resourceManager.AddFood(expedition.FoodCollected);
        expedition.State = ExpeditionState.Completed;

        expedition.Survivor.IsOnExpedition = false;
        expedition.Survivor.CurrentAction = SurvivorAction.Idle;
    }
}