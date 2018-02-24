namespace WooliesX.Contracts
{
    /// <summary>
    /// Sort option
    /// </summary>
    public enum SortOptions
    {
        /// <summary>
        /// Price from low to high
        /// </summary>
        Low,
        /// <summary>
        /// Price from high to low
        /// </summary>
        High,
        /// <summary>
        /// A - Z sort on the Name
        /// </summary>
        Ascending,
        /// <summary>
        /// Z - A sort on the Name
        /// </summary>
        Descending,
        /// <summary>
        /// This will call the "customerHistory" resource to get a list of customers orders and needs to return based on popularity
        /// </summary>
        Recommended
    }
}