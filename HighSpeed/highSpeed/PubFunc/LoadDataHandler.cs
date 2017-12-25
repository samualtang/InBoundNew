using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace highSpeed.PubFunc
{
    public class LoadDataHandler
    {
        public void LoadDatas<T>(Func<T> FetchData, Action<T> OnSuccess, Action<string> OnFailed = null)
        {
            try
            {
                T t = FetchData();
                OnSuccess(t);
            }
            catch (Exception ex)
            {
                if (OnFailed != null) OnFailed(ex.Message);
            }
        }
    }
}
