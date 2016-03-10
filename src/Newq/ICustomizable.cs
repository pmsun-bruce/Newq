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
        /// 
        /// </summary>
        void Run();
    }
}
