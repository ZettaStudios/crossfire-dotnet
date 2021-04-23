namespace crossfire_server.enums
{
    public enum RoomOptions : byte
    {
        Rounds,
        Kills,
        Time,
        EscapeModeAttackAndDefense,
        ZombieModeEasy = 5,
        ZombieModeNormal,
        ZombieModeHard,
        ZombieModeExpert,
        ZombieModeNightmare,
        ShadowEm = 11,
        ShadowInf,
        Score,
        SingleRound,
    }
}