namespace Mashup.Data.Context
{
    /// <summary>
    /// The data context configuration.
    /// </summary>
    public class DataContextConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether lazy loading enabled.
        /// </summary>
        public bool LazyLoadingEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes enabled.
        /// </summary>
        public bool AutoDetectChangesEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether proxy creation enabled.
        /// </summary>
        public bool ProxyCreationEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether validate on save enabled.
        /// </summary>
        public bool ValidateOnSaveEnabled { get; set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public string Connection { get; set; }
    }
}
