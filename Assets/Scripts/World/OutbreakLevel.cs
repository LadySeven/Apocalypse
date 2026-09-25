// ====================== OutbreakLevel.cs =========================
/*
 * Represents the current general severity of the zombie outbreak.
 *
 * Currently:
 * - Low
 * - Moderate
 * - High
 *
 * These levels provide a simple way for other systems to
 * understand the current state of the outbreak.
 *
 * Future versions may use more detailed world-state information
 * such as:
 * - Number of infected people.
 * - Zombie population.
 * - Government stability.
 * - Military presence.
 * - Civil order.
 * - Electricity and communications.
 * - Available food and medicine.
 * - Regional infection levels.
 *
 * The enum itself only describes the outbreak level.
 * It does not calculate when the level changes.
 */

public enum OutbreakLevel
{
   Low,
   Moderate,
   High,
}