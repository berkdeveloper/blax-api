namespace BlaX.CryptoAutoTrading.Domain.Core.Constants
{
    public static class AdminRoleConst
    {
        #region User Id
        /// <summary>
        /// Admin User Id - Berkant Ayar
        /// </summary>
        public const string BerkantUserId = "6";
        /// <summary>
        /// Admin User Id - Berk Sarıkamış
        /// </summary>
        private static Guid berkUserId = new("19be3093-3b66-4b78-ba3a-c4d1700ef214");
        /// <summary>
        /// Admin User Id - Achmet Pasia
        /// </summary>
        public const string PasiaUserId = "8";
        /// <summary>
        /// Admin User Id - Alper Kaya
        /// </summary>
        public const string CreatineUserId = "9";

        public static Guid BerkUserId { get => berkUserId; set => berkUserId = value; }
        #endregion
    }
}