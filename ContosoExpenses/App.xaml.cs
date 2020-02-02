using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;

namespace ContosoExpenses
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppCenter.Start("7c2e7071-3128-484f-8fee-694a32916c51",
                               typeof(Analytics), typeof(Crashes));

            var countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;
            AppCenter.SetCountryCode(countryCode);
        }
    }
}
