namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        public Function(string name, object parameter)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name) + " can't be null or empty");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            Name = name.ToUpper();
            Parameters = new List<object> { parameter };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public Function(string name, object[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Name = name.ToUpper();
            Parameters = parameters.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public Function(string name, List<object> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Name = name.ToUpper();
            Parameters = parameters;
        }

        /// <summary>
        /// Gets <see cref="Name"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<object> Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Alias
        {
            get
            {
                var alias = string.Empty;

                if (Parameters.Count > 0 && Parameters[0] is Column)
                {
                    var column = Parameters[0] as Column;

                    alias = string.Format("[{0}.{1}.{2}]", Name, column.Table.Name, column.Name);
                }
                else
                {
                    alias = string.Format("[{0}]", Name);
                }

                return alias;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", Name, GetParameters());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetParameters()
        {
            var param = string.Empty;

            if (Parameters != null && Parameters.Count > 0)
            {
                Parameters.ForEach(p => param += string.Format("{0}, ", p));
                param.Remove(param.Length - 2);
            }

            return param;
        }
    }
}
