namespace Login.Enum
{
    public enum PacketType : short
    {
        Unknown,
        #region Client To Server
        C2SLogin,
        C2SGoBackForServers,
        C2SCreateAccount,
        C2SCheckNameExists,
        C2SAccountAlreadyLoggedOn,
        C2SLoginToGameServerStep1,
        C2SExit,
        C2SLoginToGameServerStep2,
        C2SRequestExit,
        #endregion

        #region Server To Client
        S2CDisplayError,
        S2CGetServers,
        S2CGoBackForServers,
        S2CTryEnter,
        S2CCreateAccount,
        S2CCheckNameExist,
        S2CPlayerHasBeenLoggedOut,
        S2CLoginToGameServerStep1,
        S2CLoginToGameServerStep2,
        S2CExitGameInfo,
        S2CValidAccount,
        #endregion
    }
}