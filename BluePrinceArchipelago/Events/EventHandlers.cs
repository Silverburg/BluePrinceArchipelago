using Il2CppSystem;

namespace BluePrinceArchipelago.Events
{
    public class LocationEventArgs : EventArgs
    {
        public string LocationName { get; set; }
        public string LocationType { get; set; }

        public LocationEventArgs(string locationName, string locationType) { 
            LocationName = locationName;
            LocationType = locationType;
        }
    }

    public class EventHandlers {

        public delegate void LocationHandler( LocationEventArgs args);

        public static event LocationHandler LocationFound;

    }


}
