namespace RealityCS.DataLayer
{
    /// <summary>
    /// Represents a data provider manager
    /// </summary>
    public partial interface IDataProviderManager
    {
        #region Properties

        /// <summary>
        /// Gets data provider
        /// </summary>
        IRealitycsDataProvider DataProvider { get; }

        #endregion
    }
}
