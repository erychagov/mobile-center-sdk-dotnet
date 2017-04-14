﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Com.Microsoft.Azure.Mobile.Analytics;
using Com.Microsoft.Azure.Mobile.Analytics.Channel;
using Com.Microsoft.Azure.Mobile.Ingestion.Models;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Crashes;

namespace Contoso.Forms.Puppet.Droid
{
    [Activity(Label = "MCFPuppet", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            AndroidAnalytics.SetListener(new AndroidAnalyticsListener());

            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            Crashes.GetLastSessionCrashReportAsync().ContinueWith(report =>
			{
            	MobileCenterLog.Info(App.LogTag, "MobileCenter.LastThrowable=" + report.Result?.AndroidDetails?.Throwable);
            	MobileCenterLog.Info(App.LogTag, "MobileCenter.LastException=" + report.Result?.Exception);
            });
        }
    }

    public class AndroidAnalyticsListener : Java.Lang.Object, IAnalyticsListener
    {
        public void OnSendingFailed(ILog log, Java.Lang.Exception e)
        {
            MobileCenterLog.Debug(App.LogTag, "Analytics listener OnSendingFailed with exception: " + e);
        }

        public void OnSendingSucceeded(ILog log)
        {
            MobileCenterLog.Debug(App.LogTag, "Analytics listener OnSendingSucceeded");
        }

        public void OnBeforeSending(ILog log)
        {
            MobileCenterLog.Debug(App.LogTag, "Analytics listener OnBeforeSendingEventLog");
        }
    }
}
