using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;

namespace Hide_and_Seek;

public class GameController
{
    /// <summary>
    /// The number of moves the player has made
    /// </summary>
    public int MoveNumber { get; private set; } = 1;

    /// <summary>
    /// Private list of opponents the player needs to find
    /// </summary>
    public readonly IEnumerable<Opponent> Opponents = new List<Opponent>()
    {
        new Opponent("Joe"),
        new Opponent("Bob"),
        new Opponent("Ana"),
        new Opponent("Owen"),
        new Opponent("Jimmy"),
    };

    /// <summary>
    /// Private list of opponents the player has found so far
    /// </summary>
    private readonly List<Opponent> foundOpponents = new List<Opponent>();

    /// <summary>
    /// Returns true if the game is over
    /// </summary>
    public bool GameOver => Opponents.Count() == foundOpponents.Count();

    /// <summary>
    /// The player's current location in the house
    /// </summary>
    public Location CurrentLocation { get; private set; }

    /// <summary>
    /// Returns the current status to show to the player
    /// </summary>
    public string Status
    {
        get
        {
            var exits = CurrentLocation.Exits
                .OrderBy(pair => Math.Abs((int)pair.Key))
                .Select(pair =>
                pair.Value.Name + " is " + pair.Value.DirectionDesc(pair.Key));

            return $"You are in the {CurrentLocation.Name}. " +
            $"You see the following exits:" +
            Environment.NewLine + " - the " +
            String.Join(Environment.NewLine + $" - the ", exits.ToList());
        }
    }

    /// <summary>
    /// A prompt to display to the player
    /// </summary>
    public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";

    public GameController()
    {
        House.ClearHidingPlaces();

        foreach (var opponent in Opponents)
            opponent.Hide();

        CurrentLocation = House.Entry;
    }

    /// <summary>
    /// Move to the location in a direction
    /// </summary>
    /// <param name="direction">The direction to move</param>
    /// <returns>True if the player can move in that direction. False otherwise</returns>
    public bool Move(Direction direction) {
        //save prev location and attempt move to new location
        var prev = CurrentLocation.Name;
        CurrentLocation = CurrentLocation.GetExit(direction);

        //if move failed, return false, else true
        if (prev.Equals(CurrentLocation.Name)) return false;
        return true;

    }

    /// <summary>
    /// Parses input from the player and updates the status
    /// </summary>
    /// <param name="input">Input to parse</param>
    /// <returns>The results of parsing the input</returns>
    /// <exception cref="NotImplementedException"></exception>
    public string ParseInput(string input)
    {
        bool validInput = Enum.TryParse(input, out Direction d);
        if (validInput)
        {
            bool moveWorked = Move(d);
            if (moveWorked)
            {
                return $"Moving {d}";
            }
            else
            {
                return "There's no exit in that direction";
            }
        }
        else
        {
            return "That's not a valid direction";
        }
    }
}
