using Hide_and_Seek;
namespace Hide_and_Seek_Tests
{
    [TestClass]
    public sealed class LocationTests
    {
        private Location center;

        /// <summary>
        /// Initializes each unit test by setting creating a new the center location
        /// and adding a room in each direction before the test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            center = new Location("Center Room");
            Assert.AreSame("Center Room", center.ToString());
            Assert.AreEqual(0, center.ExitList.Count());

            center.AddExit(Direction.North, new Location("North Room"));
            center.AddExit(Direction.South, new Location("South Room"));
            center.AddExit(Direction.East, new Location("East Room"));
            center.AddExit(Direction.West, new Location("West Room"));
            center.AddExit(Direction.Northeast, new Location("Northeast Room"));
            center.AddExit(Direction.Southeast, new Location("Southeast Room"));
            center.AddExit(Direction.Northwest, new Location("Northwest Room"));
            center.AddExit(Direction.Southwest, new Location("Southwest Room"));
            center.AddExit(Direction.Up, new Location("Upper Room"));
            center.AddExit(Direction.Down, new Location("Lower Room"));
            center.AddExit(Direction.In, new Location("Inner Room"));
            center.AddExit(Direction.Out, new Location("Outer Room"));

            Assert.AreEqual(12, center.ExitList.Count());
        }

        /// <summary>
        /// Make sure GetExit returns the location in a direction only if it exists
        /// </summary>
        [TestMethod]
        public void TestGetExit()
        {
            var eastRoom = center.GetExit(Direction.East);
            Assert.AreEqual("East Room", eastRoom.Name);
            Assert.AreSame(center, eastRoom.GetExit(Direction.West));
            Assert.AreSame(eastRoom, eastRoom.GetExit(Direction.Up));
        }

        /// <summary>
        /// Validates that the exit lists are working
        /// </summary>
        [TestMethod]
        public void TestExitList()
        {
            List<string> exitList = center.ExitList.ToList();
            List<string> testList = [
                "North Room",
                "South Room",
                "East Room",
                "West Room",
                "Northeast Room",
                "Southwest Room",
                "Southeast Room",
                "Northwest Room",
                "Upper Room",
                "Lower Room",
                "Inner Room",
                "Outer Room",
                ];

            CollectionAssert.AreEqual(exitList, testList);

        }

        /// <summary>
        /// Validates that each room's name and return exit is created correctly
        /// </summary>
        [TestMethod]
        public void TestReturnExits()
        {
            var northRoom = center.GetExit(Direction.North);
            Assert.AreSame(center, northRoom.GetExit(Direction.South));
            Assert.AreSame(northRoom, northRoom.GetExit(Direction.Up));

            var southRoom = center.GetExit(Direction.South);
            Assert.AreSame(center, southRoom.GetExit(Direction.North));
            Assert.AreSame(southRoom, southRoom.GetExit(Direction.Up));


            var westRoom = center.GetExit(Direction.West);
            Assert.AreSame(center, westRoom.GetExit(Direction.East));
            Assert.AreSame(westRoom, westRoom.GetExit(Direction.Up));

            var eastRoom = center.GetExit(Direction.East);
            Assert.AreSame(center, eastRoom.GetExit(Direction.West));
            Assert.AreSame(eastRoom, eastRoom.GetExit(Direction.Up));

            var southwestRoom = center.GetExit(Direction.Southwest);
            Assert.AreSame(center, southwestRoom.GetExit(Direction.Northeast));
            Assert.AreSame(southwestRoom, southwestRoom.GetExit(Direction.Up));

            var southeastRoom = center.GetExit(Direction.Southeast);
            Assert.AreSame(center, southeastRoom.GetExit(Direction.Northwest));
            Assert.AreSame(southeastRoom, southeastRoom.GetExit(Direction.Up));

            var northwestRoom = center.GetExit(Direction.Northwest);
            Assert.AreSame(center, northwestRoom.GetExit(Direction.Southeast));
            Assert.AreSame(northwestRoom, northwestRoom.GetExit(Direction.Up));

            var northeastRoom = center.GetExit(Direction.Northeast);
            Assert.AreSame(center, northeastRoom.GetExit(Direction.Southwest));
            Assert.AreSame(northeastRoom, northeastRoom.GetExit(Direction.Up));

            var upperRoom = center.GetExit(Direction.Up);
            Assert.AreSame(center, upperRoom.GetExit(Direction.Down));
            Assert.AreSame(upperRoom, upperRoom.GetExit(Direction.Up));

            var lowerRoom = center.GetExit(Direction.Down);
            Assert.AreSame(center, lowerRoom.GetExit(Direction.Up));
            Assert.AreSame(lowerRoom, lowerRoom.GetExit(Direction.Down));

            var innerRoom = center.GetExit(Direction.In);
            Assert.AreSame(center, innerRoom.GetExit(Direction.Out));
            Assert.AreSame(innerRoom, innerRoom.GetExit(Direction.Up));

            var outerRoom = center.GetExit(Direction.Out);
            Assert.AreSame(center, outerRoom.GetExit(Direction.In));
            Assert.AreSame(outerRoom, outerRoom.GetExit(Direction.Up));
        }

        /// <summary>
        /// Add a hall to one of the rooms and make sure the hall room's names
        /// and return exits are created correctly
        /// </summary>
        [TestMethod]
        public void TestAddHall()
        {
            //add an east room to the east room, and verify correct placement
            Location eastEastRoom = new Location("Easteast Room");
            center.GetExit(Direction.East).AddExit(Direction.East, eastEastRoom);

            //check name is correct
            Assert.AreEqual("Easteast Room", 
                center.GetExit(Direction.East).GetExit(Direction.East).Name);
            //check object reference is correct
            Assert.AreSame(eastEastRoom, center.GetExit(Direction.East).GetExit(Direction.East));
            //check number of exits in newly added room is correct
            Assert.AreEqual(1, center.GetExit(Direction.East)
                .GetExit(Direction.East)
                .Exits.Count());

            //add an east room to the east room's east room
            //then verify correct placement
            Location eastEastEastRoom = new Location("Easteasteast Room");
            center.GetExit(Direction.East).GetExit(Direction.East)
                .AddExit(Direction.East, eastEastEastRoom);

            //check name is correct
            Assert.AreEqual("Easteasteast Room", center.GetExit(Direction.East)
                .GetExit(Direction.East)
                .GetExit(Direction.East)
                .Name);
            //check object reference is correct
            Assert.AreSame(eastEastEastRoom, center.GetExit(Direction.East)
                .GetExit(Direction.East)
                .GetExit(Direction.East));
            //check number of exits in newly added room is correct
            Assert.AreEqual(1, center.GetExit(Direction.East)
                .GetExit(Direction.East)
                .GetExit(Direction.East).Exits.Count());

            //check that eastEast Room now has 2 exits
            Assert.AreEqual(2, center.GetExit(Direction.East)
                .GetExit(Direction.East)
                .Exits.Count());
        }
    }
}
