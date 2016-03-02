namespace Newq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <param name="otherParameters"></param>
        public Function(FunctionType name, object parameter, params object[] otherParameters)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            Name = name;
            Parameters = new List<object> { parameter };

            if (otherParameters.Length > 0)
            {
                foreach (var item in otherParameters)
                {
                    Parameters.Add(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FunctionType Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<object> Parameters { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Alias
        {
            get { return string.Format("[{0}]", Name); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}({1})");
        }
    }
}
