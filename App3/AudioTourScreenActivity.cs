using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.Threading;

namespace AudioTour
{
    [Activity(Label = "TourScreen")]
    public class AudioTourScreenActivity : Activity
    {
        MediaPlayer mediaPlayer;
        ImageButton btnPlay;
        SeekBar seekBar;
        Button btnStop;
        TextView txtOut;
        Timer timer;
        bool isPlaying = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TourScreen);
            
            mediaPlayer = MediaPlayer.Create(this, Resource.Raw.controlla);
            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar1);

            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate = new TimerCallback(updateSeekBar);

            // Create a timer
            timer = new Timer(timerDelegate, null, 0, 10);

            btnPlay = FindViewById<ImageButton>(Resource.Id.btnPlay);
            btnPlay.Click += EventClickPlay;

            btnStop = FindViewById<Button>(Resource.Id.btnStop);
            btnStop.Click += EventClickStop;

            txtOut = FindViewById<TextView>(Resource.Id.txtOut);
        }
        
        public void updateSeekBar(Object state)
        {
            if (isPlaying)
            {
                seekBar.Progress = getProgressPercentage();
            }           
        }

        /**
         * Function to get Progress percentage
         * @param currentDuration
         * @param totalDuration
         * */
        public int getProgressPercentage()
        {
            Double percentage = (double)0;

            long currentSeconds = (int)(mediaPlayer.CurrentPosition / 1000);
            long totalSeconds = (int)(mediaPlayer.Duration / 1000);

            // calculating percentage
            percentage = (((double)currentSeconds) / totalSeconds) * 100;

            // return percentage
            return (int)percentage;
        }

        public void SeekBarOnClick(View view)
        {
            return;
        }

        private void EventClickPlay(object sender, EventArgs ea){
            if (btnPlay.Background.GetConstantState().Equals(GetDrawable(Resource.Drawable.playbutton).GetConstantState()))
            {
                //Plays audio
                isPlaying = true;
                mediaPlayer.Start();
                btnPlay.SetBackgroundResource(Resource.Drawable.pausebutton);
            }
            else
            {
                //Pauses audio
                isPlaying = false;
                mediaPlayer.Pause();
                btnPlay.SetBackgroundResource(Resource.Drawable.playbutton);
            }
        }

        private void EventClickStop(object sender, EventArgs ea)
        {
            isPlaying = false;
            mediaPlayer.Pause();
            mediaPlayer.SeekTo(0);
            btnPlay.SetImageDrawable(GetDrawable(Resource.Drawable.pausebutton));
        }
    }
}