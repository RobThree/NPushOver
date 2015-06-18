namespace NPushOver.RequestObjects
{
    public enum Priority
    {
        Lowest = -2,
        Low = -1,
        Normal = 0,
        High = 1,
        Emergency = 2
    }

    public enum Sound
    {
        Pushover,
        Bike,
        Bugle,
        Cashregister,
        Classical,
        Cosmic,
        Falling,
        Gamelan,
        Incoming,
        Intermission,
        Magic,
        Mechanical,
        Pianobar,
        Siren,
        Spacealarm,
        Tugboat,
        Alien,
        Climb,
        Persistent,
        Echo,
        Updown,
        None
    }

    public enum OS
    {
        Any,
        Android,
        iOS,
        Desktop
    }
}
