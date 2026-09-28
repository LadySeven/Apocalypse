// ====================== ExpeditionState.cs =========================

/*
 * Describes the current stage of an expedition.
 *
 * Currently:
 *
 * Preparing
 * The expedition has been created but has not left yet.
 *
 * TravellingToLocation
 * The survivor or group is travelling toward the target.
 *
 * Searching
 * The expedition has reached its destination and is
 * currently exploring or scavenging.
 *
 * Returning
 * The expedition has finished searching and is travelling
 * back toward the settlement.
 *
 * Completed
 * The expedition has successfully returned and finished.
 *
 * Future states may include:
 * - Interrupted
 * - Retreating
 * - Stranded
 * - Failed
 *
 * We will only add those when the gameplay requires them.
 */

public enum ExpeditionState
{
    Preparing,
    TravellingToLocation,
    Searching,
    Returning,
    Completed,
}