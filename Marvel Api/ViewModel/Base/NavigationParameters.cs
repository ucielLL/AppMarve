using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.ViewModel.Base
{
    public class NavigationParameters : Dictionary<string, object>
    {
        public NavigationParameters() { }

        public NavigationParameters(string key, object value)
        {
            Add(key, value);
        }

        public T GetValue<T>(string Keyparameter)
        {
            try
            {
                return (T)Convert.ChangeType(this[Keyparameter], typeof(T));
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(Keyparameter, ex);
            }
        }

    }
}
