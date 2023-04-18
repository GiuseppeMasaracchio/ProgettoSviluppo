public static class GenericsVault <T> 
{
    private static T playerState;
    private static T grounded;

    public static void SetPlayerInfo(T input) {
        if(input is string) playerState = input;
        if(input is bool) grounded = input;
    }

    public static T GetPlayerInfo(T input) {
        if (input is string) return playerState;
        if (input is bool) return grounded;
        return default(T);
    }
}
