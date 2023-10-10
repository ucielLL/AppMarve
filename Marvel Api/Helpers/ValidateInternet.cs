using Marvel_Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Helpers
{
    public class ValidateInternet
    {

        NetworkAccess AccessType;
        public ValidateInternet()
        {
            AccessType = Connectivity.Current.NetworkAccess;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        public void DefineActions(Action internet, Action noInternet)
        {
            {
                this.Internet = internet;
                this.NoInternet = noInternet;
               
            }
        }
        


        public void Validate()
        {
           
            if (AccessType == NetworkAccess.Internet)
            {
                Internet();
                return;
            }
            NoInternet();
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
         
            AccessType = e.NetworkAccess;
            Validate();
        }

        Action Internet;
       
        Action NoInternet;


    }
}
