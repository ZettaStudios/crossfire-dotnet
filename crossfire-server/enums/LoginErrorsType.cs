namespace crossfire_server.enums
{
    public enum LoginErrorsType : byte
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
    }
}