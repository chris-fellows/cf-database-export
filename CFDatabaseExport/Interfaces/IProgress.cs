using CFUtilities;

namespace CFDatabaseExport
{
    /// <summary>
    /// Progress interface
    /// </summary>
    public interface IProgress
    {
        /// <summary>
        /// Sets status message
        /// </summary>
        /// <param name="message"></param>
        void SetStatusMessage(Message message);

        /// <summary>
        /// Sets number of items done
        /// </summary>
        /// <param name="itemsDone"></param>
        /// <param name="itemsTotal"></param>
        void SetItemsDone(int itemsDone, int itemsTotal);
    }
}
