namespace Newq
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public abstract class Customization
    {
        /// <summary>
        /// 
        /// </summary>
        protected Context context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Customization(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }

        /// <summary>
        /// Returns a string that represents the current customization.
        /// </summary>
        /// <returns></returns>
        public abstract string GetCustomization();
    }
}
