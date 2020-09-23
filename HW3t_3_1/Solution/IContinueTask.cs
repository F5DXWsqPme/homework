namespace Solution
{
    /// <summary>
    /// Continue with task.
    /// </summary>
    public interface IContinueTask
    {
        /// <summary>
        /// Run function.
        /// </summary>
        public void Run();

        /// <summary>
        /// Set task invalid.
        /// </summary>
        public void InvalidateTask();
    }
}
