namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomizable<in T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        void SetHandler(T handler);

        /// <summary>
        /// Returns a result after running.
        /// </summary>
        bool Run();

        /// <summary>
        /// Returns a string that represents the current customization.
        /// </summary>
        /// <returns></returns>
        string GetCustomization();
    }
}
