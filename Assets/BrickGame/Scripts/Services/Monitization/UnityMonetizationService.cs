// <copyright file="UnityMonetizationService.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/01/2017 12:48</date>

using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
namespace BrickGame.Scripts.Services.Monitization
{
    /// <summary>
    /// UnityMonetizationService
    /// </summary>
    public class UnityMonetizationService : MonetizationService
    {
        private const string RevardedVideo = "rewardedVideo";
        private const string SimpleVideo = "video";
        //================================       Public Setup       =================================
        /// <inheritdoc />
        protected override bool IsShowing { get; set; }

        //================================    Systems properties    =================================
        private string _zoneId;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================

        /// <inheritdoc />
        protected override void Initialize(bool isRewarded, out bool isAvailable)
        {
#if UNITY_ADS
            isAvailable = Advertisement.isSupported;
            _zoneId = isRewarded ? RevardedVideo : SimpleVideo;
#else
            isAvailable = false;
#endif
        }

#if UNITY_ADS
        /// <inheritdoc />
        protected override void TryToShowVideo()
        {
            if (!Advertisement.IsReady(_zoneId))
            {
                Debug.LogFormat("Advertising not ready for zone '{0}'", _zoneId);
                return;
            }
            var options = new ShowOptions {resultCallback = HandleShowResult};
            IsShowing = true;
            Advertisement.Show(_zoneId, options);
        }

        private void HandleShowResult(ShowResult result)
        {
            /*
            For future useg if will be needed
            switch (result)
            {
                case ShowResult.Finished:
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Failed:
                    break;
            }*/
            IsShowing = false;
            FinishShow();
        }
#else

        protected override void TryToShowVideo()
        {
            FinishShow();
        }
#endif
    }
}