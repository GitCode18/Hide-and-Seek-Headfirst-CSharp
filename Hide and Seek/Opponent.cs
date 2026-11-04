namespace Hide_and_Seek
{
    public class Opponent
    {
        public readonly string Name;
        public Opponent(string name) => Name = name;
        public override string ToString() => Name;

        public void Hide()
        {
            //start at the Entry
            var currentLocation = House.GetLocationByName("Entry");

            //while the current room has no hiding places
            while(currentLocation is not LocationWithHidingPlace)
            {
                //enter a random room between 10 and 50 times
                //Random rand = new Random();
                int numberOfTimes = House.Random.Next(10, 50);
                for (int i = 0; i < numberOfTimes; i++)
                {
                    currentLocation = House.RandomExit(currentLocation);
                }
            }

            //perform the actual hiding action
            (currentLocation as LocationWithHidingPlace).Hide(this);

            //write the location used to Debug
            System.Diagnostics.Debug.WriteLine($"{Name} is hiding " +
                $"{(currentLocation as LocationWithHidingPlace).HidingPlace} in the {currentLocation
                .Name}");
        }
    }
}
