using CFUtilities;

namespace CFDatabaseExport
{
    /// <summary>
    /// Progress interface
    /// </summary>
    public interface IProgress
    {
        void SetStatusMessage(Message message);
    }
}
