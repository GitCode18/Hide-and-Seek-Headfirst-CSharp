namespace Hide_and_Seek
{
    public class Location
    {
        /// <summary>
        /// The name of this location
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Provide a string description of a direction to build the string for what exits can be seen
        /// </summary>
        /// <param name="d">The direction being described</param>
        /// <returns>A string describing the direction</returns>
        public string DirectionDesc (Direction d) => DescribeDirection(d);

        /// <summary>
        /// The exits out of this location
        /// </summary>
        public IDictionary<Direction, Location> Exits { get; private set; }
            = new Dictionary<Direction, Location>();

        /// <summary>
        /// The constructor sets the location name
        /// </summary>
        /// <param name="name">Name of the location</param>
        public Location(string name) => Name = name;

        public override string ToString() => Name;

        /// <summary>
        /// Returns a sequence of descriptions of the exits, sorted by direction
        /// </summary>
        public IEnumerable<string> ExitList => Exits
            .OrderBy(pair => Math.Abs((int)pair.Key))
            .Select(entry => entry.Value.ToString());

        /// <summary>
        /// Adds an exit to this location
        /// </summary>
        /// <param name="direction">Direction of the connecting location</param>
        /// <param name="connectingLocation">Connecting location to add</param>
        public void AddExit(Direction direction, Location connectingLocation)
        {
            Exits.Add(direction, connectingLocation);
            //add a returning exit back to this to connectingLocation
            connectingLocation.AddReturnExit(direction, this);
        }

        /// <summary>
        /// Gets the exit location in a direction
        /// </summary>
        /// <param name="direction">Direction of the exit location</param>
        /// <returns>The exit location, or this if there is no exit in that direction</returns>
        public Location GetExit(Direction direction) => Exits.ContainsKey(direction) ?
            Exits[direction] : this;

        /// <summary>
        /// Describes a direction (e.g "in" vs. "to the North")
        /// </summary>
        /// <param name="d">Direction to describe</param>
        /// <returns>string describing the direction</returns>
        private string DescribeDirection(Direction d) => d switch
        {
            Direction.Up => "Up",
            Direction.Down => "Down",
            Direction.In => "In",
            Direction.Out => "Out",
            _ => $"to the {d}",
        };

        /// <summary>
        /// Adds a return exit to a connecting location
        /// </summary>
        /// <param name="direction">Direction of the connecting location</param>
        /// <param name="connectingLocation">Location to add the return exit to</param>
        private void AddReturnExit(Direction direction, Location connectingLocation) =>
            Exits.Add((Direction)(-(int)direction), connectingLocation);
    }
}
