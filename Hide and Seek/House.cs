namespace Hide_and_Seek
{
    public class House
    {
        public static Location Entry;

        public static Random Random = new Random();

        public static IDictionary<String, Location> locationDictionary = new Dictionary<String, Location>();

        static House()
        {
            Entry = new Location("Entry");

            //create the rest of the house locations
            var garage = new Location("Garage");
            var hallway = new Location("Hallway");
            var kitchen = new Location("Kitchen");
            var bathroom = new Location("Bathroom");
            var livingRoom = new Location("Living Room");
            var landing = new Location("Landing");
            var masterBedroom = new Location("Master Bedroom");
            var masterBath = new Location("Master Bath");
            var secondBathroom = new Location("Second Bathroom");
            var nursery = new Location("Nursery");
            var pantry = new Location("Pantry");
            var kidsRoom = new Location("Kids Room");
            var attic = new Location("Attic");

            //add the new house locations starting from Entry
            Entry.AddExit(Direction.Out, garage);
            Entry.AddExit(Direction.East, hallway);
            hallway.AddExit(Direction.Northwest, kitchen);
            hallway.AddExit(Direction.North, bathroom);
            hallway.AddExit(Direction.South, livingRoom);
            hallway.AddExit(Direction.Up, landing);
            landing.AddExit(Direction.Northwest, masterBedroom);
            masterBedroom.AddExit(Direction.East, masterBath);
            landing.AddExit(Direction.West, secondBathroom);
            landing.AddExit(Direction.Southwest, nursery);
            landing.AddExit(Direction.South, pantry);
            landing.AddExit(Direction.Southeast, kidsRoom);
            landing.AddExit(Direction.Up, attic);

            //add locations to the dictionary
            locationDictionary.Add(Entry.Name, Entry);
            locationDictionary.Add(garage.Name, garage);
            locationDictionary.Add(hallway.Name, hallway);
            locationDictionary.Add(kitchen.Name, kitchen);
            locationDictionary.Add(bathroom.Name, bathroom);
            locationDictionary.Add(livingRoom.Name, livingRoom);
            locationDictionary.Add(landing.Name, landing);
            locationDictionary.Add(masterBedroom.Name, masterBedroom);
            locationDictionary.Add(masterBath.Name, masterBath);
            locationDictionary.Add(secondBathroom.Name, secondBathroom);
            locationDictionary.Add(nursery.Name, nursery);
            locationDictionary.Add(pantry.Name, pantry);
            locationDictionary.Add(kidsRoom.Name, kidsRoom);
            locationDictionary.Add(attic.Name, attic);
        }

        public static Location GetLocationByName(string s) =>
            locationDictionary.TryGetValue(s, out var location) ? location : Entry;

        public static Location RandomExit(Location loc)
        {
            //sort the exitList by absolute value of the keys
            var exitList = loc.Exits.OrderBy(pair => pair.Value.Name);

            //randomly decide how many locations to skip
            int toSkip = Random.Next(0, exitList.Count());
            return exitList.Skip(toSkip).First().Value;
        }
    }
}
