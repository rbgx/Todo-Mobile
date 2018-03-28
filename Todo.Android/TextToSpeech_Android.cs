using Android.Speech.Tts;
using Xamarin.Forms;
using Java.Lang;
using Todo;

[assembly: Dependency(typeof(TextToSpeechAndroid))]

namespace Todo
{
    public class TextToSpeechAndroid : Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        TextToSpeech _speaker;
        string _toSpeak;

        public void Speak(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            _toSpeak = text;
            if (_speaker == null)
                _speaker = new TextToSpeech(MainActivity.Instance, this);
            else
            {
                _speaker.Speak(_toSpeak, QueueMode.Flush, null, null);
            }
        }

        #region IOnInitListener implementation

        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                _speaker.Speak(_toSpeak, QueueMode.Flush, null, null);
            }
        }

        #endregion
    }
}