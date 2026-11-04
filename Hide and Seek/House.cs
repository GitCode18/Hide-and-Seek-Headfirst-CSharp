using System.Runtime.InteropServices.Marshalling;

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
            var garage = new LocationWithHidingPlace("Garage", "under the car");
            var hallway = new Location("Hallway");
            var kitchen = new LocationWithHidingPlace("Kitchen", "under the sink");
            var bathroom = new LocationWithHidingPlace("Bathroom", "behind the door");
            var livingRoom = new LocationWithHidingPlace("Living Room", "behind the couch");
            var landing = new Location("Landing");
            var masterBedroom = new LocationWithHidingPlace("Master Bedroom", "under the bed");
            var masterBath = new LocationWithHidingPlace("Master Bath", "behind the shower curtain");
            var secondBathroom = new LocationWithHidingPlace("Second Bathroom", "in the cabinet");
            var nursery = new LocationWithHidingPlace("Nursery", "behind the crib");
            var pantry = new LocationWithHidingPlace("Pantry", "in the food pile");
            var kidsRoom = new LocationWithHidingPlace("Kids Room", "in the closet");
            var attic = new LocationWithHidingPlace("Attic", "in a large box");

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

        /// <summary>
        /// Removes all hiders from all LocationWithHidingPlace instances
        /// </summary>
        public static void ClearHidingPlaces()
        {
            foreach(Location location in locationDictionary.Values)
            {
                if(location is LocationWithHidingPlace)
                {
                    (location as LocationWithHidingPlace).currentHiders.Clear();
                }
            }
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
