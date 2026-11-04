namespace Hide_and_Seek
{
    public class LocationWithHidingPlace : Location
    {
        public String HidingPlace {  get; private set; }
        public List<Opponent> currentHiders = new List<Opponent>();
        public LocationWithHidingPlace(string name, string hidingPlace) : base(name)
        {
            HidingPlace = hidingPlace;
        }

        /// <summary>
        /// Add the opponent to the list of opponents in the hiding spot
        /// </summary>
        /// <param name="opponent">The opponent to add</param>
        public void Hide(Opponent opponent) => currentHiders.Add(opponent);

        public List<Opponent> CheckHidingPlace()
        {
            var hiders = currentHiders;
            currentHiders = new List<Opponent>();
            return hiders;
        }
    }
}
