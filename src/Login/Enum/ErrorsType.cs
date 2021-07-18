namespace Login.Enum
{
    public enum ErrorsType : byte
    {
        NoError,
        UnknownError,
        PlayerAlreadyLoggedIn,
        ConnectionExpired,
        UnknownUsernameOrPassword,
        CouldNotConnectToTheServer,
        WelcomeByCrossfire,
        YouCannotAccessTheTestServer,
        YouDontHaveTheLastedVersionOfCrossfire,
        YouCannotJoinTheGameFor5Minutes,
        Unknown,
        Blocked,
        UnAvailableInYouRegion = 12
    }
}